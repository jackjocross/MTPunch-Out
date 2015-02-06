using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LittleMacController : MonoBehaviour {
	/*Static variable for access to this singleton*/
	public static LittleMacController LittleMac;
	
	/*Set in inspector*/
	public float tapSpeed;

	public static bool vonKaiserPlaying;

	public int health;
	public int knockdowns;
	public Image LittleMacHealth;
	public LittleMacAnimator animatorScript;

	private Time timeSinceLastPress;
	private bool isALastButtonPressed;
	private bool canShield;
	private int numberOfButtonPresses;

	void Awake(){
		if (Application.loadedLevelName.Equals("_Scene_Josh")) {
			vonKaiserPlaying = true;
		}
		else {
			vonKaiserPlaying = false;
		}

		LittleMac = this;

		canShield = true;
	}
	
	// Use this for initialization
	void Start () {
		health = SaveScene.littleMacHealth;
		animatorScript = this.GetComponent<LittleMacAnimator>();
		LittleMacHealth = GameObject.Find("Little Mac Health").GetComponent<Image>();
		LittleMacHealth.fillAmount = health * 0.01f;
		knockdowns = SaveScene.knockdowns;
	}
	
	// Update is called once per frame
	void Update () {
		if (vonKaiserPlaying) {
			/*If down key (or S) is held down, should stay in shield position*/
			if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)){
				if(canShield==true){
					if(Time.time - animatorScript.animator.GetFloat("shieldTime") < tapSpeed){
						//animatorScript.Duck();
						animatorScript.animator.SetFloat("shieldTime",0);
					}
					else{
						animatorScript.ShieldBegin();
						canShield=false;
						/*Record time of shield press*/
						animatorScript.animator.SetFloat("shieldTime",Time.time);
					}
				}
			}
			if (Input.GetKeyUp (KeyCode.DownArrow) || Input.GetKeyUp (KeyCode.S)) {
				print("lifting shield");
				animatorScript.ShieldEnd();
				canShield=true;
			}



			/*Little Mac is in various stages of Knockdown State, must quickly press buttons to advance back to idle*/
			if (animatorScript.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Falldown") || animatorScript.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Gets Up Stage 1") || animatorScript.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Gets Up Stage 2") || animatorScript.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Gets Up Stage 3")) {
				if(LittleMacController.vonKaiserPlaying && VonKaiserAnimator.VonKaiserA.animator.GetCurrentAnimatorStateInfo(0).IsName("Von Kaiser Victory")){
					return;
				}
				else if (MarioLuigiAnimator.marioLuigiA.mario.GetCurrentAnimatorStateInfo(0).IsName("Enemy Mario Victory Dance")) {
					return;
				}
				if(Input.GetKeyUp(KeyCode.X)||Input.GetKeyUp(KeyCode.Period)){
					numberOfButtonPresses++;
				}
				if(Input.GetKeyUp(KeyCode.Z)||Input.GetKeyUp(KeyCode.Comma)){
					numberOfButtonPresses++;
				}
				/*Read the number of button presses to determine if we should advance to next stage of getting up*/
				if(numberOfButtonPresses==4){
					animatorScript.animator.SetTrigger("Get Up Stage 1");
				}

				if(numberOfButtonPresses==8){
					animatorScript.animator.SetTrigger("Get Up Stage 2");
				}

				if(numberOfButtonPresses==12){

					animatorScript.animator.SetTrigger("Get Up Stage 3");
					MatchController.Match.MacGetsUp();
					MarioAnimator.MarioA.animator.SetTrigger("Fight");
					numberOfButtonPresses=0;
				}
				return;
			}


		
			/*Don't queue button presses when you're in the middle of animation*/
			if (IsAnimationActionPlaying ()) {
				return;
			}

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


			if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)){
				animatorScript.DodgeRight();
			}
			if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)){
				animatorScript.DodgeLeft();
			}
			if(Input.GetKeyDown (KeyCode.Return)){
				animatorScript.StarPunch();
			}
			/*A Button*/
			if (Input.GetKeyDown (KeyCode.X) || Input.GetKeyDown (KeyCode.Period)) {
				animatorScript.PunchRightNormal();
			}
			/*B Button*/
			if (Input.GetKeyDown (KeyCode.Z) || Input.GetKeyDown (KeyCode.Comma)) {
				animatorScript.PunchLeftNormal();
			}
			/*Select*/
			if(Input.GetKeyDown(KeyCode.Tab)){
			}
		}
		/*==============================MARIO LUIGI CONTROLS=========================================*/
		else {
			if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)){
				// little mac twitches if mario is clinging to him
				if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Stuck")) {
					LittleMacAnimator.LittleMacA.Twitch();
					return;
				}
				else if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Twitch")) {
					return;
				}
				else if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Hold Punch")) {
					return;
				}

				if(canShield==true){
					if(Time.time - animatorScript.animator.GetFloat("shieldTime") < tapSpeed){
						//animatorScript.Duck();
						animatorScript.animator.SetFloat("shieldTime",0);
					}
					else{
						animatorScript.ShieldBegin();
						canShield=false;
						/*Record time of shield press*/
						animatorScript.animator.SetFloat("shieldTime",Time.time);
					}
				}
			}
			if (Input.GetKeyUp (KeyCode.DownArrow) || Input.GetKeyUp (KeyCode.S)) {
				print("lifting shield");
				animatorScript.ShieldEnd();
				canShield=true;
			}
		
			/*GetKeyDown only returns selection for one frame so need to check GetKey to check if key is held down*/
			if (Input.GetKey (KeyCode.UpArrow)) {
				// little mac twitches if mario is clinging to him
				if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Stuck")) {
					LittleMacAnimator.LittleMacA.Twitch();
					return;
				}
				else if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Twitch")) {
					return;
				}
				else if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Hold Punch")) {
					return;
				}

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
				if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Stuck")) {
					LittleMacAnimator.LittleMacA.Twitch();
					return;
				}
				else if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Twitch")) {
					return;
				}
				else if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Hold Punch")) {
					return;
				}
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

			if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)){

				// little mac twitches if mario is clinging to him
				if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Stuck")) {
					LittleMacAnimator.LittleMacA.Twitch();
					return;
				}
				else if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Twitch")) {
					return;
				}
				else if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Hold Punch")) {
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
				else if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Hold Punch")) {
					return;
				}

				animatorScript.DodgeLeft();
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
				else if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Hold Punch")) {
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
				else if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Hold Punch")) {
					return;
				}

				animatorScript.PunchLeftNormal();
			}
		}
	}

	/*Return true if animation is playing besides idle*/
	bool IsAnimationActionPlaying(){
		if (animatorScript.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Idle")) {
			return false;
		}
		else if (animatorScript.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Idle Tired")) {
			return false;
		}
		return true;
	}
	
}
