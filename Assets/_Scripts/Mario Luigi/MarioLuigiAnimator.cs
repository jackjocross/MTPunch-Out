using UnityEngine;
using System.Collections;

public class MarioLuigiAnimator : MonoBehaviour {

	public static MarioLuigiAnimator marioLuigiA;
	public Animator mario;
	public Animator luigi;

	private bool fightStarted = false;
	private int numLuigiPunches = 0;
	private int lastAction = 0;

	private int gameSpeed = 0;

	private AnimatorStateInfo marioCurrent, luigiCurrent, littleMacCurrent;

	void Awake() {
		marioLuigiA = this;
	}

	void FixedUpdate() {
		if (!fightStarted) return;

		gameSpeed++;

		if ((gameSpeed % 10) != 0) {
			return;
		}

		marioCurrent = mario.GetCurrentAnimatorStateInfo (0);
		luigiCurrent = luigi.GetCurrentAnimatorStateInfo (0);
		littleMacCurrent = LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0);

		if (littleMacCurrent.IsName ("Little Mac Idle") && (lastAction == 0)) {
			if (marioCurrent.IsName("Mario Cling") || marioCurrent.IsName("Mario Cling Hold") || marioCurrent.IsName("Mario Cling Miss") || marioCurrent.IsName("Mario Cling Twitch")) {
				return;
			}
			mario.SetTrigger("Cling");
			lastAction++;
		}
		else if (littleMacCurrent.IsName ("Little Mac Idle") && (lastAction > 0)) {
			if (marioCurrent.IsName("Enemy Mario Punch") || luigiCurrent.IsName("Enemy Luigi Punch")) {
				return;
			}

			//MarioLuigiController.MarioLuigiC.randomPunch();

			marioPunch ();

			if (lastAction == 3) {
				lastAction = 0;
			}
			else {
				lastAction++;
			}
		}
		else if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Stuck")) {
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
	}

	public void startOfFight() {
		fightStarted = true;
	}

	public void marioBodyDodge() {
		mario.SetTrigger ("Body Dodge");
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

			LittleMacAnimator.LittleMacA.animator.SetTrigger("Punched By Left");
		}
	}
	
	public void punchedByLuigi() {
		if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Stuck") || LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Twitch")) {
			LittleMacAnimator.LittleMacA.animator.SetTrigger("Hold Punch");
		}
		else if (!LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Dodge Right")) {

			LittleMacAnimator.LittleMacA.animator.SetTrigger("Punched By Right");
		}
	}

	public void clingOnLittleMac() {
		if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Idle")) {
			LittleMacAnimator.LittleMacA.animator.SetTrigger ("Stuck");
			mario.SetTrigger("Hold");
		}
		else {
			luigi.SetTrigger("Scared");
		}
	}

	public void marioGetUp() {
		luigi.SetTrigger ("Idle");
	}

}
