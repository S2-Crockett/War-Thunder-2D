using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform player;
    void Update()
    {
        if(player)
        {
           transform.position = new Vector3(player.position.x, player.position.y, player.position.z - 1);
        }
        
    }
}
