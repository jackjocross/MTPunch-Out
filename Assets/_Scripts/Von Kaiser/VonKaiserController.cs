using UnityEngine;
using System.Collections;

public class VonKaiserController : MonoBehaviour {

	public static VonKaiserController VonKaiserC;

	public static VonKaiserAnimator VonKaiser;
	public static LittleMacAnimator LittleMac;

	public static AnimatorStateInfo LittleMacInfo;
	public static AnimatorStateInfo VonKaiserInfo;

	private int numUpdates = 0;

	void Awake() {
		VonKaiserC = this;
	}

	// Use this for initialization
	void Start () {
		this.GetComponent<VonKaiserAnimator> ().intro();
		VonKaiser = VonKaiserAnimator.VonKaiserA;
		LittleMac = LittleMacAnimator.LittleMacA;
	}

	//Punches for testing
	void FixedUpdate () {
		++numUpdates;
		if (numUpdates % 100 == 0) {
			VonKaiser.punch();
		}
	}

	// Update is called once per frame
	void Update() {

		LittleMacInfo = LittleMac.animator.GetCurrentAnimatorStateInfo (0);
		VonKaiserInfo = VonKaiser.animator.GetCurrentAnimatorStateInfo (0);

		// possibly put these in Little Mac Controller so that they only happen once the key is pressed? -- THIS MAY HAVE BEEN RIGHT... DO STATIC STUFF AND DONT FORGET TO RE ADD THE PUBLICS IN THE INSPECTOR
		if ((LittleMacInfo.IsName("Little Mac Punch Left Normal") || LittleMacInfo.IsName("Little Mac Punch Right Normal")) && VonKaiserInfo.IsName("Von Kaiser Idle")) {
			VonKaiser.bodyBlock ();
		} 
		else if ((LittleMacInfo.IsName("Little Mac Punch Left Face") || LittleMacInfo.IsName("Little Mac Punch Right Face")) && VonKaiserInfo.IsName("Von Kaiser Idle")) {
			VonKaiser.headBlock();
		}
		else if ((LittleMacInfo.IsName("Little Mac Punch Left Normal") || LittleMacInfo.IsName("Little Mac Punch Right Normal")) && VonKaiserInfo.IsName("Von Kaiser Punch")) {
			VonKaiser.bodyBlock ();
		}
//		else if ((LittleMacInfo.IsName("Little Mac Punch Left Face") || LittleMacInfo.IsName("Little Mac Punch Right Face")) && VonKaiserInfo.IsName("Von Kaiser Punch")) {
//			VonKaiser.headHit ();
//		}
//		else if ((LittleMacInfo.IsName("Little Mac Punch Left Face") || LittleMacInfo.IsName("Little Mac Punch Right Face")) && VonKaiserInfo.IsName("Von Kaiser Sucker Face")) {
//			VonKaiser.headHit ();
//		}
	}
	
}
