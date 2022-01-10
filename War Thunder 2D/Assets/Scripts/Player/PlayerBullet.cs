using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerBullet : MonoBehaviour
{
    [Header("Bullet Settings")] 
    public float bulletSpeed;
    public float manualDestruction = 1.5f;

    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;
    private SpriteRenderer _spriteRenderer;
    private Transform _playerTransform;
    [System.NonSerialized] public GameObject player;

    void Start()
    {
        if (player)
        {
            _playerTransform = player.transform;
            var rotation = _playerTransform.rotation;
            transform.rotation = new Quaternion(rotation.x, rotation.y, rotation.z + 0, rotation.w);
            transform.position = player.transform.position;
        }

        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _rigidbody.velocity = transform.up * 20;
        StartCoroutine(ManualDestroy());
    }
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
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        
    }
    
    IEnumerator ManualDestroy()
    {
        yield return new WaitForSeconds(manualDestruction);
        Destroy(gameObject);
    }
}