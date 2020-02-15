using UnityEngine;

namespace Projectiles
{
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField] private int damage = 100;

        public int GetDamage()
        {
            return damage;
        }

        public void Hit()
        {
            Destroy(gameObject);
        }
    }
}
