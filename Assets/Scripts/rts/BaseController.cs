using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour {
	public GameObject unitPrefab;

	public float spawnRate; // seconds per unit
	public uint energy;

	private float tick;

	// Use this for initialization
	void Start() {
		tick = 0.0f;
	}

	// Update is called once per frame
	void Update() {
		tick += Time.deltaTime;

		if (tick > spawnRate) {
			tick = 0.0f;
			spawnUnit();
		}
	}

	private void spawnUnit() {
		Instantiate(unitPrefab);
	}
}
