using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(destroyExplosion());
    }
    
    IEnumerator destroyExplosion()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
