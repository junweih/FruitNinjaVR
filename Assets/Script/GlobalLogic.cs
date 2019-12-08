using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalLogic : MonoBehaviour
{
    public GameObject fruitShooter;
    public GameObject bombShooter;
    public int score;
    public TextMesh scoreUI;
    public int health;
    //public GameObject SoundEffect;
    public float waitTime;
    private float curWaitT;
    private Vector3 spawnCenter;
    public AudioClip alertClip;
    public AudioClip fruitSpawnClip;
    public AudioClip bombSpawnClip;
    public float fruitPossibility;
    public GameObject player;
    private
    //private SoundEffects Sound;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        health = 3;
        scoreUI = GameObject.Find("ScoreUI").GetComponent<TextMesh>();
        //Sound = SoundEffect.GetComponent<SoundEffects>();
    }

    void randomSpawnCenter()
    {
        spawnCenter = transform.position;
        float rng = Random.Range(2.0f, 4.2f);
        spawnCenter.x += Mathf.Sin(rng) * 8f;
        spawnCenter.z += Mathf.Cos(rng) * 8f;
        spawnCenter.y += Random.Range(1.3f, 1.6f);
        AudioSource.PlayClipAtPoint(alertClip, spawnCenter);
    }

    Vector3 randomSpawnPoint()
    {
        Vector3 spwanPoint = spawnCenter;
        float rng = Random.Range(-3.14f, 3.14f);
        spwanPoint.x += Mathf.Sin(rng) * 1f;
        spwanPoint.z += Mathf.Cos(rng) * 1f;
        spwanPoint.y += Random.Range(-0.5f, 0.5f);
        return spwanPoint;
    }
    

    private void Update()
    {
        curWaitT -= Time.deltaTime;
        if (curWaitT < 0)
        {
            Debug.Log("RESET CENTER");
            randomSpawnCenter();
            float rng = Random.Range(0.0f, 1.0f);
            GameObject go;
            if (rng < fruitPossibility)
            {
                go = Instantiate(fruitShooter);
                
            } else
            {
                go = Instantiate(bombShooter);
                //go.transform.rotation = Quaternion.LookRotation(player.transform.position - go.transform.position);
            }
            Rigidbody tmp = go.GetComponent<Rigidbody>();
            go.transform.position = spawnCenter;
            go.transform.rotation = Quaternion.LookRotation(player.transform.position - go.transform.position);
            curWaitT = waitTime;
        }
        scoreUI.text = "Score : " + score.ToString();
        if (health < 0)
        {
            SceneManager.LoadScene("MainMenu");
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
