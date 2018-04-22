using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
	public int life;
	public int basicDamage;

	public int autodestruct;

	public float speed;

	private Faction faction;
	private Vector3 target;

	// Use this for initialization
	void Start() {
		GetComponentInChildren<SpriteRenderer>().sprite = getFactionSprite(faction);
		GetComponentInChildren<SpriteRenderer>().color = Base.getFactionColor(faction);
	}

	// Update is called once per frame
	void Update() {
		move();
	}

	public void attack(Unit other) {
		other.receiveDamage(basicDamage);
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

	public static int getUnitCost(Faction faction) {
		if (faction == Faction.FACTION_ALLIES) {
			return 2;
		}

		return 0;
	}

	private void move() {
		Vector3 toTarget = (target - transform.position).normalized;

		float angle = Vector3.SignedAngle(Vector3.up, toTarget, transform.forward);
		transform.rotation = Quaternion.Euler(0, 0, angle);

		Vector3 forward = transform.TransformDirection(Vector3.up);
		Vector3 lateral = transform.TransformDirection(Vector3.right);
		transform.position += forward * speed * Time.deltaTime;
		transform.position += lateral * Random.Range(-10.0f, 10.0f) * Time.deltaTime;
	}

	private static Sprite getFactionSprite(Faction faction) {
		switch (faction) {
			case Faction.FACTION_ALLIES:
				return Resources.Load("alliedBasicUnit", typeof(Sprite)) as Sprite;

			case Faction.FACTION_ENEMIES:
				return Resources.Load("enemyBasicUnit", typeof(Sprite)) as Sprite;
		}

		return null;
	}

	void OnCollisionEnter2D(Collision2D collisionInfo) {
		if (collisionInfo.gameObject.CompareTag("Base unit")) {
			Unit unit = collisionInfo.gameObject.GetComponent<Unit>();

			if (unit.getFaction() != getFaction()) {
				attack(unit);
			}
		}
	}
}
