using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameArea : MonoBehaviour
{
	[SerializeField] private TetrisGame tetrisGame;

	private Block[] blocks;

	public int width = 10;
	public int height = 25;
	void Start() {
		blocks = new Block[25*10];
	}
	
	void Update() {
		
	}

	public void tick()
	{
		int lineCount = 0;
		for (int row = height - 1; row >= 0; --row)
		{
			bool fullLine = true;
			for (int i = 0; i < width && fullLine; ++i)
			{
				if (getBlock(i, row) == null)
					fullLine = false;
			}

			if (fullLine)
			{
				++lineCount;
				for (int i = 0; i < width; ++i) {
					Block b = blocks[row * width + i];
					if (b) {
						GameObject.Destroy(b.gameObject);
					}
				}

				for (int j = row + 1; j < height; ++j) {
					for (int i = 0; i < width; ++i) {
						Block b = blocks[j * width + i];
						blocks[(j - 1) * width + i] = b;
						if (b) {
							b.SetPos(i, j-1);
						}
					}
				}

				for (int i = 0; i < width; ++i) {
					blocks[(height-1) * width + i] = null;
				}
			}
		}

		if (lineCount > 0)
		{
			tetrisGame.onTetrisLine(lineCount);
		}
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
					if (col < 0 || col >= width) {
						return false;
					}
					if (row < 0 || row >= height) {
						return false;
					}
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
		currentPiece.setPos(Random.Range(0, 10-4), 18);
	}
}
