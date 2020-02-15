using UnityEngine;

namespace Projectiles
{
    public class ProjectileSpinner : MonoBehaviour
    {
        private const float SpeedOfSpin = 1000f;
        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0, 0, SpeedOfSpin * Time.deltaTime);
        }
    }
}
