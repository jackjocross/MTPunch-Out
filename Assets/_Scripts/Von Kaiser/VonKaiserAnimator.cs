using UnityEngine;
using System.Collections;

public class VonKaiserAnimator : MonoBehaviour {

	public Animator animator;

	public void punch() {
		animator.SetTrigger ("Punch");
	}

	public void intro() {
		animator.SetTrigger ("Intro");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
