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
    public GameObject deathExplosion;

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
        if (died)
            return;

        GameObject victim = collision.collider.gameObject;

        if (victim.CompareTag("player"))
        {
            Die(collision.contacts[0].point);
            if (global)
                global.health--;
            Destroy(this.gameObject);
            return;
        }
    }

    public void Die(Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(bombExplode, pos);

        GameObject psObj = Instantiate(deathExplosion, pos, Quaternion.identity) as GameObject;
        ParticleSystem ps = psObj.GetComponent<ParticleSystem>();
        float startTime = ps.main.startLifetime.constantMax;
        float duration = ps.main.duration;
        float totalDuration = startTime + duration;
        Destroy(psObj, totalDuration);
    }
}
