using UnityEngine;
using System.Collections;

public class MarioAnimator : MonoBehaviour {

	public static MarioAnimator MarioA;

	public Animator animator;
	public AudioClip MarioFightSound;
	public AudioClip MarioCountSound;
	public AudioClip MarioTKOSound;

	private AudioSource source;

	public void startLittleMac() {
		LittleMacAnimator.LittleMacA.Walk ();
	}

	public void Awake() {
		source=this.GetComponent<AudioSource>();
		source.panLevel=0;
		MarioA = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void intro() {
		animator.SetTrigger ("Intro");
	}
	
	public void enter() {
		animator.SetTrigger ("Enter");
	}
	
	/*Animation Event that triggers Mario's sound to occur every time he says fight*/
	public void MarioFight(){
		source.PlayOneShot(MarioFightSound,1f);
		/*Fight precedes Little Mac getting up after Von Kaiser is knocked down*/
		if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Retreat")) {
			LittleMacAnimator.LittleMacA.animator.SetTrigger("Entrance");
		}
		/*Fight precedes Little Mac getting up after he is knocked down*/
		if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Gets Up Stage 3")) {
			LittleMacAnimator.LittleMacA.animator.SetTrigger("Get Up Walk");
			VonKaiserAnimator.VonKaiserA.animator.SetTrigger("Return To Fight");
			VonKaiserAnimator.VonKaiserA.animator.SetBool("Punch",true);
			if(LittleMacController.LittleMac.knockdowns==1){
				LittleMacController.LittleMac.health=100/3;
				LittleMacController.LittleMac.LittleMacHealth.fillAmount=.333f;
			}
			if(LittleMacController.LittleMac.knockdowns==2){
				LittleMacController.LittleMac.health=40;
				LittleMacController.LittleMac.LittleMacHealth.fillAmount=.4f;
			}
		}
	}

	public void MarioTKO(){
		source.PlayOneShot(MarioTKOSound,1f);
	}

	/*Animation Event that triggers sound to play when Mario is counting*/
	public void MarioCountDown(int number){

		if (number == 0) {
			/*Von Kaiser gets TKOed*/
			if(VonKaiserController.VonKaiserC.knockdowns==3){
				animator.SetTrigger ("TKO");
				LittleMacAnimator.LittleMacA.animator.SetTrigger("Victory");
			}
			/*Little Mac gets TKOed*/
			if(LittleMacController.LittleMac.knockdowns==3){
				animator.SetTrigger("TKO");
			}
		}
		if(number==1){
			if(VonKaiserController.VonKaiserC.knockdowns==1){
				animator.SetTrigger("Intro");
				VonKaiserController.VonKaiserHealth.fillAmount=.5f;
				VonKaiserController.health=16f;
				if (VonKaiserAnimator.VonKaiserA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Von Kaiser Knockdown Right")) {
					VonKaiserAnimator.VonKaiserA.animator.SetTrigger("Get Up Right");
				}
				if (VonKaiserAnimator.VonKaiserA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Von Kaiser Knockdown Left")) {
					VonKaiserAnimator.VonKaiserA.animator.SetTrigger("Get Up Left");
				}
			}
		}

		if(number==3){
			if(VonKaiserController.VonKaiserC.knockdowns==2){
				animator.SetTrigger("Intro");
				VonKaiserController.VonKaiserHealth.fillAmount=.5f;
				VonKaiserController.health=16f;
				if (VonKaiserAnimator.VonKaiserA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Von Kaiser Knockdown Right")) {
					VonKaiserAnimator.VonKaiserA.animator.SetTrigger("Get Up Right");
				}
				if (VonKaiserAnimator.VonKaiserA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Von Kaiser Knockdown Left")) {
					VonKaiserAnimator.VonKaiserA.animator.SetTrigger("Get Up Left");
				}
			}
		}
		source.PlayOneShot(MarioCountSound,1f);

		/*If Mario reaches ten, KO*/
		if (number == 10) {
			animator.SetTrigger("KO");
			/*Determine who lost by looking at health*/
			if(VonKaiserController.health<=0){

			}
			if(LittleMacController.LittleMac.health<=0){
				VonKaiserAnimator.VonKaiserA.animator.SetTrigger("Victory");
			}
		}
	}
}
