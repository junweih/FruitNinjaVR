using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class Defend : MonoBehaviour {
    private GlobalLogic global;
	void Start () {
        GameObject tmp = GameObject.Find("Global");
        if (tmp) {
            global = tmp.GetComponent<GlobalLogic>();
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        GameObject victim = collision.collider.gameObject;
        
        if (victim.CompareTag("bomb"))
        {
            victim.GetComponent<BombScript>().died = true;
            return;
        }
    }
}
