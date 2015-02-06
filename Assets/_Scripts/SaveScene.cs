using UnityEngine;
using System.Collections;

public class SaveScene : MonoBehaviour {

	public static SaveScene saveData;

	public static int littleMacHealth;
	public static float marioHealth;
	public static float luigiHealth;
	public static float vonKaiserHealth;
	public static int roundNum;
	public static int hearts;
	public static int stars;
	public static int knockdowns;

	void Awake() {
		saveData = this;
		DontDestroyOnLoad (this);
		if (Application.loadedLevelName.Equals ("_Scene_Start")) {
			roundNum = 1;
			marioHealth = 32;
			luigiHealth = 32;
			vonKaiserHealth = 32;
			littleMacHealth = 100;
			hearts = 20;
			stars = 0;
			knockdowns = 0;
		}
	}

}
