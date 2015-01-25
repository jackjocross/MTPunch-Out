using UnityEngine;
using System.Collections;

public class VonKaiserAnimator : MonoBehaviour {

	public static VonKaiserAnimator VonKaiserA;

	public Animator animator;
	public Animator marioAnimator;

	void Awake() {
		VonKaiserA = this;
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

		VonKaiserController.health -= 10;
		VonKaiserController.VonKaiserHealth.fillAmount -= 0.1f;
		++VonKaiserController.numHeadHits;

		if (VonKaiserController.health <= 0) {
			animator.SetTrigger("Knockdown Left");
			marioAnimator.SetTrigger ("Enter");
			VonKaiserController.health = 100;
			VonKaiserController.VonKaiserHealth.fillAmount = 1f;

		}
		else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Von Kaiser Sucker Face") && VonKaiserController.numHeadHits > 5) {
			animator.SetTrigger ("Large Head Hit Left");
			VonKaiserController.numHeadHits = 0;
		}
		else {
			animator.SetTrigger ("Head Hit Left");
		}
	}

	public void rightHeadHit() {

		VonKaiserController.health -= 10;
		VonKaiserController.VonKaiserHealth.fillAmount -= 0.1f;
		++VonKaiserController.numHeadHits;

		if (VonKaiserController.health <= 0) {
			animator.SetTrigger("Knockdown Right");
			marioAnimator.SetTrigger ("Enter");
			VonKaiserController.health = 100;
			VonKaiserController.VonKaiserHealth.fillAmount = 1f;
		}
		else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Von Kaiser Sucker Face") && VonKaiserController.numHeadHits > 5) {
			animator.SetTrigger ("Large Head Hit Right");
			VonKaiserController.numHeadHits = 0;
		}
		else {
			animator.SetTrigger ("Head Hit Right");
		}
	}

	public void leftBodyHit() {
		VonKaiserController.health -= 10;
		VonKaiserController.VonKaiserHealth.fillAmount -= 0.1f;

		if (VonKaiserController.health <= 0) {
			animator.SetTrigger("Knockdown Left");
			marioAnimator.SetTrigger ("Enter");
			VonKaiserController.health = 100;
			VonKaiserController.VonKaiserHealth.fillAmount = 1f;
		}
		else {
			animator.SetTrigger ("Body Hit");
		}
	}

	public void rightBodyHit() {
		VonKaiserController.health -= 10;
		VonKaiserController.VonKaiserHealth.fillAmount -= 0.1f;
		
		if (VonKaiserController.health <= 0) {
			animator.SetTrigger("Knockdown Right");
			marioAnimator.SetTrigger ("Enter");
			VonKaiserController.health = 100;
			VonKaiserController.VonKaiserHealth.fillAmount = 1f;
		}
		else {
			animator.SetTrigger ("Body Hit");
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
