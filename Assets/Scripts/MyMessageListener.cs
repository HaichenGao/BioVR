using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MyMessageListener : MonoBehaviour
{
    //double left_new;
    //double right_new;

    public double left;
    public double right;

    public int Avg_data_l = 200;
    public int Avg_data_r = 200;

    public int dataRec = 500;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMessageArrived(string msg)
    {
        int[] raw = msg.Split(',').Select(int.Parse).ToArray();
        Avg_data_l = (int)((19 * Avg_data_l + raw[0]) / 20);
        Avg_data_r = (int)((19 * Avg_data_r + raw[1]) / 20);
        left = (double)Avg_data_l / dataRec;
        right = (double)Avg_data_r / dataRec;

        if(left > 1)
        {
            left = 1;
        }

        if(right > 1)
        {
            right = 1;
        }

        //Debug.Log("ArrivedL:" + left.ToString("0.00"));
        //Debug.Log("ArrivedR:" + right.ToString("0.00"));
        //Debug.Log("ArrivedL:" + msg);
    }

    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}
