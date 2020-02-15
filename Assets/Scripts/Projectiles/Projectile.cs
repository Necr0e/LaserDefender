using UnityEngine;

namespace Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float projectileLifetime = 3f;

        private void OnEnable()
        {
            Invoke("DisableProjectile", projectileLifetime);
        }
        private void DisableProjectile()
        {
            gameObject.SetActive(false);
        }
        private void OnDisable()
        {
            CancelInvoke();
        }
    }
}
