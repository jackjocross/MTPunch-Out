using UnityEngine;
using System.Collections;

public class LifeScript : MonoBehaviour {

	public static LifeScript LifeController;

	public GameObject[] digits;

	public GameObject lifeOnes;
	public GameObject lifeTens;
	public static bool isMacTired;

	public int numLives;

	void Awake() {
		LifeController = this;
		isMacTired = false;
	}

	void Start() {
		numLives = SaveScene.hearts;
	}

	public void addLife(int life) {
		numLives += life;
		int tempLives = numLives;

		lifeOnes.GetComponent<SpriteRenderer>().sprite = digits[tempLives % 10].GetComponent<SpriteRenderer>().sprite;
		tempLives = tempLives / 10;

		if (tempLives == 0) {
			lifeTens.GetComponent<SpriteRenderer> ().sprite = null;
		} else {
			lifeTens.GetComponent<SpriteRenderer>().sprite = digits[tempLives % 10].GetComponent<SpriteRenderer>().sprite;
		}

	}

	public void removeLife(int life) {

		numLives -= life;
		int tempLives = numLives;

		lifeOnes.GetComponent<SpriteRenderer>().sprite = digits[tempLives % 10].GetComponent<SpriteRenderer>().sprite;
		tempLives = tempLives / 10;

		if (tempLives == 0) {
			lifeTens.GetComponent<SpriteRenderer> ().sprite = null;
		} else {
			lifeTens.GetComponent<SpriteRenderer> ().sprite = digits [tempLives % 10].GetComponent<SpriteRenderer> ().sprite;
		}

		if ((numLives == 0) && (!isMacTired)) {
			isMacTired = true;
			LittleMacAnimator.LittleMacA.animator.SetTrigger("Tired");
		}

	}
}
