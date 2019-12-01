using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalLogic : MonoBehaviour
{
    public GameObject[] fruits;
    public int score;
    public TextMesh scoreUI;
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
        score = 0;
        health = 3;
        scoreUI = GameObject.Find("ScoreUI").GetComponent<TextMesh>();
    }

    private void Update()
    {
        scoreUI.text = "Score : " + score.ToString();
        if (health < 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    IEnumerator Spawn()
    {
        while(true)
        {
            GameObject go = Instantiate(fruits[Random.Range(0, fruits.Length)]);
            Rigidbody tmp = go.GetComponent<Rigidbody>();

            Vector3 pos = transform.position;
            float rng = Random.Range(-3.14f, 3.14f);
            pos.x += Mathf.Sin(rng) * 8f;
            pos.z += Mathf.Cos(rng) * 8f;
            pos.y += Random.Range(1.3f, 1.6f);
            go.transform.position = pos;

            Vector3 target = transform.position;
            rng = Random.Range(-3.14f, 3.14f);
            target.x += Mathf.Sin(rng) * 0.5f;
            target.z += Mathf.Cos(rng) * 0.5f;

            Vector3 vel = target - pos;
            vel.y = 0f;
            vel = vel.normalized * 2.2f;
            vel.y = 1.0f;
            tmp.velocity = vel;
            tmp.angularVelocity = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
            tmp.useGravity = false;

            if(go.GetComponent<DestroyFruit>())
            {
                go.GetComponent<DestroyFruit>().useGravity = true;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject tmp = other.gameObject;
        if (tmp.CompareTag("fruitPieces") || tmp.CompareTag("fruit"))
        {
            tmp.GetComponent<DestroyFruit>().died = true;
        } else if (tmp.CompareTag("bomb"))
        {
            tmp.GetComponent<BombScript>().died = true;
        }
    }
}
