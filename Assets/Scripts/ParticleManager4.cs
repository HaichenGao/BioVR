using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.VFX;


public class ParticleManager4 : MonoBehaviour
{
 
    VisualEffect visualEffect;
    MyMessageListener shoulderData;
    ThresholdCalibrate thresholdCalibrate;
    TimerRL timerRelaxingL;
    TimerRR timerRelaxingR;
    TimerSL timerSpreadingL;
    TimerSR timerSpreadingR;
    TimerGL timerGatheringL;
    TimerGR timerGatheringR;
    TimerFL timerFadingL;
    TimerFR timerFadingR;
    Timer resetTimerL;
    Timer resetTimerR;

    GameObject[] flowers;

    GameObject[] RuneL;

    GameObject[] RuneR;


    [SerializeField]
    GameObject[] beads = new GameObject[6];

    public AudioSource audioBefore;
    //public AudioSource audioAfter;

    public GameObject oL1;
    public GameObject oL2;
    public GameObject oL3;

    public GameObject oR1;
    public GameObject oR2;
    public GameObject oR3;

    float setTensionThresholdL;
    float setTensionThresholdR;
    float setRelaxationThresholdL;
    float setRelaxationThresholdR;

    [SerializeField]
    int upperLimit = 2;

    [SerializeField]
    int lowerLimit = 0;

    [SerializeField]
    float tensionTime = 3;

    [SerializeField]
    float spreadingTime = 3f;

    [SerializeField]
    float relaxationTime = 10;

    [SerializeField]
    float fadeTime = 1;

    [SerializeField]
    float resetTime = 10;

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

    public int enlightenL = 0;
    public int enlightenR = 0;

    bool leftGatheringStart = true;
    public bool leftSpreadingStart = false;
    bool leftRelaxingStart = false;
    bool rightGatheringStart = true;
    public bool rightSpreadingStart = false;
    bool rightRelaxingStart = false;
    public bool isRelaxationFinishedL = false;
    public bool isRelaxationFinishedR = false;



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
        resetTimerL = GameObject.Find("ResetTimerL").GetComponent<Timer>();
        resetTimerR = GameObject.Find("ResetTimerR").GetComponent<Timer>();
        thresholdCalibrate = GameObject.Find("SerialController").GetComponent<ThresholdCalibrate>();

        foreach(GameObject sphere in SphereL)
        {
            sphere.SetActive(false);
        }

        foreach (GameObject sphere in SphereR)
        {
            sphere.SetActive(false);
        }

        visualEffect.SetInt("Min", lowerLimit);
        visualEffect.SetInt("MaxL", upperLimit);
        visualEffect.SetInt("MaxR", upperLimit);
        visualEffect.SetInt("IntensityL", 5);
        visualEffect.SetInt("IntensityR", 5);

        enableLeftGathering = 5;
        visualEffect.SetInt("EnableLeftGathering", enableLeftGathering);

        enableRightGathering = 5;
        visualEffect.SetInt("EnableRightGathering", enableRightGathering);

        setTensionThresholdL = thresholdCalibrate.tensionThresholdL;
        setTensionThresholdR = thresholdCalibrate.tensionThresholdR;
        setRelaxationThresholdL = thresholdCalibrate.relaxationThresholdL;
        setRelaxationThresholdR = thresholdCalibrate.relaxationThresholdR;

        flowers = GameObject.FindGameObjectsWithTag("Flowers");
        foreach (GameObject flowers in flowers)
        {
            flowers.SetActive(false);
        }

        //RuneL = GameObject.FindGameObjectsWithTag("RuneL");
        //foreach (GameObject rune in RuneL)
        //{
        //    rune.SetActive(false);
        //    //Debug.Log(rune.name);
        //}

        //RuneR = GameObject.FindGameObjectsWithTag("RuneR");
        //foreach (GameObject rune in RuneR)
        //{
        //    rune.SetActive(false);
        //    //Debug.Log(rune.name);
        //}

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

        visualEffect.SetFloat("TimerFadingL", timerFadingL.CurrentTime);
        visualEffect.SetFloat("TimerFadingR", timerFadingR.CurrentTime);

