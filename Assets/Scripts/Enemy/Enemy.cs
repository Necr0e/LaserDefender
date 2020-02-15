using Projectiles;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [Header("Enemy Assignments")]
#pragma warning disable 0649
        [SerializeField] private GameObject enemyProjectile;
        [SerializeField] private GameObject enemyExplosionVfx;
        [SerializeField] private AudioClip enemyFireSfx;
        [SerializeField] private AudioClip enemyDeathSfx;
        [SerializeField] private string projectileTag;
#pragma warning restore 0649
        [Header("Audio Volume")] 
        [SerializeField] [Range(0, 1)] private float enemyDeathSfxVolume = 0.75f;
        [SerializeField] [Range(0, 1)] private float enemyProjectileSfxVolume = 0.50f;
        [Header("Enemy Stats")]
        [SerializeField] private float health = 100;
        [SerializeField] private int scoreValue = 150;
        [Header("Enemy Projectile Values")]
        [SerializeField] private float projectileVelocity = 5f;   
        [SerializeField] private float minTimeBetweenShots = 0.2f;
        [SerializeField] private float maxTimeBetweenShots = 2f;
        private const float VfxLifetime = 1f;
        private float shotCounter;
        private SpriteRenderer renderOnScreen;
        private void Start()
        {
            if (enemyProjectile == null) return;
            renderOnScreen = gameObject.GetComponentInChildren<SpriteRenderer>();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
        private void Update()
        {
            CountdownAndFire(); 
        }
        private void CountdownAndFire()
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0f)
            {
                if (renderOnScreen.isVisible)
                {
                    Fire();
                    shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
                    AudioSource.PlayClipAtPoint(enemyFireSfx, Camera.main.transform.position, enemyProjectileSfxVolume);
                }
            }
        }
        private void Fire()
        {
            CreateProjectile();
        }
        private void CreateProjectile()
        {
            Transform projectileTransform = transform;
            GameObject projectile = ObjectPooler.Instance.GetPooledObject(projectileTag, projectileTransform.position, projectileTransform.rotation);
            if (projectile == null) return;
            projectile.SetActive(true);
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileVelocity);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            var damageDealer = other.gameObject.GetComponent<DamageDealer>();
            ProcessHit(damageDealer);
        }
        private void ProcessHit(DamageDealer damageDealer)
        {
            health -= damageDealer.GetDamage();
            damageDealer.Hit();
            if (health <= 0)
            {
                KillObject();
            }
        }
        private void KillObject()
        {
            Destroy(gameObject);
            GameObject explosion = Instantiate(enemyExplosionVfx, transform.position, Quaternion.identity);
            Destroy(explosion, VfxLifetime);
            FindObjectOfType<GameSession>().AddToScore(scoreValue);
            if (Camera.main != null)
            {
                AudioSource.PlayClipAtPoint(enemyDeathSfx, Camera.main.transform.position,enemyDeathSfxVolume); 
            }
        }
    }
}
