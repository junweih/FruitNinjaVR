using UnityEngine;
using System.Collections;
using Valve.VR;

[RequireComponent (typeof(Rigidbody))]
public class CutMesh : MonoBehaviour {

	public Material capMaterial;
    private GlobalLogic global;
    public SteamVR_Action_Vibration hapticAction;    private SteamVR_TrackedObject TrackedObject;

    private bool hapticStart = false;


    void Start () {
 
        GameObject tmp = GameObject.Find("Global");
        if (tmp) {
            global = tmp.GetComponent<GlobalLogic>();
        }
        //trackedobject = this.getcomponent<steamvr_trackedobject>();
        //print((int)(trackedobject.index));
	}

    private void RightHandPulse(float duratiton, float frequencey, float amplitute)
    {
        hapticAction.Execute(0, duratiton, frequencey, amplitute, SteamVR_Input_Sources.RightHand);
    }

    private float timer = 0f;
    private void Update()
    {
        if (hapticStart)
        {
            RightHandPulse(1.5f, 1f, 1f);
            timer += Time.deltaTime;
            if (timer > 0.5f)
            {
                timer = 0f;
                hapticStart = false;
            }
        }

        // Check if we have reached beyond 2 seconds.
        // Subtracting two is more accurate over time than resetting to zero.

    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject victim = collision.collider.gameObject;

        if (victim.CompareTag("bomb"))
        {
            hapticStart = true;
            BombScript bs = victim.GetComponent<BombScript>();
            if (bs.died)
                return;
            
            global.health--;
            bs.Die();
            Destroy(victim);
            return;
        }

        if (!(victim.CompareTag("fruit") || victim.CompareTag("fruitPieces")))
        {
            return;
        }
        
        if (victim.CompareTag("fruit"))
        {
            for(int i = 0; i <100; i++)
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
        }

        pieces[0].tag = "fruitPieces";
        pieces[1].tag = "fruitPieces";
    
    }
}
