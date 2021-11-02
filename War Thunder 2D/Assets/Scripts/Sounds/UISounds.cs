using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISounds : MonoBehaviour, ISelectHandler
{
    public AudioClip audioClip;
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        OnSelect(eventData);
        audioSource.PlayOneShot(audioClip, 0.5f);
    }

    void OnSelect(BaseEventData eventData)
    {
        
    }
}
