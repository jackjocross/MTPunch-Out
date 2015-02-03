using UnityEngine;
using System.Collections;

public class RoundText : MonoBehaviour {

	public GameObject roundOne, roundTwo, roundThree;

	// Use this for initialization
	void Start () {
		if (SaveScene.roundNum == 1) {
			roundOne.SetActive(true);
		}
		else if (SaveScene.roundNum == 2) {
			roundTwo.SetActive(true);
		}
		else {
			roundThree.SetActive(true);
		} 
	}
}
