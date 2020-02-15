using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyPathing : MonoBehaviour
    {
        private WaveConfig waveConfig;
        private List<Transform> enemyWaypoints;
        private int waypointIndex = 0;
        private void Start()
        {
            enemyWaypoints = waveConfig.GetWaypoints();
            transform.position = enemyWaypoints[waypointIndex].position;
        }

        private void Update()
        { 
            MoveToWaypoint();
        }

        public void SetWaveConfig(WaveConfig waveConfig)
        {
            this.waveConfig = waveConfig;
        }
        private void MoveToWaypoint()
        {
            if (waypointIndex <= enemyWaypoints.Count - 1)
            {
                Vector2 targetPosition = enemyWaypoints[waypointIndex].transform.position;
                float movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
                if (transform.position.Equals(targetPosition))
                {
                    waypointIndex++;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
