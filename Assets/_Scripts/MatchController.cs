using UnityEngine;
using System.Collections;

public class MatchController : MonoBehaviour {
	const int LIVES=20;
	const int NUMBEROFKNOCKDOWNS=3;

	public static MatchController Match;

	/*Audio Clips to be handle by match controller signaling end and beginning of round/match*/
	public AudioClip VonKaiserIntroduction;
	public AudioClip FightMusic;
	public AudioClip Bell;
	public AudioClip VonKaiserFalldownMusic;
	public AudioClip MatchEnd;

	/*Scripts that we need access to*/
	public VonKaiserAnimator VonKaiser;
	public LittleMacAnimator LittleMac;
	public ClockScript clock;
	public GameObject mario;
	public LifeScript lifeScript;

	public int roundNum;

	private int startTime = 0;
	/*Bool to determine which music to play*/
	private bool introduction;
	private bool fightMusic;

	void Awake(){
		Match = this;
		roundNum = 1;
	}

	// Use this for initialization
	void Start () {;
		introduction = false;
		fightMusic = false;
		lifeScript.addLife(LIVES);
		audio.clip = Bell;
		audio.Play ();
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
			/*Play Von Kaiser Introduction if it hasn't been played*/
			if(introduction==false){
				audio.clip=VonKaiserIntroduction;
				audio.Play ();
				introduction=true;
				return;
			}
			/*Play the fight music if it has not been played*/
			if(fightMusic==false){ 
				audio.clip=FightMusic;
				audio.Play();
				fightMusic=true;
				return;
			}
		}
	}

	public void VonKaiserKnockDown(){
		audio.Stop ();
		audio.clip = VonKaiserFalldownMusic;
		audio.Play ();
	}

	public void VonKaiserGetsUp(){
		audio.Stop ();
		audio.clip = FightMusic;
		audio.Play ();
	}

	public void EndOfMatch(){
		audio.PlayOneShot(MatchEnd,1f);
	}


}
