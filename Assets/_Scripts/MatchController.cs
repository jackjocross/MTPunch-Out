using UnityEngine;
using System.Collections;

public class MatchController : MonoBehaviour {
	const int NUMBEROFKNOCKDOWNS=3;

	public static MatchController Match;
	public int roundNum;

	/*Audio Clips to be handle by match controller signaling end and beginning of round/match*/
	public AudioClip VonKaiserIntroduction;
	public AudioClip MarioLuigiIntroduction;
	public AudioClip FightMusic;
	public AudioClip Bell;
	public AudioClip VonKaiserFalldownMusic;
	public AudioClip LittleMacFalldownMusic;

	/*Scripts that we need access to*/
	public LittleMacAnimator LittleMac;
	public ClockScript clock;
	public GameObject mario;
	public LifeScript lifeScript;

	/*Bool to determine which music to play*/
	private bool introduction;
	private bool fightMusic;

	void Awake(){
		Match = this;
		roundNum = SaveScene.roundNum;
	}

	// Use this for initialization
	void Start () {;
		introduction = false;
		fightMusic = false;
		lifeScript.addLife(SaveScene.hearts);
		audio.clip = Bell;
		audio.Play ();
	}

	// Update is called once per frame
	void Update () {
		if (!audio.isPlaying) {
			/*Play Von Kaiser Introduction if it hasn't been played*/
			if(introduction==false){
				if (Application.loadedLevelName.Equals("_Scene_Josh")) {
					audio.clip=VonKaiserIntroduction;
				}
				else {
					audio.clip = MarioLuigiIntroduction;
				}
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
		MarioAnimator.MarioA.animator.SetTrigger("Fight");
		LittleMacAnimator.LittleMacA.animator.SetTrigger("Entrance");
	}

	public void MacFallsDown(){
		audio.Stop();
		audio.clip = LittleMacFalldownMusic;
		audio.Play();
	}

	public void MacGetsUp(){
		audio.Stop();
		audio.clip = FightMusic;
		audio.Play();
	}

	public void stopMusic() {
		audio.Stop();
	}

}
