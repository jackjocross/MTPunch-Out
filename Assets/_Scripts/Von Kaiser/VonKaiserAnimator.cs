using UnityEngine;
using System.Collections;

public class VonKaiserAnimator : MonoBehaviour {

	public static VonKaiserAnimator VonKaiserA;

	public Animator animator;
	public Animator marioAnimator;

	public enum punchStates {Head, Body, Notset};
	public static int curPunchState;

	public int consecutiveHits = 0;

	public AudioClip VonKaiserBeginFallDown;
	public AudioClip VonKaiserClimaxFallDown;
	public AudioClip SuccessfulMacHeadPunch;
	public AudioClip SuccessfulMacAbPunch;
	public AudioClip Block;

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
			print ("setting punch state to head");
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
			print ("setting punch state to head");
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
			print ("punch state is head!");
			return;
		}
		else {
			curPunchState = (int)punchStates.Body;
			print ("setting punch state to body");

		}

		/*Successful body punch by little mac*/
		source.PlayOneShot(SuccessfulMacAbPunch,1f);

		VonKaiserController.health -= 1;
		VonKaiserController.VonKaiserHealth.fillAmount -= 0.1f;
		++consecutiveHits;

		if (VonKaiserController.health <= 0) {
			VonKaiserController.VonKaiserC.knockdowns++;
			animator.SetTrigger("Knockdown Left");
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
			print ("punch state is head!");
			return;
		}
		else {
			curPunchState = (int)punchStates.Body;
			print ("setting punch state to body");

		}

		source.PlayOneShot(SuccessfulMacAbPunch,1f);

		VonKaiserController.health -= 10;
		VonKaiserController.VonKaiserHealth.fillAmount -= 0.1f;
		++consecutiveHits;
		
		if (VonKaiserController.health <= 0) {
			VonKaiserController.VonKaiserC.knockdowns++;
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
			animator.SetTrigger ("Body Hit");
		}
	}

	public void BeginKnockDown(){
		source.clip = VonKaiserBeginFallDown;
		source.Play();
	}

	public void ClimaxKnockDown(){
		source.Stop();
		source.clip = VonKaiserClimaxFallDown;
		source.Play();
		marioAnimator.SetTrigger ("Enter");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
