using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	public GameObject defeatScreen;
	public GameObject winScreen;

	void Start () {
		defeatScreen.SetActive(false);
		winScreen.SetActive(false);
	}

	public void onGameDefeat() {
		defeatScreen.SetActive(true);
	}

	public void onGameWin() {
		winScreen.SetActive(true);
	}
}
