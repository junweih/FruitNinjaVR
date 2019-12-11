using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalLogic : MonoBehaviour
{
    public GameObject fruitShooter;
    public GameObject bombShooter;
    public int score;
    public int health;
    //public GameObject SoundEffect;
    public float waitTime;
    public AudioClip alertClip;
    public AudioClip fruitSpawnClip;
    public AudioClip bombSpawnClip;
    public float fruitPossibility;
    public GameObject player;

    public int[] goals;

    private float curWaitT;
    private Vector3 spawnCenter;
    private TextMesh scoreUI;
    private TextMesh levelUI;

    private float curLevel;


    //private SoundEffects Sound;
    // Start is called before the first frame update
    void Start()
    {
        scoreUI = GameObject.Find("ScoreUI").GetComponent<TextMesh>();
        levelUI = GameObject.Find("LevelUI").GetComponent<TextMesh>();
        //Sound = SoundEffect.GetComponent<SoundEffects>();
        curLevel = 0.0f;
        setNextLevel();
    }

    void setNextLevel()
    {
        score = 0;
        health = 3;
        curLevel += 1.0f;
        fruitPossibility = 1.0f / (0.33f * curLevel + 0.67f);
        waitTime = 13.5f / (curLevel + 2.0f) + 1.5f;
        curWaitT = waitTime;
        levelUI.text = "Level " + ((int)curLevel).ToString() + " Goal: " + goals[(int)curLevel - 1].ToString();
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

    void updateLevel()
    {
        if(score >= goals[(int)curLevel - 1])
        {
            foreach (GameObject shooter in GameObject.FindGameObjectsWithTag("shooter"))
            {
                Destroy(shooter);
            }

            foreach (GameObject fruit in GameObject.FindGameObjectsWithTag("fruit"))
            {
                fruit.GetComponent<DestroyFruit>().Die();
                fruit.GetComponent<DestroyFruit>().died = true;
            }

            foreach (GameObject fruitP in GameObject.FindGameObjectsWithTag("fruitPieces"))
            {
                fruitP.GetComponent<DestroyFruit>().died = true;
            }

            setNextLevel();
        }
    }
    

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log(Input.mousePosition);
            score += 10;
        }

        updateLevel();

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
