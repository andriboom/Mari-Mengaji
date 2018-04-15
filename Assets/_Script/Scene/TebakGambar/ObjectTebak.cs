using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectTebak : MonoBehaviour {

	public GameObject frontCard;
	public Image hijaiyahContainer;

    private bool isShow = false;

    private void Start()
    {
        frontCard.GetComponent<Image>().enabled = true;
        GetComponent<Button>().onClick.AddListener(OpenCard);
    }

    public void Flip() {
        if (!isShow) {
            frontCard.SetActive(true);
            print("Open Card");
            TebakGambarController.Instance.flipped.Add(this);
            isShow = true;
        } else
        {
            frontCard.SetActive(false);
            print("Close Card");
            isShow = false;
        }
    }

    public void OpenCard()
    {
        if (!isShow && TebakGambarController.Instance.flipped.Count < 2)
        {
            StartCoroutine(Opening());
        }
    }

    public void CloseCard() {
        if (isShow)
        {
            StartCoroutine(Closing());
        }
    }

    private IEnumerator Closing()
    {
        yield return new WaitForSeconds(1.5f);
        isShow = false;
        LeanTween.rotateY(gameObject, 90f, 0.25f);
        yield return new WaitForSeconds(0.25f);
        frontCard.SetActive(false);
        LeanTween.rotateY(gameObject, 0f, 0.25f);
        yield return new WaitForSeconds(0.25f);
        GetComponent<Button>().enabled = true;
        TebakGambarController.Instance.flipped.Clear();
        print("Card Closed");

    }

    private IEnumerator Opening()
    {
        isShow = true;
        TebakGambarController.Instance.flipped.Add(this);
        LeanTween.rotateY(gameObject, 90f, 0.25f);
        yield return new WaitForSeconds(0.25f);
        frontCard.SetActive(true);
        LeanTween.rotateY(gameObject, 0f, 0.25f);
        print("Open Card");
        GetComponent<Button>().enabled = false;
    }

}
