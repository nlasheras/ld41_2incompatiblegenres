using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{

	public Text hp;
	public Text energy;
	public Text enemyHp;

	private RTSManager rtsManager;

	void Start () {
		GameObject rtsManagerObject = GameObject.Find("RTSManager");
		if (rtsManagerObject != null)
		{
			rtsManager = rtsManagerObject.GetComponent<RTSManager>();
		}
	}
	
	void Update () {
		if (rtsManager) {
			var playerBase = rtsManager.playerBase;
			hp.text = String.Format("HP: {0}", playerBase.life);
			energy.text = String.Format("ENERGY: {0}", playerBase.energy);

			var enemyBase = rtsManager.enemyBase;
			enemyHp.text = String.Format("ALIEN BASE\n{0}", enemyBase.life);
		}
	}
}
