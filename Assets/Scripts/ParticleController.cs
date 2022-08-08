using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ParticleController : MonoBehaviour
{
    VisualEffect visualEffect;

    int particleAmountLeft;

    int particleAmountRight;

    [SerializeField]
    int tensionSpeed = 50;

    [SerializeField]
    int relaxationSpeed = 100;

    [SerializeField]
    int upperLimit = 200;

    [SerializeField]
    int lowerLimit = 2500;

    int enableLeft = 0;
    int enableRight = 0;

    bool flagq = false;
    bool flagw = false;

    // Start is called before the first frame update
    void Start()
    {
        visualEffect = this.GetComponent<VisualEffect>();
        particleAmountLeft = lowerLimit;
        particleAmountRight = lowerLimit;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Q)){
            particleAmountLeft -= tensionSpeed;
            enableLeft = 5;
            visualEffect.SetInt("ParticleAmountLeft", particleAmountLeft);
            visualEffect.SetInt("EnableLeft", enableLeft);

            flagq = true;

            if(Input.GetKey(KeyCode.Q) && particleAmountLeft < upperLimit)
            {
                particleAmountLeft = upperLimit;
            }

        }

        if (Input.GetKey(KeyCode.A) && flagq == true)
        {
            particleAmountLeft += relaxationSpeed;
            enableLeft = 5;
            visualEffect.SetInt("ParticleAmountLeft", particleAmountLeft);
            visualEffect.SetInt("EnableLeft", enableLeft);

            if(Input.GetKey(KeyCode.A) && particleAmountLeft > lowerLimit)
            {
                enableLeft = 0;
                visualEffect.SetInt("EnableLeft", enableLeft);
                //Debug.Log(enableLeft);
                particleAmountLeft = lowerLimit;
                flagq = false;
                flagw = false;
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            particleAmountRight -= tensionSpeed;
            enableRight = 5;
            visualEffect.SetInt("ParticleAmountRight", particleAmountRight);
            visualEffect.SetInt("EnableRight", enableRight);

            flagw = true;

            if (Input.GetKey(KeyCode.W) && particleAmountRight < upperLimit)
            {
                particleAmountRight = upperLimit;
            }

        }

        if (Input.GetKey(KeyCode.S) && flagw == true)
        {
            particleAmountRight += relaxationSpeed;
            enableRight = 5;
            visualEffect.SetInt("ParticleAmountRight", particleAmountRight);
            visualEffect.SetInt("EnableRight", enableRight);

            if (Input.GetKey(KeyCode.S) && particleAmountRight > lowerLimit)
            {
                enableRight = 0;
                visualEffect.SetInt("EnableRight", enableRight);
                particleAmountRight = lowerLimit;
                flagq = false;
                flagw = false;
            }
        }
    }
}
