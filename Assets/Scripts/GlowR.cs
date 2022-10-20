using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class GlowR : MonoBehaviour
{
    public int numberRightSphere; //start from 0
    public float waitingTime = 14f;
    public float glow = 50f;
    float tranSpeed = 0.01f;
    float emissiveSpeed = 1f;

    TimerW waitingTimer;
    ParticleManager3 PM;
    float transparency = 0f;
    float emissiveIntensity = 1f;

    bool isLightened = false;

    Color color = new Color(1f, 1f, 1f, 0f);
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
        if (PM.isRelaxationFinishedR == true && numberRightSphere == PM.currentIterationR && isLightened == false)
        {
            Debug.Log("1");
            StartCoroutine(EnLighten());
            isLightened = true;
        }

        if (transparency > 1f)
        {
            waitingTimer.ResetTimer();
        }


    }

    IEnumerator EnLighten()
    {
        Debug.Log("2");
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
