using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClockScript : MonoBehaviour {

	public static ClockScript clock;
	
	public GameObject[] digits;
	public GameObject minutesObj;
	public GameObject TSecObj;
	public GameObject OSecObj;

	public AudioClip RoundEnd;
	private AudioSource source;

	public static bool pauseTime;
	private int updateTime = 0;
	private int realTime = 0;
	private bool gameEnded = false;
	
	void Awake(){
		pauseTime = true;
		clock = this;
		source=this.GetComponent<AudioSource>();
		source.panLevel=0;
	}

	void FixedUpdate() {
		timerUpdate ();
	}

	void timerUpdate() {
		if (pauseTime) {
			return;
		}
	
		updateTime++;
		// If a full second has passed
		if (updateTime % 17 == 0) {
			realTime++;

			// if three minutes have passed
			if ((realTime >= 181) && !gameEnded) {
				gameEnded = true;
				freezeFighters();
				MatchController.Match.stopMusic();
				Invoke("nextRound", 2f);
				endBell();
				Invoke ("endBell", RoundEnd.length - .4f);
				Invoke ("endBell", 2 * RoundEnd.length - .8f);
			}	

			if (!gameEnded) {
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

	public void freezeFighters() {
		LittleMacAnimator.LittleMacA.animator.enabled = false;
		MarioAnimator.MarioA.animator.enabled = false;

		if (LittleMacController.vonKaiserPlaying) {
			VonKaiserAnimator.VonKaiserA.animator.enabled = false;
		}
		else {
			MarioLuigiAnimator.marioLuigiA.mario.enabled = false;
			MarioLuigiAnimator.marioLuigiA.luigi.enabled = false;
		}
	}

	public void nextRound() {
		SaveScene.littleMacHealth = LittleMacController.LittleMac.health;
		SaveScene.hearts = LifeScript.LifeController.numLives;
		SaveScene.knockdowns = LittleMacController.LittleMac.knockdowns;
		
		if (MatchController.Match.roundNum == 1) {
			SaveScene.roundNum = 2;
			
			if (Application.loadedLevelName.Equals("_Scene_Mario_Luigi")) {
				// save all the data to be loaded in the next round
				SaveScene.marioHealth = MarioLuigiController.marioHealth;
				SaveScene.luigiHealth = MarioLuigiController.luigiHealth;
				Application.LoadLevel("_Scene_Round_Two_ML");
			}
			else {
				SaveScene.vonKaiserHealth = VonKaiserController.health;
				Application.LoadLevel("_Scene_Round_Two_Original");
			}
		}
		else if (MatchController.Match.roundNum == 2) {
			SaveScene.roundNum = 3;
			
			if (Application.loadedLevelName.Equals("_Scene_Mario_Luigi")) {
				// save all the data to be loaded in the next round
				SaveScene.marioHealth = MarioLuigiController.marioHealth;
				SaveScene.luigiHealth = MarioLuigiController.luigiHealth;
				Application.LoadLevel("_Scene_Round_Three_ML");
			}
			else {
				SaveScene.vonKaiserHealth = VonKaiserController.health;
				Application.LoadLevel("_Scene_Round_Three_Original");
			}
		}
	}

	public void endBell() {
		source.PlayOneShot (RoundEnd, 1f);
	}

}
