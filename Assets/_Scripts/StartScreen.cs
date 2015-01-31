using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {
	
	public GameObject boxingGloves;

	private int currentSelection = 0;

	// Update is called once per frame
	void Update () {
		if (currentSelection == 0) {
			if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
				currentSelection = 1;
				boxingGloves.transform.position = new Vector3(0.0f, -1.63f, 0.0f);
			}
			else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Comma) || Input.GetKeyDown(KeyCode.Period)) {
				Application.LoadLevel("_Scene_Round_One_Original");
			}
		}
		else {
			if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
				currentSelection = 0;
				boxingGloves.transform.position = new Vector3(0.0f, -0.63f, 0.0f);
			}
			else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Comma) || Input.GetKeyDown(KeyCode.Period)) {
				Application.LoadLevel("_Scene_Mario_Luigi");
			}
		}

	}
}
