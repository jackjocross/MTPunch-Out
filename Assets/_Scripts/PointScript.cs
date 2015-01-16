using UnityEngine;
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
		pointsTens.GetComponent<SpriteRenderer>().sprite = digits[tempScore % 10].GetComponent<SpriteRenderer>().sprite;
		tempScore = tempScore / 10;
		pointsHund.GetComponent<SpriteRenderer>().sprite = digits[tempScore % 10].GetComponent<SpriteRenderer>().sprite;
		tempScore = tempScore / 10;
		pointsThou.GetComponent<SpriteRenderer>().sprite = digits[tempScore % 10].GetComponent<SpriteRenderer>().sprite;
		tempScore = tempScore / 10;
		pointsTThou.GetComponent<SpriteRenderer>().sprite = digits[tempScore % 10].GetComponent<SpriteRenderer>().sprite;
		tempScore = tempScore / 10;
		pointsHThou.GetComponent<SpriteRenderer>().sprite = digits[tempScore % 10].GetComponent<SpriteRenderer>().sprite;
	}

	void resetScore() {
		curScore = 0;
	}

	void FixedUpdate() {
		addPoints (1);
	}
}
