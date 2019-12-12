using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIconScipt : MonoBehaviour
{
    public int id;
    private GlobalLogic global;
    bool activated;
    // Start is called before the first frame update
    void Start()
    {
        activated = true;
        GameObject tmp = GameObject.Find("Global");
        if (tmp)
        {
            global = tmp.GetComponent<GlobalLogic>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(id > global.health && activated)
        {
            GetComponent<Renderer>().enabled = false;
            return;
        }
        if (id <= global.health && !activated)
        {
            GetComponent<Renderer>().enabled = true;
        }
    }
}
