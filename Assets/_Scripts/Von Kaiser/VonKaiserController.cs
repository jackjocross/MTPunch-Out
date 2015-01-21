using UnityEngine;
using System.Collections;

public class VonKaiserController : MonoBehaviour {

	private int numUpdates = 0;

	// Use this for initialization
	void Start () {
		this.GetComponent<VonKaiserAnimator> ().intro();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		++numUpdates;
		if (numUpdates % 300 == 0) {
			this.GetComponent<VonKaiserAnimator>().punch();
//			float nextPos = this.transform.position.y - 100;
//			this.transform.position = new Vector3(this.transform.position.x, nextPos, this.transform.position.z);
		}
	}
}
