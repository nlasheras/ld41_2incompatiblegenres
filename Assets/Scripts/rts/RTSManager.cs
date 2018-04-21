﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Faction {
	FACTION_ALLIES = 0,
	FACTION_ENEMIES
}

public class RTSManager : MonoBehaviour {

	public GameObject basePrefab;

	private Unit[] enemyUnits;
	private Unit[] playerUnits;

	private Base playerBase;
	private Base enemyBase;

	// Use this for initialization
	void Start() {
		Vector3 playerPosition = new Vector3(12.6f, 0, 0);
		GameObject go = Instantiate(basePrefab, playerPosition, transform.rotation);
		playerBase = go.GetComponent<Base>();
		playerBase.setFaction(Faction.FACTION_ALLIES);

		Vector3 enemyPosition = new Vector3(50, 50, 0);
		go = Instantiate(basePrefab, enemyPosition, transform.rotation);
		enemyBase = go.GetComponent<Base>();
		enemyBase.setFaction(Faction.FACTION_ENEMIES);

		playerBase.setTarget(enemyPosition);
		enemyBase.setTarget(playerPosition);
	}

	// Update is called once per frame
	void Update() {

	}
}