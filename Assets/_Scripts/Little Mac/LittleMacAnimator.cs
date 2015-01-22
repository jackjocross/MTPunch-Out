using UnityEngine;
using System.Collections;

public class LittleMacAnimator : MonoBehaviour {

	public static LittleMacAnimator LittleMacA;

	public Animator animator;

	void Awake(){
		animator=GetComponent<Animator>();
		LittleMacA = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DodgeRight(){
		animator.SetTrigger("Dodge Right");
	}

	public void DodgeLeft(){
		animator.SetTrigger("Dodge Left");
	}

	public void ShieldBegin(){
		animator.SetBool ("Shield",true);
	}

	public void ShieldEnd(){
		animator.SetBool("Shield",false);
	}

	public void PunchRightFace(){
		animator.SetTrigger("Punch Right Face");
	}

	public void PunchLeftFace(){
		animator.SetTrigger("Punch Left Face");
	}

	public void PunchRightNormal(){
		animator.SetTrigger("Punch Right Normal");
	}

	public void PunchLeftNormal(){
		animator.SetTrigger("Punch Left Normal");
	}

	public void Duck(){
		animator.SetTrigger("Duck");
	}

	public void StarPunch(){
		animator.SetTrigger("Star Punch");
	}
	
	public void Victory(){
		animator.SetTrigger("Victory");
	}

	public void Walk(){
		animator.SetTrigger("Walk");
	}
}
