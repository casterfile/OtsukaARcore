﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrowBall : MonoBehaviour {

	private float power = 300.0f;

	private Vector3 startPos;
	bool isSetlocation = false;
	void Update(){
		if (isSetlocation == false) {
			transform.position = Vector3.zero;
			transform.localPosition = new Vector3 (0.0f, 1.0f, -7.38f);//Vector3.zero;
			GetComponent<Rigidbody> ().velocity = Vector3.zero;
		}
	}

	void OnMouseDown() {
		startPos = Input.mousePosition;
		startPos.z = transform.position.z - Camera.main.transform.position.z;
		startPos = Camera.main.ScreenToWorldPoint(startPos);
	}

	void OnMouseUp() {
		var endPos = Input.mousePosition;
		endPos.z = transform.position.z - Camera.main.transform.position.z;
		endPos = Camera.main.ScreenToWorldPoint(endPos);

		var force = endPos - startPos;
		force.z = force.magnitude;
		force.Normalize();

		GetComponent<Rigidbody>().AddForce(force * power);
		ReturnBall();
	}

	IEnumerator ReturnBall() {
		yield return new WaitForSeconds(4.0f);

	}
}
