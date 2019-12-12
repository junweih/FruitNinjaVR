using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

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

    public SteamVR_Action_Vibration hapticAction;
    public AudioSource heartBeat;


    //private SoundEffects Sound;
    // Start is called before the first frame update
    void Start()
    {
        scoreUI = GameObject.Find("ScoreUI").GetComponent<TextMesh>();
        levelUI = GameObject.Find("LevelUI").GetComponent<TextMesh>();
        //Sound = SoundEffect.GetComponent<SoundEffects>();
        heartBeat = GetComponent<AudioSource>();
        curLevel = 0.0f;
        waitBewteenLevel = waitTimeBewteenLevel;
        setNextLevel();
    }

    private void HandPulse(float duratiton, float frequencey, float amplitute)
    {
        hapticAction.Execute(0, duratiton, frequencey, amplitute, SteamVR_Input_Sources.RightHand);
        hapticAction.Execute(0, duratiton, frequencey, amplitute, SteamVR_Input_Sources.LeftHand);
    }

    void setNextLevel()
    {
        score = 0;
        health = 3;
        curLevel += 1.0f;
        fruitPossibility = 1.0f / (0.33f * curLevel + 0.67f);
        waitTime = 5f / (curLevel + 2.0f) + 1.5f;
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

            if (curLevel == 5.0f)
            {
                SceneManager.LoadScene("WinScene");
            }

        setNextLevel();
    }

    private float waitTimeBewteenLevel = 6.0f;
    private float waitBewteenLevel;
    private bool levelWaiting = false;

    private Vector3 lastFruitSpwan = new Vector3(7.7f, -6.97f, -2.77f);
    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log(Input.mousePosition);
            score += 10;
        }

        if (score >= goals[(int)curLevel - 1])
        {
            levelWaiting = true;
        }

        if (levelWaiting)
        {
            waitBewteenLevel -= Time.deltaTime;

            if(waitBewteenLevel < 0.8f * waitTimeBewteenLevel)
            {
                heartBeat.Play(0);
                HandPulse(1.5f, 1f, 1f);
            }

            if (waitBewteenLevel < 0f)
            {
                updateLevel();
                waitBewteenLevel = waitTimeBewteenLevel;
                levelWaiting = false;
            }
        }
        

        curWaitT -= Time.deltaTime;
        if (curWaitT < 0 && !levelWaiting)
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
            shootfruits shootFruit = go.GetComponent<shootfruits>();
            
            Rigidbody tmp = go.GetComponent<Rigidbody>();
            
            if (waitTime < 3.5f && Vector3.Distance(lastFruitSpwan, spawnCenter) > 5f )
            {
                shootFruit.canPlay = true;
            }
            go.transform.position = spawnCenter;
            go.transform.rotation = Quaternion.LookRotation(player.transform.position - go.transform.position);
            curWaitT = waitTime;
            lastFruitSpwan = spawnCenter;
        }
        scoreUI.text = "Score : " + score.ToString();
        if (health < 0)
        {
            SceneManager.LoadScene("LoseScene");
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
