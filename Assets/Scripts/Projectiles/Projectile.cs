using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileLifetime = 3f;
    private void Start()
    {
        Destroy(this.gameObject, projectileLifetime);
    }
}
