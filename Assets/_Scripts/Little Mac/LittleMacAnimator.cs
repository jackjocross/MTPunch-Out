﻿using UnityEngine;
using System.Collections;

public class LittleMacAnimator : MonoBehaviour {
	public Animator animator;

	void Awake(){
		animator=GetComponent<Animator>();
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

	public void Shield(){
		animator.SetTrigger ("Shield");
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
	
}
