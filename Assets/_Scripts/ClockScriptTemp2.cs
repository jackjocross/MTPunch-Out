﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClockScriptTemp2 : MonoBehaviour {

	public static ClockScriptTemp2 clock;

	public GameObject[] digits;
	public GameObject minutesObj;
	public GameObject TSecObj;
	public GameObject OSecObj;

	public AudioClip RoundEnd;
	private AudioSource source;

	private int updateTime = 0;
	private int realTime = 0;
	private bool started = false;

	void Awake(){
		clock = this;
		source=this.GetComponent<AudioSource>();
		source.panLevel=0;
	}

	void FixedUpdate() {
		if (started) timerUpdate ();
	}
	
	public void startTimer() {
		started = true;
	}

	void timerUpdate() {
		updateTime++;
		// If a full second has passed
		if (updateTime % 17 == 0) {
			realTime++;
			// if three minutes have passed
			if (realTime >= 180) {
				//Freeze Scene and play round end bell
				Time.timeScale=0;
				source.PlayOneShot(RoundEnd,1f);
				StartCoroutine(EndOfRound());
			}		
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

	IEnumerator EndOfRound(){
		print("BEFORE");
		yield return new WaitForSeconds(2f);
		print ("After end of round");
		if (MatchController.Match.roundNum == 1) {
			Application.LoadLevel("_Scene_Round_Two_Original");
		}
		else if (MatchController.Match.roundNum == 2) {
			Application.LoadLevel("_Scene_Round_Three_Original");
		}
		MatchController.Match.roundNum++;
	}
}
