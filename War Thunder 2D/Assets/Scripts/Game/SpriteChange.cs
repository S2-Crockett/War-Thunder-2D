using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChange : MonoBehaviour
{
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
    public Rotation rotation;

    public Transform transform_;
    private SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;
    private float angle;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rotation = Rotation.NORTH;
    }

    // Update is called once per frame
    void Update()
    {
        
        angle = transform.eulerAngles.z;

        AngleCheck(Rotation.NORTH, 337.5, 22.5);
        AngleCheck(Rotation.NORTH_WEST, 67.5, 22.5);
        AngleCheck(Rotation.WEST, 112.5, 67.5);
        AngleCheck(Rotation.SOUTH_WEST, 157.5, 112.5);
        AngleCheck(Rotation.SOUTH, 202.5, 157.5);
        AngleCheck(Rotation.SOUTH_EAST, 247.5, 202.5);
        AngleCheck(Rotation.EAST, 292.5, 247.5);
        AngleCheck(Rotation.NORTH_EAST, 337.5, 292.5);


        switch (rotation)
        {
            case Rotation.NORTH:
            {
                    spriteRenderer.sprite = spriteArray[0];
                    break;
            }
            case Rotation.NORTH_WEST:
                {
                    spriteRenderer.sprite = spriteArray[5];
                    break;
                }
            case Rotation.WEST:
                {
                    spriteRenderer.sprite = spriteArray[6];
                    break;
                }
            case Rotation.SOUTH_WEST:
                {
                    spriteRenderer.sprite = spriteArray[5];
                    break;
                }
            case Rotation.SOUTH:
                {
                    spriteRenderer.sprite = spriteArray[0];
                    break;
                }
            case Rotation.SOUTH_EAST:
                {
                    spriteRenderer.sprite = spriteArray[1];
                    break;
                }
            case Rotation.EAST:
                {
                    spriteRenderer.sprite = spriteArray[2];
                    break;
                }
            case Rotation.NORTH_EAST:
                {
                    spriteRenderer.sprite = spriteArray[1];                 
                    break;
                }
        }
    }

    void AngleCheck(Rotation rotateOne, double less, double more)
    {
        if (rotateOne != Rotation.NORTH)
        {
            if (angle < less && angle > more)
            {
                rotation = rotateOne;
            }
        }
        else
        {
                rotation = rotateOne;
        }
    }

}
