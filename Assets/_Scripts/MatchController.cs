using UnityEngine;
using System.Collections;

public class MatchController : MonoBehaviour {
	public AudioClip VonKaiserIntroduction;
	public AudioClip FightMusic;
	public AudioClip Bell;

	public VonKaiserAnimator VonKaiser;
	public LittleMacAnimator LittleMac;
	public ClockScript clock;
	public GameObject mario;

	private int startTime = 0;
//	private bool canCallDelay = true;
	private bool introduction;
	private bool fightMusic;

	// Use this for initialization
	void Start () {;
		introduction = false;
		fightMusic = false;
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
				LittleMac.Walk();
				audio.Play();
				fightMusic=true;
				return;
			}
		}
	}

}
