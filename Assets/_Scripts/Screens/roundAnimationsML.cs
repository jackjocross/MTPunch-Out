using UnityEngine;
using System.Collections;

public class roundAnimationsML : MonoBehaviour {

	public GameObject littleMac;
	public GameObject littleMacAlt;

	public bool animating = true;

	private int numUpdates = 0;

	void FixedUpdate() {
		if (animating) {
			++numUpdates;
			if ((numUpdates % 20) == 0) {
				if (littleMacAlt.activeSelf == false) {
					littleMacAlt.SetActive(true);
				}
				else {
					littleMacAlt.SetActive(false);
				}
			}
		}
	}
	
}