        #region Left Shoulder
        //Left shoulder: gathering particles
        if (shoulderLeft >= setTensionThresholdL && (timerGatheringL.CurrentTime < tensionTime) && leftGatheringStart == true && leftSpreadingStart == false &&currentIterationL <= cycle)
        {
            visualEffect.SetInt("IntensityL", 50);
            SphereL[currentIterationL].SetActive(true);
            timerGatheringL.TimerStart = true;
            resetTimerL.ResetTimer();
        }
        else if (timerGatheringL.CurrentTime >= tensionTime && timerGatheringL.TimerStart == true)
        {
            timerGatheringL.ResetTimer();
            leftGatheringStart = false;
            leftSpreadingStart = true;
            leftRelaxingStart = true;
            visualEffect.SetBool("LeftRelaxingStart", leftRelaxingStart);
            enableLeftGathering = 0;
            visualEffect.SetInt("EnableLeftGathering", enableLeftGathering);
            timerFadingL.TimerStart = true;
        }
        else if (shoulderLeft < setTensionThresholdL && timerGatheringL.TimerStart == true)
        {
            timerGatheringL.TimerStart = false;
            visualEffect.SetInt("IntensityL", 5);
            resetTimerL.TimerStart = true;
        }

        if(timerFadingL.CurrentTime >= fadeTime)
        {
            timerFadingL.ResetTimer();
        }

        if (resetTimerL.CurrentTime >= resetTime)
        {
            timerGatheringL.ResetTimer();
            resetTimerL.ResetTimer();
        }

        //Left shoulder: spreading particles
        if (shoulderLeft < setRelaxationThresholdL && timerSpreadingL.TimerStart == false && leftSpreadingStart == true && currentIterationL < cycle)
        {
                                     
            visualEffect.SetInt("SphereL", currentIterationL);
            timerSpreadingL.TimerStart = true;
            timerRelaxingL.TimerStart = true;
            enableLeftSpreading = 5;
            visualEffect.SetInt("EnableLeftSpreading", enableLeftSpreading);

        }
        else if (timerSpreadingL.CurrentTime >= spreadingTime && timerSpreadingL.TimerStart == true)
        {
            isRelaxationFinishedL = true;
            leftSpreadingStart = false;
            enableLeftSpreading = 0;
            visualEffect.SetInt("EnableLeftSpreading", enableLeftSpreading);
            timerSpreadingL.ResetTimer();
            
        }
        else if (shoulderLeft >= setRelaxationThresholdL && timerSpreadingL.TimerStart == true)
        {
            timerSpreadingL.TimerStart = false;
            enableLeftSpreading = 0;
            visualEffect.SetInt("EnableLeftSpreading", enableLeftSpreading);
        }


        if (timerRelaxingL.CurrentTime >= relaxationTime && timerRelaxingL.TimerStart == true && leftSpreadingStart == false)
        {
            timerRelaxingL.ResetTimer();
            if (currentIterationL < cycle - 1)
            {
                visualEffect.SetInt("IntensityL", 5);
                leftGatheringStart = true;
                leftRelaxingStart = false;
                isRelaxationFinishedL = false;
                visualEffect.SetBool("LeftRelaxingStart", leftRelaxingStart);
                enableLeftGathering = 5;
                visualEffect.SetInt("EnableLeftGathering", enableLeftGathering);
                currentIterationL += 1;
            }else{
                currentIterationL += 1;
            }

            if (currentIterationL == 10)
            {
                oL1.GetComponent<Animator>().SetBool("oL1", true);
                RuneL[0].SetActive(true);
                enlightenL += 1;
            }

            if (currentIterationL == 20)
            {
                oL2.GetComponent<Animator>().SetBool("oL2", true);
                RuneL[1].SetActive(true);
                enlightenL += 1;
            }

            if (currentIterationL == 30)
            {
                oL3.GetComponent<Animator>().SetBool("oL3", true);
                RuneL[2].SetActive(true);
                enlightenL += 1;
            }

        }
        #endregion



        #region Right Shoulder
        //Right shoulder: gathering particles
        if (shoulderRight >= setTensionThresholdR && (timerGatheringR.CurrentTime < tensionTime) && rightGatheringStart == true && currentIterationR <= cycle)
        {
            visualEffect.SetInt("IntensityR", 50);
            SphereR[currentIterationR].SetActive(true);
            timerGatheringR.TimerStart = true;
            resetTimerR.ResetTimer();

        }
        else if (timerGatheringR.CurrentTime >= tensionTime && timerGatheringR.TimerStart == true)
        {
            timerGatheringR.ResetTimer();
            rightGatheringStart = false;
            rightSpreadingStart = true;
            rightRelaxingStart = true;
            visualEffect.SetBool("RightRelaxingStart", rightRelaxingStart);
            enableRightGathering = 0;
            visualEffect.SetInt("EnableRightGathering", enableRightGathering);
            timerFadingR.TimerStart = true;
        }
        else if (shoulderRight < setTensionThresholdR && timerGatheringR.TimerStart == true)
        {
            timerGatheringR.TimerStart = false;
            visualEffect.SetInt("IntensityR", 5);
            resetTimerR.TimerStart = true;
        }

