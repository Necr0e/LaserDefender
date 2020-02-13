using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Assignments")]
#pragma warning disable 0649
    [SerializeField] private GameObject playerProjectile;
    [SerializeField] private GameObject playerDeathVfx;
    [SerializeField] private AudioClip playerProjectileSfx;
    [SerializeField] private AudioClip playerDeathSfx;
#pragma warning restore 0649
    [Header("Audio Volume")] 
    [SerializeField] [Range(0, 1)] private float playerDeathSfxVolume = 0.75f;
    [SerializeField] [Range(0, 1)] private float playerProjectileSfxVolume = 0.25f;
    [Header("Player Stats")]
    [SerializeField] private int health = 300;
    
    private const float MoveSpeed = 10f;
    private const float Padding = 0.5f;
    private const float ProjectileFiringPeriod = 3f;
    private const float ProjectileVelocity = 5f;
    private const float VfxLifetime = 1f;

    private Coroutine firingCoroutine;
    private float xMin, xMax, yMin, yMax;
    private Transform moveToLocation;
    private Level levelLoaded;
    
    private void Start()
    {
        if (Camera.main == null) return;
        if (playerProjectile == null) return;
        moveToLocation = GetComponent<Transform>();
        levelLoaded = FindObjectOfType<Level>();
        SetUpMoveBoundaries();
    }
    private void Update()
    {
        Move();
        Fire();
    }

   private void Move()
   {
       float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * MoveSpeed;
       float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * MoveSpeed;
       Vector3 newPosition = moveToLocation.position;
       float newXPos = Mathf.Clamp(newPosition.x + deltaX, xMin, xMax);
       float newYPos = Mathf.Clamp(newPosition.y + deltaY, yMin, yMax);
       moveToLocation.position = new Vector2(newXPos, newYPos);
   }

   //TODO: create object pool for lasers
   private void Fire()
   {
       if(Input.GetButtonDown("Fire1"))
       {
           firingCoroutine = StartCoroutine(FireContinuously());
           AudioSource.PlayClipAtPoint(playerProjectileSfx, Camera.main.transform.position, playerProjectileSfxVolume);
       }

       if (Input.GetButtonUp("Fire1"))
       {
           StopCoroutine(firingCoroutine);
       }
   }

   IEnumerator FireContinuously()
   {
       while (true)
       {
           GameObject laser = Instantiate(playerProjectile, transform.position, Quaternion.identity);
           laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, ProjectileVelocity);
           yield  return new WaitForSeconds(ProjectileFiringPeriod);
       }
       
   }

   private void SetUpMoveBoundaries()
   {
       Camera gameCamera = Camera.main;
       if (gameCamera != null)
       {
           xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + Padding;
           xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - Padding;
           yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + Padding;
           yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - Padding;
       }
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
       var damageDealer = other.gameObject.GetComponent<DamageDealer>();
       if (!damageDealer)
       {
           return;
       }
       ProcessHit(damageDealer);
   }

   private void ProcessHit(DamageDealer damageDealer)
   {
       health -= damageDealer.GetDamage();
       damageDealer.Hit();
       if (health <= 0)
       {
            KillPlayer();
            levelLoaded.LoadGameOver();
       }
   }

   private void KillPlayer()
   {
       Destroy(gameObject);
       GameObject explosion = Instantiate(playerDeathVfx, transform.position, Quaternion.identity);
       Destroy(explosion, VfxLifetime);
        AudioSource.PlayClipAtPoint(playerDeathSfx, Camera.main.transform.position, playerDeathSfxVolume);
   }

   public int GetHealth()
   {
       return health;
   }
}
