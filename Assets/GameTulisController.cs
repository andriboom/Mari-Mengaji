using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTulisController : MonoBehaviour {
    
    public GameObject[] allHijaiyah;
    public GameObject resultPerRound;
    public Transform container;
    private GameObject activeHijaiyah;
    private int indexHjyh;

    private void Awake()
    {
        //Instance = this;
    }

    private void Start()
    {
        indexHjyh = 0;
        InitNewHijaiyah(indexHjyh);
    }

    public void InitNewHijaiyah(int ind)
    {
        RemoveChildContainer();
        activeHijaiyah = Instantiate(allHijaiyah[ind], container);
    }

    public void Next()
    {
        indexHjyh++;
        if (indexHjyh >= allHijaiyah.Length)
            indexHjyh = 0;

        InitNewHijaiyah(indexHjyh);
    }

    public void Prev()
    {
        indexHjyh--;
        if (indexHjyh < 0)
            indexHjyh = allHijaiyah.Length -1;

        InitNewHijaiyah(indexHjyh);
    }

    public void ResultAnim()
    {
        GameObject img = resultPerRound.transform.Find("LuarBaisa").gameObject;
        StartCoroutine(ResultAnimation(img));
    }

    private void RemoveChildContainer ()
    {
        activeHijaiyah = null;
        if (container.childCount > 0)
        {
            Transform[] chld = container.GetComponentsInChildren<Transform>();
            foreach (Transform go in chld)
            {
                if (go.name != container.name)
                    Destroy(go.gameObject);
            }
        }
    }

    IEnumerator ResultAnimation(GameObject resImg)
    {
        resultPerRound.SetActive(true);
        resImg.SetActive(true);
        resImg.transform.localScale = Vector2.zero;
        LeanTween.scale(resImg.GetComponent<RectTransform>(), Vector2.one, 0.5f);
        yield return new WaitForSeconds(1.5f);
        LeanTween.scale(resImg.GetComponent<RectTransform>(), Vector2.zero, 0.5f);
        yield return new WaitForSeconds(0.5f);
        resImg.SetActive(false);
        resultPerRound.SetActive(false);

    }

}
