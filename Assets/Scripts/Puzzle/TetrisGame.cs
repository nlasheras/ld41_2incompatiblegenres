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

		gameArea = new GameArea();

		tickTime = speed;
	}
	
	void Update() {
		if (currentPiece == null) {
			currentPiece = nextPiece;
			nextPiece = getRandomPiece();
		}

		tickTime -= Time.deltaTime;

		if (tickTime < 0.0f)
		{
			tick();
			tickTime = speed;
		}

	}

	private Tetrimino getRandomPiece() {
		
		return new Tetrimino();
	}

	private void tick() {
		currentPiece.tick();

		if (gameArea.isGrounded(currentPiece))
		{
			gameArea.getBlocks(currentPiece);
			GameObject.Destroy(currentPiece);
			currentPiece = null;
		}

		gameArea.tick();
	}
}
