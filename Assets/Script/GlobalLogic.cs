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
    private float randomTime;
    public float waitTime;
    private float curWaitT;
    private Vector3 spwanCenter;
    public float randomSlot;
    public AudioClip spawnClip;
    bool pauseGeneration;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
        score = 0;
        health = 3;
        waitTime = 3.0f;
        pauseGeneration = false;
        scoreUI = GameObject.Find("ScoreUI").GetComponent<TextMesh>();
        randomSpawnCenter();
    }

    void randomSpawnCenter()
    {
        randomTime = randomSlot;
        spwanCenter = transform.position;
        float rng = Random.Range(-1.57f, 1.57f);
        spwanCenter.x += Mathf.Sin(rng) * 8f;
        spwanCenter.z += Mathf.Cos(rng) * 8f;
        spwanCenter.y += Random.Range(1.3f, 1.6f);
        AudioSource.PlayClipAtPoint(spawnClip, spwanCenter);
    }

    Vector3 randomSpawnPoint()
    {
        Vector3 spwanPoint = spwanCenter;
        float rng = Random.Range(-3.14f, 3.14f);
        spwanPoint.x += Mathf.Sin(rng) * 1f;
        spwanPoint.z += Mathf.Cos(rng) * 1f;
        spwanPoint.y += Random.Range(-0.5f, 0.5f);
        return spwanPoint;
    }
    

    private void Update()
    {
        float deltaT = Time.deltaTime;
        if (pauseGeneration)
        {
      
            curWaitT -= deltaT;
            if (curWaitT < 0)
            {
                Debug.Log("RESET CENTER");
                pauseGeneration = false;
                randomSpawnCenter();
            }
        } else
        {
            randomTime -= deltaT;
            if (randomTime < 0)
            {
                Debug.Log("start count reset center");
                curWaitT = waitTime;
                pauseGeneration = true;
            }
        }
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
            if (pauseGeneration)
            {
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                GameObject go = Instantiate(fruits[Random.Range(0, fruits.Length)]);

                Rigidbody tmp = go.GetComponent<Rigidbody>();

                go.transform.position = randomSpawnPoint();

                Vector3 target = transform.position;
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

                if (go.GetComponent<DestroyFruit>())
                {
                    go.GetComponent<DestroyFruit>().useGravity = true;
                }

                yield return new WaitForSeconds(0.5f);
            }
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
