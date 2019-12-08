﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class CutMesh : MonoBehaviour {

	public Material capMaterial;
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
            global.health--;
            BombScript bs = victim.GetComponent<BombScript>();
            bs.playExplosionsound();
            Destroy(victim);
            return;
        }

        if (!(victim.CompareTag("fruit") || victim.CompareTag("fruitPieces")))
        {
            return;
        }
        
        if (victim.CompareTag("fruit"))
        {
            victim.GetComponent<cutSound>().playClip();
            DestroyFruit df = victim.GetComponent<DestroyFruit>();
            if (!df.died)
            {
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
