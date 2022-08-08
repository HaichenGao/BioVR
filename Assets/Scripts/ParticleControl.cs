using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ParticleControl : MonoBehaviour
{
    VisualEffect visualEffect;
    MyMessageListener shoulderData;
    TimerGL timerGatheringL;
    TimerGR timerGatheringR;
    TimerSL timerSpreadingL;
    TimerSR timerSpreadingR;

    [SerializeField]
    int tensionSpeed = 50;

    [SerializeField]
    int relaxationSpeed = 100;

    [SerializeField]
    int upperLimit = 200;

    [SerializeField]
    int lowerLimit = 2500;

    [SerializeField]
    float tensionThreshold = 0.6f;

    [SerializeField]
    int tensionTime = 3;

    [SerializeField]
    int relaxationTime = 5;


    int enableLeftGathering = 0;
    int enableRightGathering = 0;
    int enableLeftSpreading = 0;
    int enableRightSpreading = 0;

    bool leftGatheringStart = true;
    bool leftSpreadingStart = false;
    bool rightGatheringStart = true;
    bool rightSpreadingStart = false;




    // Start is called before the first frame update
    void Start()
    {
        visualEffect = this.GetComponent<VisualEffect>();
        shoulderData = GameObject.Find("SerialController").GetComponent<MyMessageListener>();
        timerGatheringL = GameObject.Find("[CameraRig]").GetComponent<TimerGL>();
        timerGatheringR = GameObject.Find("[CameraRig]").GetComponent<TimerGR>();
        timerSpreadingL = GameObject.Find("[CameraRig]").GetComponent<TimerSL>();
        timerSpreadingR = GameObject.Find("[CameraRig]").GetComponent<TimerSR>();

        visualEffect.SetInt("Min", lowerLimit);
        visualEffect.SetInt("Max", upperLimit);
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

        //visualEffect.SetInt("EnableLeftGathering", enableLeftGathering);
        //visualEffect.SetInt("EnableRightGathering", enableRightGathering);

        //timerGatheringL.TimerStart = true;
        //Debug.Log("G:" + timerGatheringL.CurrentTime);

        //timerSpreadingL.TimerStart = true;
        //Debug.Log("S:" + timerSpreadingL.CurrentTime);

        //Left shoulder: gathering particles
        if (shoulderLeft >= tensionThreshold && timerGatheringL.TimerStart == false && leftGatheringStart == true)
        {
            timerGatheringL.TimerStart = true;
            enableLeftGathering = 5;
            visualEffect.SetInt("EnableLeftGathering", enableLeftGathering);
        }
        else if (timerGatheringL.CurrentTime >= tensionTime && timerGatheringL.TimerStart == true)
        {

            enableLeftGathering = 0;
            visualEffect.SetInt("EnableLeftGathering", enableLeftGathering);
            timerGatheringL.ResetTimer();
            leftGatheringStart = false;
            leftSpreadingStart = true;
        }
        else
        {
            timerGatheringL.TimerStart = false;
            enableLeftGathering = 0;
            visualEffect.SetInt("EnableLeftGathering", enableLeftGathering);
        }

        //Left shoulder: spreading particles
        if (shoulderLeft < tensionThreshold && timerSpreadingL.TimerStart == false && leftSpreadingStart == true)
        {
            timerSpreadingL.TimerStart = true;
            enableLeftSpreading = 5;
            visualEffect.SetInt("EnableLeftSpreading", enableLeftSpreading);

        }
        else if (timerSpreadingL.CurrentTime >= relaxationTime && timerSpreadingL.TimerStart == true)
        {
            enableLeftSpreading = 0;
            visualEffect.SetInt("EnableLeftSpreading", enableLeftSpreading);
            timerSpreadingL.ResetTimer();
            leftGatheringStart = true;
            leftSpreadingStart = false;

        }
        else
        {
            timerSpreadingL.TimerStart = false;
            enableLeftSpreading = 0;
            visualEffect.SetInt("EnableLeftSpreading", enableLeftSpreading);

        }

        //Right shoulder: gathering particles
        if (shoulderRight >= tensionThreshold && timerGatheringR.TimerStart == false && rightGatheringStart == true)
        {
            timerGatheringR.TimerStart = true;
            enableRightGathering = 5;
            visualEffect.SetInt("EnableRightGathering", enableRightGathering);
        }
        else if (timerGatheringR.CurrentTime >= tensionTime && timerGatheringR.TimerStart == true)
        {

            enableRightGathering = 0;
            visualEffect.SetInt("EnableRightGathering", enableRightGathering);
            timerGatheringR.ResetTimer();
            rightGatheringStart = false;
            rightSpreadingStart = true;
        }
        else
        {
            timerGatheringR.TimerStart = false;
            enableRightGathering = 0;
            visualEffect.SetInt("EnableRightGathering", enableRightGathering);
        }

        //Right shoulder: spreading particles
        if (shoulderRight < tensionThreshold && timerSpreadingR.TimerStart == false && rightSpreadingStart == true)
        {
            timerSpreadingR.TimerStart = true;
            enableRightSpreading = 5;
            visualEffect.SetInt("EnableRightSpreading", enableRightSpreading);

        }
        else if (timerSpreadingR.CurrentTime >= relaxationTime && timerSpreadingR.TimerStart == true)
        {
            enableRightSpreading = 0;
            visualEffect.SetInt("EnableRightSpreading", enableRightSpreading);
            timerSpreadingR.ResetTimer();
            rightGatheringStart = true;
            rightSpreadingStart = false;

        }
        else
        {
            timerSpreadingR.TimerStart = false;
            enableRightSpreading = 0;
            visualEffect.SetInt("EnableRightSpreading", enableRightSpreading);

        }
    }
}
