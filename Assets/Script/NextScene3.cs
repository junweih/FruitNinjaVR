using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene3 : MonoBehaviour
{
    // Start is called before the first frame update
    public float t;
    public bool enable;
    void Start()
    {
        t = 1f;
        enable = false;
        Rigidbody tmp = GetComponent<Rigidbody>();
        tmp.angularVelocity = new Vector3(0f, 1.5f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(enable)
        {
            t -= Time.deltaTime;
            if(t < 0)
            {
                Application.Quit();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        enable = true;
    }
}
