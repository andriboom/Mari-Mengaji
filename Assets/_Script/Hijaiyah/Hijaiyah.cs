using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hijaiyah : MonoBehaviour {

	public string hijaiyah;
	public int ind;
	public AudioClip clip;
	private Button button;
	

	private void Start()
	{
	}

	public void TriggerAction1() {
		if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Normal")) {
			GetComponent<Animator>().SetTrigger("Action 1");
            ClipStorage.Instance.PlaySoundHijaiyah(ind);
			//MengajiController.Instance.audioContainer.clip = clip;
			//MengajiController.Instance.PlayHijaiyahSound();
		}
	}

}
