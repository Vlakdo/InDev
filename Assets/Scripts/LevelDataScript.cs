using UnityEngine;
using System.Collections;

public class LevelDataScript : MonoBehaviour {

	public int currentLevel = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NextLevel(){
	
		Application.LoadLevel ("Level" + (currentLevel+1));
		//Application.LoadLevel ("InitScene");
	}
}
