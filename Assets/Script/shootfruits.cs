using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootfruits : MonoBehaviour
{
    public GameObject[] fruits;
    public AudioClip fruitSpawnClip;
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
        if(lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            int index = Random.Range(0, fruits.Length);
            GameObject go = Instantiate(fruits[index]);

            Rigidbody tmp = go.GetComponent<Rigidbody>();

            go.transform.position = gameObject.transform.position;
            
            AudioSource.PlayClipAtPoint(fruitSpawnClip, go.transform.position, 0.8f);

            GameObject player = GameObject.Find("Camera");
            Vector3 target = player.transform.position;
            float rng = Random.Range(-3.14f, 3.14f);
            target.x += Mathf.Sin(rng) * 0.5f;
            target.z += Mathf.Cos(rng) * 0.5f;

            Vector3 vel = target - go.transform.position;
            vel.y = 0f;
            vel = vel.normalized * 2.2f;
            vel.y = 1.0f;
            tmp.velocity = vel;
            tmp.angularVelocity = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
            tmp.useGravity = false;
            
            go.GetComponent<DestroyFruit>().useGravity = true;

            yield return new WaitForSeconds(6.0f);
        }
    }
}
