using UnityEngine;
using System.Collections;

public class MarioAnimator : MonoBehaviour {

	public Animator animator;
	public AudioClip MarioFightSound;
	public AudioClip MarioCountSound;

	private AudioSource source;

	public void startLittleMac() {
		print ("startLittleMac called!");
		LittleMacAnimator.LittleMacA.Walk ();
	}

	public void Awake() {
		source=this.GetComponent<AudioSource>();
		source.panLevel=0;
		print (source);
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
		if (LittleMacAnimator.LittleMacA.animator.GetCurrentAnimatorStateInfo (0).IsName ("Little Mac Retreat")) {
			LittleMacAnimator.LittleMacA.animator.SetTrigger("Entrance");
		}
	}

	/*Animation Event that triggers sound to play when Mario is counting*/
	public void MarioCountDown(int number){
		print(number);
		print (VonKaiserController.VonKaiserC.knockdowns);
		if (number == 0) {
			if(VonKaiserController.VonKaiserC.knockdowns==3){
				print ("loading mario luigi scene");
				Application.LoadLevel("_Scene_Mario_Luigi");
				animator.SetTrigger ("TKO");
				LittleMacAnimator.LittleMacA.animator.SetTrigger("Victory");
			}
		}
		if(number==1){
			if(VonKaiserController.VonKaiserC.knockdowns==1){
				animator.SetTrigger("Fight");
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
				animator.SetTrigger("Fight");
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
	}
}
