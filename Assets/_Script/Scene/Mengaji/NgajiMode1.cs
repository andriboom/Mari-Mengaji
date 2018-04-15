using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NgajiMode1 : MonoBehaviour {

	public GameObject[] hijaiyahs;
	public Transform parentHijaiyah;
	public Text latinContainer;
	public AudioSource audioContainer;
	[HideInInspector]
	public GameObject activeHijaiyah;
	[HideInInspector]
	public int indexH;
	private List<GameObject> AllHijaiyah = new List<GameObject>();

	public void Init() {
		indexH = 0;
		SpawnHijaiyah (indexH);
		for (int i = 0; i < hijaiyahs.Length; i++)
		{
			AllHijaiyah.Add(hijaiyahs[i]);
		}
	}

	public void SpawnHijaiyah(int i) {
		RemoveHijaiyah();
		GameObject go = Instantiate(hijaiyahs[i], parentHijaiyah);
		Hijaiyah h = go.GetComponent<Hijaiyah> ();
		go.GetComponent<Button>().onClick.AddListener(h.TriggerAction1);
		h.hijaiyah = go.name;
		h.hijaiyah = h.hijaiyah.Replace("(Clone)", string.Empty);
		latinContainer.text = h.hijaiyah;
		activeHijaiyah = go;
	}

	private void RemoveHijaiyah() {
		if (parentHijaiyah.childCount > 0) {
			RectTransform[] childs = parentHijaiyah.GetComponentsInChildren<RectTransform>();
			foreach (Transform tr in childs)
			{
				if (tr.name != parentHijaiyah.name)
					Destroy(tr.gameObject);
			}
			activeHijaiyah = null;
		}
	}

	public void Next() {
		indexH += 1;
		MengajiController.Instance.auto = false;
		if (indexH >= hijaiyahs.Length)
			indexH = 0;
		
		SpawnHijaiyah(indexH);
		activeHijaiyah.GetComponent<Hijaiyah>().TriggerAction1();
	}

	public void Prev() {
		indexH -= 1;
		MengajiController.Instance.auto = false;
		if (indexH < 0)
			indexH = hijaiyahs.Length - 1;
		SpawnHijaiyah(indexH);
		activeHijaiyah.GetComponent<Hijaiyah>().TriggerAction1();		
	}

}
