using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
#pragma warning disable 0649
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject pathPrefab;
#pragma warning restore 0649
    [SerializeField] private float timeBetweenSpawns = 0.5f;
    [SerializeField] private float randomSpawnFactor = 0.3f;
    [SerializeField] private int numberOfEnemies = 10;
    [SerializeField] private float moveSpeed = 2f;


    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }
    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach(Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }
    public float GetTimeBetweenSpawns()
    {
        return timeBetweenSpawns;
    }
    public float GetRandomSpawnFactor()
    {
        return randomSpawnFactor;
    }
    
    public int GetNumberOfEnemies()
    {
        return numberOfEnemies;
    }
    
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
}
