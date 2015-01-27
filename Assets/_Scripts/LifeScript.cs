using UnityEngine;
using System.Collections;

public class LifeScript : MonoBehaviour {

	public static LifeScript LifeController;

	public GameObject[] digits;

	public GameObject lifeOnes;
	public GameObject lifeTens;

	public int numLives = 0;

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
	}
}
