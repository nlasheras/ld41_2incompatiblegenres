using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour {

	void Start () {
		SceneManager.LoadScene("rts", LoadSceneMode.Additive);
		SceneManager.LoadScene("puzzle", LoadSceneMode.Additive);
		SceneManager.LoadScene("ui", LoadSceneMode.Additive);
	}
	
}
