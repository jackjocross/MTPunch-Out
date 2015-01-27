using UnityEngine;
using System.Collections;

public class StarScript : MonoBehaviour {

	public GameObject[] digits;
	public GameObject starCount;

	public static StarScript StarAccess;

	public int numStars = 0;
	
	void Awake() {
		StarAccess = this;
	}

	public void addStar() {
		++numStars;
		starCount.GetComponent<SpriteRenderer>().sprite = digits[numStars].GetComponent<SpriteRenderer>().sprite;
	}

	public void removeStar() {
		--numStars;
		starCount.GetComponent<SpriteRenderer>().sprite = digits[numStars].GetComponent<SpriteRenderer>().sprite;
	}

	public int getNumStars() {
		return numStars;
	}
}
