using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LittleMacController : MonoBehaviour {

	static public LittleMacController LittleMac;

	/*Set in inspector*/
	public float distanceX;
	public float distanceY;
	public float tapSpeed;

	float minX;
	float maxX;
	float maxY;

	float originalX;
	float originalY;
	float previousX;
	float previousY;

	private LittleMacAnimator animatorScript;

	void Awake(){

	}
	
	// Use this for initialization
	void Start () {
		animatorScript=this.GetComponent<LittleMacAnimator>();

		minX=transform.position.x-distanceX;
		maxX=transform.position.x+distanceX;
		originalX = transform.position.x;
		previousX = originalX;

		maxY = transform.position.y + distanceY;
		originalY = transform.position.y;
		previousY = originalY;
	}
	
	// Update is called once per frame
	void Update () {
		//print(this.transform.position.x);

		if(IsAnimationActionPlaying()){
//				print ("idle state");
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

		/*Directional Inputs*/

		/*If down key (or S) is held down, should stay in shield position*/
		if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)){
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
		}
		/*B Button*/
		if (Input.GetKeyDown (KeyCode.Z) || Input.GetKeyDown (KeyCode.Comma)) {

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
