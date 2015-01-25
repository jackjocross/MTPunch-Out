using UnityEngine;
using System.Collections;

public class MarioAnimator : MonoBehaviour {

	public Animator animator;
	
	public void intro() {
		animator.SetTrigger ("Intro");
	}

	public void enter() {
		animator.SetTrigger ("Enter");
	}

	public void Awake() {
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
