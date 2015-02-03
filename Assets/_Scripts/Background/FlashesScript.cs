using UnityEngine;
using System.Collections;

public class FlashesScript : MonoBehaviour {

	public GameObject[] largeFlashes;
	public GameObject[] smallFlashes;

	private bool isFlashing = false;

	// Use this for initialization
	void Start () {
		flashAll ();
	}

	void flashAll() {
		isFlashing = true;
	}

	void stopFlashing() {
		isFlashing = false;
	}

	void FixedUpdate() {
		int willFlash = Random.Range (0, 9);
		if (willFlash == 0) {
			int randomNumber = Random.Range (0, 4);
			LFlashScript cur = (LFlashScript)largeFlashes [randomNumber].GetComponent (typeof(LFlashScript));
			StartCoroutine (cur.flash ());
		} else if (willFlash == 1) {
			int randomNumber = Random.Range (0, 4);
			SFlashScript cur = (SFlashScript)smallFlashes [randomNumber].GetComponent (typeof(SFlashScript));
			StartCoroutine (cur.flash ());
		}

	}

}
