using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMove : MonoBehaviour
{
    public float amplitude = 0.015f;
    public float currentTime = 0.0f;
    Vector3 initialPosition;
    // Start is called before the first frame update
    void Awake()
    {
        initialPosition = gameObject.transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = initialPosition + new Vector3(amplitude * (Mathf.Sin(2.07f * currentTime) + Mathf.Sin(1.65f * currentTime) + Mathf.Sin(0.23f * currentTime) + Mathf.Sin(0.37f * currentTime)), amplitude * (Mathf.Sin(2.07f * currentTime + 0.5f * Mathf.PI) + Mathf.Sin(0.96f * currentTime + 0.5f * Mathf.PI) + Mathf.Sin(1.98f * currentTime) + Mathf.Sin(1.08f * currentTime)), 0f);
        currentTime += 1 * Time.deltaTime;
    }
}
