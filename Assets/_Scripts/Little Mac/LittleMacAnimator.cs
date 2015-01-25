using UnityEngine;
using System.Collections;

public class LittleMacAnimator : MonoBehaviour {

	public static LittleMacAnimator LittleMacA;

	public Animator animator;

	private VonKaiserController VonKaiser;
	private AudioSource source;

	/*Audio Clips triggered by Little Mac*/
	public AudioClip SuccessfulHeadPunch;
	public AudioClip SuccessfulAbPunch;

	void Awake(){
		animator=GetComponent<Animator>();
		source=GetComponent<AudioSource>();
		LittleMacA = this;
	}

	void Start () {
		VonKaiser = VonKaiserController.VonKaiserC;
	}
	
	// Update is called once per frame
	void Update () {
		/*Get Current States of Von Kaiser and Little Mac*/
		AnimatorStateInfo LittleMacStateInfo=animator.GetCurrentAnimatorStateInfo(0);
		AnimatorStateInfo VonKaiserStateInfo = VonKaiserController.VonKaiserInfo;

		if (LittleMacStateInfo.IsName ("Little Mac Idle")) {
			//If Von Kaiser at at climax of left punch, Little Mac gets hit
			if(VonKaiserStateInfo.IsName ("Von Kaiser Punch")){
				animator.SetTrigger("Punched By Left");
			}
		}
		if (LittleMacStateInfo.IsName ("Little Mac Punch Right Face Climax") || LittleMacStateInfo.IsName ("Little Mac Punch Left Face Climax")) {
			source.clip=SuccessfulHeadPunch;
			if(!source.isPlaying){
				source.Play();
			}
			else{
				source.Stop();
				source.Play();
			}
		}
		if (LittleMacStateInfo.IsName ("Little Mac Punch Left Normal Climax") || LittleMacStateInfo.IsName ("Little Mac Punch Right Normal Climax")) {
			source.clip=SuccessfulAbPunch;
			if(!source.isPlaying){
				source.Play ();
			}
			else{
				source.Stop();
				source.Play ();
			}
		}
		if (LittleMacStateInfo.IsName ("Little Mac Shield")) {
			/*If Little Mac has his shield and Von Kaiser punches, block it, and return to idle*/
			if(VonKaiserStateInfo.IsName("Von Kaiser Punch")){
				animator.SetBool("Shield",false);
			}
		}
	}

	public void DodgeRight(){
		animator.SetTrigger("Dodge Right");
	}

	public void DodgeLeft(){
		animator.SetTrigger("Dodge Left");
	}

	public void ShieldBegin(){
		animator.SetBool ("Shield",true);
	}

	public void ShieldEnd(){
		animator.SetBool("Shield",false);
	}

	public void PunchRightFace(){
		animator.SetTrigger("Punch Right Face");
	}

	public void PunchLeftFace(){
		animator.SetTrigger("Punch Left Face");
	}

	public void PunchRightNormal(){
		animator.SetTrigger("Punch Right Normal");
	}

	public void PunchLeftNormal(){
		animator.SetTrigger("Punch Left Normal");
	}

	public void Duck(){
		animator.SetTrigger("Duck");
	}

	public void StarPunch(){
		animator.SetTrigger("Star Punch");
	}
	
	public void Victory(){
		animator.SetTrigger("Victory");
	}

	public void Walk(){
		animator.SetTrigger("Walk");
	}
}
