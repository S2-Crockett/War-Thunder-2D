using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")] 
    public float bulletSpeed;
    public float manualDestruction = 1.5f;

    [Header("Sprite")] 
    public Sprite bulletSprite;

    protected Rigidbody2D _rigidbody;
    protected Transform _onwerTransform;
    
    [System.NonSerialized] public GameObject owner;
    [System.NonSerialized] public Vector3 direction;
    
    public void Start()
    {
        if (owner)
        {
            _onwerTransform = owner.transform;
            var rotation = _onwerTransform.rotation;
            transform.rotation = new Quaternion(rotation.x, rotation.y, rotation.z + 0, rotation.w);
            transform.position = owner.transform.position;
        }

        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = direction * 15.0f;
        
        StartCoroutine(ManualDestroy());
    }

    IEnumerator ManualDestroy()
    {
        yield return new WaitForSeconds(manualDestruction);
        Destroy(gameObject);
    }
}
