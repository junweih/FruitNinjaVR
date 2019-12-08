using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootbombs : MonoBehaviour
{
    public GameObject bomb;
    public AudioClip bombSpawnClip;
    public float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            GameObject go = Instantiate(bomb);

            Rigidbody tmp = go.GetComponent<Rigidbody>();

            go.transform.position = gameObject.transform.position;

            AudioSource.PlayClipAtPoint(bombSpawnClip, go.transform.position, 0.7f);

            GameObject player = GameObject.Find("Camera");
            Vector3 target = player.transform.position;

            Vector3 vel = target - go.transform.position;
            vel.y = 0f;
            vel = vel.normalized * 2.2f;
            vel.y = 1.0f;
            tmp.velocity = vel;
            tmp.angularVelocity = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
            tmp.useGravity = false;

            yield return new WaitForSeconds(6.0f);
        }
    }
}
