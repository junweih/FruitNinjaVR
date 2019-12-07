using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    // Update is called once per frame
    private float t;
    public bool died;
    private GlobalLogic global;
    public AudioClip bombExplode;
    private void Start()
    {
        t = 2.0f;
        died = false;

        GameObject tmp = GameObject.Find("Global");
        if (tmp)
        {
            global = tmp.GetComponent<GlobalLogic>();
        }
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

    void OnCollisionEnter(Collision collision)
    {
        GameObject victim = collision.collider.gameObject;

        if (victim.CompareTag("player"))
        {
            AudioSource.PlayClipAtPoint(bombExplode, victim.transform.position);
            if (global)
                global.health--;
            Destroy(this.gameObject);
            return;
        }
    }
}
