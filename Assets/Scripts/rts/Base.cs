using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {
	public GameObject unitPrefab;

	public float spawnRate; // seconds per unit

	public int energy;

	private float tick;

	private Faction faction;

	private Vector3 target;

	// Use this for initialization
	void Start() {
		tick = spawnRate;
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
		Vector3 temp =  new Vector3(transform.position.x, transform.position.y + getOffset(faction), transform.position.z);
		GameObject go = Instantiate(unitPrefab, temp, transform.rotation);
		go.name = String.Format("Unit_{0}", instanceId);
		Unit unit = go.GetComponent<Unit>();
		unit.setFaction(faction);
		unit.setTarget(target);

		++instanceId;
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

	public static Color getFactionColor(Faction faction) {
		switch(faction) {
			case Faction.FACTION_ALLIES:
				return Color.blue;

			case Faction.FACTION_ENEMIES:
				return Color.red;

			default:
				return Color.white;
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

		GetComponentInChildren<SpriteRenderer>().color = getFactionColor(faction);
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
		GameObject.Destroy(other.gameObject);
		receiveDamage(10);
	}
}
