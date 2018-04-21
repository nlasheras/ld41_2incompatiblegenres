﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisGame : MonoBehaviour
{
	private Tetrimino nextPiece;
	private Tetrimino currentPiece;

	private GameArea gameArea;

	public float speed = 1.0f;

	private float tickTime;

	public GameObject blockPrefab;

	void Start() {
		nextPiece = getRandomPiece();
		currentPiece = null;

		gameArea = GetComponentInChildren<GameArea>();

		tickTime = speed;
	}
	
	void Update() {
		if (currentPiece == null) {
			currentPiece = nextPiece;
			gameArea.show(currentPiece);
			nextPiece = getRandomPiece();
			nextPiece.setPos(10-4, 25-4);
		}

		tickTime -= Time.deltaTime;

		processInput();

		if (tickTime < 0.0f)
		{
			tick();
			tickTime = speed;
		}
	}

	void processInput()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow) && gameArea.canOffset(currentPiece, -1, 0))
		{
			currentPiece.setPos(currentPiece.col - 1, currentPiece.row);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow) && gameArea.canOffset(currentPiece, 1, 0))
		{
			currentPiece.setPos(currentPiece.col + 1, currentPiece.row);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow) && gameArea.canOffset(currentPiece, 0, -1))
		{
			currentPiece.setPos(currentPiece.col, currentPiece.row - 1);
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			currentPiece.rotate(1);
			if (!gameArea.canOffset(currentPiece, 0, 0))
			{
				currentPiece.rotate(-1);
			}
		}
	}

	private Tetrimino getRandomPiece() {
		Tetrimino.Shape shape = Tetrimino.getRandomShape();
		return new Tetrimino(shape, blockPrefab);
	}

	private void tick() {
		currentPiece.tick();

		if (gameArea.isGrounded(currentPiece))
		{
			gameArea.join(currentPiece);
			currentPiece = null;
		}

		gameArea.tick();
	}
}
