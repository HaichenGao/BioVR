using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Camera camera;

    Vector3 positionCameraZ;
    Vector3 positionCameraX;
    // Start is called before the first frame update
    void Start()
    {
        camera = gameObject.GetComponent<Camera>();
        positionCameraZ = new Vector3(-0.54f, 0.83f, 4.22f);
        positionCameraX = new Vector3(-0.31f, 0.66f, 2.13f);
        //rotationCameraZ = new Vector3(5.97f, 188.89f, 0);
        camera.transform.position = positionCameraZ;
        //camera.transform.rotation = Quaternion.Euler(5.97f, 188.89f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            camera.transform.position = positionCameraZ;
        }

        if (Input.GetKey(KeyCode.X))
        {
            camera.transform.position = positionCameraX;
        }
    }
}
