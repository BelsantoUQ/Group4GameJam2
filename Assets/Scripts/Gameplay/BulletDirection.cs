using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BulletDirection : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DeactivateAfterDelay(.5f));
    }
    
    private IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
    
}