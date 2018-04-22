using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	public GameObject defeatScreen;

	void Start () {
		defeatScreen.SetActive(false);
	}

	public void onTetrisDefeat() {
		defeatScreen.SetActive(true);
	}
}
