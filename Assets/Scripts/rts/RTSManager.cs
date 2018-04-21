using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSManager : MonoBehaviour {
	private Unit[] enemyUnits;
	private Unit[] playerUnits;

	private Base playerBase;

	// Use this for initialization
	void Start() {
		playerBase = GetComponent<Base>();
	}

	// Update is called once per frame
	void Update() {

	}
}
