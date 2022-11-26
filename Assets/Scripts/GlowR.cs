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
    ParticleManager4 PM;
    float transparency = 0f;
    float emissiveIntensity = 0.5f;

    bool isLightened = false;

    Color color = new Color(1f, 1f, 1f, 0f);
    public AudioSource Lighten;
    // Start is called before the first frame update
    void Start()
    {
        waitingTimer = gameObject.GetComponent<TimerW>();
        PM = GameObject.Find("Breaking_Velocity_Emitter").GetComponent<ParticleManager4>();
        gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", color);
    }

    // Update is called once per frame
    void Update()
    {
        if (PM.isRelaxationFinishedR == true && numberRightSphere == PM.currentIterationR && isLightened == false)
        {
            StartCoroutine(EnLighten());
            Lighten.Play();
            isLightened = true;
            PM.isRelaxationFinishedR = false;
            PM.currentIterationR += 1;

            if (PM.currentIterationR == 10)
            {
                StartCoroutine(AnioR1(5f));
                PM.RuneR[0].SetActive(true);
                PM.enlightenR += 1;
            }

            if (PM.currentIterationR == 20)
            {
                StartCoroutine(AnioR2(5f));
                PM.RuneR[1].SetActive(true);
                PM.enlightenR += 1;
            }

            if (PM.currentIterationR == 30)
            {
                StartCoroutine(AnioR3(5f));
                PM.RuneR[2].SetActive(true);
                PM.enlightenR += 1;
            }
        }

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

    IEnumerator AnioR1(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PM.oR1.GetComponent<Animator>().SetBool("oR1", true);
    }

    IEnumerator AnioR2(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PM.oR2.GetComponent<Animator>().SetBool("oR2", true);
    }

    IEnumerator AnioR3(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PM.oR3.GetComponent<Animator>().SetBool("oR3", true);
    }
}
