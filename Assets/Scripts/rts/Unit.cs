using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
	public int life;
	public int damage;

	public float speed;

	public GameObject enemyBase;

	private Vector3 target;
	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		move();
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

	private void move() {
		Vector3 toTarget = (enemyBase.transform.position - transform.position).normalized;
		float angle = Vector3.Angle(Vector3.up, toTarget);
		transform.rotation = Quaternion.Euler(0, 0, -angle);
		Vector3 forward = transform.TransformDirection(Vector3.up);
		Vector3 lateral = transform.TransformDirection(Vector3.right);
		transform.position += forward * 4 * Time.deltaTime;
		transform.position += lateral * Random.Range(-10.0f, 10.0f) * Time.deltaTime;
	}
}
