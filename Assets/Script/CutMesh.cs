using UnityEngine;
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

        if (victim.CompareTag("fruit"))
        {
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
            pieces[0].AddComponent<DestroyFruit>();
        }

        if (!pieces[1].GetComponent<Rigidbody>())
        {
            pieces[1].AddComponent<Rigidbody>();
            MeshCollider tmp = pieces[1].AddComponent<MeshCollider>();
            tmp.convex = true;
        }

        if (!pieces[1].GetComponent<DestroyFruit>())
        {
            pieces[1].AddComponent<DestroyFruit>();
        }

        pieces[0].tag = "fruitPieces";
        pieces[1].tag = "fruitPieces";
    
    }
}
