using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    private float timer = 1.0f;
    private bool Active = false;

    private void Start()
    {
        Active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            if (timer >= 0)
            {
                timer -= 1.0f * Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
     
            }
        }
    }
}
