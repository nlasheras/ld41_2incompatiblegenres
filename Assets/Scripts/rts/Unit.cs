using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
	public int life;
	public int damage;

	public float speed;

	private Faction faction;
	private Vector3 target;

	// Use this for initialization
	void Start() {
		GetComponentInChildren<SpriteRenderer>().color = Base.getFactionColor(faction);
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

	public void setFaction(Faction faction) {
		this.faction = faction;
	}

	public Faction getFaction() {
		return this.faction;
	}

	public void setTarget(Vector3 target) {
		this.target = target;
	}

	public Vector3 getTarget() {
		return this.target;
	}

	private void move() {
		Vector3 toTarget = (target - transform.position).normalized;

		// TODO: Fix angle to take into account direction
		float angle = Vector3.Angle(Vector3.up, toTarget);
		transform.rotation = Quaternion.Euler(0, 0, -angle);
		Vector3 forward = transform.TransformDirection(Vector3.up);
		Vector3 lateral = transform.TransformDirection(Vector3.right);
		transform.position += forward * 4 * Time.deltaTime;
		transform.position += lateral * Random.Range(-10.0f, 10.0f) * Time.deltaTime;
	}
}
