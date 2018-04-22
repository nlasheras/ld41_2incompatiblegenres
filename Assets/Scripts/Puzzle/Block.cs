using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

	void Start () {
	}

	private static Color RGBToColor(int r, int g, int b)
	{
		return new Color(r / 255.0f, g / 255.0f, b / 255.0f);
	}

	private static Color cyan = RGBToColor(42, 240, 243);
	private static Color blue = RGBToColor(15, 75, 134);
	private static Color orange = RGBToColor(245, 120, 39);
	private static Color yellow = RGBToColor(255, 231, 91);
	private static Color green = RGBToColor(62, 136, 72);
	private static Color purple = RGBToColor(103, 54, 107);
	private static Color red = RGBToColor(230, 56, 65);

	private static Color getColor(Tetrimino.Shape shape) {
		switch (shape) {
			case Tetrimino.Shape.SHAPE_I: return cyan;
			case Tetrimino.Shape.SHAPE_J: return blue;
			case Tetrimino.Shape.SHAPE_L: return orange;
			case Tetrimino.Shape.SHAPE_O: return yellow;
			case Tetrimino.Shape.SHAPE_S: return green;
			case Tetrimino.Shape.SHAPE_T: return purple;
			case Tetrimino.Shape.SHAPE_Z: return red;
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
