using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClockScript : MonoBehaviour {

	public GameObject[] digits;
	public GameObject minutesObj;
	public GameObject TSecObj;
	public GameObject OSecObj;

	private int updateTime = 0;
	
	void FixedUpdate() {
		updateTime++;
		// If a full second has passed
		if (updateTime % 17 == 0) {
			int seconds = updateTime / 17;
			int minutes = seconds / 60;
			seconds = seconds - (minutes * 60);
			int ones = seconds % 10;
			int tens = seconds / 10;

			minutesObj.GetComponent<SpriteRenderer>().sprite = digits[minutes].GetComponent<SpriteRenderer>().sprite;
			TSecObj.GetComponent<SpriteRenderer>().sprite = digits[tens].GetComponent<SpriteRenderer>().sprite;
			OSecObj.GetComponent<SpriteRenderer>().sprite = digits[ones].GetComponent<SpriteRenderer>().sprite;
		}
	}
}
