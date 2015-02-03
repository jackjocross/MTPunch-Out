using UnityEngine;
using System.Collections;

public class roundOneML : MonoBehaviour {

	public roundAnimationsML animations;

	private bool scrollDown = false;
	private int numUpdates = 0;

	void Update() {
		if (Input.GetKeyDown (KeyCode.Return)) {
			scrollDown = true;
			animations.animating = false;
			animations.littleMac.SetActive(false);
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
				Application.LoadLevel("_Scene_Mario_Luigi");
			}
		}
	}
	
}
