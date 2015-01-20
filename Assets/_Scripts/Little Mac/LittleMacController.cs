using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LittleMacController : MonoBehaviour {
	/*Set in inspector*/
	public float distanceX;
	public float distanceY;
	public float tapSpeed;

	float minX;
	float maxX;
	float maxY;
	float minY;
	float originalX;
	float originalY;
	float previousX;
	float previousY;

	private LittleMacAnimator animatorScript;
	private int numberOfStars;
	
	// Use this for initialization
	void Start () {
		animatorScript=this.GetComponent<LittleMacAnimator>();	
		minX=transform.position.x-distanceX;
		maxX=transform.position.x+distanceX;
		originalX = transform.position.x;
		previousX = originalX;
	
		maxY = transform.position.y + distanceY;
		minY = transform.position.y - distanceY;
		originalY = transform.position.y;
		previousY = originalY;

		numberOfStars=0;

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
		if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)){
			if(Time.time-animatorScript.animator.GetFloat("shieldTime")<tapSpeed){
				animatorScript.Duck();
				animatorScript.animator.SetFloat("shieldTime",0);
			}
			else{
				animatorScript.Shield();
				/*Record time of shield press*/
				animatorScript.animator.SetFloat("shieldTime",Time.time);
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

	void OnAnimatorMove(){
		int currentStateHash=animatorScript.animator.GetCurrentAnimatorStateInfo(0).tagHash;
		AnimationInfo[] animationInfo = animatorScript.animator.GetCurrentAnimationClipState(0);
		float time=animationInfo[0].clip.length;

		//Get Current Position
		Vector3 position = transform.position;

		if(currentStateHash==Animator.StringToHash("Dodge Left")){
			//If Current Position is start, start moving to the left
			if(position.x>=originalX){
				position.x-=((distanceX*2.0f)*(Time.deltaTime/time));
			}
			//If Current Position is end, start moving to the right
			else if(position.x<=minX){
				position.x+=(distanceX*2.0f)*(Time.deltaTime/time);
			}
			//Current Position is somewhere in the middle of start and end
			else{
				if(previousX<position.x){
					position.x+=(distanceX*2.0f)*(Time.deltaTime/time);
				}
				else{
					position.x-=(distanceX*2.0f)*(Time.deltaTime/time);
				}
			}
			previousX=transform.position.x;
			transform.position = position;
		}

		if (currentStateHash == Animator.StringToHash ("Dodge Right")) {
			//If current position is start, start moving to the right
			if(position.x<=originalX){
				position.x+=((distanceX*2.0f)*(Time.deltaTime/time));
			}
			//If current position is end, start moving to the right
			else if(position.x>=maxX){
				position.x-=((distanceX*2.0f)*(Time.deltaTime/time));
			}
			//Curent position somewhere in middle
			else{
				if(previousX>position.x){
					position.x-=(distanceX*2.0f)*(Time.deltaTime/time);
				}
				else{
					position.x+=(distanceX*2.0f)*(Time.deltaTime/time);
				}
			}
			previousX=transform.position.x;
			transform.position=position;
		}

		if(currentStateHash==Animator.StringToHash("Punch Right Face") || currentStateHash==Animator.StringToHash("Punch Left Face")){
			//If Current Position is start, start moving up
			if(position.y<=originalY){
				position.y+=(distanceY*2.0f)*(Time.deltaTime/time);
			}
			//If Current Position is end, start moving down
			else if(position.y>=maxY){
				position.y-=(distanceY*2.0f)*(Time.deltaTime/time);
			}
			else{
				if(previousY<position.y){
					position.y+=(distanceY*2.0f)*(Time.deltaTime/time);
				}
				else{
					position.y-=(distanceY*2.0f)*(Time.deltaTime/time);
				}
			}
			previousY=transform.position.y;
			transform.position=position;
		}
	}

}
