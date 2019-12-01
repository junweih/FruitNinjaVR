using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFruit : MonoBehaviour
{
    // Update is called once per frame
    private float t;
    public bool died;
    private void Start()
    {
        t = 2.0f;
        died = false;
    }

    void Update()
    {
        if(died)
        {
            t -= Time.deltaTime;
            if(t < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 gravity = -0.6f * Vector3.up;
        GetComponent<Rigidbody>().AddForce(gravity, ForceMode.Acceleration);
    }
}
