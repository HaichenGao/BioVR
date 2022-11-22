using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guidance : MonoBehaviour
{
    bool rLFlag = false;
    bool rRFlag = false;
    bool tLFlag = false;
    bool tRFlag = false;

    ParticleManagerGuidance PM;

    public float shoulderL;
    public float shoulderR;

    public float tensionThresholdL = 0.6f;
    public float tensionThresholdR = 0.6f;
    public float relaxationThresholdL = 0f;
    public float relaxationThresholdR = 0f;
    // Start is called before the first frame update
    void Start()
    {
        shoulderL = relaxationThresholdL;
        shoulderR = relaxationThresholdR;
        PM = gameObject.GetComponent<ParticleManagerGuidance>();
        StartCoroutine(TensionL(5f, 0.1f, relaxationThresholdL, tensionThresholdL));
        StartCoroutine(TensionR(5f, 0.1f, relaxationThresholdR, tensionThresholdR));
    }

    // Update is called once per frame
    void Update()
    {
        if(PM.leftSpreadingStart == true)
        {
            StartCoroutine(RelaxationL(1.5f, relaxationThresholdL));
        }

        if (PM.rightSpreadingStart == true)
        {
            StartCoroutine(RelaxationR(1.5f, relaxationThresholdR));
        }

        if (PM.leftGatheringStart == true && tLFlag == true)
        {
            tLFlag = false;
            StartCoroutine(TensionL(3.1f, 0.1f, relaxationThresholdL, tensionThresholdL + 0.05f));
        }

        if (PM.rightGatheringStart == true && tRFlag == true)
        {
            tRFlag = false;
            StartCoroutine(TensionR(3.1f, 0.1f, relaxationThresholdR, tensionThresholdR + 0.05f));
        }
    }

    IEnumerator TensionL(float waitTime, float timeInterval , float startValue, float endValue)
    {
        
        yield return new WaitForSeconds(waitTime);
        //shoulderL = startValue;
        while (shoulderL <= endValue)
        {
            shoulderL += 0.01f;
            yield return new WaitForSeconds(timeInterval);
        }
        tLFlag = true;
        
    }

    IEnumerator TensionR(float waitTime, float timeInterval, float startValue, float endValue)
    {
        
        yield return new WaitForSeconds(waitTime);
        //shoulderR = startValue;
        while (shoulderR <= endValue)
        {
            shoulderR += 0.01f;
            yield return new WaitForSeconds(timeInterval);
        }
        tRFlag = true;
    }

    IEnumerator RelaxationL(float waitTime, float endValue)
    {
        yield return new WaitForSeconds(waitTime);
        shoulderL = endValue;
        //rLFlag = true;
    }

    IEnumerator RelaxationR(float waitTime, float endValue)
    {
        yield return new WaitForSeconds(waitTime);
        shoulderR = endValue;
        //rRFlag = true;
    }
}
