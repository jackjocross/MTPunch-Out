using UnityEngine;
using System.Collections;

public class MarioLuigiAnimator : MonoBehaviour {

	public static MarioLuigiAnimator marioLuigiA;
	public Animator mario;
	public Animator luigi;

	public AudioSource audio;
	public AudioClip littleMacMiss;

	private int numLuigiPunches = 0;
	private int lastAction = 0;

	public static bool fightStarted;

	private long gameSpeed = 0;

	private AnimatorStateInfo marioCurrent, luigiCurrent, littleMacCurrent;

	void Awake() {
		marioLuigiA = this;
		fightStarted = false;
		audio=this.GetComponent<AudioSource>();
		audio.panLevel=0;
	}

	void FixedUpdate() {
		if ((fightStarted == false) && (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Idle"))) {
			fightStarted = true;
		}
		else if (fightStarted == false) {
			return;
		}


		gameSpeed++;

		if ((gameSpeed % 10) != 0) {
			return;
		}
		marioCurrent = mario.GetCurrentAnimatorStateInfo (0);
		luigiCurrent = luigi.GetCurrentAnimatorStateInfo (0);
		littleMacCurrent = LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0);

		if (luigiCurrent.IsName ("Enemy Luigi Down")) {
			return;
		}

		if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Stuck")) {

			if (luigiCurrent.IsName("Enemy Luigi Punch")) {
				return;
			}
			
			if (numLuigiPunches == 5) {
				mario.SetTrigger("Release");
				LittleMacAnimator.LittleMacA.animator.SetTrigger("Released");
				numLuigiPunches = 0;
				return;
			}
			

			luigi.SetTrigger("Punch");
			numLuigiPunches++;
		}


		if ((littleMacCurrent.IsName ("Little Mac Idle") || littleMacCurrent.IsName("Little Mac Idle Tired")) && (lastAction == 0)) {
			if (marioCurrent.IsName("Enemy Mario Cling") || marioCurrent.IsName("Enemy Mario Cling Hold") || marioCurrent.IsName("Enemy Mario Cling Miss") || marioCurrent.IsName("Enemy Mario Cling Twitch")) {
				return;
			}
			lastAction++;
			mario.SetTrigger("Cling");

		}
		else if ((littleMacCurrent.IsName ("Little Mac Idle") || littleMacCurrent.IsName("Little Mac Idle Tired")) && (lastAction > 0)) {
			if (marioCurrent.IsName("Enemy Mario Punch") || marioCurrent.IsName("Enemy Mario Cling Miss")) {
				return;
			}

			marioPunch ();

			if (lastAction == 3) {
				lastAction = 0;
			}
			else {
				lastAction++;
			}
		}
	}

	public void marioBodyDodge() {
		mario.SetTrigger ("Body Dodge");
		if (!luigi.GetCurrentAnimatorStateInfo (0).IsName ("Enemy Luigi Down")) {
			audio.PlayOneShot (littleMacMiss);
		}
	}
	
	public void luigiBodyDodge() {
		luigi.SetTrigger ("Body Dodge");
	}

	public void marioPunch() {
		mario.SetTrigger ("Punch");
	}

	public void luigiPunch() {
		if (luigi.GetCurrentAnimatorStateInfo (0).IsName ("Enemy Luigi Punch")) return;
		luigi.SetTrigger ("Punch");
	}

	public void punchedByMario() {
		// if little mac isn't dodging, he gets punched
		if (!LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Dodge Left")) {
			if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Dodge Right") && (luigi.GetCurrentAnimatorStateInfo(0).IsName("Enemy Luigi Down"))) {
				return;
			}

			if (isLittleMacKnockdown()) {
				return;
			}
			LittleMacAnimator.LittleMacA.animator.SetTrigger("Punched By Left");
			LittleMacController.LittleMac.health -= 5;
			LittleMacController.LittleMac.LittleMacHealth.fillAmount-=.05f;
		}
	}

	public void punchedByMarioSolo() {
		if (!LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Dodge Left") || !LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Dodge Right")) {
			if (isLittleMacKnockdown()) {
				return;
			}
			LittleMacAnimator.LittleMacA.animator.SetTrigger("Punched By Right");
			LittleMacController.LittleMac.health -= 5;
			LittleMacController.LittleMac.LittleMacHealth.fillAmount-=.05f;
		}
	}
	
	public void punchedByLuigi() {
		if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Stuck") || LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Twitch")) {
			if (isLittleMacKnockdown()) {
				return;
			}
			LittleMacAnimator.LittleMacA.animator.SetTrigger("Hold Punch");
			LittleMacController.LittleMac.health -= 5;
			LittleMacController.LittleMac.LittleMacHealth.fillAmount-=.05f;
		}
		else if (!LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Dodge Right")) {
			if (isLittleMacKnockdown()) {
				return;
			}
			LittleMacAnimator.LittleMacA.animator.SetTrigger("Punched By Right");
			LittleMacController.LittleMac.health -= 5;
			LittleMacController.LittleMac.LittleMacHealth.fillAmount-=.05f;
		}
	}

	public void clingOnLittleMac() {
		if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Idle")) {
			print ("clinging on little mac");
			LittleMacAnimator.LittleMacA.animator.SetTrigger ("Stuck");
			mario.SetTrigger("Hold");
		}
		else {
			print ("Setting luigi to scared");
			luigi.SetTrigger("Scared");
		}
	}

	public void marioGetUp() {
		luigi.SetTrigger ("Idle");
	}

	private bool isLittleMacKnockdown() {
		if (LittleMacController.LittleMac.health <= 0) {
			LittleMacAnimator.LittleMacA.animator.SetTrigger("Falldown");
			return true;
		}

		return false;
	}

	public void marioEncourage() {
		luigi.SetTrigger ("Reborn");
	}

	public void macTired() {
		if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Dodge Right Tired") || LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Dodge Left Tired")) {
			LifeScript.LifeController.addLife(20);
			LittleMacAnimator.LittleMacA.animator.SetTrigger("Untired");
			LifeScript.isMacTired = false;
		}
	}

}
