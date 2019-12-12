using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitNJump : MonoBehaviour
{
    public float t;
    public bool enable;
    // Start is called before the first frame update
    void Start()
    {
        t = 3f;
        enable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (enable)
        {
            t -= Time.deltaTime;
            if (t < 0)
            {
                SceneManager.LoadScene("MainMenu");
                enable = false;
            }
        }
    }
}
