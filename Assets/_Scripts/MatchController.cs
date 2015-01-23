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
		if (!audio.isPlaying) {
			if(firstTime==false){ 
				audio.clip=FightMusic;
				LittleMac.Walk();
				audio.Play();
				firstTime=true;
			}
		}
	}

}
