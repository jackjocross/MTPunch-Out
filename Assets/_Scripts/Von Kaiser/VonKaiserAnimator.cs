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

	public AudioClip VonKaiserBeginFallDown;
	public AudioClip VonKaiserClimaxFallDown;
	public AudioClip SuccessfulMacHeadPunch;
	public AudioClip SuccessfulMacAbPunch;
	public AudioClip Block;
	public AudioClip StarSound;

	private AudioSource source;

	void Awake() {
		VonKaiserA = this;
		curPunchState = (int)punchStates.Notset;
		source=this.GetComponent<AudioSource>();
		source.panLevel=0;
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
		source.PlayOneShot(Block,1f);
		LifeScript.LifeController.removeLife(1);
	}

	public void headBlock() {
		if (animator.GetCurrentAnimatorStateInfo(0).IsName ("Von Kaiser Head Block")) return;
		animator.SetTrigger ("Head Block");
		source.PlayOneShot(Block,1f);
		LifeScript.LifeController.removeLife(1);
	}

	public void leftHeadHit() {

		if (curPunchState == (int)punchStates.Body) {
			animator.SetTrigger("Head Block");
			return;
		}
		else {
			curPunchState = (int)punchStates.Head;
		}

		source.PlayOneShot(SuccessfulMacHeadPunch,1f);
		VonKaiserController.health -= 1;
		VonKaiserController.VonKaiserHealth.fillAmount -= .03215f;
		++consecutiveHits;

		if (VonKaiserController.health <= 0) {
			VonKaiserController.VonKaiserC.knockdowns++;
			animator.SetTrigger("Knockdown Left");
			/*Little Mac retreats while Mario counts down*/
			LittleMacController.LittleMac.animatorScript.animator.SetTrigger("Retreat");
			PointScript.PointController.addPoints(1010);
		}
		else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Von Kaiser Sucker Face") && consecutiveHits > 5) {
			animator.SetTrigger ("Large Head Hit Left");
			consecutiveHits = 0;
			PointScript.PointController.addPoints(10);
		}
		else {
			animator.SetTrigger ("Head Hit Left");
			PointScript.PointController.addPoints(10);
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
		/*Succesful Head Punch by little mac*/
		source.PlayOneShot(SuccessfulMacHeadPunch,1f);


		VonKaiserController.health -=1;
		VonKaiserController.VonKaiserHealth.fillAmount -= .03125f;
		++consecutiveHits;

		if (VonKaiserController.health <= 0) {
			VonKaiserController.VonKaiserC.knockdowns++;
			animator.SetTrigger("Knockdown Right");
			LittleMacController.LittleMac.animatorScript.animator.SetTrigger("Retreat");
			PointScript.PointController.addPoints(1010);
		}
		else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Von Kaiser Sucker Face") && consecutiveHits > 5) {
			animator.SetTrigger ("Large Head Hit Right");
			consecutiveHits = 0;
			PointScript.PointController.addPoints(10);
		}
		else {
			animator.SetTrigger ("Head Hit Right");
			PointScript.PointController.addPoints(10);
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


		/*Successful body punch by little mac*/
		source.PlayOneShot(SuccessfulMacAbPunch,1f);

		VonKaiserController.health -= 1;
		VonKaiserController.VonKaiserHealth.fillAmount -= 0.03125f;
		++consecutiveHits;

		if (VonKaiserController.health <= 0) {
			VonKaiserController.VonKaiserC.knockdowns++;
			animator.SetTrigger("Body Knockdown Left");
			LittleMacController.LittleMac.animatorScript.animator.SetTrigger("Retreat");
			marioAnimator.SetTrigger("Enter");
			PointScript.PointController.addPoints(1010);
		}
		else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Von Kaiser Sucker Face") && consecutiveHits > 5) {
			animator.SetTrigger ("Large Head Hit Right");
			consecutiveHits = 0;
			PointScript.PointController.addPoints(10);
		}
		else {
			animator.SetTrigger ("Body Hit");
			PointScript.PointController.addPoints(10);
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

		source.PlayOneShot(SuccessfulMacAbPunch,1f);

		VonKaiserController.health -= 1;
		VonKaiserController.VonKaiserHealth.fillAmount -= 0.03125f;
		++consecutiveHits;
		
		if (VonKaiserController.health <= 0) {
			VonKaiserController.VonKaiserC.knockdowns++;
			animator.SetTrigger("Body Knockdown Right");
			LittleMacController.LittleMac.animatorScript.animator.SetTrigger("Retreat");
			marioAnimator.SetTrigger ("Enter");
			PointScript.PointController.addPoints(1010);
		}
		else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Von Kaiser Sucker Face") && consecutiveHits > 5) {
			animator.SetTrigger ("Large Head Hit Right");
			consecutiveHits = 0;
			PointScript.PointController.addPoints(10);
		}
		else {
			animator.SetTrigger ("Body Hit");
			PointScript.PointController.addPoints(10);
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
			PointScript.PointController.addPoints(110);
			leftBodyHit();
		}
		else if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName ("Little Mac Punch Right Normal Climax")) {
			starPunched = true;
			if (StarScript.StarAccess.getNumStars() < 3) {
				StarScript.StarAccess.addStar();
			}
			PointScript.PointController.addPoints(110);
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

	public void BeginKnockDown(){
		source.clip = VonKaiserBeginFallDown;
		source.Play();
	}

	public void ClimaxKnockDown(){
		source.Stop();
		source.clip = VonKaiserClimaxFallDown;
		source.Play();
		MatchController.Match.VonKaiserKnockDown();
		marioAnimator.SetTrigger ("Enter");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
