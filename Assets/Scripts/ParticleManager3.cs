using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ParticleManager3 : MonoBehaviour
{
    VisualEffect visualEffect;
    MyMessageListener shoulderData;
    TimerRL timerRelaxingL;
    TimerRR timerRelaxingR;
    TimerSL timerSpreadingL;
    TimerSR timerSpreadingR;
    TimerGL timerGatheringL;
    TimerGR timerGatheringR;
    TimerFL timerFadingL;
    TimerFR timerFadingR;

    [SerializeField]
    int upperLimit = 2;

    [SerializeField]
    int lowerLimit = 0;

    [SerializeField]
    float tensionThreshold = 0.5f;

    [SerializeField]
    float tensionTime = 3;

    [SerializeField]
    float spreadingTime = 3f;

    [SerializeField]
    float relaxationTime = 10;

    [SerializeField]
    float fadeTime = 1;

    [SerializeField]
    int cycle = 30;

    [SerializeField]
    GameObject[] SphereL = new GameObject[30];


    [SerializeField]
    GameObject[] SphereR = new GameObject[30];


    int enableLeftGathering = 0;
    int enableRightGathering = 0;
    int enableLeftSpreading = 0;
    int enableRightSpreading = 0;

    public int currentIterationL = 0;
    public int currentIterationR = 0;

    bool leftGatheringStart = true;
    public bool leftSpreadingStart = false;
    bool leftRelaxingStart = false;
    bool rightGatheringStart = true;
    public bool rightSpreadingStart = false;
    bool rightRelaxingStart = false;

    //bool leftSpawningStart = true;
    //bool rightSpawningStart = true;

    public float timerGathering;
    public float timerSpreading;
    public float timerRelaxing;


    // Start is called before the first frame update
    void Start()
    {
        visualEffect = this.GetComponent<VisualEffect>();
        shoulderData = GameObject.Find("SerialController").GetComponent<MyMessageListener>();
        timerSpreadingL = GameObject.Find("[CameraRig]").GetComponent<TimerSL>();
        timerSpreadingR = GameObject.Find("[CameraRig]").GetComponent<TimerSR>();
        timerRelaxingL = GameObject.Find("[CameraRig]").GetComponent<TimerRL>();
        timerRelaxingR = GameObject.Find("[CameraRig]").GetComponent<TimerRR>();
        timerGatheringL = GameObject.Find("[CameraRig]").GetComponent<TimerGL>();
        timerGatheringR = GameObject.Find("[CameraRig]").GetComponent<TimerGR>();
        timerFadingL = GameObject.Find("[CameraRig]").GetComponent<TimerFL>();
        timerFadingR = GameObject.Find("[CameraRig]").GetComponent<TimerFR>();

        foreach(GameObject sphere in SphereL)
        {
            sphere.SetActive(false);
        }

        foreach (GameObject sphere in SphereR)
        {
            sphere.SetActive(false);
        }

        visualEffect.SetInt("Min", lowerLimit);
        visualEffect.SetInt("Max", upperLimit);

        enableLeftGathering = 5;
        visualEffect.SetInt("EnableLeftGathering", enableLeftGathering);

        enableRightGathering = 5;
        visualEffect.SetInt("EnableRightGathering", enableRightGathering);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float shoulderLeft;
        float shoulderRight;
        shoulderLeft = Mathf.Round((float)shoulderData.left * 100f) / 100f;
        shoulderRight = Mathf.Round((float)shoulderData.right * 100f) / 100f;
        visualEffect.SetFloat("ParticleAmountLeft", shoulderLeft);
        visualEffect.SetFloat("ParticleAmountRight", shoulderRight);

        timerGathering = timerGatheringL.CurrentTime;
        timerSpreading = timerSpreadingL.CurrentTime;
        timerRelaxing = timerRelaxingL.CurrentTime;

        //visualEffect.SetFloat("TimerGathering", timerGatheringL.CurrentTime);
        visualEffect.SetFloat("TimerFadingL", timerFadingL.CurrentTime);
        visualEffect.SetFloat("TimerFadingR", timerFadingR.CurrentTime);

        //Left shoulder: gathering particles
        if (shoulderLeft >= tensionThreshold && (timerGatheringL.CurrentTime < tensionTime) && leftGatheringStart == true && currentIterationL <= cycle)
        {
            SphereL[currentIterationL].SetActive(true);
            timerGatheringL.TimerStart = true;
        }
        else if (timerGatheringL.CurrentTime >= tensionTime && timerGatheringL.TimerStart == true)
        {
            timerGatheringL.ResetTimer();
            leftGatheringStart = false;
            leftSpreadingStart = true;
            leftRelaxingStart = true;
            visualEffect.SetBool("LeftRelaxingStart", leftRelaxingStart);
            visualEffect.SetBool("LeftSpreadingStart", leftSpreadingStart);
            enableLeftGathering = 0;
            visualEffect.SetInt("EnableLeftGathering", enableLeftGathering);
            timerFadingL.TimerStart = true;
        }
        else
        {
            timerGatheringL.TimerStart = false;
        }

        if(timerFadingL.CurrentTime >= fadeTime)
        {
            timerFadingL.ResetTimer();
        }

        //Left shoulder: spreading particles
        if (shoulderLeft < tensionThreshold && timerSpreadingL.TimerStart == false && leftSpreadingStart == true && currentIterationL < cycle)
        {
            leftSpreadingStart = false;
            visualEffect.SetInt("SphereL", currentIterationL);
            timerSpreadingL.TimerStart = true;
            timerRelaxingL.TimerStart = true;
            enableLeftSpreading = 5;
            visualEffect.SetInt("EnableLeftSpreading", enableLeftSpreading);

        }
        else if (timerSpreadingL.CurrentTime >= spreadingTime && timerSpreadingL.TimerStart == true)
        {
            enableLeftSpreading = 0;
            visualEffect.SetInt("EnableLeftSpreading", enableLeftSpreading);
            timerSpreadingL.ResetTimer();
            
            
            //visualEffect.SetBool("LeftSpreadingStart", leftSpreadingStart);


        }
        //else
        //{
        //    timerSpreadingL.TimerStart = false;
        //}

        if (timerRelaxingL.CurrentTime >= relaxationTime && timerRelaxingL.TimerStart == true)
        {
            timerRelaxingL.ResetTimer();
            if (currentIterationL < cycle - 1)
            {
                leftGatheringStart = true;
                leftRelaxingStart = false;
                visualEffect.SetBool("LeftRelaxingStart", leftRelaxingStart);
                enableLeftGathering = 5;
                visualEffect.SetInt("EnableLeftGathering", enableLeftGathering);
                currentIterationL += 1;
            }else{
                currentIterationL += 1;
            }
            
        }

        //Right shoulder: gathering particles
        if (shoulderRight >= tensionThreshold && (timerGatheringR.CurrentTime < tensionTime) && rightGatheringStart == true && currentIterationR <= cycle)
        {
            SphereR[currentIterationR].SetActive(true);
            timerGatheringR.TimerStart = true;
            //enableRightGathering = 0;
            //visualEffect.SetInt("EnableRightGathering", enableRightGathering);
        }
        else if (timerGatheringR.CurrentTime >= tensionTime && timerGatheringR.TimerStart == true)
        {
            timerGatheringR.ResetTimer();
            rightGatheringStart = false;
            rightSpreadingStart = true;
            rightRelaxingStart = true;
            visualEffect.SetBool("RightRelaxingStart", rightRelaxingStart);
            visualEffect.SetBool("RightSpreadingStart", rightSpreadingStart);
            enableRightGathering = 0;
            visualEffect.SetInt("EnableRightGathering", enableRightGathering);
            timerFadingR.TimerStart = true;
        }
        else
        {
            timerGatheringR.TimerStart = false;
        }

        if (timerFadingR.CurrentTime >= fadeTime)
        {
            timerFadingR.ResetTimer();
        }

        //Right shoulder: spreading particles
        if (shoulderRight < tensionThreshold && timerSpreadingR.TimerStart == false && rightSpreadingStart == true && currentIterationR < cycle)
        {
            rightSpreadingStart = false;
            visualEffect.SetInt("SphereR", currentIterationR);
            timerSpreadingR.TimerStart = true;
            timerRelaxingR.TimerStart = true;
            enableRightSpreading = 5;
            visualEffect.SetInt("EnableRightSpreading", enableRightSpreading);

        }
        else if (timerSpreadingR.CurrentTime >= spreadingTime && timerSpreadingR.TimerStart == true)
        {
            enableRightSpreading = 0;
            visualEffect.SetInt("EnableRightSpreading", enableRightSpreading);
            timerSpreadingR.ResetTimer();
            
            
            //visualEffect.SetBool("RightSpreadingStart", rightSpreadingStart);
            //currentIterationR += 1;

        }
        //else
        //{
        //    timerSpreadingR.TimerStart = false;
        //}

        if (timerRelaxingR.CurrentTime >= relaxationTime && timerRelaxingR.TimerStart == true)
        {
            timerRelaxingR.ResetTimer();
            if (currentIterationR < cycle -1)
            {
                rightGatheringStart = true;
                rightRelaxingStart = false;
                visualEffect.SetBool("RightRelaxingStart", rightRelaxingStart);
                enableRightGathering = 5;
                visualEffect.SetInt("EnableRightGathering", enableRightGathering);
                currentIterationR += 1;
            }else{
                currentIterationR += 1;
            }

        }


    }
}
