using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene1 : MonoBehaviour
{
    // Start is called before the first frame update
    public float t;
    public bool enabled;
    void Start()
    {
        t = 1f;
        enabled = false;
        Rigidbody tmp = GetComponent<Rigidbody>();
        tmp.angularVelocity = new Vector3(0f, 1.5f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(enabled)
        {
            t -= Time.deltaTime;
            if(t < 0)
            {
                SceneManager.LoadScene("GamePlayScene");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        enabled = true;
    }
}
