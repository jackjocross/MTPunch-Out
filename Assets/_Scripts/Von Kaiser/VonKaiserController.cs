using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VonKaiserController : MonoBehaviour {
	
	public static VonKaiserController VonKaiserC;

	public static VonKaiserAnimator VonKaiser;
	public static LittleMacAnimator LittleMac;

	public static AnimatorStateInfo LittleMacInfo;
	public static AnimatorStateInfo VonKaiserInfo;

	public static Image VonKaiserHealth;
	
	public static int health;
	public static int numHeadHits;

	private int numUpdates = 0;

	void Awake() {
		VonKaiserC = this;
		health = 100;
	}

	// Use this for initialization
	void Start () {
		VonKaiser = VonKaiserAnimator.VonKaiserA;
		LittleMac = LittleMacAnimator.LittleMacA;
		VonKaiserHealth = GameObject.Find ("Von Kaiser Health").GetComponent<Image>();
		VonKaiser.intro ();
	}

	//Punches for testing
	void FixedUpdate () {
//		++numUpdates;
//		if (numUpdates % 300 == 0) {
//			VonKaiser.punch();
//		}
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
	}
	
}
