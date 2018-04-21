using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArea : MonoBehaviour
{
	private Block[] blocks;

	public int width = 10;
	public int height = 25;
	void Start() {
		blocks = new Block[25*10];
	}
	
	void Update() {
		
	}

	public void tick() {

	}

	public bool isGrounded(Tetrimino piece) {
		return !canOffset(piece, 0, -1);
	}

	public bool canOffset(Tetrimino piece, int x, int y) {
		for (int j = 0; j < 4; ++j) {
			int row = piece.row + j + y;
			for (int i = 0; i < 4; ++i) {
				Block pieceBlock = piece.getBlock(i, j);
				if (pieceBlock) {
					int col = piece.col + i + x;
					if (col < 0 || col > width) return false;
					if (row < 0 || row > height) return false;
					Block areaBlock = getBlock(col, row);
					if (areaBlock != null) {
						return false;
					}
				}
			}
		}
		return true;
	}

	private Block getBlock(int x, int y) {
		if (x >= 0 && x < width && y >= 0 && y < height) {
			return blocks[y * width + x];
		}
		return null;
	}

	public void join(Tetrimino piece) {
		for (int j = 0; j < 4; ++j) {
			int row = piece.row + j;
			for (int i = 0; i < 4; ++i) {
				Block pieceBlock = piece.getBlock(i, j);
				if (pieceBlock) {
					int col = piece.col + i;
					blocks[row * width + col] = pieceBlock; 
				}
			}
		}
	}

	public void show(Tetrimino currentPiece) {
		for (int j = 0; j < 4; ++j) {
			for (int i = 0; i < 4; ++i) {
				Block b = currentPiece.getBlock(i, j);
				if (b) {
					b.transform.parent = transform;
				}
			}
		}
		currentPiece.setPos(0, 19);
	}
}
