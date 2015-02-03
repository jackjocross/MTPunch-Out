using UnityEngine;
using System.Collections;

public class LittleMacAnimator : MonoBehaviour {

	public static LittleMacAnimator LittleMacA;

	public Animator animator;
	public AudioClip LittleMacDodge;
	public AudioClip LitteMacStarPunchWindUp;
	public AudioClip VonKaiserJabMiss;
	public AudioClip VonKaiserUppercut;

	private AudioSource source;

	void Awake(){
		animator=GetComponent<Animator>();
		source=GetComponent<AudioSource>();
		source.panLevel = 0;
		LittleMacA = this;
	}

	// Update is called once per frame
	void Update () {
		/*Get Current States of Von Kaiser and Little Mac*/
		AnimatorStateInfo LittleMacStateInfo=animator.GetCurrentAnimatorStateInfo(0);
		AnimatorStateInfo VonKaiserStateInfo = VonKaiserController.VonKaiserInfo;

		/*If Little Mac is idle, check if he's getting punched*/
		if (LittleMacStateInfo.IsName ("Little Mac Idle")) {
			//If Von Kaiser at at climax of left punch, Little Mac gets hit

			if(VonKaiserStateInfo.IsName ("Von Kaiser Punch Climax")){
				animator.SetTrigger("Punched By Left");
				StarScript.StarAccess.removeStar();
				LittleMacController.LittleMac.health-=10;
				LittleMacController.LittleMac.LittleMacHealth.fillAmount-=.1f;
				if(LittleMacController.LittleMac.LittleMacHealth.fillAmount<=0f){
					Falldown();
				}
				if(MatchController.Match.lifeScript.numLives<3){
					MatchController.Match.lifeScript.removeLife(MatchController.Match.lifeScript.numLives);
				}
				else{
					MatchController.Match.lifeScript.removeLife(3);
				}
			}
			if(VonKaiserStateInfo.IsName("Von Kaiser Upper Climax") || VonKaiserStateInfo.IsName("Von Kaiser Upper Climax 0")){
				print ("uppercut");
				animator.SetTrigger("Punched By Right");
				StarScript.StarAccess.removeStar();
				LittleMacController.LittleMac.health-=20;
				if(LittleMacController.LittleMac.LittleMacHealth.fillAmount-.2f<=0){
					LittleMacController.LittleMac.LittleMacHealth.fillAmount=0f;
					Falldown();
				}
				else{
					LittleMacController.LittleMac.LittleMacHealth.fillAmount-=.2f;
				}

				if(MatchController.Match.lifeScript.numLives<3){
					MatchController.Match.lifeScript.removeLife(MatchController.Match.lifeScript.numLives);
				}
				else{
					MatchController.Match.lifeScript.removeLife(3);
				}
			}
		}


		if (VonKaiserStateInfo.IsName ("Von Kaiser Punch Climax")) {
			if(LittleMacStateInfo.IsName("Little Mac Dodge Left")){
				source.PlayOneShot(VonKaiserJabMiss,1f);
			}
			if(LittleMacStateInfo.IsName ("Little Mac Dodge Right")){
				source.PlayOneShot(VonKaiserJabMiss,1f);
			}
		}

		if (VonKaiserStateInfo.IsName ("Von Kaiser Upper Climax")|| VonKaiserStateInfo.IsName("Von Kaiser Upper Climax 0")) {
			if(LittleMacStateInfo.IsName("Little Mac Dodge Left")){
				source.PlayOneShot(VonKaiserUppercut,1f);
			}
			if(LittleMacStateInfo.IsName ("Little Mac Dodge Right")){
				source.PlayOneShot(VonKaiserUppercut,1f);
			}
		}

		if (LittleMacStateInfo.IsName ("Little Mac Shield")) {
			/*If Little Mac has his shield and Von Kaiser punches, block it, and return to idle*/
			if(VonKaiserStateInfo.IsName("Von Kaiser Punch Retreat")){
				animator.SetBool("Shield",false);
			}
		}

		if (LittleMacStateInfo.IsName ("Little Mac Falldown")) {

		}
	}

	public void DodgeRight(){
		source.PlayOneShot(LittleMacDodge,1f);
		animator.SetTrigger("Dodge Right");
	}

	public void DodgeLeft(){
		source.PlayOneShot(LittleMacDodge,1f);
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
		if (LifeScript.LifeController.numLives == 0) {
			return;
		}
		animator.SetTrigger("Star Punch");
		source.PlayOneShot(LitteMacStarPunchWindUp,1f);
	}
	
	public void Victory(){
		animator.SetTrigger("Victory");
	}

	public void Walk(){
		animator.SetTrigger("Walk");
	}

	public void Falldown(){
		animator.SetBool ("Falldown", true);
	}

	public void Twitch() {
		animator.SetTrigger ("Twitch");
		MarioLuigiAnimator.marioLuigiA.mario.SetTrigger ("Twitch");
	}

	public void backToStartScreen() {
		Application.LoadLevel("_Scene_Start");
	}

	public void marioLuigiBodyDodge() {

		// suppress console errors
		if (!Application.loadedLevelName.Equals ("_Scene_Mario_Luigi")) {
			return;
		}

		if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Idle")) {
			//print ("little mac is done punching");
			return;
		}
		else if (MarioLuigiAnimator.marioLuigiA.mario.GetCurrentAnimatorStateInfo(0).IsName("Enemy Mario Dodge")) {
			//print ("mario and luigi are already punching");
			return;
		}
		else if (MarioLuigiAnimator.marioLuigiA.mario.GetCurrentAnimatorStateInfo(0).IsName("Enemy Mario Punch")) {
			//print ("mario is punching little mac");
			return;
		}

		MarioLuigiAnimator.marioLuigiA.marioBodyDodge ();
		MarioLuigiAnimator.marioLuigiA.luigiBodyDodge ();
	}

	public void luigiBodyPunch() {
		// suppress console errors
		if (!Application.loadedLevelName.Equals ("_Scene_Mario_Luigi")) {
			return;
		}

		if (MarioLuigiAnimator.marioLuigiA.luigi.GetCurrentAnimatorStateInfo(0).IsName("Enemy Luigi Scared")) {
			//MarioLuigiAnimator.marioLuigiA.luigi.SetTrigger("Punched");
			MarioLuigiController.MarioLuigiC.luigiHit(true);
		}
	}

	public void luigiHeadPunch() {
		// suppress console errors
		if (!Application.loadedLevelName.Equals ("_Scene_Mario_Luigi")) {
			return;
		}

		if (MarioLuigiAnimator.marioLuigiA.luigi.GetCurrentAnimatorStateInfo(0).IsName("Enemy Luigi Scared")) {
			//MarioLuigiAnimator.marioLuigiA.luigi.SetTrigger("Head Punched");
			MarioLuigiController.MarioLuigiC.luigiHit(false);
		}
	}
	
}
