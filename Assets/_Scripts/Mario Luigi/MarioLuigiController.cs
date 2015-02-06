using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MarioLuigiController : MonoBehaviour {

	public static MarioLuigiController MarioLuigiC;

	public static Image marioHealthBar;
	public static Image luigiHealthBar;

	public static float marioHealth;
	public static float luigiHealth;
	
	void Awake() {
		MarioLuigiC = this;
	}

	void Start() {
		marioHealth = SaveScene.marioHealth;
		luigiHealth = SaveScene.luigiHealth;
		
		marioHealthBar = GameObject.Find ("Mario Health").GetComponent<Image>();
		marioHealthBar.fillAmount = marioHealth * 0.03125f;
		luigiHealthBar = GameObject.Find ("Luigi Health").GetComponent<Image>();
		luigiHealthBar.fillAmount = luigiHealth * 0.03125f;

		if (luigiHealth <= 0) {
			MarioLuigiAnimator.marioLuigiA.luigi.SetTrigger("Start Down");
			MarioLuigiAnimator.marioLuigiA.mario.SetTrigger("Intro To Solo");
		}
	}

	public void luigiHit(bool bodyHit) {
		PointScript.PointController.addPoints(10);
		luigiHealth -= 1;
		luigiHealthBar.fillAmount -= 0.03215f;

		if (luigiHealth == 0) {
			PointScript.PointController.addPoints(1010);
			MarioLuigiAnimator.marioLuigiA.luigi.SetTrigger("Fall Down");
			MarioLuigiAnimator.marioLuigiA.mario.SetTrigger("Luigi Dead");
		}
		else if (bodyHit) {
			MarioLuigiAnimator.marioLuigiA.luigi.SetTrigger("Punched");
		}
		else {
			MarioLuigiAnimator.marioLuigiA.luigi.SetTrigger("Head Punched");
		}
	}

	public void marioHit(bool bodyHit) {
		PointScript.PointController.addPoints(10);
		marioHealth -= 1;
		marioHealthBar.fillAmount -= 0.03215f;
		
		if (marioHealth == 0) {
			PointScript.PointController.addPoints(1010);
			MarioLuigiAnimator.marioLuigiA.mario.SetTrigger("Knockdown");
		}
		else if (bodyHit) {
			MarioLuigiAnimator.marioLuigiA.mario.SetTrigger("Hit");
		}
		else {
			MarioLuigiAnimator.marioLuigiA.mario.SetTrigger("Head Hit");
		}
	}

	public void randomPunch() {
		int randNum = Random.Range (0, 2);
		print (randNum);
		if (randNum == 0) {
			MarioLuigiAnimator.marioLuigiA.marioPunch();
		}
		else {
			MarioLuigiAnimator.marioLuigiA.luigiPunch();
		}
	}
}
