using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TebakGambarController : MonoBehaviour
{
    public static TebakGambarController Instance;

    public GameObject prefabTebakGambar;
    public GameObject resultPerRound;
    public GameObject finalResult;
    public Sprite[] spriteHijaiyah;
    public Vector2[] gridLevel;
    public Vector2[] settingSizeGrid;
    public GridLayoutGroup tileGridPanel;
    public List<ObjectTebak> flipped = new List<ObjectTebak>();
    private int indexQuestion = 0;
    private int correctlyAnswered;
    private int[] hijaiyahChoosen;
    private bool isChecking = false;
    private GameObject[] spawnedObjectsTebak;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitNewQuestion();
    }

    private void Update()
    {
        if (flipped.Count == 2 && !isChecking) {
            StartCoroutine(CheckTheAnswer());
        }
    }


    public void NewQuestion(int totalHijaiyah)
    {
        RemoveQuestion();
        correctlyAnswered = 0;
        tileGridPanel.cellSize = new Vector2(settingSizeGrid[indexQuestion].x, settingSizeGrid[indexQuestion].y);

        GameObject[] temp = new GameObject[totalHijaiyah * 2];
        spawnedObjectsTebak = new GameObject[totalHijaiyah * 2];
        int n = 0;
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < totalHijaiyah; j++)
            {
                GameObject go = Instantiate(prefabTebakGambar, tileGridPanel.transform) as GameObject;

                ObjectTebak ot = go.GetComponent<ObjectTebak>();
                ot.hijaiyahContainer.sprite = spriteHijaiyah[hijaiyahChoosen[j]];

                temp[n] = go;
                temp[n].SetActive(false);
                n++;
            }
        }

        spawnedObjectsTebak = temp;
        spawnedObjectsTebak.Shuffle();

        for (int i = 0; i < spawnedObjectsTebak.Length; i++)
        {
            GameObject gg = Instantiate(spawnedObjectsTebak[i], tileGridPanel.transform);
            gg.SetActive(true);
            gg.name = gg.name.Replace("(Clone)", string.Empty);
            spawnedObjectsTebak[i] = gg;
            StartCoroutine(ShowTheImageFirst(gg));
        }

        //foreach (GameObject go in temp)
        //{
        //    Destroy(go);
        //}
        
    }

    public void InitNewQuestion()
    {
        GetRandomHijaiyah((int)gridLevel[indexQuestion].x, (int)gridLevel[indexQuestion].y);
        NewQuestion(hijaiyahChoosen.Length);
    }


    private IEnumerator CheckTheAnswer() {

        isChecking = true;

        if (flipped[0].hijaiyahContainer.sprite == flipped[1].hijaiyahContainer.sprite)
        {
            correctlyAnswered++;
            yield return new WaitForSeconds(1f);
            flipped.Clear();
            if (correctlyAnswered == hijaiyahChoosen.Length)
            {
                if (indexQuestion < 4)
                {
                    print("Correct");
                    StartCoroutine(ResultCorrect());
                }
                else
                {
                    finalResult.SetActive(true);
                    PasangResult result = finalResult.GetComponentInChildren<PasangResult>();
                    StartCoroutine(result.CoFinalResult(10));
                    print("GameOver");
                }
            }
        }
        else
        {
            flipped[0].CloseCard();
            flipped[1].CloseCard();
            yield return new WaitForSeconds(1.5f);
            print("False");
        }

        isChecking = false;

    }

    private IEnumerator ResultCorrect()
    {
        resultPerRound.SetActive(true);
        GameObject correct = resultPerRound.transform.Find("LuarBiasa").gameObject;
        correct.SetActive(true);
        correct.transform.localScale = Vector2.zero;
        LeanTween.scale(correct, Vector2.one, 1f);
        yield return new WaitForSeconds(2f);
        LeanTween.scale(correct, Vector2.zero, 1f);
        yield return new WaitForSeconds(1f);
        correct.SetActive(false);
        resultPerRound.SetActive(false);
        indexQuestion++;
        InitNewQuestion();
    }

    private IEnumerator ShowTheImageFirst(GameObject obj)
    {
        ObjectTebak ot = obj.GetComponent<ObjectTebak>();
        ot.frontCard.SetActive(true);
        ot.GetComponent<Button>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        LeanTween.rotateY(obj, 90f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        ot.frontCard.SetActive(false);
        LeanTween.rotateY(obj, 0f, 0.5f);
        ot.GetComponent<Button>().enabled = true;
    }


    private void SpawnObjectTebak(GameObject obj)
    {
        GameObject go = Instantiate(obj, tileGridPanel.transform);
        ObjectTebak ot = go.GetComponent<ObjectTebak>();
        //ot.hijaiyahContainer.sprite = spriteHijaiyah[hjyh];
        //spawnedObjectsTebak.Add(go);
    }

    private void GetRandomHijaiyah(int x, int y)
    {
        tileGridPanel.constraintCount = x;

        //Cari Hijaiyah yang akan d.tebak
        int totalGrid = x * y;
        hijaiyahChoosen = new int[totalGrid / 2];
        //hijaiyahChoosen[0] = Random.Range(0, spriteHijaiyah.Length);
        for (int i = 0; i < hijaiyahChoosen.Length; i++)
        {
            hijaiyahChoosen[i] = Random.Range(0, spriteHijaiyah.Length);
            for (int j = 0; j < hijaiyahChoosen.Length; j++)
            {
                if (hijaiyahChoosen[i] == hijaiyahChoosen[j])
                {
                    print("Same");
                    hijaiyahChoosen[i] = Random.Range(0, spriteHijaiyah.Length);
                }
            }
        }
    }

    private void RemoveQuestion()
    {
        if (tileGridPanel.transform.childCount > 0)
        {
            Transform[] childs = tileGridPanel.GetComponentsInChildren<Transform>();
            foreach (Transform ch in childs)
            {
                if (ch.name != tileGridPanel.gameObject.name)
                    Destroy(ch.gameObject);
            }
        }
    }

}
