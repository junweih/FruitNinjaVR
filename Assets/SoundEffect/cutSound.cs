using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class cutSound : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip cutClip;

    public void playClip()
    {
        AudioSource.PlayClipAtPoint(cutClip, GetComponent<Transform>().position);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
