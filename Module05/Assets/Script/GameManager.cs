using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	private static GameManager instance = null;
    public static GameManager Instance => instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

	public int			playerHP;
	private int			playerHPMax;
	public GameObject	camera;
	public GameObject	player;
	private Vector3		vectCamera;
	private Animator	animPlayer;
	public bool			alive;
	public bool			toBlack;
	public GameObject	leaf;
	private float		moveTime;
	private float		nextMove;
	private Vector3		moveLeaf;
	private int			score;
	public GameObject	ending;
	private Color		modifTrans;
	private int			totalScore;
	private int			totalDeath;
	public GameObject	canvaUI;

	void Start() {
		playerHPMax = playerHP;
		vectCamera = new Vector3(0, 2, -5);
		moveTime = 0.001f;
		nextMove = 0f;
		moveLeaf = new Vector3(0, 0.06f, 0);
		score = 0;
		totalScore = 0;
		totalDeath = 0;
		modifTrans = new Color(0, 0, 0, 0.15f);
		animPlayer = player.transform.GetChild(0).GetComponent<Animator>();
		leaf.transform.position = new Vector3(Random.Range(-7f, 14f), 10, 0);
		if (PlayerPrefs.GetInt("HP") == -1)
			PlayerPrefs.SetInt("HP", playerHPMax);
		//score = PlayerPrefs.GetInt("score");
	}

	void OnLevelWasLoaded(){
		camera = GameObject.FindGameObjectsWithTag("mainCam")[0];
		player = GameObject.FindGameObjectsWithTag("PlayerCtct")[0];
		leaf = GameObject.FindGameObjectsWithTag("leaf")[0];
		ending = GameObject.FindGameObjectsWithTag("Ending")[0];
		canvaUI = GameObject.FindGameObjectsWithTag("canvaUI")[0];
		animPlayer = player.transform.GetChild(0).GetComponent<Animator>();
		leaf.transform.position = new Vector3(Random.Range(-7f, 14f), 10, 0);
	}

	void Update() {
		camera.transform.position = player.transform.position + vectCamera;
		if (toBlack) {
			toBlack = false;
			camera.transform.GetChild(0).GetComponent<Animator>().SetTrigger("toBlack");
		}
		if (Time.time < nextMove)
			return;
		nextMove = Time.time + moveTime;
		leaf.transform.position -= moveLeaf;
		if (leaf.transform.position.y < -4f)
			leaf.transform.position = new Vector3(Random.Range(-7f, 14f), 10, 0);
	}

	public void	atkPlayer(int atk) {
		playerHP -= atk;
		PlayerPrefs.SetInt("HP", playerHP);
		animPlayer.SetTrigger("takeDamage");
		canvaUI.transform.GetChild(playerHP).gameObject.SetActive(false);
		if (playerHP <= 0) {
			deathPlayer();
		}
	}

	public void deathPlayer() {
		animPlayer.SetTrigger("isDead");
		alive = false;
		totalDeath++;
	}

	public int	getHpMax() {
		return (playerHPMax);
	}

	public int	getScore() {
		return (score);
	}

	public void	scoreInc() {
		score += 5;
		totalScore += 5;
		PlayerPrefs.SetInt("score", score);
		PlayerPrefs.SetInt("totalScore", totalScore);
		canvaUI.transform.GetChild(4).transform.GetComponent<Text>().text = score.ToString();
		if (score == 25)
			canvaUI.transform.GetChild(4).transform.GetComponent<Text>().color = new Color(11, 170, 0, 255);
		ending.transform.GetComponent<SpriteRenderer>().color += modifTrans;
	}

	public void	changeScene() {
		score = 0;
		playerHP = playerHPMax;
		PlayerPrefs.SetInt("HP", playerHP);
		PlayerPrefs.SetInt("stage", PlayerPrefs.GetInt("stage") + 1);
		PlayerPrefs.SetInt("score", score);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
