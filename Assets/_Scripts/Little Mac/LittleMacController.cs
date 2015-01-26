using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LittleMacController : MonoBehaviour {

	public static LittleMacController LittleMac;


	/*Set in inspector*/
	public float tapSpeed;

	private LittleMacAnimator animatorScript;

	void Awake(){
		LittleMac = this;
	}
	
	// Use this for initialization
	void Start () {
		animatorScript=this.GetComponent<LittleMacAnimator>();
	}
	
	// Update is called once per frame
	void Update () {

		/*GetKeyDown only returns selection for one frame so need to check GetKey to check if key is held down*/
		if (Input.GetKey (KeyCode.UpArrow)) {
			/*Right Face Punch*/
			if(Input.GetKeyDown(KeyCode.X)){
				animatorScript.PunchRightFace();
				vonKaiserRightHeadHit();
				return;
			}
			if(Input.GetKeyDown(KeyCode.Period)){
				animatorScript.PunchRightFace();
				return;
			}
			/*Left Punch Face*/
			if(Input.GetKeyDown(KeyCode.Z)){
				animatorScript.PunchLeftFace();
				vonKaiserLeftHeadHit();
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
			if(Time.time-animatorScript.animator.GetFloat("shieldTime")<tapSpeed){
				animatorScript.Duck();
				animatorScript.animator.SetFloat("shieldTime",0);
			}
			else{
				animatorScript.ShieldBegin();
				/*Record time of shield press*/
				animatorScript.animator.SetFloat("shieldTime",Time.time);
			}
		}
		if (Input.GetKeyUp (KeyCode.DownArrow) || Input.GetKeyUp (KeyCode.S)) {
			animatorScript.ShieldEnd();
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
			vonKaiserRightBodyHit();
		}
		/*B Button*/
		if (Input.GetKeyDown (KeyCode.Z) || Input.GetKeyDown (KeyCode.Comma)) {
			animatorScript.PunchLeftNormal();
			vonKaiserLeftBodyHit();
		}
		/*Select*/
		if(Input.GetKeyDown(KeyCode.Tab)){
		}
	}

	/*Return true if animation is playing besides idle*/
	bool IsAnimationActionPlaying(){
		return !animatorScript.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Idle");
	}

	void vonKaiserLeftHeadHit() {
		if (VonKaiserController.VonKaiserInfo.IsName("Von Kaiser Sucker Face") || VonKaiserController.VonKaiserInfo.IsName("Von Kaiser Punch Retreat") || VonKaiserController.VonKaiserInfo.IsName("Von Kaiser Upper Hang")) {					
			VonKaiserController.VonKaiser.leftHeadHit ();
		}
	}

	void vonKaiserRightHeadHit() {
		if (VonKaiserController.VonKaiserInfo.IsName("Von Kaiser Sucker Face") || VonKaiserController.VonKaiserInfo.IsName("Von Kaiser Punch Retreat") || VonKaiserController.VonKaiserInfo.IsName("Von Kaiser Upper Hang")) {					
			VonKaiserController.VonKaiser.rightHeadHit ();
		}
	}

	void vonKaiserLeftBodyHit() {
		if (VonKaiserController.VonKaiserInfo.IsName("Von Kaiser Upper Hang") || VonKaiserController.VonKaiserInfo.IsName("Von Kaiser Sucker Face 0")) {					
			VonKaiserController.VonKaiser.leftBodyHit ();
		}
	}

	void vonKaiserRightBodyHit() {
		if (VonKaiserController.VonKaiserInfo.IsName("Von Kaiser Upper Hang") || VonKaiserController.VonKaiserInfo.IsName("Von Kaiser Sucker Face 0")) {					
			VonKaiserController.VonKaiser.rightBodyHit ();
		}
	}


}
