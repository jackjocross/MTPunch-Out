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
	public AudioClip LittleMacBlock;

	private AudioSource source;

	private int numMarioPunches = 0;
	public bool lastBattleHappened = false;

	void Awake(){
		animator=GetComponent<Animator>();
		source=GetComponent<AudioSource>();
		source.panLevel = 0;
		LittleMacA = this;
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
		print ("shield begin");
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
				MarioLuigiAnimator.marioLuigiA.audio.PlayOneShot(MarioLuigiAnimator.marioLuigiA.littleMacMiss);
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
				MarioLuigiAnimator.marioLuigiA.audio.PlayOneShot(MarioLuigiAnimator.marioLuigiA.littleMacMiss);
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
		if (state.IsName ("Little Mac Star Punch")) {
			return true;
		}

		if (state.IsName ("Little Mac Shield")) {
			return true;
		}

		if (state.IsName ("Little Mac Idle Tired")) {
			return true;
		}

		return false;
	}

	/*Animation Event that signals the beginning of Little Mac falling down sequence*/
	public void LittleMacKnockoutBegin(){
		source.PlayOneShot(LittleMacKnockdown);
		ClockScript.pauseTime = true;
	}

	/*Animation Event that signals the end of Little Mac falling down sequence*/
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

	/*How Little Mac reacts to the peak of Von Kaisers jab*/
	public void handleVonKaiserJab(){
		/*Get Current States of Von Kaiser and Little Mac*/
		AnimatorStateInfo LittleMacStateInfo=animator.GetCurrentAnimatorStateInfo(0);
		AnimatorStateInfo VonKaiserStateInfo = VonKaiserController.VonKaiserInfo;

		if (isLittleMacPunchable ()) {
			/*If Little Mac is In Shield stance, end shield if hit by a jab*/
			if (LittleMacStateInfo.IsName ("Little Mac Shield")) {
				source.PlayOneShot(LittleMacBlock,1f);
				ShieldEnd();
				return;
			}

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
			/*Remove the correct number of hearts (don't reach negative indexes)*/
			if(MatchController.Match.lifeScript.numLives<3){
				MatchController.Match.lifeScript.removeLife(MatchController.Match.lifeScript.numLives);
			}
			else{
				MatchController.Match.lifeScript.removeLife(3);
			}
		}
	}

	public void handleVonKaiserUppercut(){
		source.PlayOneShot(VonKaiserUppercut,1f);
		if (isLittleMacPunchable ()) {
			StarScript.StarAccess.removeStar(1);
			LittleMacController.LittleMac.health-=20;
			if(LittleMacController.LittleMac.LittleMacHealth.fillAmount-.2f<=0){
				StarScript.StarAccess.removeStar(StarScript.StarAccess.getNumStars());
				LittleMacController.LittleMac.LittleMacHealth.fillAmount=0f;
				LittleMacController.LittleMac.knockdowns++;
				Falldown();
			}
			else{
				animator.SetTrigger("Punched By Right");
				StarScript.StarAccess.removeStar(1);
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

	public void StarPunchClimax(){
		AnimatorStateInfo VonKaiserState=VonKaiserAnimator.VonKaiserA.animator.GetCurrentAnimatorStateInfo(0);
		/*If Von Kaiser is stunned,instant knockdown*/
		if (VonKaiserState.IsName ("Von Kaiser Sucker Face") || VonKaiserState.IsName ("Von Kaiser Head Hit Right")||VonKaiserState.IsName("Large Head Hit Right")||VonKaiserState.IsName("Large Head Hit Left")||VonKaiserState.IsName("Head Hit Left")) {
			VonKaiserController.health=0;
			VonKaiserController.VonKaiserHealth.fillAmount=0f;
			VonKaiserAnimator.VonKaiserA.animator.SetTrigger("Knockdown Right");
			VonKaiserController.VonKaiserC.knockdowns++;
			PointScript.PointController.addPoints(1500);
			animator.SetTrigger("Retreat");
			return;
		}
		else{
			VonKaiserController.health-=.16f;
			if(VonKaiserController.health<=0){
				VonKaiserController.health=0;
				VonKaiserController.VonKaiserHealth.fillAmount=0f;
				VonKaiserAnimator.VonKaiserA.animator.SetTrigger("Knockdown Right");
				VonKaiserController.VonKaiserC.knockdowns++;
				PointScript.PointController.addPoints(1500);
				animator.SetTrigger("Retreat");
				return;
			}
			VonKaiserController.VonKaiserHealth.fillAmount-=.16f;
			VonKaiserAnimator.VonKaiserA.animator.SetTrigger("Large Head Hit Right");
			PointScript.PointController.addPoints(500);
		}
	}

	/*Handle the appropiate reaction to a normal punch by Little Mac*/
	/*
	public void NormalPunchClimax(){
		//Get Current States of Von Kaiser and Little Mac
		AnimatorStateInfo LittleMacStateInfo=animator.GetCurrentAnimatorStateInfo(0);
		AnimatorStateInfo VonKaiserStateInfo = VonKaiserController.VonKaiserInfo;

		//If Von Kaiser is idle, body block
		if (VonKaiserStateInfo.IsName ("Von Kaiser Idle")) {
			VonKaiserAnimator.VonKaiserA.animator.SetTrigger ("Body Block");
			source.PlayOneShot(LittleMacBlock,1f);
			LifeScript.LifeController.removeLife(1);
		}

		if (VonKaiserStateInfo.IsName ("Von Kaiser Punch Retreat")) {

		}
	}

	public void HeadPunchClimax(){
		print ("HEAD PUNCH");
		//Get Current States of Von Kaiser and Little Mac
		AnimatorStateInfo LittleMacStateInfo=animator.GetCurrentAnimatorStateInfo(0);
		AnimatorStateInfo VonKaiserStateInfo = VonKaiserController.VonKaiserInfo;
		//Right Head Punch
		if (LittleMacStateInfo.IsName ("Little Mac Punch Right Face Climax")) {
			print ("PUNCH BY RIGHT");
			/*If Von Kaiser is idle, head block and make sure consecutive hits is 0
			if (VonKaiserStateInfo.IsName ("Von Kaiser Idle")) {
				print ("Idle");
				VonKaiserAnimator.VonKaiserA.consecutiveHits=0;
				VonKaiserAnimator.VonKaiserA.animator.SetTrigger ("Head Block");
				source.PlayOneShot(LittleMacBlock,1f);
				LifeScript.LifeController.removeLife(1);
			}

			if (VonKaiserStateInfo.IsName ("Von Kaiser Punch Retreat")) {
				print ("PUNCH RETREAT");
				VonKaiserAnimator.VonKaiserA.consecutiveHits=1;
				VonKaiserController.health -= 1;
				VonKaiserController.VonKaiserHealth.fillAmount -= .03215f;
				if(CheckForKnockDown("Right")){
					return;
				}
				VonKaiserAnimator.VonKaiserA.animator.SetTrigger("Head Hit Right");
			}

			if(VonKaiserStateInfo.IsName ("Von Kaiser Sucker Face")) {
				print ("SUCKER FACE");
				VonKaiserController.health-=1;
				VonKaiserController.VonKaiserHealth.fillAmount -= .03215f;
				if(CheckForKnockDown("Right")){
					return;
				}
				if(VonKaiserAnimator.VonKaiserA.consecutiveHits>5){
					animator.SetTrigger ("Large Head Hit Right");
					VonKaiserAnimator.VonKaiserA.consecutiveHits = 0;
				}
				else{
					VonKaiserAnimator.VonKaiserA.consecutiveHits++;
					VonKaiserAnimator.VonKaiserA.animator.SetTrigger("Head Hit Right");
				}
			}
		}
		//Left Head Punch
		else{
			//If Von Kaiser is idle, head block and make sure consecutive hits is 0
			if (VonKaiserStateInfo.IsName("Von Kaiser Idle")) {
				VonKaiserAnimator.VonKaiserA.consecutiveHits=0;
				VonKaiserAnimator.VonKaiserA.animator.SetTrigger ("Head Block");
				source.PlayOneShot(LittleMacBlock,1f);
				LifeScript.LifeController.removeLife(1);
			}
			
			if (VonKaiserStateInfo.IsName ("Von Kaiser Punch Retreat")) {
				VonKaiserAnimator.VonKaiserA.consecutiveHits=1;
				VonKaiserController.health -= 1;
				VonKaiserController.VonKaiserHealth.fillAmount -= .03215f;
				if(CheckForKnockDown("Left")){
					return;
				}
				VonKaiserAnimator.VonKaiserA.animator.SetTrigger("Head Hit Left");
			}
			
			if(VonKaiserStateInfo.IsName ("Von Kaiser Sucker Face")) {
				VonKaiserController.health -= 1;
				VonKaiserController.VonKaiserHealth.fillAmount -= .03215f;
				if(CheckForKnockDown("Left")){
					return;
				}
				if(VonKaiserAnimator.VonKaiserA.consecutiveHits>5){
					animator.SetTrigger ("Large Head Hit Left");
					VonKaiserAnimator.VonKaiserA.consecutiveHits = 0;
				}
				else{
					VonKaiserAnimator.VonKaiserA.consecutiveHits++;
					VonKaiserAnimator.VonKaiserA.animator.SetTrigger("Head Hit Left");
				}
			}
		}
	}*/

	bool CheckForKnockDown(string side){
		if (VonKaiserController.health <= 0) {
			VonKaiserController.VonKaiserC.knockdowns++;
			animator.SetTrigger("Knockdown "+side);
			
			/*Little Mac retreats while Mario counts down*/
			LittleMacController.LittleMac.animatorScript.animator.SetTrigger("Retreat");
			PointScript.PointController.addPoints(1010);
			VonKaiserAnimator.VonKaiserA.consecutiveHits=0;
			return true;
		}
		else{
			PointScript.PointController.addPoints(10);
			return false;
		}
	}

	public void PunchedByVonKaiser(){
		source.PlayOneShot(VonKaiserSuccessfulJab,1f);
	}

	public void resumeTimer() {
		print ("resuming time!");
		ClockScript.pauseTime = false;
	}

	public void pauseTimer() {
		ClockScript.pauseTime = true;
	}
	
}
