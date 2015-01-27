using UnityEngine;
using System.Collections;

public class MarioController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<MarioAnimator> ().intro ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
