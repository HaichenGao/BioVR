using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    [SerializeField]
    float sensitivity = 3f;

    //Transform trans = Camera.main.transform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform trans = Camera.main.transform;
        trans.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);
        trans.Rotate(-Input.GetAxis("Mouse Y") * sensitivity, 0, 0);
        trans.Rotate(0, 0, -Input.GetAxis("ZandX") * 90 * Time.deltaTime);
    }
}
