using System.Collections;
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
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			currentPiece.setPos(currentPiece.col - 1, currentPiece.row);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			currentPiece.setPos(currentPiece.col + 1, currentPiece.row);
		}
	}

	private Tetrimino getRandomPiece() {
		
		return new Tetrimino(blockPrefab);
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
