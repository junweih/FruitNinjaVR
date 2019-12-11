using UnityEngine;
using System.Collections;
using Valve.VR;

[RequireComponent (typeof(Rigidbody))]
public class Defend : MonoBehaviour {
    private GlobalLogic global;
    public SteamVR_Action_Vibration hapticAction;
    private bool hapticStart = false;
    void Start () {
        GameObject tmp = GameObject.Find("Global");
        if (tmp) {
            global = tmp.GetComponent<GlobalLogic>();
        }
    }

    private void LeftHandPulse(float duratiton, float frequencey, float amplitute)
    {
        hapticAction.Execute(0, duratiton, frequencey, amplitute, SteamVR_Input_Sources.LeftHand);
    }
    private float timer = 0f;
    private void Update()
    {
        if (hapticStart)
        {
            LeftHandPulse(1.5f, 1f, 1f);
            timer += Time.deltaTime;
            if (timer > 0.50f)
            {
                timer = 0f;
                hapticStart = false;
            }
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject victim = collision.collider.gameObject;
        
        if (victim.CompareTag("bomb"))
        {
            hapticStart = true;
            victim.GetComponent<BombScript>().died = true;
            return;
        }
    }
}
