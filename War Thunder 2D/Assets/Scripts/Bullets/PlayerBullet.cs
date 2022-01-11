using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerBullet : Bullet
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this != null)
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyHealth>().UpdateHealth(-1);
                Destroy(gameObject);
            }
        }
    }
}