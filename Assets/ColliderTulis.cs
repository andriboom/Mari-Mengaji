using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTulis : MonoBehaviour {

    public bool isTriggered = false;
    private GameObject touchedObject;

    private void Update()
    {
        if (touchedObject == null)
        {
            isTriggered = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Trail" && !isTriggered)
        {
            isTriggered = true;
            print("Trigger = " + isTriggered);
            touchedObject = collision.gameObject;
        }
    }

}
