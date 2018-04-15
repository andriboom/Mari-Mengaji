using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{

    public GameObject trailPrefab;
    public BoxCollider2D[] outsideArea;
    public ObjectTulis activeObject;
    private GameObject thisTrail;
    private Vector3 startPos;
    private Vector3 startPosColl;
    private Plane objPlane;
    private List<GameObject> activeTrails = new List<GameObject>();
    private List<GameObject> goColliders = new List<GameObject> ();
    private GameObject go;

    // Use this for initialization
    void Start()
    {
        objPlane = new Plane(Camera.main.transform.forward * -1, this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (ObjectTulis.isActiveWriting)
        {
            if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
            {
                Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);

#if UNITY_EDITOR
                mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
#else
                mRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
#endif
                
                float rayDistance;

                if (objPlane.Raycast(mRay, out rayDistance))
                {
                    startPos = mRay.GetPoint(rayDistance);
                    startPosColl = mRay.GetPoint(rayDistance);
                }

                thisTrail = (GameObject)Instantiate(trailPrefab, startPos, Quaternion.identity, this.transform);

                go = Instantiate(new GameObject("Collision"), startPosColl, Quaternion.identity, this.transform);
                BoxCollider2D coll = go.AddComponent<BoxCollider2D>();
                Rigidbody2D rgbd = go.AddComponent<Rigidbody2D>();
                rgbd.gravityScale = 0;
                coll.size = new Vector2(0.5f, 0.5f);
                coll.isTrigger = true;
                go.tag = "Trail";

            }
            else if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0)))
            {

                if (thisTrail != null)
                {
                    Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
#if UNITY_EDITOR
                    mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
#else
                    mRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
#endif
                    float rayDistance;

                    if (objPlane.Raycast(mRay, out rayDistance))
                    {
                        thisTrail.transform.position = mRay.GetPoint(rayDistance);
                    }

                    if (Vector3.Distance(thisTrail.transform.position, startPosColl) > 0.5)
                    {
                        go.transform.position = thisTrail.transform.position;
                        startPosColl = thisTrail.transform.position;
                    }

                    if (go.GetComponent<BoxCollider2D>().IsTouching(outsideArea[0]) || go.GetComponent<BoxCollider2D>().IsTouching(outsideArea[1]))
                    {
                        Destroy(thisTrail);
                        Destroy(go);
                    }
                }

            }
            else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
            {

                if (thisTrail != null)
                {
                    if (Vector3.Distance(thisTrail.transform.position, startPos) < 0.1)
                    {
                        Destroy(thisTrail);
                        Destroy(go);
                    }

                }

                activeTrails.Add(thisTrail);
                goColliders.Add(go);

                if (activeObject.IsAllColliderTriggered())
                {
                    if (activeTrails.Count > 0)
                    {
                        for (int i = 0; i < activeTrails.Count; i++)
                        {
                            Destroy(activeTrails[i]);
                            Destroy(goColliders[i]);
                        }
                        
                        goColliders.Clear();
                        activeTrails.Clear();
                    }

                    StartCoroutine(activeObject.SuccessWriting());
                    print("WIN");
                }

            }
        } 
    }

}
