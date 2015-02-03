using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LittleMacController : MonoBehaviour {

	public static LittleMacController LittleMac;
	
	/*Set in inspector*/
	public float tapSpeed;
	public int health;
	public Image LittleMacHealth;

	public LittleMacAnimator animatorScript;

	void Awake(){
		LittleMac = this;
	}
	
	// Use this for initialization
	void Start () {
		health = SaveScene.littleMacHealth;
		animatorScript = this.GetComponent<LittleMacAnimator>();
		LittleMacHealth = GameObject.Find("Little Mac Health").GetComponent<Image>();
		LittleMacHealth.fillAmount = health * 0.01f;
	}
	
	// Update is called once per frame
	void Update () {

		/*GetKeyDown only returns selection for one frame so need to check GetKey to check if key is held down*/
		if (Input.GetKey (KeyCode.UpArrow)) {
			/*Right Face Punch*/
			if(Input.GetKeyDown(KeyCode.X)){
				animatorScript.PunchRightFace();
				return;
			}
			if(Input.GetKeyDown(KeyCode.Period)){
				animatorScript.PunchRightFace();
				return;
			}
			/*Left Punch Face*/
			if(Input.GetKeyDown(KeyCode.Z)){
				animatorScript.PunchLeftFace();
				return;
			}
			if(Input.GetKeyDown (KeyCode.Comma)){
				animatorScript.PunchLeftFace();
				return;
			}
		}
		if (Input.GetKey(KeyCode.W)){
			/*Right Punch Face*/
			if(Input.GetKeyDown(KeyCode.X)){
				animatorScript.PunchRightFace();
				return;
			}
			if(Input.GetKeyDown(KeyCode.Period)){
				animatorScript.PunchRightFace();
				return;
			}
			/*Left Punch Face*/
			if(Input.GetKeyDown(KeyCode.Z)){
				animatorScript.PunchLeftFace();
				return;
			}
			if(Input.GetKeyDown (KeyCode.Comma)){
				animatorScript.PunchLeftFace();
				return;
			}
		}

		/*Directional Inputs*/

		/*If down key (or S) is held down, should stay in shield position*/
		if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)){

			// little mac twitches if mario is clinging to him
			if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Stuck")) {
				LittleMacAnimator.LittleMacA.Twitch();
				return;
			}
			else if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Twitch")) {
				return;
			}

			if(Time.time-animatorScript.animator.GetFloat("shieldTime")<tapSpeed){
				//animatorScript.Duck();
				animatorScript.animator.SetFloat("shieldTime",0);
			}
			else{
				animatorScript.ShieldBegin();
				/*Record time of shield press*/
				animatorScript.animator.SetFloat("shieldTime",Time.time);
			}
		}
		if (Input.GetKeyUp (KeyCode.DownArrow) || Input.GetKeyUp (KeyCode.S)) {

			// little mac twitches if mario is clinging to him
			if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Stuck")) {
				LittleMacAnimator.LittleMacA.Twitch();
				return;
			}
			else if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Twitch")) {
				return;
			}

			animatorScript.ShieldEnd();
		}

		if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)){

			// little mac twitches if mario is clinging to him
			if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Stuck")) {
				LittleMacAnimator.LittleMacA.Twitch();
				return;
			}
			else if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Twitch")) {
				return;
			}

			animatorScript.DodgeRight();
		}
		if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)){

			// little mac twitches if mario is clinging to him
			if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Stuck")) {
				LittleMacAnimator.LittleMacA.Twitch();
				return;
			}
			else if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Twitch")) {
				return;
			}

			animatorScript.DodgeLeft();
		}
		if(Input.GetKeyDown (KeyCode.Return)){

			// little mac twitches if mario is clinging to him
			if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Stuck")) {
				LittleMacAnimator.LittleMacA.Twitch();
				return;
			}
			else if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Twitch")) {
				return;
			}

			animatorScript.StarPunch();
		}

		/*A Button*/
		if (Input.GetKeyDown (KeyCode.X) || Input.GetKeyDown (KeyCode.Period)) {

			// little mac twitches if mario is clinging to him
			if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Stuck")) {
				LittleMacAnimator.LittleMacA.Twitch();
				return;
			}
			else if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Twitch")) {
				return;
			}

			animatorScript.PunchRightNormal();
		}
		/*B Button*/
		if (Input.GetKeyDown (KeyCode.Z) || Input.GetKeyDown (KeyCode.Comma)) {

			// little mac twitches if mario is clinging to him
			if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Stuck")) {
				LittleMacAnimator.LittleMacA.Twitch();
				return;
			}
			else if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Twitch")) {
				return;
			}

			animatorScript.PunchLeftNormal();
		}
		/*Select*/
		if(Input.GetKeyDown(KeyCode.Tab)){
		}
	}

	/*Return true if animation is playing besides idle*/
	bool IsAnimationActionPlaying(){
		return !animatorScript.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Idle");
	}



}
