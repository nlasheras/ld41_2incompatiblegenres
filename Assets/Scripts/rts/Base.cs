﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {
	public GameObject unitPrefab;

	private float spawnRate; // seconds per unit

	public Sprite enemyBase;

	public Sprite alliedBase;

	public int energy;

	public int life;

	private float tick;

	private Faction faction;

	private RTSManager rtsManager;

	private Vector3 target;

	private bool isPaused;

	// Use this for initialization
	void Start() {
		tick = spawnRate;
		GetComponentInChildren<SpriteRenderer>().sprite = getFactionSprite(faction);
		initialize(faction);
	}

	// Update is called once per frame
	void Update() {
		if (isPaused) {
			return;
		}

		tick -= Time.deltaTime;

		if (tick < 0 && spawnRate != 0) {
			tick = spawnRate;
			spawnUnit();
		}
	}

	public void pause() {
		isPaused = true;

		foreach (GameObject unitObject in getUnits()) {
			unitObject.GetComponent<Unit>().pause();
		}
	}

	public void resume() {
		isPaused = false;

		foreach (GameObject unitObject in getUnits()) {
			unitObject.GetComponent<Unit>().resume();
		}
	}

	private static int instanceId = 0;

	private void spawnUnit() {
		if (energy < Unit.getUnitCost(faction)) {
			return;
		}

		Vector3 temp =  new Vector3(transform.position.x, transform.position.y + getOffset(faction), transform.position.z);
		GameObject go = Instantiate(unitPrefab, temp, transform.rotation);
		go.name = String.Format("Unit_{0}", instanceId);
		Unit unit = go.GetComponent<Unit>();
		unit.setFaction(faction);
		unit.setTarget(target);

		++instanceId;

		spendEnergy(Unit.getUnitCost(faction));
	}

	private void initialize(Faction faction) {
		switch (faction) {
			case Faction.FACTION_ALLIES:
			energy = 50;
			life = 100;
			spawnRate = 5.0f;
			break;

			case Faction.FACTION_ENEMIES:
			life = 200;
			spawnRate = 4.5f;
			break;
		}
	}

	private static float getOffset(Faction faction) {
		switch (faction) {
			case Faction.FACTION_ENEMIES:
				return -1.0f;

			case Faction.FACTION_ALLIES:
				return 1.0f;
		}

		return 0.0f;
	}

	public void receiveDamage(int damage) {
		life -= damage;

		notifyDamage(damage);

		// TODO: Trigger damage receive info to other side
		if (life <= 0) {
			// For now just destroy object. Will trigger event game over to other side
			notifyBaseDestroyed();
			GameObject.Destroy(this.gameObject);
			life = 0;
		}
	}

	private void notifyDamage(int damage) {
		if (faction == Faction.FACTION_ENEMIES) {
			return;
		}

		fetchRTSManager();

		if (rtsManager != null) {
			rtsManager.onBaseDamaged(damage);
		}
	}

	private void notifyBaseDestroyed() {
		fetchRTSManager();

		if (rtsManager != null) {
			switch (faction) {
				case Faction.FACTION_ALLIES:
					rtsManager.onRTSLose();
					break;

				case Faction.FACTION_ENEMIES:
					rtsManager.onRTSWin();
					break;
			}
		}

		rtsManager.onTetrisDefeat();
	}

	private void fetchRTSManager() {
		if (rtsManager == null) {
			GameObject go = GameObject.Find("RTSManager");

			if (go != null) {
				rtsManager = go.GetComponent<RTSManager>();
			}
		}
	}

	public void replenishEnergy(int count) {
		int deltaEnergy = Unit.getUnitCost(faction);


		spawnUnit();
		switch (count) {
			case 2:
				deltaEnergy *= 3;
				break;

			case 3:
				spawnUnit();
				deltaEnergy *= 3;
				break;

			case 4:
				spawnUnit();
				deltaEnergy *= 5;
				break;
		}

		this.energy += deltaEnergy;
	}

	public void spendEnergy(int energy) {
		this.energy -= energy;
	}

	public void setFaction(Faction faction) {
		this.faction = faction;
		switch(faction) {
			case Faction.FACTION_ALLIES:
				this.gameObject.name = String.Format("Allied base");
				break;

			case Faction.FACTION_ENEMIES:
				this.gameObject.name = String.Format("Enemy base");
				break;
		}
	}

	private Sprite getFactionSprite(Faction faction) {
		switch (faction) {
			case Faction.FACTION_ALLIES:
				return alliedBase;

			case Faction.FACTION_ENEMIES:
				return enemyBase;
		}

		return null;
	}

	public Faction GetFaction() {
		return this.faction;
	}

	public void setTarget(Vector3 target) {
		this.target = target;
	}

	public Vector3 getTarget() {
		return this.target;
	}

	void OnTriggerEnter2D(Collider2D other) {
		Unit unit = other.gameObject.GetComponent<Unit>();

		if (unit.getFaction() == faction) {
			return;
		}

		receiveDamage(unit.explodeAttack);
		GameObject.Destroy(other.gameObject);
	}

	private GameObject[] getUnits() {
		return GameObject.FindGameObjectsWithTag("Base unit");
	}
}
