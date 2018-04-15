using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipStorage : MonoBehaviour {

	public static ClipStorage Instance {set; get;}
    public AudioSource audioSource;
    public AudioClip[] hijaiyahClip;
    public AudioClip menuIntro;

    void Awake()
	{
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			Destroy(gameObject);
		}
	}

    public void PlaySoundHijaiyah(int ind)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = hijaiyahClip[ind];
            audioSource.Play();
        }
    }

    public void PlaySoundIntro(AudioClip clip)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }

    }

}
