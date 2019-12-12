using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFruit : MonoBehaviour
{
    // Update is called once per frame
    private float t;
    public bool died;
    public bool useGravity;
    public GameObject deathExplosion;
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
        if (useGravity)
        {
            Vector3 gravity = -0.6f * Vector3.up;
            GetComponent<Rigidbody>().AddForce(gravity, ForceMode.Acceleration);
        }
    }
    public void Die()
    {
        GameObject psObj = Instantiate(deathExplosion, gameObject.transform.position, Quaternion.LookRotation(Vector3.up, -Vector3.forward)) as GameObject;
        ParticleSystem ps = psObj.GetComponent<ParticleSystem>();
        float startTime = ps.main.startLifetime.constantMax;
        float duration = ps.main.duration;
        float totalDuration = startTime + duration;
        Destroy(psObj, totalDuration);
    }
}
