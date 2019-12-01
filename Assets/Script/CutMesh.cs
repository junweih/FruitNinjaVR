using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class CutMesh : MonoBehaviour {

	public Material capMaterial;
    private GlobalLogic global;
	void Start () {
        global = (GameObject.Find("Global")).GetComponent<GlobalLogic>();
	}

    void OnCollisionEnter(Collision collision)
    {
        GameObject victim = collision.collider.gameObject;
        victim.GetComponent<cutSound>().playClip();
        if (victim.CompareTag("fruit"))
        {
            global.score += 10;
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
}
