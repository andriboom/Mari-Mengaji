using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MengajiController : MonoBehaviour {
	
	public static MengajiController Instance;
	public GameObject mode1Panel;
	public GameObject mode2Panel;
	public Text OnMode;
	public AudioSource audioContainer;
	public Animator animatorScene;
	public bool isMode1 = true;
	public bool auto {set; get;}

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		isMode1 = true;
		this.Init();
	}

	private void Init() {
		mode1Panel.GetComponent<NgajiMode1>().Init();
		// mode1Panel.SetActive(true);
		// mode2Panel.SetActive(false);
		auto = false;
	}

	public void SwitchMode() {
		isMode1 = !isMode1;
		auto = false;
		if (isMode1) {
			// mode1Panel.SetActive(true);
			// mode2Panel.SetActive(false);
			animatorScene.SetBool("IsMode2", false);				
			OnMode.text = "Mode 1";
			mode1Panel.GetComponent<NgajiMode1>().Init();
		} else {
			// mode1Panel.SetActive(false);
			// mode2Panel.SetActive(true);
			// animatorScene.SetTrigger("IntroMode2");	
			animatorScene.SetBool("IsMode2", true);					
			OnMode.text = "Mode 2";
			mode2Panel.GetComponent<NgajiMode2>().Init();			
		}
	}

	private void Update() {
//		if (!isMode1 && auto) {
//			#if UNITY_EDITOR
//			if (Input.GetMouseButtonDown(0)) {
//				auto = false;
//			}
//			#elif UNITY_ANDROID
//			if (Input.GetTouch(0).phase == TouchPhase.Ended) {
//				auto = false;
//			}
//			#endif
//		}
	}

	//public void PlayHijaiyahSound() {
	//	if (audioContainer.clip != null)
	//		audioContainer.Play();
	//}

	public void AutoPlay() {
		if (!auto) {
			StartCoroutine ("AutoPlaying");
		}
	}

	IEnumerator AutoPlaying () {
        
		auto = true;

		if (isMode1) {
			NgajiMode1 mode1 = mode1Panel.GetComponent<NgajiMode1>();
			mode1.Init();
			while (auto) {
				mode1.SpawnHijaiyah(mode1.indexH);
				mode1.activeHijaiyah.GetComponent<Button> ().enabled = false;
				mode1.activeHijaiyah.GetComponent<Hijaiyah> ().TriggerAction1();
				if (!auto)
					break;
				yield return new WaitForSeconds(2f);
				mode1.indexH++;
			}

			mode1.activeHijaiyah.GetComponent<Button> ().enabled = true;			
		} else {
			NgajiMode2 mode2 = mode2Panel.GetComponent<NgajiMode2>();
			mode2.Init();
			
			foreach (GameObject go in mode2.hijaiyahs) {
				go.GetComponent<Button>().enabled = false;
			}
			
			int i = 0;
			
			while (auto) {
				foreach (GameObject go in mode2.hijaiyahs) {
					go.GetComponent<Button>().enabled = false;
				}
				mode2.TriggerAction2(i);
				if (!auto)
					break;
				yield return new WaitForSeconds(2f);
				i++;
			}
			
		}
	}
}