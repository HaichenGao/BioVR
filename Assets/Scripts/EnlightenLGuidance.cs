using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnlightenLGuidance : MonoBehaviour
{
    public int numberLeftSphere; 

    public float glow = 50f;
    float tranSpeed = 0.02f;
    float emissiveSpeed = 0.5f;


    ParticleManagerGuidance PM;
    float transparency = 0f;
    float emissiveIntensity = 1f;

    bool isLightened = false;

    Color color = new Color(0.13f, 0.67f, 0.1f, 1f);

    //public AudioSource Lighten;
    // Start is called before the first frame update
    void Start()
    {

        PM = GameObject.Find("Breaking_Velocity_Emitter").GetComponent<ParticleManagerGuidance>();
        gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", color);
    }

    // Update is called once per frame
    void Update()
    {
        if(numberLeftSphere == PM.enlightenL && isLightened == false)
        {
            StartCoroutine(EnLighten(3f));
            //Lighten.Play();
            isLightened = true;
        }


    }

    IEnumerator EnLighten(float time)
    {
        yield return new WaitForSeconds(time);
        while (transparency <= 1f)
        {
            yield return new WaitForSeconds(0.01f);
            transparency += tranSpeed;
            color = new Color(0.0514f, 0.4283f, 0.8396f, transparency);
            emissiveIntensity += emissiveSpeed;
            gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", color);
            gameObject.GetComponent<Renderer>().material.SetColor("_EmissiveColor", color * emissiveIntensity);
        }
    }
}
