using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tetrimino
{
	public enum Shape
	{
		SHAPE_I = 0, 
		SHAPE_J,
		SHAPE_L,
		SHAPE_O,
		SHAPE_S,
		SHAPE_T, 
		SHAPE_Z,
		SHAPE_COUNT
	}

	public enum Rotation
	{
		ROTATION_0 = 0,
		ROTATION_90,
		ROTATION_180,
		ROTATION_270,
	}

	public static Shape getRandomShape() {
		Array values = Enum.GetValues(typeof(Shape));
		return (Shape) values.GetValue((int) Random.Range(0, values.Length));
	}

	private List<int> getBlockIndexes(Shape shape) {
		switch (shape) {
			case Shape.SHAPE_I: return new List<int>() {0, 1, 2, 3};
			case Shape.SHAPE_J: return new List<int>() {0, 1, 2, 4};
			case Shape.SHAPE_L: return new List<int>() {0, 1, 2, 6};
			case Shape.SHAPE_O: return new List<int>() {1, 2, 4, 5};
			case Shape.SHAPE_S: return new List<int>() {0, 1, 5, 6};
			case Shape.SHAPE_T: return new List<int>() {0, 1, 2, 5};
			case Shape.SHAPE_Z: return new List<int>() {0, 1, 4, 5};
		}
		return new List<int>() {0, 1, 2, 3} ;
	}

	public Block[] blocks;

	public int row { get; private set; }
	public int col { get; private set; }

	private static int instanceId = 0;

	private Shape shape;
	private Rotation rotation;
	public Tetrimino(Shape shape, GameObject blockPrefab) {
		this.shape = shape;
		rotation = Rotation.ROTATION_0;

		blocks = new Block[4*4];
		
		List<int> blockIndexes = getBlockIndexes(shape);

		for (int i = 0; i < 16; ++i) {
			int blockIndex = blockIndexes.IndexOf(i);
			if (blockIndex >= 0) {
				GameObject go = GameObject.Instantiate(blockPrefab);
				go.name = String.Format("tetrimino_{0}_{1}", instanceId, blockIndex);
				Block b = go.GetComponent<Block>();
				b.SetShape(shape);
				blocks[i] = b;
			} else {
				blocks[i] = null;

			}
		}

		++instanceId;
	}
	
	public void tick() {
		setPos(col, row - 1);
	}

	// TODO: maybe we can delay this update 
	private void UpdateBlocksPosition() {
		for (int j = 0; j < 4; ++j) {
			for (int i = 0; i < 4; ++i) {
				int index = getBlockIndexUsingRotation(i, j);
				Block b = blocks[index];
				if (b) {
					b.SetPos(col + i, row + j);
				}
			}
		}
	}

	public void setPos(int x, int y) {
		col = x;
		row = y;
		UpdateBlocksPosition();
	}

	private int getBlockIndexUsingRotation(int col, int row) {
		switch (rotation) {
			case Rotation.ROTATION_90: return col * 4 + row;
			case Rotation.ROTATION_180: return row * 4 + (3 - col); 
			case Rotation.ROTATION_270: return col * 4 + (3 - row);
		}
		return row * 4 + col;
	}

	public Block getBlock(int col, int row) {
		int index = getBlockIndexUsingRotation(col, row);
		return blocks[index];
	}

	public void rotate(int direction)
	{
		int newRotate = (int)rotation + direction;
		if (newRotate < (int)Rotation.ROTATION_0) rotation = Rotation.ROTATION_270;
		else if (newRotate > (int) Rotation.ROTATION_270) rotation = Rotation.ROTATION_0;
		else rotation = (Rotation) (newRotate);
		UpdateBlocksPosition();
	}
}
