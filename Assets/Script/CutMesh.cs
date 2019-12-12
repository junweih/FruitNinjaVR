﻿using UnityEngine;
using System.Collections;
using Valve.VR;

[RequireComponent (typeof(Rigidbody))]
public class CutMesh : MonoBehaviour {

	public Material capMaterial;
    private GlobalLogic global;
    public SteamVR_Action_Vibration hapticAction;
    SteamVR_Behaviour_Pose trackedObj;
    private bool hapticStart = false;
    public AudioClip[] swordWipes;
    private AudioSource swordWipe;

    void Start () {
 
        GameObject tmp = GameObject.Find("Global");
        if (tmp) {
            global = tmp.GetComponent<GlobalLogic>();
        }

        trackedObj = GetComponent<SteamVR_Behaviour_Pose>();
        //trackedobject = this.getcomponent<steamvr_trackedobject>();
        //print((int)(trackedobject.index));
        swordWipe = GetComponent<AudioSource>();
    }

    private void RightHandPulse(float duratiton, float frequencey, float amplitute)
    {
        hapticAction.Execute(0, duratiton, frequencey, amplitute, SteamVR_Input_Sources.RightHand);
    }

    private float timer = 0f;
    private float wipeTimer = 0f;
    private bool wiping = false;
    private void Update()
    {
        if ((trackedObj.GetVelocity()).magnitude >= 1f && !wiping)
        {
            wiping = true;
            int index = Random.Range(0, swordWipes.Length - 1);
            
            
            if(index!=0)
            {
                Debug.Log(index);
                swordWipe.clip = swordWipes[index];
                swordWipe.Play(0);
            }
            
        }

        if(wiping)
        {
            wipeTimer += Time.deltaTime;
            if (wipeTimer > 1f)
            {
                wipeTimer = 0f;
                wiping = false;
            }
        }
        if (hapticStart)
        {
            RightHandPulse(1.5f, 1f, 1f);
            timer += Time.deltaTime;
            if (timer > 0.75f)
            {
                timer = 0f;
                hapticStart = false;
            }
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject victim = collision.collider.gameObject;

        if(trackedObj)
        {
            if((trackedObj.GetVelocity()).magnitude < 1f)
            {
                return;
            }
        }

        if (victim.CompareTag("bomb"))
        {
            hapticStart = true;
            BombScript bs = victim.GetComponent<BombScript>();
            if (bs.died)
                return;
            
            global.health--;
            bs.Die(collision.contacts[0].point);
            Destroy(victim);
            return;
        }

        if (trackedObj)
        {
            if ((trackedObj.GetVelocity()).magnitude < 0.8f)
            {
                return;
            }
        }

        if (!(victim.CompareTag("fruit") || victim.CompareTag("fruitPieces")))
        {
            return;
        }
        
        if (victim.CompareTag("fruit"))
        {

            for(int i = 0; i < 100; i++)
            {
                RightHandPulse(1.5f, 1f, 1f);
            }
            

            victim.GetComponent<cutSound>().playClip();
            DestroyFruit df = victim.GetComponent<DestroyFruit>();
            if (!df.died)
            {
                df.Die();
                if (global)
                {
                    global.score += 10;
                }
            }
        }

        GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);

        if (!pieces[0].GetComponent<Rigidbody>())
        {
            pieces[0].AddComponent<Rigidbody>();
            MeshCollider tmp = pieces[0].AddComponent<MeshCollider>();
            tmp.convex = true;
        }

        if (!pieces[0].GetComponent<DestroyFruit>())
        {
            DestroyFruit df = pieces[0].AddComponent<DestroyFruit>();
            df.useGravity = true;
            df.sliced = true;
        } else
        {
            DestroyFruit df = pieces[0].GetComponent<DestroyFruit>();
            df.sliced = true;
        }

        if (!pieces[1].GetComponent<Rigidbody>())
        {
            pieces[1].AddComponent<Rigidbody>();
            MeshCollider tmp = pieces[1].AddComponent<MeshCollider>();
            tmp.convex = true;
        }

        if (!pieces[1].GetComponent<DestroyFruit>())
        {
            DestroyFruit df = pieces[1].AddComponent<DestroyFruit>();
            df.useGravity = true;
            df.sliced = true;
        } else
        {
            DestroyFruit df = pieces[1].GetComponent<DestroyFruit>();
            df.sliced = true;
        }

        pieces[0].tag = "fruitPieces";
        pieces[1].tag = "fruitPieces";
    
    }
}
