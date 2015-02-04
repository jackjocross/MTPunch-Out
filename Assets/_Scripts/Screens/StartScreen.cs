using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {
	
	public GameObject boxingGloves;

	public AudioClip Intro;
	private AudioSource source;
	private int currentSelection = 0;
	
	void Awake(){
		source=this.GetComponent<AudioSource>();
		source.panLevel=0;
	}

	// Update is called once per frame
	void Update () {
		if (currentSelection == 0) {
			if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
				currentSelection = 1;
				boxingGloves.transform.position = new Vector3(0.0f, -1.63f, 0.0f);
			}
			else if (Input.GetKeyDown(KeyCode.Return)) {
				Application.LoadLevel("_Scene_Round_One_Original");
			}
		}
		else {
			if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
				currentSelection = 0;
				boxingGloves.transform.position = new Vector3(0.0f, -0.63f, 0.0f);
			}
			else if (Input.GetKeyDown(KeyCode.Return)) {
				Application.LoadLevel("_Scene_Round_One_ML");
			}
		}

	}
}
