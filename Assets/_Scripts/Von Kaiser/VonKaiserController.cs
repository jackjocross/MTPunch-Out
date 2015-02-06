using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VonKaiserController : MonoBehaviour {
	
	public static VonKaiserController VonKaiserC;

	public static AnimatorStateInfo LittleMacInfo;
	public static AnimatorStateInfo VonKaiserInfo;

	public static Image VonKaiserHealth;
	
	public static float health;
	public int knockdowns;


	void Awake() {
		VonKaiserC = this;
		knockdowns=0;
	}

	// Use this for initialization
	void Start () {
		health = SaveScene.vonKaiserHealth;
		VonKaiserHealth = GameObject.Find ("Von Kaiser Health").GetComponent<Image>();
		VonKaiserHealth.fillAmount = health * 0.03125f;
		VonKaiserAnimator.VonKaiserA.intro ();
	}

	// Update is called once per frame
	void Update() {

		LittleMacInfo = LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0);
		VonKaiserInfo = VonKaiserAnimator.VonKaiserA.animator.GetCurrentAnimatorStateInfo (0);


		// logic for when von kaiser can be hit / block
		if (VonKaiserInfo.IsName("Von Kaiser Idle") || VonKaiserInfo.IsName("Von Kaiser Idle 0") || VonKaiserInfo.IsName("Von Kaiser Idle 0 0")) {
			VonKaiserAnimator.curPunchState = (int)VonKaiserAnimator.punchStates.Notset;
			VonKaiserAnimator.VonKaiserA.consecutiveHits = 0;
		}
		if ((LittleMacInfo.IsName("Little Mac Punch Left Normal Climax") || LittleMacInfo.IsName("Little Mac Punch Right Normal Climax")) && VonKaiserInfo.IsName("Von Kaiser Idle")) {
			VonKaiserAnimator.VonKaiserA.bodyBlock ();
		} 
		else if ((LittleMacInfo.IsName("Little Mac Punch Left Face Climax") || LittleMacInfo.IsName("Little Mac Punch Right Face Climax")) && VonKaiserInfo.IsName("Von Kaiser Idle")) {
			VonKaiserAnimator.VonKaiserA.headBlock();
		}
		else if ((LittleMacInfo.IsName("Little Mac Punch Left Normal Climax") || LittleMacInfo.IsName("Little Mac Punch Right Normal Climax")) && VonKaiserInfo.IsName("Von Kaiser Punch")) {
//			VonKaiserAnimator.VonKaiserA.bodyBlock ();
		}
		else if (LittleMacInfo.IsName("Little Mac Punch Left Face Climax") && VonKaiserInfo.IsName("Von Kaiser Punch Retreat")) {
			VonKaiserAnimator.VonKaiserA.leftHeadHit();
		}
		else if (LittleMacInfo.IsName("Little Mac Punch Right Face Climax") && VonKaiserInfo.IsName("Von Kaiser Punch Retreat")) {
			VonKaiserAnimator.VonKaiserA.rightHeadHit();
		}
		else if (LittleMacInfo.IsName("Little Mac Punch Left Face Climax") && VonKaiserInfo.IsName("Von Kaiser Sucker Face")) {
			VonKaiserAnimator.VonKaiserA.leftHeadHit();
		}
		else if (LittleMacInfo.IsName("Little Mac Punch Right Face Climax") && VonKaiserInfo.IsName("Von Kaiser Sucker Face")) {
			VonKaiserAnimator.VonKaiserA.rightHeadHit();
		}
		else if (LittleMacInfo.IsName("Little Mac Punch Left Normal Climax") && VonKaiserInfo.IsName("Von Kaiser Sucker Face")) {
			VonKaiserAnimator.VonKaiserA.leftBodyHit();
		}
		else if (LittleMacInfo.IsName("Little Mac Punch Right Normal Climax") && VonKaiserInfo.IsName("Von Kaiser Sucker Face")) {
			VonKaiserAnimator.VonKaiserA.rightBodyHit();
		}
		else if (LittleMacInfo.IsName("Little Mac Punch Left Face Climax") && VonKaiserInfo.IsName("Von Kaiser Upper Hang")) {
			VonKaiserAnimator.VonKaiserA.leftHeadHit();
		}
		else if (LittleMacInfo.IsName("Little Mac Punch Right Face Climax") && VonKaiserInfo.IsName("Von Kaiser Upper Hang")) {
			VonKaiserAnimator.VonKaiserA.rightHeadHit();
		}
		else if (LittleMacInfo.IsName("Little Mac Punch Left Face Climax") && VonKaiserInfo.IsName("Von Kaiser Upper Hang 0")) {
			VonKaiserAnimator.VonKaiserA.leftHeadHit();
		}
		else if (LittleMacInfo.IsName("Little Mac Punch Right Face Climax") && VonKaiserInfo.IsName("Von Kaiser Upper Hang 0")) {
			VonKaiserAnimator.VonKaiserA.rightHeadHit();
		}
		else if (LittleMacInfo.IsName("Little Mac Punch Left Normal Climax") && VonKaiserInfo.IsName("Von Kaiser Upper Hang")) {
			print ("body punch successful");
			VonKaiserAnimator.VonKaiserA.leftBodyHit();
		}
		else if (LittleMacInfo.IsName("Little Mac Punch Right Normal Climax") && VonKaiserInfo.IsName("Von Kaiser Upper Hang")) {
			print ("body punch successful");
			VonKaiserAnimator.VonKaiserA.rightBodyHit();
		}
		else if (LittleMacInfo.IsName("Little Mac Punch Left Normal Climax") && VonKaiserInfo.IsName("Von Kaiser Upper Hang 0")) {
			print ("body punch successful");
			VonKaiserAnimator.VonKaiserA.leftBodyHit();
		}
		else if (LittleMacInfo.IsName("Little Mac Punch Right Normal Climax") && VonKaiserInfo.IsName("Von Kaiser Upper Hang 0")) {
			print ("body punch successful");
			VonKaiserAnimator.VonKaiserA.rightBodyHit();
		}
	}

	public void VonKaiserGetsUp(){
		MatchController.Match.VonKaiserGetsUp();
	}

	/*This function handles Von Kaiser at the peak of his jab to determine if a hit occurs*/
	public void VonKaiserJabClimax(){
		//print ("called jab climax");
		LittleMacAnimator.LittleMacA.handleVonKaiserJab();	
	}

	/*This function handles Von Kaiser at the peak of his uppercut to determien if a hit occurs*/
	public void VonKaiserUppercutClimax(){
		LittleMacAnimator.LittleMacA.handleVonKaiserUppercut();
	}
	
}
