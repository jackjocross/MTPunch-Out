using UnityEngine;
using System.Collections;

public class MatchController : MonoBehaviour {
	public AudioClip VonKaiserIntroduction;
	public AudioClip FightMusic;

	public LittleMacAnimator LittleMac;

	bool firstTime;
	// Use this for initialization
	void Start () {;
		firstTime = false;
		audio.clip = VonKaiserIntroduction;
		audio.Play();
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
