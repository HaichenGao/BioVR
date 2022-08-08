using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCameraRight : MonoBehaviour
{
    [SerializeField]
    float sensitivity = 1.7f;

    [SerializeField]
    float maxYAngle = 80f;

    //Transform z;
    //Transform x;

    Vector2 currentRotation;

    // Start is called before the first frame update
    void Start()
    {
        //z.position = new Vector3(-0.54f, 0.83f, 4.22f);
        //z.rotation = Quaternion.Euler(5.97f, 188.89f, 0);
        //Camera.main.transform.position = z.position;
        //Camera.main.transform.rotation = z.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
        currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
        currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
        Camera.main.transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);

        //if (Input.GetKey(KeyCode.Z))
        //{
        //    Camera.main.transform.position = 
        //}
    }
}
