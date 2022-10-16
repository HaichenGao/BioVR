using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ding : MonoBehaviour
{
    public AudioSource beadSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DropSound()
    {
        beadSound.Play();
    }
}
