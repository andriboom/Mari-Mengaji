using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasangResult : MonoBehaviour {

	public GameObject[] stars;
	private Animator animator;
	private int totalStar;

	public void FinalResult(int score) {
		foreach (GameObject go in stars)
			go.SetActive(false);
		
		totalStar = Mathf.CeilToInt(score/2);
		for (int i = 0; i < totalStar; i++)
		{
			stars[i].SetActive(true);
		}
	}
	
	public IEnumerator CoFinalResult(int score) {
		
		transform.localScale = Vector2.zero;
		foreach (GameObject go in stars)
			go.SetActive(false);
		
		LeanTween.scale(gameObject.GetComponent<RectTransform>(), Vector3.one, 1.25f);
		yield return new WaitForSeconds(1.25f);
		
		totalStar = Mathf.CeilToInt(score/2);

		for (int i = 0; i < totalStar; i++)
		{
			stars[i].SetActive(true);
			stars[i].transform.localScale = Vector2.zero;
			LeanTween.scale(stars[i].GetComponent<RectTransform>(), Vector2.one, 0.8f);
			yield return new WaitForSeconds(1f);
		}
	}

}
