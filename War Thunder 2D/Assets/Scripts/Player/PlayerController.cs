using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float rotSpeed;
    public float speed;

    [Header("Sprite Rotation")]
    public Sprite[] spriteArray;

    [Header("Weapon Settings")] 
    public GameObject bullet;
    public float bulletSpeed;
    
    [Header("Audio")]
    public AudioClip shootingAudio;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    [System.NonSerialized] public GameState _currentGameState;
    private float _angle;
    private Rotation _currentRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
        //set the initial transform rotation to be flying right across the screen.
        transform.rotation = Quaternion.Euler(0, 0, -90.0f);
    }

    public void ResetPlayerMovement()
    {
        _rigidbody.velocity = transform.up * speed;
        transform.rotation = Quaternion.Euler(0, 0, -90.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentGameState != GameState.Lose)
        {
            CalculatePlayerMovement();
            UpdateSpriteRotation();
            HandleShooting();
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.angularVelocity = 0.0f;
        }
    }

    private void CalculatePlayerMovement()
    {
        _rigidbody.velocity = transform.up * speed;
        if (_currentGameState == GameState.Playing)
        {
            float angle = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg;
            if (angle != 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, -angle), Time.deltaTime * rotSpeed);
            }
        }
    }

    private void HandleShooting()
    {
        if (_currentGameState == GameState.Playing)
        {
            if(Input.GetButtonDown("Submit"))
            {
                SoundManager.instance.PlayPlayerOneShot(shootingAudio);
                GameObject bullets = Instantiate(bullet);
                bullets.AddComponent<PlayerBullet>().owner = gameObject;
                bullets.AddComponent<PlayerBullet>().direction = transform.up;
            }
        }
    }

    private void UpdateSpriteRotation()
    {
        _angle = transform.eulerAngles.z;
        CheckRotationAngle(Rotation.NORTH, 337.5, 22.5);
        CheckRotationAngle(Rotation.NORTH_WEST, 67.5, 22.5);
        CheckRotationAngle(Rotation.WEST, 112.5, 67.5);
        CheckRotationAngle(Rotation.SOUTH_WEST, 157.5, 112.5);
        CheckRotationAngle(Rotation.SOUTH, 202.5, 157.5);
        CheckRotationAngle(Rotation.SOUTH_EAST, 247.5, 202.5);
        CheckRotationAngle(Rotation.EAST, 292.5, 247.5);
        CheckRotationAngle(Rotation.NORTH_EAST, 337.5, 292.5);
        
        switch (_currentRotation)
        {
            case Rotation.NORTH:
            {
                _spriteRenderer.sprite = spriteArray[0];
                break;
            }
            case Rotation.NORTH_WEST:
            {
                _spriteRenderer.sprite = spriteArray[5];
                break;
            }
            case Rotation.WEST:
            {
                _spriteRenderer.sprite = spriteArray[6];
                break;
            }
            case Rotation.SOUTH_WEST:
            {
                _spriteRenderer.sprite = spriteArray[5];
                break;
            }
            case Rotation.SOUTH:
            {
                _spriteRenderer.sprite = spriteArray[0];
                break;
            }
            case Rotation.SOUTH_EAST:
            {
                _spriteRenderer.sprite = spriteArray[1];
                break;
            }
            case Rotation.EAST:
            {
                _spriteRenderer.sprite = spriteArray[2];
                break;
            }
            case Rotation.NORTH_EAST:
            {
                _spriteRenderer.sprite = spriteArray[1];                 
                break;
            }
        }
    }

    private void CheckRotationAngle(Rotation rotateOne, double less, double more)
    {
        if (rotateOne != Rotation.NORTH)
        {
            if (_angle < less && _angle > more)
            {
                _currentRotation = rotateOne;
            }
        }
        else
        {
            _currentRotation = rotateOne;
        }
    }
}

public enum Rotation
{
    NORTH,
    NORTH_EAST,
    EAST,
    SOUTH_EAST, 
    SOUTH,
    SOUTH_WEST,
    WEST,
    NORTH_WEST
}
