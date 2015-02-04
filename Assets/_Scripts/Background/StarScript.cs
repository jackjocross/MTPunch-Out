﻿using UnityEngine;
using System.Collections;

public class StarScript : MonoBehaviour {

	public GameObject[] digits;
	public GameObject starCount;

	public static StarScript StarAccess;

	public int numStars;
	
	void Awake() {
		StarAccess = this;
	}

	void Start() {
		numStars = SaveScene.stars;
	}

	public void addStar() {
		if (numStars == 3) {
			return;
		}
		++numStars;
		starCount.GetComponent<SpriteRenderer>().sprite = digits[numStars].GetComponent<SpriteRenderer>().sprite;
	}

	public void removeStar(int star) {
		if (numStars > 0) {
			numStars -= star;
		}
		starCount.GetComponent<SpriteRenderer>().sprite = digits[numStars].GetComponent<SpriteRenderer>().sprite;
	}

	public int getNumStars() {
		return numStars;
	}
}
