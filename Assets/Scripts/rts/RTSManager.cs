using System.Collections;
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

	public Base playerBase;
	public Base enemyBase;

	// Use this for initialization
	void Start() {
		Vector3 playerPosition = new Vector3(32f, 0, 0);
		GameObject go = Instantiate(basePrefab, playerPosition, transform.rotation);
		playerBase = go.GetComponent<Base>();
		playerBase.setFaction(Faction.FACTION_ALLIES);

		Vector3 enemyPosition = new Vector3(32, 50, 0);
		go = Instantiate(basePrefab, enemyPosition, transform.rotation);
		enemyBase = go.GetComponent<Base>();
		enemyBase.setFaction(Faction.FACTION_ENEMIES);

		playerBase.setTarget(enemyPosition);
		enemyBase.setTarget(playerPosition);
	}

	// Update is called once per frame
	void Update() {

	}

	private TetrisGame tetrisGame = null;

	private void fetchTetrisGame() {
		if (tetrisGame == null) {
			GameObject go = GameObject.Find("TetrisGame");

			if (go != null) {
				tetrisGame = go.GetComponent<TetrisGame>();
			}
		}
	}
	public void onBaseDamaged(int damage) {
		fetchTetrisGame();

		if (tetrisGame != null) {
			tetrisGame.onBaseDamaged(damage);
		}
	}

	public void onRTSWin() {
		fetchTetrisGame();
	}

	public void onRTSLose() {
		fetchTetrisGame();
	}

	public void onTetrisLine(int count) {
		playerBase.replenishEnergy(count);
	}
}
