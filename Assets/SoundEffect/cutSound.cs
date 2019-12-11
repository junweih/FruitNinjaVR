using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class cutSound : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip cutClip;
    private AudioSource spwanAudio;
    public void playClip()
    {
        spwanAudio.Pause();
        AudioSource.PlayClipAtPoint(cutClip, GetComponent<Transform>().position);
    }

    public void playSpwan()
    {
        spwanAudio.Play(0);
    }
    void Start()
    {
        spwanAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
