using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

	void Start () {
	}

	private static Color getColor(Tetrimino.Shape shape) {
		switch (shape) {
			case Tetrimino.Shape.SHAPE_I: return Color.red;
			case Tetrimino.Shape.SHAPE_J: return Color.white;
			case Tetrimino.Shape.SHAPE_L: return Color.magenta;
			case Tetrimino.Shape.SHAPE_O: return Color.blue;
			case Tetrimino.Shape.SHAPE_S: return Color.green;
			case Tetrimino.Shape.SHAPE_T: return Color.yellow;
			case Tetrimino.Shape.SHAPE_Z: return Color.cyan;
		}
		return Color.white;
	}

	public void SetShape(Tetrimino.Shape shape) {
		GetComponentInChildren<SpriteRenderer>().color = getColor(shape);
	}
	
	void Update () {
		
	}

	public void SetPos(int x, int y) {
		transform.localPosition = new Vector3(x, y, 0);
	}
}
