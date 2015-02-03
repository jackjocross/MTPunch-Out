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
	}

	public void luigiHit(bool bodyHit) {
		luigiHealth -= 1;
		luigiHealthBar.fillAmount -= 0.03215f;

		if (luigiHealth == 0) {
			MarioLuigiAnimator.marioLuigiA.luigi.SetTrigger("Fall Down");
		}
		else if (bodyHit) {
			MarioLuigiAnimator.marioLuigiA.luigi.SetTrigger("Punched");
		}
		else {
			MarioLuigiAnimator.marioLuigiA.luigi.SetTrigger("Head Punched");
		}
	}

	public void marioHit() {
		marioHealth -= 1;
		marioHealthBar.fillAmount -= 0.03215f;
		
		if (marioHealth == 0) {
			print ("mario is dead");
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
