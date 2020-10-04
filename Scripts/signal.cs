using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class signal : MonoBehaviour
{

    public GameObject satellite;
    public GameObject particle;
    private bool onCircuit = false;
    private bool onMars = false;
    private int state = 0;
    public int counter = 0;
    public GameObject reader;
    public GameObject writer;
    public GameObject toSpace;
    public Light voltimeter;
    public Slider energy;

    public void write(bool isZero = false)
    {
        writer.transform.position = isZero ? new Vector3(570, 67, 0) : new Vector3(570, -250, 0);
    }

    public void changeToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        toSpace.active = false;
        if (onMars)
        {
            energy.value -= 0.0003f;
            if (energy.value <= 0)
            {
                toSpace.active = true;
                Destroy(particle);
            }
            else
            {
                toSpace.active = false;
            }
        }

        voltimeter.color = onMars ? Color.green : Color.red;
        float scale = 0.005f;
        Vector3 direction = satellite.transform.position - transform.position;
        direction.x *= scale;
        direction.y *= scale;
        direction.z *= scale;

        if (particle)
        {
            Vector3 distance = particle.transform.position - transform.position;
            if (distance.x > 0 & distance.y > 0 & distance.z > 0 & !onMars)
            {
                particle.transform.position -= direction;
            }
            else
            {
                onMars = true;
            }
            if (onMars & !onCircuit)
            {
                particle.transform.position = new Vector3(0, 1.6f, 0);
                onCircuit = true;
            }
            else if (onCircuit)
            {
                counter++;
                if (counter > 200)
                {
                    counter = 0;
                }

                if (counter < 100)
                {
                    reader.transform.position = new Vector3(365, 67, 0);
                }
                else
                {
                    reader.transform.position = new Vector3(-115, -250, 0);
                }

                scale = 0.013f;
                switch (state)
                {
                    case 0:
                        direction = new Vector3(5, 1.6f, 0) - new Vector3(0, 1.6f, 0);
                        direction.x *= scale;
                        direction.y *= scale;
                        direction.z *= scale;
                        particle.transform.position += direction;
                        distance = new Vector3(5, 1.6f, 0) - particle.transform.position;
                        if (distance.x < 0)
                        {
                            state = 1;
                        }
                        break;
                    case 1:
                        direction = new Vector3(0, 1.6f, 8.66025403784f) - new Vector3(5, 1.6f, 0);
                        direction.x *= scale;
                        direction.y *= scale;
                        direction.z *= scale;
                        particle.transform.position += direction;
                        distance = new Vector3(0, 1.6f, 8.66025403784f) - particle.transform.position;
                        if (distance.x > 0 & distance.z < 0)
                        {
                            state = 2;
                        }
                        break;
                    case 2:
                        direction = new Vector3(-5, 1.6f, 0) - new Vector3(0, 1.6f, 8.66025403784f);
                        direction.x *= scale;
                        direction.y *= scale;
                        direction.z *= scale;
                        particle.transform.position += direction;
                        distance = new Vector3(-5, 1.6f, 0) - particle.transform.position;
                        if (distance.x > 0 & distance.z > 0)
                        {
                            state = 3;
                        }
                        break;
                    case 3:
                        direction = new Vector3(0, 1.6f, 0) - new Vector3(-5, 1.6f, 0);
                        direction.x *= scale;
                        direction.y *= scale;
                        direction.z *= scale;
                        particle.transform.position += direction;
                        distance = new Vector3(0, 1.6f, 0) - particle.transform.position;
                        if (distance.x < 0)
                        {
                            state = 0;
                        }
                        break;
                }
            }
        }
        
        


    }
}
