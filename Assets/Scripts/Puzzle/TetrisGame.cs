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

	private bool playing = true;

	void Start() {
		nextPiece = getRandomPiece();
		currentPiece = null;

		gameArea = GetComponentInChildren<GameArea>();

		tickTime = speed;

		playing = true;
	}
	
	void Update() {

		if (!playing) return;

		if (currentPiece == null) {
			currentPiece = nextPiece;
			gameArea.show(currentPiece);
			nextPiece = getRandomPiece();
			nextPiece.setPos(9, 23);
		}

		tickTime -= Time.deltaTime;

		processInput();

		if (tickTime < 0.0f)
		{
			tick();
			tickTime = speed;
		}
	}

	void onKeyDown()
	{
		if (gameArea.canOffset(currentPiece, 0, -1))
			currentPiece.setPos(currentPiece.col, currentPiece.row - 1);
		else {
			gameArea.join(currentPiece);
			gameArea.tick(); // NL: force update of pieces
			currentPiece = null;
		}
	}

	private static float KEY_DOWN_REPEAT_TIME = 0.05f; 
	private float repeatKeyDown = 0.0f;
	void processInput()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow) && gameArea.canOffset(currentPiece, -1, 0)) {
			currentPiece.setPos(currentPiece.col - 1, currentPiece.row);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow) && gameArea.canOffset(currentPiece, 1, 0)) {
			currentPiece.setPos(currentPiece.col + 1, currentPiece.row);
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			repeatKeyDown -= Time.deltaTime;
			if (repeatKeyDown < 0.0f) {
				onKeyDown();
				repeatKeyDown = KEY_DOWN_REPEAT_TIME;
			}
		} else {
			repeatKeyDown = KEY_DOWN_REPEAT_TIME;
		}

		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			currentPiece.rotate(1);
			if (!gameArea.canOffset(currentPiece, 0, 0))
			{
				currentPiece.rotate(-1);
			}
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			while (gameArea.canOffset(currentPiece, 0, -1))
				currentPiece.setPos(currentPiece.col, currentPiece.row - 1);
			gameArea.join(currentPiece);
			gameArea.tick(); // NL: force update of pieces
			currentPiece = null;
		}
	}

	private Tetrimino getRandomPiece() {
		Tetrimino.Shape shape = Tetrimino.getRandomShape();
		return new Tetrimino(shape, blockPrefab);
	}

	private void tick() {

		if (gameArea.isGrounded(currentPiece)) {
			gameArea.join(currentPiece);
			currentPiece = null;
		} else {
			if (!Input.GetKeyDown(KeyCode.DownArrow))
				currentPiece.tick();
		}

		gameArea.tick();
	}

	private RTSManager rtsManager = null;
	private RTSManager GetRtsManager() {
		if (rtsManager == null) {
			GameObject rtsManagerObject = GameObject.Find("RTSManager");
			if (rtsManagerObject != null) {
				rtsManager = rtsManagerObject.GetComponent<RTSManager>();
			}
		}
		return rtsManager;
	}

	public void onTetrisLine(int count) {
		RTSManager rts = GetRtsManager();
		if (rts != null) {
			rts.onTetrisLine(count);
		}
	}

	public void PauseGame() {
		playing = false;
		speed = 0.0f;
	}

	public void onTetrisDefeat() {
		PauseGame();
		
		GameObject uiManagerObject = GameObject.Find("UIManager");
		if (uiManagerObject) {
			uiManagerObject.GetComponent<UIManager>().onGameDefeat();
		}

		GameObject rtsManagerObject = GameObject.Find("RTSManager");
		if (rtsManagerObject) {
			rtsManagerObject.GetComponent<RTSManager>().onTetrisDefeat();
		}
	}

	private float maxSpeedLosePerDamage = 0.8f;
	private float lifeAtMaxDamage = 25.0f;
	public void onBaseDamaged(int life) {
		float speedDamagePerLife = maxSpeedLosePerDamage / (100.0f - lifeAtMaxDamage);
		float speedDamage = speedDamagePerLife * life;
		speed = Mathf.Max(speed - speedDamage, 1.0f - maxSpeedLosePerDamage);
	}

	public void onRTSWin() {
		GameObject uiManagerObject = GameObject.Find("UIManager");

		if (uiManagerObject) {
			uiManagerObject.GetComponent<UIManager>().onGameWin();
		}

		PauseGame();
	}

	public void onRTSLose() {
		GameObject uiManagerObject = GameObject.Find("UIManager");
		if (uiManagerObject) {
			uiManagerObject.GetComponent<UIManager>().onGameDefeat();
		}

		PauseGame();
	}
}
