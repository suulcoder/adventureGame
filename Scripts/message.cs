using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class message : MonoBehaviour
{
    public GameObject particle1;
    public GameObject particle2;
    public GameObject earth;
    public GameObject mars;
    public bool activate = false;

    public void generate()
    {
        activate = true;
    }

    public void changeToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private bool evaluateDistance(Vector3 initial, Vector3 end)
    {
        Vector3 diference = end - initial;
        return diference.x > 0 || diference.y > 0 || diference.z > 0;
    }

    // Start is called before the first frame update
    void Start()
    {   
        Vector3 myPosition = transform.position;
        particle1.transform.position = myPosition;
        particle2.transform.position = myPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (activate)
        {
            float myScale = 0.001f;
            Vector3 toearth = (earth.transform.position - transform.position);
            Vector3 toMars = (mars.transform.position - transform.position);

            if (evaluateDistance(particle1.transform.position, earth.transform.position))
            {
                particle1.transform.position += new Vector3(
                    toearth.x * myScale,
                    toearth.y * myScale,
                    toearth.z * myScale
                );
            }

            if (evaluateDistance(mars.transform.position, particle2.transform.position))
            {
                particle2.transform.position += new Vector3(
                    toMars.x * myScale,
                    toMars.y * myScale,
                    toMars.z * myScale
                );
            }
        }
    }
}
