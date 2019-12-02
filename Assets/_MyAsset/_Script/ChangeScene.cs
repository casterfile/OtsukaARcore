using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour {
	private AudioSource ClickMusic;
	void Start(){
		ClickMusic = GameObject.Find("ClickMusic").GetComponent<AudioSource>();
	}
	// Update is called once per frame
	public void UpdateChangeScene (string NameScene) {
		ClickMusic.Play(0);
		if (Tutorial.currentStage == 4) {
			Tutorial.currentStage = 1;
			Application.LoadLevel(NameScene);
		} else {
			Application.LoadLevel(NameScene);
		}

	}

	public void ExitGame(){
		Application.Quit();
	}

	public void PlayGain(){
		Tutorial.currentStage = 1;
		Application.LoadLevel("Scene_01_Tutorial");
	}
}
