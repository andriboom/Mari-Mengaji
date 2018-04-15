using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NgajiMode2 : MonoBehaviour {

	public GameObject panelShow;
	public GameObject[] hijaiyahs;
	public Transform parentShow;
	private Hijaiyah activeHijaiyah;
	private int indexHijaiyah;
	private List<Hijaiyah> AllHijaiyah = new List<Hijaiyah> ();

	public void Init() {
		indexHijaiyah = 0;
		panelShow.SetActive(false);
		foreach (GameObject hjy in hijaiyahs)
			AllHijaiyah.Add(hjy.GetComponent<Hijaiyah> ());
	}

	public void TriggerAction2(int i) {
		StartCoroutine(Showing(i));
	}

	private void Update () {
		if (MengajiController.Instance.auto) {
			#if UNITY_EDITOR
			if (Input.GetMouseButtonDown(0)) {
				if(EventSystem.current.IsPointerOverGameObject(0)) {
					MengajiController.Instance.auto = false;
				}
			}
			#elif UNITY_ANDROID
			foreach (Touch touch in Input.touches) {
				int id = touch.fingerId;
				if (EventSystem.current.IsPointerOverGameObject(id)) {
					MengajiController.Instance.auto = false;
				}
			}
			#endif
		}
	}

	IEnumerator Showing(int i) {
		panelShow.SetActive(true);
		GameObject go = Instantiate(hijaiyahs[i], parentShow);
		//go.GetComponentInChildren<Transform>().localScale = new Vector3(0.8f, 0.8f, 0.8f);
		go.GetComponent<Button>().enabled = false;
		go.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
		go.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
		go.GetComponent<RectTransform>().localPosition = Vector3.zero;	
		go.GetComponent<Hijaiyah>().TriggerAction1();
		yield return new WaitForSeconds (2f);
		Destroy(go);
		panelShow.SetActive(false);
	}

}
