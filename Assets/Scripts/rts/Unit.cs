using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
	public int life;
	public int damage;

	public float speed;

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	public void attack(Unit other) {
		other.receiveDamage(damage);
	}

	public void receiveDamage(int damage) {
		life -= damage;

		if (life < 0) {
			GameObject.Destroy(this.gameObject);
		}
	}
}
