using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpinner : MonoBehaviour
{
    private const float SpeedOfSpin = 1000f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, SpeedOfSpin * Time.deltaTime);
    }
}
