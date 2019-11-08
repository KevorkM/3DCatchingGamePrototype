using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour {

float ballIntervalMax=2f;
float ballIntervalMin =.2f;
float timeToMinimumInterval = 30f;
public float gameTime;

public Camera mainCamera;
public Text scoreText;
public Text gameOverText;
public PlayerController player;
public GameObject ballPrefab;

Vector3 leftBound;
Vector3 rightBound;

int score = 0;
float gameTimer;
float ballTimer;
bool gameOver;

	void Start () {
		Time.timeScale = 1;
		ballTimer= ballIntervalMax;

		scoreText.enabled = true;
		gameOverText.enabled = false;

		leftBound = mainCamera.ViewportToWorldPoint(new Vector3(0,1,-mainCamera.transform.localPosition.z));
		player.OnCollectBall += OnCollectBall;
	
	}
	
	void Update () {
		GameOverLogic();
		printScoreText();
		SetUpTimers();
	}

	void GameOverLogic(){
		 
		 if (gameOver){
			 if (Input.GetKeyDown("r")){
				 SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			 }
			 scoreText.enabled = false;
			 gameOverText.enabled = true;

			 gameOverText.text = "Game Over Total Score: " + score + "\nPress R to restart!";
			 return;
		 }
	}

	void printScoreText(){
		scoreText.text = "Score: "+ score + "\n Time Left: " +Mathf.Floor(Mathf.Max((gameTime-gameTimer),0));
		}

		void SetUpTimers(){
			gameTimer += Time.deltaTime;
			ballTimer -= Time.deltaTime;

			if (ballTimer <= 0){
				float IntervalPercentage = Mathf.Min(gameTimer / timeToMinimumInterval, 1);

				ballTimer = ballIntervalMax-(ballIntervalMax-ballIntervalMin)*IntervalPercentage;

				GameObject ball = GameObject.Instantiate<GameObject>(ballPrefab);

				ball.transform.SetParent(this.transform);
				ball.transform.position = new Vector3 (Random.Range(-5,5), leftBound.y + 2, 0);
			}
			if (gameTimer > gameTime){
				OnGameOver();
			}
		}

		void OnCollectBall(){
			if (!gameOver){
				score = score +100;
			}
		}

		void OnGameOver(){
			gameOver = true;
			Time.timeScale=0;
		}
}
