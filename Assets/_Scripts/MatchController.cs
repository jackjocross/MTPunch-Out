using UnityEngine;
using System.Collections;

public class MatchController : MonoBehaviour {
	public AudioClip VonKaiserIntroduction;
	public AudioClip FightMusic;

	public VonKaiserAnimator VonKaiser;
	public LittleMacAnimator LittleMac;
	public ClockScript clock;
	public GameObject mario;

	private int startTime = 0;
//	private bool canCallDelay = true;


	bool firstTime;
	// Use this for initialization
	void Start () {;
		firstTime = false;
		audio.clip = VonKaiserIntroduction;
		audio.Play();
	}

	void FixedUpdate() {
		++startTime;

		if (startTime == 500) {
			clock.startTimer();
		}

	}

	// Update is called once per frame
	void Update () {

//		if (LittleMac.animator.GetCurrentAnimatorStateInfo(0).IsName("Little Mac Punch Left Normal")) {
////			print ("called delayAnimation");
//
//			VonKaiser.bodyBlock ();
//
////			if (canCallDelay) {
////				delayAnimation (5.16f);	
////				VonKaiser.punch ();
////			}
//		}

		if (!audio.isPlaying) {
			if(firstTime==false){ 
				audio.clip=FightMusic;
				LittleMac.Walk();
				audio.Play();
				firstTime=true;
			}
		}
	}

//
//	IEnumerator delayAnimation (float seconds) {
//		canCallDelay = false;
//		print ("got to yield");
//		yield return new WaitForSeconds(seconds);
//		print ("got to punch");
//		canCallDelay = true;
//	}

}
