using UnityEngine;
using System.Collections;

public class MatchController : MonoBehaviour {
	const int LIVES=20;

	public static MatchController Match;

	/*Audio Clips to be handle by match controller signaling end and beginning of round/match*/
	public AudioClip VonKaiserIntroduction;
	public AudioClip FightMusic;
	public AudioClip Bell;

	/*Scripts that we need access to*/
	public VonKaiserAnimator VonKaiser;
	public LittleMacAnimator LittleMac;
	public ClockScript clock;
	public GameObject mario;
	public LifeScript lifeScript;

	private int startTime = 0;
//	private bool canCallDelay = true;
	/*Bool to determine which music to play*/
	private bool introduction;
	private bool fightMusic;

	void Awake(){
		Match = this;
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

}
