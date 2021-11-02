using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doublepoints : MonoBehaviour
{
    
    public ScoreScript score;
    public AudioClip powerupClip;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            audioSource.Play();
            score.DoubleScore();
            Destroy(gameObject);
        }
    }

}
