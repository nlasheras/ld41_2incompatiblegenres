using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetrimino
{
	public Block[] blocks;

	public int row { get; private set; }
	public int col { get; private set; }

	public Tetrimino(GameObject blockPrefab) {
		blocks = new Block[4*4];
		
		GameObject go = GameObject.Instantiate(blockPrefab);
		Block b = go.GetComponent<Block>();

		blocks[0] = b;
		for (int i = 1; i < 16; ++i) {
			blocks[i] = null;
		}

	}
	
	public void tick() {
		setPos(col, row - 1);
	}

	public void setPos(int x, int y) {
		col = x;
		row = y;
		for (int j = 0; j < 4; ++j) {
			for (int i = 0; i < 4; ++i) {
				Block b = blocks[j * 4 + i];
				if (b) {
					b.transform.localPosition = new Vector3(col + i, row + j, 0);
				}
			}
		}
	}

	public Block getBlock(int row, int col) {
		return blocks[row * 4 + col];
	}
}
