using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;



public class SoundList : MonoBehaviour
{
    AudioSource m_AudioSource;
    public List<AudioClip> clipList = new List<AudioClip>();
    public AudioMixerGroup Output;


    public void PlayRandomSound()
    {
        if (clipList == null || clipList.Count == 0)
            return;

        int index = Random.Range(0, clipList.Count);
        

        m_AudioSource.clip = clipList[index];
        m_AudioSource.Play();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_AudioSource = gameObject.AddComponent<AudioSource>();
        m_AudioSource.outputAudioMixerGroup = Output;

        //m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
