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

		if (luigiCurrent.IsName ("Enemy Luigi Down")) {
			return;
		}

		if (LittleMacAnimator.LittleMacA.lastBattleHappened == true) {
			if (luigiCurrent.IsName("Enemy Luigi Punch")) {
				return;
			}

			print ("luigi should be punching!");
			
			if (numLuigiPunches == 5) {
				mario.SetTrigger("Release");
				LittleMacAnimator.LittleMacA.animator.SetTrigger("Released");
				numLuigiPunches = 0;
				return;
			}
			
			print ("telling luigi to punch!");
			
			luigi.SetTrigger("Punch");
			numLuigiPunches++;
		}

		if (littleMacCurrent.IsName ("Little Mac Idle") && (lastAction == 0)) {
			if (marioCurrent.IsName("Enemy Mario Cling") || marioCurrent.IsName("Enemy Mario Cling Hold") || marioCurrent.IsName("Enemy Mario Cling Miss") || marioCurrent.IsName("Enemy Mario Cling Twitch")) {
				return;
			}
			lastAction++;
			mario.SetTrigger("Cling");

		}
		else if (littleMacCurrent.IsName ("Little Mac Idle") && (lastAction > 0)) {
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

			print ("telling luigi to punch!");

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
			LittleMacAnimator.LittleMacA.animator.SetTrigger("Little Mac Falldown");
			return true;
		}

		return false;
	}

	public void marioEncourage() {
		luigi.SetTrigger ("Reborn");
	}

}
