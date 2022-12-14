using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ParticleManager : MonoBehaviour
{
    VisualEffect visualEffect;
    MyMessageListener shoulderData;
    TimerRL timerRelaxingL;
    TimerRR timerRelaxingR;
    TimerSL timerSpreadingL;
    TimerSR timerSpreadingR;
    TimerGL timerGatheringL;
    TimerGR timerGatheringR;

    [SerializeField]
    int tensionSpeed = 50;

    [SerializeField]
    int relaxationSpeed = 100;

    [SerializeField]
    int upperLimit = 2;

    [SerializeField]
    int lowerLimit = 0;

    [SerializeField]
    float tensionThreshold = 0.6f;

    [SerializeField]
    float tensionTime = 3;

    [SerializeField]
    float spreadingTime = 4;

    [SerializeField]
    float relaxationTime = 10;

    [SerializeField]
    int cycle = 10;


    int enableLeftGathering = 0;
    int enableRightGathering = 0;
    int enableLeftSpreading = 0;
    int enableRightSpreading = 0;

    int currentIterationL = 0;
    int currentIterationR = 0;

    bool leftGatheringStart = true;
    bool leftSpreadingStart = false;
    bool rightGatheringStart = true;
    bool rightSpreadingStart = false;

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

        visualEffect.SetFloat("TimerGathering", timerGathering);

        //Left shoulder: gathering particles
        if (shoulderLeft >= tensionThreshold && timerGatheringL.TimerStart == false && leftGatheringStart == true && currentIterationL <= cycle)
        {
            timerGatheringL.TimerStart = true;
            enableLeftGathering = 0;
            visualEffect.SetInt("EnableLeftGathering", enableLeftGathering);
        }
        else if (timerGatheringL.CurrentTime >= tensionTime && timerGatheringL.TimerStart == true)
        {
            timerGatheringL.ResetTimer();
            leftGatheringStart = false;
            leftSpreadingStart = true;
        }
        else
        {
            timerGatheringL.TimerStart = false;
        }

        //Left shoulder: spreading particles
        if (shoulderLeft < tensionThreshold && timerSpreadingL.TimerStart == false && leftSpreadingStart == true)
        {
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
            leftGatheringStart = true;
            leftSpreadingStart = false;
            currentIterationL += 1;

        }
        else
        {
            timerSpreadingL.TimerStart = false;
        }

        if (timerRelaxingL.CurrentTime >= relaxationTime && timerRelaxingL.TimerStart == true)
        {
            timerRelaxingL.ResetTimer();
            if (currentIterationL < 10)
            {
                enableLeftGathering = 5;
                visualEffect.SetInt("EnableLeftGathering", enableLeftGathering);
            }
            
        }

        //Right shoulder: gathering particles
        if (shoulderRight >= tensionThreshold && timerGatheringR.TimerStart == false && rightGatheringStart == true && currentIterationR <= cycle)
        {
            timerGatheringR.TimerStart = true;
            enableRightGathering = 0;
            visualEffect.SetInt("EnableRightGathering", enableRightGathering);
        }
        else if (timerGatheringR.CurrentTime >= tensionTime && timerGatheringR.TimerStart == true)
        {
            timerGatheringR.ResetTimer();
            rightGatheringStart = false;
            rightSpreadingStart = true;
        }
        else
        {
            timerGatheringR.TimerStart = false;
        }

        //Right shoulder: spreading particles
        if (shoulderRight < tensionThreshold && timerSpreadingR.TimerStart == false && rightSpreadingStart == true)
        {
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
            rightGatheringStart = true;
            rightSpreadingStart = false;
            currentIterationR += 1;

        }
        else
        {
            timerSpreadingR.TimerStart = false;
        }

        if (timerRelaxingR.CurrentTime >= relaxationTime && timerRelaxingR.TimerStart == true)
        {
            timerRelaxingR.ResetTimer();
            if (currentIterationR < 10)
            {
                enableRightGathering = 5;
                visualEffect.SetInt("EnableRightGathering", enableRightGathering);
            }

        }

        //Right shoulder: gathering particles
        //if (shoulderRight >= tensionThreshold && timerGatheringR.TimerStart == false && rightGatheringStart == true && currentIterationR <= cycle)
        //{
        //    timerGatheringR.TimerStart = true;
        //    enableRightGathering = 5;
        //    visualEffect.SetInt("EnableRightGathering", enableRightGathering);
        //}
        //else if (timerGatheringR.CurrentTime >= tensionTime && timerGatheringR.TimerStart == true)
        //{

        //    enableRightGathering = 0;
        //    visualEffect.SetInt("EnableRightGathering", enableRightGathering);
        //    timerGatheringR.ResetTimer();
        //    rightGatheringStart = false;
        //    rightSpreadingStart = true;
        //}
        //else
        //{
        //    timerGatheringR.TimerStart = false;
        //    enableRightGathering = 0;
        //    visualEffect.SetInt("EnableRightGathering", enableRightGathering);
        //}

        ////Right shoulder: spreading particles
        //if (shoulderRight < tensionThreshold && timerSpreadingR.TimerStart == false && rightSpreadingStart == true)
        //{
        //    timerSpreadingR.TimerStart = true;
        //    enableRightSpreading = 5;
        //    visualEffect.SetInt("EnableRightSpreading", enableRightSpreading);

        //}
        //else if (timerSpreadingR.CurrentTime >= relaxationTime && timerSpreadingR.TimerStart == true)
        //{
        //    enableRightSpreading = 0;
        //    visualEffect.SetInt("EnableRightSpreading", enableRightSpreading);
        //    timerSpreadingR.ResetTimer();
        //    rightGatheringStart = true;
        //    rightSpreadingStart = false;
        //    currentIterationR += 1;

        //}
        //else
        //{
        //    timerSpreadingR.TimerStart = false;
        //    enableRightSpreading = 0;
        //    visualEffect.SetInt("EnableRightSpreading", enableRightSpreading);

        //}
    }
}
