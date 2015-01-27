using UnityEngine;
using System.Collections;

public class VonKaiserAnimator : MonoBehaviour {

	public static VonKaiserAnimator VonKaiserA;

	public Animator animator;
	public Animator marioAnimator;

	public enum punchStates {Head, Body, Notset};
	public static int curPunchState;

	public int consecutiveHits = 0;

	private bool starPunched = false;
	private bool blockWhilePunch = false;
	private bool LMPunching = false;

	void Awake() {
		VonKaiserA = this;
		curPunchState = (int)punchStates.Notset;
	}

	public void intro() {
		animator.SetTrigger ("Intro");
	}

	public void punch() {
		if (animator.GetCurrentAnimatorStateInfo(0).IsName ("Von Kaiser Punch")) return;
		animator.SetTrigger ("Punch");
	}

	public void bodyBlock() {
		if (animator.GetCurrentAnimatorStateInfo(0).IsName ("Von Kaiser Body Block")) return;
		animator.SetTrigger ("Body Block");
	}

	public void headBlock() {
		if (animator.GetCurrentAnimatorStateInfo(0).IsName ("Von Kaiser Head Block")) return;
		animator.SetTrigger ("Head Block");
	}

	public void leftHeadHit() {

		if (curPunchState == (int)punchStates.Body) {
			animator.SetTrigger("Head Block");
			return;
		}
		else {
			curPunchState = (int)punchStates.Head;
		}

		VonKaiserController.health -= 5;
		print (VonKaiserController.health);
		VonKaiserController.VonKaiserHealth.fillAmount -= 0.05f;
		++consecutiveHits;

		if (VonKaiserController.health <= 0) {
			animator.SetTrigger("Knockdown Left");
			/*Little Mac retreats while Mario counts down*/
			LittleMacController.LittleMac.animatorScript.animator.SetBool("Retreat",true);
			marioAnimator.SetTrigger ("Enter");
			VonKaiserController.health = 100;
			VonKaiserController.VonKaiserHealth.fillAmount = 1f;

		}
		else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Von Kaiser Sucker Face") && consecutiveHits > 5) {
			animator.SetTrigger ("Large Head Hit Left");
			consecutiveHits = 0;
		}
		else {
			animator.SetTrigger ("Head Hit Left");
		}
	}

	public void rightHeadHit() {

		if (curPunchState == (int)punchStates.Body) {
			animator.SetTrigger("Head Block");
			return;
		}
		else {
			curPunchState = (int)punchStates.Head;
		}

		VonKaiserController.health -= 5;
		print (VonKaiserController.health);
		VonKaiserController.VonKaiserHealth.fillAmount -= 0.05f;
		++consecutiveHits;

		if (VonKaiserController.health <= 0) {
			animator.SetTrigger("Knockdown Right");
			marioAnimator.SetTrigger ("Enter");
			VonKaiserController.health = 100;
			VonKaiserController.VonKaiserHealth.fillAmount = 1f;
		}
		else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Von Kaiser Sucker Face") && consecutiveHits > 5) {
			animator.SetTrigger ("Large Head Hit Right");
			consecutiveHits = 0;
		}
		else {
			animator.SetTrigger ("Head Hit Right");
		}
	}

	public void leftBodyHit() {
		if (curPunchState == (int)punchStates.Head) {
			animator.SetTrigger("Body Block");
			return;
		}
		else {
			curPunchState = (int)punchStates.Body;
		}

		VonKaiserController.health -= 5;
		VonKaiserController.VonKaiserHealth.fillAmount -= 0.05f;
		++consecutiveHits;

		if (VonKaiserController.health <= 0) {
			animator.SetTrigger("Body Knockdown Left");
			marioAnimator.SetTrigger ("Enter");
			VonKaiserController.health = 100;
			VonKaiserController.VonKaiserHealth.fillAmount = 1f;
		}
		else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Von Kaiser Sucker Face") && consecutiveHits > 5) {
			animator.SetTrigger ("Large Head Hit Right");
			consecutiveHits = 0;
		}
		else {
			animator.SetTrigger ("Body Hit");
		}
	}

	public void rightBodyHit() {
		if (curPunchState == (int)punchStates.Head) {
			animator.SetTrigger("Body Block");
			return;
		}
		else {
			curPunchState = (int)punchStates.Body;
		}

		VonKaiserController.health -= 5;
		VonKaiserController.VonKaiserHealth.fillAmount -= 0.05f;
		++consecutiveHits;
		
		if (VonKaiserController.health <= 0) {
			animator.SetTrigger("Body Knockdown Right");
			marioAnimator.SetTrigger ("Enter");
			VonKaiserController.health = 100;
			VonKaiserController.VonKaiserHealth.fillAmount = 1f;
		}
		else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Von Kaiser Sucker Face") && consecutiveHits > 5) {
			animator.SetTrigger ("Large Head Hit Right");
			consecutiveHits = 0;
		}
		else {
			animator.SetTrigger ("Body Hit");
		}
	}

	public void bodyStarPunch() {
		// return if a punch has already been registered
		if (starPunched == true) {
			return;
		}

		if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName ("Little Mac Punch Left Normal Climax")) {
			starPunched = true;
			if (StarScript.StarAccess.getNumStars() < 3) {
				StarScript.StarAccess.addStar();
			}
			leftBodyHit();
		}
		else if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName ("Little Mac Punch Right Normal Climax")) {
			starPunched = true;
			if (StarScript.StarAccess.getNumStars() < 3) {
				StarScript.StarAccess.addStar();
			}
			rightBodyHit();
		}
	}

	public void resetStarPunched() {
		starPunched = false;
	}

	public void punchLeadBlock() {
		// return if a block has already been registered
		if (blockWhilePunch == true) {
			return;
		}

		if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Punch Left Face Lead Up") 
		    || LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Punch Left Face Climax")
		    || LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Punch Left Face Follow Through")
		    || LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Punch Right Face Lead Up")
		    || LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Punch Right Face Climax")
		    || LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Punch Right Face Follow Through")) {
			blockWhilePunch = true;
			headBlock();
		}
		else if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Punch Left Normal Lead Up") 
		    || LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Punch Left Normal Climax")
		    || LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Punch Left Normal Follow Through")
		    || LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Punch Right Normal Lead Up")
		    || LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Punch Right Normal Climax")
		    || LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Punch Right Normal Follow Through")) {			
			blockWhilePunch = true;
			bodyBlock();
		}
	}

	public void resetBlockWhilePunch() {
		blockWhilePunch = false;
	}

	public void littleMacPunching() {
		if (LMPunching == true) {
			return;
		}

		if( LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Punch Left Face Lead Up") 
			|| LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Punch Left Face Climax")
			|| LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Punch Left Face Follow Through")
		   || LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Punch Left Normal Lead Up") 
		   || LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Punch Left Normal Climax")
		   || LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Punch Left Normal Follow Through")) {
			LMPunching = true;
			LittleMacAnimator.LittleMacA.animator.SetTrigger("Punched By Left");
		}
	}

	public void resetLittleMacPunching() {
		LMPunching = false;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
