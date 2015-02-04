using UnityEngine;
using System.Collections;

public class LittleMacAnimator : MonoBehaviour {

	public static LittleMacAnimator LittleMacA;

	public Animator animator;
	public AudioClip LittleMacDodge;
	public AudioClip LitteMacStarPunchWindUp;
	public AudioClip VonKaiserJabMiss;
	public AudioClip VonKaiserUppercut;

	public AudioClip VonKaiserSuccessfulJab;
	public AudioClip LittleMacKnockdown;
	public AudioClip LittleMacHitsTheFloor;

	private AudioSource source;

	private int numMarioPunches = 0;
	public bool lastBattleHappened = false;

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

		/*If Little Mac is In Shield stance, end shield if hit by a jab*/
		if (LittleMacStateInfo.IsName ("Little Mac Shield")) {
			if(VonKaiserStateInfo.IsName ("Von Kaiser Punch Climax")){
				ShieldEnd();
				return;
			}
		}

		/*If Little Mac is in a punchable state*/
		if (isLittleMacPunchable()) {
			/*If Von Kaiser is at climax of left punch, Little Mac gets hit*/
			if(VonKaiserStateInfo.IsName ("Von Kaiser Punch Climax")){
				audio.PlayOneShot(VonKaiserSuccessfulJab,1f);
				StarScript.StarAccess.removeStar(1);
				LittleMacController.LittleMac.health-=10;
				LittleMacController.LittleMac.LittleMacHealth.fillAmount-=.1f;
				/*Determine if Little Mac has been knocked down and play correct response animation*/
				if(LittleMacController.LittleMac.LittleMacHealth.fillAmount<=0f){
					LittleMacController.LittleMac.knockdowns++;
					Falldown();
				}
				else{
					animator.SetTrigger("Punched By Left");
				}
				if(MatchController.Match.lifeScript.numLives<3){
					MatchController.Match.lifeScript.removeLife(MatchController.Match.lifeScript.numLives);
				}
				else{
					MatchController.Match.lifeScript.removeLife(3);
				}
			}
			/*If Von Kaiser is at climax of his uppercut, Little Mac gets hit*/
			if(VonKaiserStateInfo.IsName("Von Kaiser Upper Climax") || VonKaiserStateInfo.IsName("Von Kaiser Upper Climax 0")){
				print ("uppercut");
				source.PlayOneShot(VonKaiserJabMiss,1f);
				StarScript.StarAccess.removeStar(1);
				LittleMacController.LittleMac.health-=20;
				if(LittleMacController.LittleMac.LittleMacHealth.fillAmount-.2f<=0){
					LittleMacController.LittleMac.LittleMacHealth.fillAmount=0f;
					LittleMacController.LittleMac.knockdowns++;
					Falldown();
				}
				else{
					animator.SetTrigger("Punched By Right");
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
		/*if (VonKaiserAnimator.VonKaiserA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Von Kaiser Punch")) {
			return;
		}*/
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
		animator.SetTrigger("Falldown");
		VonKaiserAnimator.VonKaiserA.animator.SetBool("Punch",false);
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
		else if (MarioLuigiAnimator.marioLuigiA.luigi.GetCurrentAnimatorStateInfo(0).IsName("Enemy Luigi Scared")) {
			return;
		}
		else if (MarioLuigiAnimator.marioLuigiA.luigi.GetCurrentAnimatorStateInfo(0).IsName("Enemy Luigi Punched")) {
			return;
		}
		else if (MarioLuigiAnimator.marioLuigiA.luigi.GetCurrentAnimatorStateInfo(0).IsName("Enemy Luigi Head Punched")) {
			return;
		}
		else if (MarioLuigiAnimator.marioLuigiA.mario.GetCurrentAnimatorStateInfo(0).IsName("Enemy Mario Idle Solo")) {
			return;
		}
		else if (MarioLuigiAnimator.marioLuigiA.mario.GetCurrentAnimatorStateInfo(0).IsName("Enemy Mario Hit")) {
			return;
		}

		LifeScript.LifeController.removeLife (1);
		if (LifeScript.LifeController.numLives == 0) {
			// TODO Make the tired state
			print ("all out of lives!!");
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

	public void marioBodyPunch() {
		// suppress console errors
		if (!Application.loadedLevelName.Equals ("_Scene_Mario_Luigi")) {
			return;
		}
		
		if (MarioLuigiAnimator.marioLuigiA.mario.GetCurrentAnimatorStateInfo(0).IsName("Enemy Mario Idle Solo")) {

			if ((MarioLuigiController.marioHealth == 1) && !lastBattleHappened){
				lastEpicBattle();
			}
			else if (numMarioPunches == 3) {
				MarioLuigiAnimator.marioLuigiA.mario.SetTrigger("Dodge Solo");
				numMarioPunches = 0;
			}
			else {
				MarioLuigiController.MarioLuigiC.marioHit(false);
				numMarioPunches++;
			}
		}
	}
	
	public void marioHeadPunch() {
		// suppress console errors
		if (!Application.loadedLevelName.Equals ("_Scene_Mario_Luigi")) {
			return;
		}

		if ((MarioLuigiController.marioHealth == 1) && !lastBattleHappened) {
			lastEpicBattle();
		}
		else if (MarioLuigiAnimator.marioLuigiA.mario.GetCurrentAnimatorStateInfo (0).IsName ("Enemy Mario Hit")) {
			return;
		}

		if (MarioLuigiAnimator.marioLuigiA.mario.GetCurrentAnimatorStateInfo(0).IsName("Enemy Mario Idle Solo")) {
			if (numMarioPunches == 3) {
				MarioLuigiAnimator.marioLuigiA.mario.SetTrigger("Dodge Solo");
				numMarioPunches = 0;
			}
			else {
				MarioLuigiController.MarioLuigiC.marioHit(false);
				numMarioPunches++;
			}
		}
	}

	public bool isLittleMacPunchable(){
		AnimatorStateInfo state=animator.GetCurrentAnimatorStateInfo(0);
		if(state.IsName("Little Mac Idle")){
			return true;
		}
		if(state.IsName("Little Mac Punch Right Normal")){
			return true;
		}
		if (state.IsName ("Little Mac Punch Right Face")) {
			return true;
		}
		if (state.IsName ("Little Mac Punch Left Face")) {
			return true;
		}
		if (state.IsName ("Little Mac Punch Left Normal")) {
			return true;
		}
		return false;
	}

	
	public void LittleMacKnockoutBegin(){
		source.PlayOneShot(LittleMacKnockdown);
	}
	
	public void LittleMacKnockoutEnd(){
		VonKaiserAnimator.VonKaiserA.animator.SetTrigger("Retreat");
		MarioAnimator.MarioA.animator.SetTrigger("Enter");
	}

	public void lastEpicBattle() {
		lastBattleHappened = true;
		MarioLuigiAnimator.marioLuigiA.mario.SetTrigger("Last Cling");
//		MarioLuigiAnimator.marioLuigiA.luigi.SetTrigger ("Reborn");
		MarioLuigiController.luigiHealth = 1;
		MarioLuigiController.marioHealthBar.fillAmount = 0.03125f;
	}

}
