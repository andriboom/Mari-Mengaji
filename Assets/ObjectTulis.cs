using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectTulis : MonoBehaviour {

    public static ObjectTulis Instance;
    public Sprite transparentImage;
    public Sprite fillImage;
    public static bool isActiveWriting = true;

    private LineDrawer lineDrawer;
    private GameTulisController controller;
    private ColliderTulis[] colls;
    private int totalCollider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
        } else
        {
            Debug.LogWarning("Instance exist");
        }
        controller = FindObjectOfType<GameTulisController>();
        lineDrawer = FindObjectOfType<LineDrawer>();
    }

    private void Start()
    {
        isActiveWriting = true;
        lineDrawer.activeObject = this;
        colls = GetComponentsInChildren<ColliderTulis>();
        GetComponent<Image>().sprite = transparentImage;
        totalCollider = colls.Length;
    }
    
    public bool IsAllColliderTriggered() {
        foreach (ColliderTulis clst in colls) {
            if (clst.isTriggered == false)
            {
                return false;
            }
        }
        return true;
    }
    
    public IEnumerator SuccessWriting() {
        print("Nice");
        GetComponent<Image>().sprite = fillImage;
        isActiveWriting = false;
        yield return new WaitForSeconds(1.8f);
        controller.Next();
    }
}
