using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour {

	private RectTransform rectTrans;

	private void Start() {
		rectTrans = GetComponent<RectTransform>();
	}

	private void Update()
	{
		rectTrans.Translate(-1f,0,0);
		if (rectTrans.localPosition.x < -1300f)
			rectTrans.localPosition = new Vector2(1300f, 0f);
	}
	
}
