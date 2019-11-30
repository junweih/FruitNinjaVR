using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFruits : MonoBehaviour
{
    public GameObject[] fruits;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while(true)
        {
            GameObject go = Instantiate(fruits[Random.Range(0, fruits.Length)]);
            Rigidbody tmp = go.GetComponent<Rigidbody>();

            tmp.velocity = new Vector3(0f, 5f, 2.5f);
            tmp.angularVelocity = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
            tmp.useGravity = true;

            Vector3 pos = transform.position;
            pos.x += Random.Range(-1f, 1f);
            go.transform.position = pos;

            yield return new WaitForSeconds(1f);
        }
    }
}
