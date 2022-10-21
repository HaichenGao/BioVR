using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowL : MonoBehaviour
{
    public int numberLeftSphere; //start from 0
    public float waitingTime = 5f;
    public float glow = 50f;
    float tranSpeed = 0.01f;
    float emissiveSpeed = 0.5f;

    TimerW waitingTimer;
    ParticleManager3 PM;
    float transparency = 0f;
    float emissiveIntensity = 1f;

    bool isLightened = false;

    Color color = new Color(1f, 1f, 1f, 0f);

    public AudioSource Lighten;
    // Start is called before the first frame update
    void Start()
    {
        waitingTimer = gameObject.GetComponent<TimerW>();
        PM = GameObject.Find("Breaking_Velocity_Emitter").GetComponent<ParticleManager3>();
        gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", color);
    }

    // Update is called once per frame
    void Update()
    {
        if(PM.isRelaxationFinishedL == true && numberLeftSphere == PM.currentIterationL && isLightened == false)
        {
            StartCoroutine(EnLighten());
            Lighten.Play();
            isLightened = true;
        }

        //while(waitingTimer.CurrentTime >= waitingTime && transparency <= 1f)
        //{
        //    transparency += tranSpeed;
        //    color = new Color(0.0514f, 0.4283f, 0.8396f, transparency);
        //    emissiveIntensity += emissiveSpeed;
        //    gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", color);
        //    gameObject.GetComponent<Renderer>().material.SetColor("_EmissiveColor", color * emissiveIntensity);
        //}

        

        if (transparency > 1f)
        {
            waitingTimer.ResetTimer();
        }


    }

    IEnumerator EnLighten()
    {
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
