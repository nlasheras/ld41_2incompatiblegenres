using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {
	public GameObject unitPrefab;

	public float spawnRate; // seconds per unit

	public Sprite enemyBase;
	public Sprite alliedBase;

	public int energy;

	private float tick;

	private Faction faction;

	private Vector3 target;

	// Use this for initialization
	void Start() {
		tick = spawnRate;
		GetComponentInChildren<SpriteRenderer>().sprite = getFactionSprite(faction);
	}

	// Update is called once per frame
	void Update() {
		tick -= Time.deltaTime;

		if (tick < 0 && spawnRate != 0) {
			tick = spawnRate;
			spawnUnit();
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

		receiveDamage(Unit.getUnitCost(faction));
	}

	private void setInitialEnergy(Faction faction) {
		switch (faction) {
			case Faction.FACTION_ALLIES:
			energy = 100;
			break;

			case Faction.FACTION_ENEMIES:
			energy = 200;
			break;
		}
	}

	private static float getOffset(Faction faction) {
		switch (faction) {
			case Faction.FACTION_ENEMIES:
				return -5.0f;

			case Faction.FACTION_ALLIES:
				return 5.0f;
		}

		return 0.0f;
	}

	public void receiveDamage(int damage) {
		energy -= damage;

		// TODO: Trigger damage receive info to other side
		if (energy <= 0) {
			// For now just destroy object. Will trigger event game over to other side
			GameObject.Destroy(this.gameObject);
		}
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

		receiveDamage(unit.autodestruct);
		GameObject.Destroy(other.gameObject);
	}
}
