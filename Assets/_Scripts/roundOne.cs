using UnityEngine;
using System.Collections;

public class roundOne : MonoBehaviour {

	public roundAnimations animations;

	private bool scrollDown = false;
	private int numUpdates = 0;

	void Update() {
		if (Input.GetKeyDown (KeyCode.Z) || Input.GetKeyDown (KeyCode.X) || Input.GetKeyDown (KeyCode.Comma) || Input.GetKeyDown (KeyCode.Period)) {
			scrollDown = true;
			animations.animating = false;
			animations.vonKaiser.SetActive(false);
			animations.littleMac.SetActive(false);
			animations.vonKaiserAlt.SetActive(false);
			animations.littleMacAlt.SetActive(false);
		}
	}

	void FixedUpdate() {
		if (scrollDown == true) {
			++numUpdates;
			if (numUpdates < 80) {
				Vector3 addPos = new Vector3(transform.position.x, 0.15f, transform.position.z);
				transform.position += addPos; 
			}
		
			else if (numUpdates == 200) {
				Application.LoadLevel("_Scene_Jack");
			}
		}
	}
	
}
