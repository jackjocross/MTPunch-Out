using UnityEngine;
using System.Collections;

public class VonKaiserAnimator : MonoBehaviour {

	public static VonKaiserAnimator VonKaiserA;

	public Animator animator;

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

	public void headHit() {
//		if (animator.GetCurrentAnimatorStateInfo(0).IsName ("Von Kaiser Head Hit") || 
//		    animator.GetCurrentAnimatorStateInfo(0).IsName ("Von Kaiser Large Head Hit")) {
//			return;
//		}

		animator.SetTrigger ("Head Hit");

		int numHeadHits = animator.GetInteger ("Num Head Hits");
		if (numHeadHits == 6) {
			animator.SetInteger ("Num Head Hits", 0);
		}
		else {
			animator.SetInteger ("Num Head Hits", ++numHeadHits);
		}

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
