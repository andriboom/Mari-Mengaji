using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasangGameController : MonoBehaviour {

	public static PasangGameController Instance;
	public GameObject resultPerRound;
	public GameObject finalResultPanel;
	public GameObject prefabParticleCorrect;
	public _ScoringGame scoring;
	public Sprite[] hijaiyahSprite;
	public Image[] positionPertanyaan;
	public Image[] positionJawaban;
	public AudioSource audioSource;
	public DraggableObject dragAnswer;
	public int alreadyAnswered = 0;
	private int[] correctAnswer = new int[3];
	private bool isRoundActive = true;
	private Vector3 originalPosition;
	private RaycastHit hit;
	
	void Awake()
	{
		Instance = this;
	}
	private void Start()
	{
		InitNewRound();
	}

	void Update()
	{	
		if (isRoundActive) {
		#if UNITY_EDITOR
			// if (Input.GetMouseButtonDown(0)) {
			// 	Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			// 	if (Physics.Raycast(ray, out hit, 1000f, LayerMask.GetMask("PasangJawaban"))) {
			// 		if (hit.transform) {
			// 			originalPosition = hit.transform.position;
			// 			print(originalPosition);
			// 		}
			// 	}
			// }
			
			// if (Input.GetMouseButton(0)) {
			// 	hit.transform.position = Input.mousePosition;
			// }
		#endif
		}
	}

	public void InitNewRound() {
		isRoundActive = true;
		SpawnPertanyaan();
		SpawnJawaban();
		alreadyAnswered = 0;
	}

	public void Correct() {
		alreadyAnswered++;
		if(alreadyAnswered >= 3) {
			print ("Tambah benar point 1");
			scoring.correctScore += 1;
			StartCoroutine(ResultAnimation(resultPerRound.transform.Find("LuarBiasa").gameObject));
			if ((scoring.correctScore + scoring.falseScore) < 10)
				Invoke("InitNewRound", 1.8f);
		}
	}

	public void Wrong() {
		if (alreadyAnswered < 3) {
			print ("Tambah salah point 1");
			scoring.falseScore += 1;
			StartCoroutine(ResultAnimation(resultPerRound.transform.Find("CobaLagi").gameObject));		
		}		
	}

	IEnumerator ResultAnimation(GameObject resImg) {
		resultPerRound.SetActive(true);
		resImg.SetActive(true);
		scoring.UpdateScore();
		resImg.transform.localScale = Vector2.zero;
		LeanTween.scale(resImg.GetComponent<RectTransform>(), Vector2.one, 0.5f);
		yield return new WaitForSeconds(1.5f);
		LeanTween.scale(resImg.GetComponent<RectTransform>(), Vector2.zero, 0.5f);
		yield return new WaitForSeconds(0.5f);
		resImg.SetActive(false);
		resultPerRound.SetActive(false);

		if ((scoring.correctScore + scoring.falseScore) >= 10) {
			finalResultPanel.SetActive(true);
			PasangResult result = finalResultPanel.GetComponentInChildren<PasangResult> ();
			//result.FinalResult(scoring.correctScore);
			StartCoroutine(result.CoFinalResult(scoring.correctScore));
		}
	}
	public void SpawnPertanyaan() {
		RemovePertanyaan();
		Sprite[] pertanyaan = KocokPertanyaan();
		for (int i = 0; i < 3; i++)
		{
			//GameObject go = Instantiate(pertanyaan[i], positionPertanyaan[i]);
			positionPertanyaan[i].sprite = pertanyaan[i];
			positionPertanyaan[i].color = new Color32(225,225,225,71);
		}
	}

	private void RemovePertanyaan() {
		foreach (Image prnt in positionPertanyaan)
		{
			if (prnt.transform.childCount > 0) {
				Transform[] trns = prnt.GetComponentsInChildren<Transform>();
				foreach (Transform t in trns)
				{
					if (t.name != prnt.name)
						Destroy(t.gameObject);
				}
			}
		}
	}

	private Sprite[] KocokPertanyaan() {
		Sprite[] gos = new Sprite[3];
		
		correctAnswer[0] = Random.Range(0, hijaiyahSprite.Length);
		correctAnswer[1] = Random.Range(0, hijaiyahSprite.Length);
		correctAnswer[2] = Random.Range(0, hijaiyahSprite.Length);

		while (correctAnswer[0] == correctAnswer[1] || correctAnswer[0] == correctAnswer[2])
			correctAnswer[0] = Random.Range(0, hijaiyahSprite.Length);	
		while (correctAnswer[1] == correctAnswer[0] || correctAnswer[1] == correctAnswer[2])
			correctAnswer[1] = Random.Range(0, hijaiyahSprite.Length);
		while (correctAnswer[2] == correctAnswer[0] || correctAnswer[2] == correctAnswer[1])
			correctAnswer[2] = Random.Range(0, hijaiyahSprite.Length);
		
		gos[0] = hijaiyahSprite[correctAnswer[0]];
		gos[1] = hijaiyahSprite[correctAnswer[1]];
		gos[2] = hijaiyahSprite[correctAnswer[2]];
		
		return gos;
	}
	
	public void SpawnJawaban() {
		RemoveJawaban();
		Sprite[] jawaban = KocokJawaban();
		// bool a = true;
		int i = 0;
		while (true)
		{
			int r = Random.Range(0, 5);
			Image image = positionJawaban[r];
			if (image.sprite != null)
				continue;
			
			image.sprite = jawaban[i];
			image.color = new Color32(255,255,255, 255);
			image.raycastTarget = true;
			BoxCollider2D col = image.gameObject.GetComponent<BoxCollider2D> ();
			col.isTrigger = true;
			//spriteRenderer.color = new Color(0,0,0);
			i++;
			if (i > 4)
				break;
		}
	}

	private void RemoveJawaban() {
		foreach (Image prnt in positionJawaban)
		{
			//SpriteRenderer spr = prnt.GetComponent<SpriteRenderer>();
			if (prnt.sprite != null)
				prnt.sprite = null;
		}
	}

	private Sprite[] KocokJawaban() {
		Sprite[] gos = new Sprite[5];
		int[] random = new int[5];
		for (int i = 0; i < 5; i++)
		{
			
			if (i >= 3) { 
				random[i] = Random.Range(0, hijaiyahSprite.Length);
			
				foreach (int t in random)
				{
					while (random[i] == t || random[i] == correctAnswer[0] || random[i] == correctAnswer[1] || random[i] == correctAnswer[2])
						random[i] = Random.Range(0, hijaiyahSprite.Length);
				}

				// gos[i] = prefabJawaban[random[i]];
			} else {
				random[i] = correctAnswer[i];
			}
			gos[i] = hijaiyahSprite[random[i]];
			
		}
		
		return gos;
	}

}
