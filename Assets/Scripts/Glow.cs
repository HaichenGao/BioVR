using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour
{
    public int numberLeftSphere; //start from 0
    public float waitingTime = 5f;
    public float glow = 50f;

    TimerWL waitingTimer;
    ParticleManager2 PM;
    float transparency = 0f;
    float emissiveIntensity = 1f;
    Color color = new Color(1f, 1f, 1f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        waitingTimer = GameObject.Find("Hint_Sphere").GetComponent<TimerWL>();
        PM = GameObject.Find("Breaking_Velocity_Emitter").GetComponent<ParticleManager2>();
        gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", color);
    }

    // Update is called once per frame
    void Update()
    {
        if(PM.leftSpreadingStart == true)
        {
            waitingTimer.TimerStart = true;
        }

        while(waitingTimer.CurrentTime >= waitingTime && numberLeftSphere == PM.currentIterationL && transparency <= 1f)
        {
            transparency += 0.0f;
            color = new Color(0f, 0.88f, 0.7f, transparency);
            emissiveIntensity += 0.03f;
            gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", color);
            gameObject.GetComponent<Renderer>().material.SetColor("_EmissiveColor", color * emissiveIntensity);


        }

        //if (transparency >= 1f)
        //{
        //    waitingTimer.ResetTimer();
        //}


    }
}
