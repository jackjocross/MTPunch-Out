using UnityEngine;
using System.Collections;

public class PointScript : MonoBehaviour {

	public static PointScript PointController;

	public GameObject[] digits;

	public GameObject pointsOnes;
	public GameObject pointsTens;
	public GameObject pointsHund;
	public GameObject pointsThou;
	public GameObject pointsTThou;
	public GameObject pointsHThou;

	private int curScore = 0;

	void Awake(){
		PointController = this;
		resetScore();
	}

	void Start(){
		addPoints(SaveScene.Points);
	}

	public void addPoints (int points) {

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

	public int getCurrentScore(){
		return curScore;
	}

	void resetScore() {
		curScore = 0;
	}

	void FixedUpdate() {
//		addPoints (1);
	}
}
