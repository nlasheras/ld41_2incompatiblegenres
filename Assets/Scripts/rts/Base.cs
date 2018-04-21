using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {
	public GameObject unitPrefab;

	public float spawnRate; // seconds per unit
	public int energy;

	private float tick;

	// Use this for initialization
	void Start() {
		tick = spawnRate;
	}

	// Update is called once per frame
	void Update() {
		tick -= Time.deltaTime;

		if (tick < 0) {
			tick = spawnRate;
			spawnUnit();
		}
	}

	private static int instanceId = 0;
	private void spawnUnit() {
		Vector3 temp =  new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
		GameObject go = Instantiate(unitPrefab, temp, transform.rotation);
		go.name = String.Format("Unit_{0}", instanceId);
		++instanceId;
	}

	public void receiveDamage(int damage) {
		energy -= damage;

		// TODO: Trigger damage receive info to other side
		if (energy < 0) {
			// For now just destroy object. Will trigger event game over to other side
			GameObject.Destroy(this.gameObject);
		}
	}
}