        if (timerFadingR.CurrentTime >= fadeTime)
        {
            timerFadingR.ResetTimer();
        }

        if (resetTimerR.CurrentTime >= resetTime)
        {
            timerGatheringR.ResetTimer();
            resetTimerR.ResetTimer();
        }

        //Right shoulder: spreading particles
        if (shoulderRight < setRelaxationThresholdR && timerSpreadingR.TimerStart == false && rightSpreadingStart == true && currentIterationR < cycle)
        {
            //rightSpreadingStart = false;
            visualEffect.SetInt("SphereR", currentIterationR);
            timerSpreadingR.TimerStart = true;
            timerRelaxingR.TimerStart = true;
            enableRightSpreading = 5;
            visualEffect.SetInt("EnableRightSpreading", enableRightSpreading);

        }
        else if (timerSpreadingR.CurrentTime >= spreadingTime && timerSpreadingR.TimerStart == true)
        {
            isRelaxationFinishedR = true;
            rightSpreadingStart = false;
            enableRightSpreading = 0;
            visualEffect.SetInt("EnableRightSpreading", enableRightSpreading);
            timerSpreadingR.ResetTimer();
            

        }
        else if (shoulderRight >= setRelaxationThresholdR && timerSpreadingR.TimerStart == true)
        {
            timerSpreadingR.TimerStart = false;
            enableRightSpreading = 0;
            visualEffect.SetInt("EnableRightSpreading", enableRightSpreading);
        }


        if (timerRelaxingR.CurrentTime >= relaxationTime && timerRelaxingR.TimerStart == true && rightSpreadingStart == false)
        {
            timerRelaxingR.ResetTimer();
            if (currentIterationR < cycle -1)
            {
                visualEffect.SetInt("IntensityR", 5);
                rightGatheringStart = true;
                rightRelaxingStart = false;
                isRelaxationFinishedR = false;
                visualEffect.SetBool("RightRelaxingStart", rightRelaxingStart);
                enableRightGathering = 5;
                visualEffect.SetInt("EnableRightGathering", enableRightGathering);
                currentIterationR += 1;
            }else{
                currentIterationR += 1;
            }

            if (currentIterationR == 10)
            {
                oR1.GetComponent<Animator>().SetBool("oR1", true);
                RuneR[0].SetActive(true);
                enlightenR += 1;
            }

            if (currentIterationR == 20)
            {
                oR2.GetComponent<Animator>().SetBool("oR2", true);
                RuneR[1].SetActive(true);
                enlightenR += 1;
            }

            if (currentIterationR == 30)
            {
                oR3.GetComponent<Animator>().SetBool("oR3", true);
                RuneR[2].SetActive(true);
                enlightenR += 1;
            }

        }
        #endregion

        if(currentIterationL == 30 && currentIterationR == 30)
        {
            StartCoroutine(WaitToFinish(4f, "finish"));
            StartCoroutine(WaitToExecute(12f, flowers, "blooming"));
            audioBefore.Play();
            //audioAfter.Stop();
        }
    }

    IEnumerator WaitToExecute(float time, GameObject[] group, string id)
    {
        yield return new WaitForSeconds(time);
        ActivateFlowers(group, id);
        
    }

    IEnumerator WaitToFinish(float time, string id)
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < beads.Length; i++)
        {
            beads[i].GetComponent<Animator>().SetBool(id, true);
            yield return new WaitForSeconds(0.7f);
        }
    }

    IEnumerator SetObjectActive(float time, GameObject o)
    {
        yield return new WaitForSeconds(time);
        o.SetActive(false);
    }

    void ActivateFlowers(GameObject[] group, string id)
    {
        foreach (GameObject flowers in group)
        {
            flowers.SetActive(true);
            flowers.GetComponent<Animator>().SetBool(id, true);
        }
    }
}
