using UnityEngine;
using System.Collections;

public class roundAnimations : MonoBehaviour {

	public GameObject vonKaiser;
	public GameObject littleMac;
	public GameObject vonKaiserAlt;
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
			else if ((numUpdates % 25) == 0) {
				if (vonKaiserAlt.activeSelf == false) {
					vonKaiserAlt.SetActive(true);
				}
				else {
					vonKaiserAlt.SetActive(false);
				}
			}
		}
	}
	
}
