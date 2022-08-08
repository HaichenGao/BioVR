using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerGL : MonoBehaviour
{
    float currentTime = 0f;
    bool timerStart = false;

    // Start is called before the first frame update

    public bool TimerStart
    {
        set { timerStart = value; }
        get { return timerStart; }
    }

    public float CurrentTime
    {
        get { return currentTime; }
    }

    // Update is called once per frame
    void Update()
    {
        if(timerStart == true)
        {
            CountingTime();
        }
    }

    public void CountingTime()
    {
        currentTime += 1 * Time.deltaTime;
    }

    public void ResetTimer()
    {
        currentTime = 0f;
        timerStart = false;
    }
}
