using UnityEngine;
using System.Collections;

public class StarScript : MonoBehaviour {

	public GameObject[] digits;

	public GameObject starCount;

	public int numStars = 0;

	void addStar() {
		++numStars;
		starCount.GetComponent<SpriteRenderer>().sprite = digits[numStars].GetComponent<SpriteRenderer>().sprite;
	}

	void removeStar() {
		--numStars;
		starCount.GetComponent<SpriteRenderer>().sprite = digits[numStars].GetComponent<SpriteRenderer>().sprite;
	}
}
