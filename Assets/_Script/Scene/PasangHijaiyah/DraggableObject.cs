using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour
{
    private bool dragging = false;
    private Vector2 originalPosition;
    private Transform objectTODrag;
    private Image objectDragImage;
    private string DRAGGABLE_TAG = "DragUIObject";
    private string DROPPABLE_TAG = "DropUIObject";
    private List<RaycastResult> hitObjects = new List<RaycastResult> ();

    void Update() {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) {
            objectTODrag = GetDraggableObjectTransform();
            if (objectTODrag != null) {

                objectTODrag.SetAsLastSibling();
                originalPosition = objectTODrag.position;
                objectDragImage = objectTODrag.GetComponent<Image> ();
                objectDragImage.raycastTarget = false;
                PasangGameController.Instance.dragAnswer = objectTODrag.GetComponent<DraggableObject> ();

                dragging = true;
            }
            
        }

        if (dragging) {
            objectTODrag.position = Input.mousePosition;
        }

        if (Input.GetMouseButtonDown(0)) {
            dragging = false;
            if (objectTODrag != null) {
                Transform dropPlace = GetDroppableObjectTransform();
                
                if (dropPlace != null) {
                    if (dropPlace.GetComponent<Image> ().sprite == objectDragImage.sprite) {
                        PasangGameController.Instance.Correct();
                        dropPlace.GetComponent<Image> ().sprite = objectDragImage.sprite;
                        dropPlace.GetComponent<Image> ().color = Color.white;
                        dropPlace.parent.Find("Particle").GetComponent<UIParticleSystem>().Play();
                        objectDragImage.sprite = null;
                        objectDragImage.color = new Color32(225, 225, 225, 0);
                        objectTODrag.position = originalPosition;
                        print ("Correct");
                    } else if (dropPlace.GetComponent<Image> ().sprite != objectDragImage.sprite) {
                        PasangGameController.Instance.Wrong();           
                        objectTODrag.position = originalPosition;
                        objectDragImage.raycastTarget = true;
                        print ("Wrong");
                    }
                } else {
                    objectTODrag.position = originalPosition;
                    objectDragImage.raycastTarget = true;                
                    print("It's not drop place back to original position");
                }
            }
            PasangGameController.Instance.dragAnswer = null;
            objectTODrag = null;
        }
#else
        if (Input.GetTouch(0).phase == TouchPhase.Began) {
            objectTODrag = GetDraggableObjectTransform();
            if (objectTODrag != null) {

                objectTODrag.SetAsLastSibling();
                originalPosition = objectTODrag.position;
                objectDragImage = objectTODrag.GetComponent<Image> ();
                objectDragImage.raycastTarget = false;
                PasangGameController.Instance.dragAnswer = objectTODrag.GetComponent<DraggableObject> ();

                dragging = true;
            }
            
        }

        if (dragging) {
            objectTODrag.position = Input.mousePosition;
        }

        if (Input.GetTouch(0).phase == TouchPhase.Ended) {
            dragging = false;

            if (objectTODrag != null) {
                Transform dropPlace = GetDroppableObjectTransform();
                
                if (dropPlace != null) {
                    if (dropPlace.GetComponent<Image> ().sprite == objectDragImage.sprite) {
                        PasangGameController.Instance.Correct();
                        dropPlace.GetComponent<Image> ().sprite = objectDragImage.sprite;
                        dropPlace.GetComponent<Image> ().color = Color.white;
                        dropPlace.parent.Find("Particle").GetComponent<UIParticleSystem>().Play();
                        objectDragImage.sprite = null;
                        objectDragImage.color = new Color32(225, 225, 225, 0);
                        objectTODrag.position = originalPosition;
                        print ("Correct");
                    } else if (dropPlace.GetComponent<Image> ().sprite != objectDragImage.sprite) {
                        PasangGameController.Instance.Wrong();           
                        objectTODrag.position = originalPosition;
                        objectDragImage.raycastTarget = true;
                        print ("Wrong");
                    }
                } else {
                    objectTODrag.position = originalPosition;
                    objectDragImage.raycastTarget = true;                
                    print("It's not drop place back to original position");
                }
            }
            PasangGameController.Instance.dragAnswer = null;
            objectTODrag = null;
        }
#endif
    }

    private GameObject GetObjectUnderMouse() {
        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;
        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0) return null;

        return hitObjects.First().gameObject;
    }

    private Transform GetDraggableObjectTransform() {
        GameObject clickedObject = GetObjectUnderMouse();

        if (clickedObject != null && clickedObject.tag == DRAGGABLE_TAG)
            return clickedObject.transform;
        
        return null;
    }

    private Transform GetDroppableObjectTransform() {
        GameObject clickedObject = GetObjectUnderMouse();

        if (clickedObject != null && clickedObject.tag == DROPPABLE_TAG)
            return clickedObject.transform;
        
        return null;
    }

}
