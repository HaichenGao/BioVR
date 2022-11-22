using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    bool fIsPressed = false;
    bool gIsPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F) && fIsPressed == false)
        {
            SceneManager.LoadScene("BioVR_Game 5");
            fIsPressed = true;
        }

        if (Input.GetKeyUp(KeyCode.G) && gIsPressed == false)
        {
            SceneManager.LoadScene("BioVR_Game 6");
            gIsPressed = true;
        }
    }
}
