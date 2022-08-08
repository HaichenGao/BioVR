using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField]
    float startTime;

    [SerializeField]
    float currentTime;

    bool timerStart = false;

    
    public float StartTime
    {
        set { startTime = value; }
    }

    public float CurrentTime
    {
        get { return currentTime; }
        set { currentTime = value; }
    }

    public bool TimerStart
    {
        get { return timerStart; }
        set { timerStart = value; }
    }

    public void CountdownStart()
    {
        
        if(currentTime > 0f)
        {
            currentTime -= 1 * Time.deltaTime;
            //timerStart = true;

        }
        else
        {
            currentTime = 0f;
            timerStart = false;
            
        }
        
    }

    public void ResetTimer()
    {
        currentTime = startTime;
        //timerStart = false;
    }
}
