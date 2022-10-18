using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class ThresholdCalibrate : MonoBehaviour
{
    MyMessageListener myMessageListener;
    Timer myTimer;

    List<double> tensionData_L = new List<double>();
    List<double> tensionData_R = new List<double>();
    List<double> relaxationData_L = new List<double>();
    List<double> relaxationData_R = new List<double>();

    public float timeWindow = 3f;
    public float tensionThresholdOffsetL = 0.1f;
    public float tensionThresholdOffsetR = 0.1f;
    public float relaxationThresholdOffsetL = 0.1f;
    public float relaxationThresholdOffsetR = 0.1f;

    public float tensionThresholdL;
    public float tensionThresholdR;
    public float relaxationThresholdL;
    public float relaxationThresholdR;


    bool tensionRecordFinish = false;
    bool relaxationRecordFinish = false;

    bool isTPressed = false;
    bool isRPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        myMessageListener = this.GetComponent<MyMessageListener>();
        myTimer = this.GetComponent<Timer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.T) && isTPressed == false)
        {
            isTPressed = true;
            Debug.Log("T is pressed");
            StartCoroutine(TensionRecord());
        }

        if (Input.GetKeyDown(KeyCode.R) && isRPressed == false)
        {
            isRPressed = true;
            Debug.Log("R is pressed");
            StartCoroutine(RelaxationRecord());
        }

        if (myTimer.TimerStart == true && myTimer.CurrentTime >= timeWindow)
        {
            myTimer.TimerStart = false;
            myTimer.ResetTimer();
        }

        if(tensionRecordFinish == true)
        {
            tensionThresholdL = Mathf.Round((float)(tensionData_L.Average() * (1 - tensionThresholdOffsetL) * 100f)) / 100f;
            tensionThresholdR = Mathf.Round((float)(tensionData_R.Average() * (1 - tensionThresholdOffsetR) * 100f)) / 100f;
            tensionRecordFinish = false;
            Debug.Log("tensionThresholdL: " + tensionThresholdL);
            Debug.Log("tensionThresholdR: " + tensionThresholdR);
        }

        if (relaxationRecordFinish == true)
        {
            relaxationThresholdL = Mathf.Round((float)(relaxationData_L.Average() * (1 + relaxationThresholdOffsetL) * 100f)) / 100f;
            relaxationThresholdR = Mathf.Round((float)(relaxationData_R.Average() * (1 + relaxationThresholdOffsetR) * 100f)) / 100f;
            relaxationRecordFinish = false;
            Debug.Log("relaxationThresholdL: " + relaxationThresholdL);
            Debug.Log("relaxationThresholdR: " + relaxationThresholdR);
        }

    }

    

    IEnumerator TensionRecord()
    {
        myTimer.TimerStart = true;
        while (myTimer.TimerStart == true)
        {
            yield return new WaitForSeconds(0.1f);
            tensionData_L.Add(myMessageListener.left);
            tensionData_R.Add(myMessageListener.right);
        }
        tensionRecordFinish = true;
        //foreach(double item in tensionData_L)
        //{
        //    Debug.Log("item:" + item.ToString());
        //}

    }

    IEnumerator RelaxationRecord()
    {
        myTimer.TimerStart = true;
        while (myTimer.TimerStart == true)
        {
            yield return new WaitForSeconds(0.1f);
            relaxationData_L.Add(myMessageListener.left);
            relaxationData_R.Add(myMessageListener.right);
        }
        relaxationRecordFinish = true;
        //foreach (double item in tensionData_L)
        //{
        //    Debug.Log("item:" + item.ToString());
        //}
    }
}
