﻿using UnityEngine;
using System.Collections;

public class PointScript : MonoBehaviour {

	public GameObject[] digits;

	public GameObject pointsOnes;
	public GameObject pointsTens;
	public GameObject pointsHund;
	public GameObject pointsThou;
	public GameObject pointsTThou;
	public GameObject pointsHThou;

	private int curScore = 0;
	
	void addPoints (int points) {

		curScore += points;
		int tempScore = curScore;

		pointsOnes.GetComponent<SpriteRenderer>().sprite = digits[tempScore % 10].GetComponent<SpriteRenderer>().sprite;
		tempScore = tempScore / 10;
		if (tempScore == 0) {
			pointsTens.GetComponent<SpriteRenderer>().sprite = null;
		} else {
			pointsTens.GetComponent<SpriteRenderer>().sprite = digits[tempScore % 10].GetComponent<SpriteRenderer>().sprite;
		}

		tempScore = tempScore / 10;

		if (tempScore == 0) {
			pointsHund.GetComponent<SpriteRenderer>().sprite = null;
		} else {
			pointsHund.GetComponent<SpriteRenderer>().sprite = digits[tempScore % 10].GetComponent<SpriteRenderer>().sprite;
		}

		tempScore = tempScore / 10;

		if (tempScore == 0) {
			pointsThou.GetComponent<SpriteRenderer>().sprite = null;
		} else {
			pointsThou.GetComponent<SpriteRenderer>().sprite = digits[tempScore % 10].GetComponent<SpriteRenderer>().sprite;
		}

		tempScore = tempScore / 10;

		if (tempScore == 0) {
			pointsTThou.GetComponent<SpriteRenderer>().sprite = null;
		} else {
			pointsTThou.GetComponent<SpriteRenderer>().sprite = digits[tempScore % 10].GetComponent<SpriteRenderer>().sprite;
		}

		tempScore = tempScore / 10;

		if (tempScore == 0) {
			pointsHThou.GetComponent<SpriteRenderer>().sprite = null;
		} else {
			pointsHThou.GetComponent<SpriteRenderer>().sprite = digits[tempScore % 10].GetComponent<SpriteRenderer>().sprite;
		}

	}

	void resetScore() {
		curScore = 0;
	}

	void FixedUpdate() {
//		addPoints (1);
	}
}
