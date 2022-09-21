using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowL : MonoBehaviour
{
    public int numberLeftSphere; //start from 0
    public float waitingTime = 5f;
    public float glow = 50f;
    public float tranSpeed = 0.01f;
    public float emissiveSpeed = 0.03f;

    TimerW waitingTimer;
    ParticleManager2 PM;
    float transparency = 0f;
    float emissiveIntensity = 1f;

    Color color = new Color(1f, 1f, 1f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        waitingTimer = gameObject.GetComponent<TimerW>();
        PM = GameObject.Find("Breaking_Velocity_Emitter").GetComponent<ParticleManager2>();
        gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", color);
    }

    // Update is called once per frame
    void Update()
    {
        if(PM.leftSpreadingStart == true && numberLeftSphere == PM.currentIterationL)
        {
            //gameObject.SetActive(true);
            waitingTimer.TimerStart = true;
        }

        while(waitingTimer.CurrentTime >= waitingTime && transparency <= 1f)
        {
            transparency += tranSpeed;
            color = new Color(0.88f, 0.44f, 0f, transparency);
            emissiveIntensity += emissiveSpeed;
            gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", color);
            gameObject.GetComponent<Renderer>().material.SetColor("_EmissiveColor", color * emissiveIntensity);
        }

        if (transparency > 1f)
        {
            waitingTimer.ResetTimer();
        }


    }
}
