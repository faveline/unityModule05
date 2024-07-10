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
	public GameObject	canvaUI;
	public GameObject	collectible;
	private int			tmpB;

	void Start() {
		playerHPMax = playerHP;
		vectCamera = new Vector3(0, 2, -5);
		moveTime = 0.001f;
		nextMove = 0f;
		moveLeaf = new Vector3(0, 0.06f, 0);
		modifTrans = new Color(0, 0, 0, 0.17f);
		animPlayer = player.transform.GetChild(0).GetComponent<Animator>();
		leaf.transform.position = new Vector3(Random.Range(-7f, 14f), 10, 0);
		if (PlayerPrefs.GetInt("HP") == -1 || PlayerPrefs.GetInt("HP") == 0)
			PlayerPrefs.SetInt("HP", playerHPMax);
		else {
			playerHP = PlayerPrefs.GetInt("HP");
			if (playerHP < playerHPMax) {
				for (int i = playerHP; i < playerHPMax; i++)
					canvaUI.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
		score = PlayerPrefs.GetInt("score");
		canvaUI.transform.GetChild(4).transform.GetComponent<Text>().text = score.ToString();
		if (PlayerPrefs.GetInt("score") >= 25)
			canvaUI.transform.GetChild(4).transform.GetComponent<Text>().color = new Color(11, 170, 0, 255);
		tmpB = PlayerPrefs.GetInt("mapSauv");
		for (int i = collectible.transform.childCount - 1; i >= 0; i--) {
			if (tmpB - Mathf.Pow(2, i) >= 0) {
				Destroy(collectible.transform.Find(i.ToString()).gameObject);
				tmpB -= Mathf.RoundToInt(Mathf.Pow(2, i));
			}
		}
	}

	void OnLevelWasLoaded(){
		camera = GameObject.FindGameObjectsWithTag("mainCam")[0];
		player = GameObject.FindGameObjectsWithTag("PlayerCtct")[0];
		leaf = GameObject.FindGameObjectsWithTag("leaf")[0];
		ending = GameObject.FindGameObjectsWithTag("Ending")[0];
		canvaUI = GameObject.FindGameObjectsWithTag("canvaUI")[0];
		collectible = GameObject.FindGameObjectsWithTag("collect")[0];
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
		PlayerPrefs.SetInt("nbrDeath", PlayerPrefs.GetInt("nbrDeath") + 1);
	}

	public int	getHpMax() {
		return (playerHPMax);
	}

	public int	getScore() {
		return (score);
	}

	public void	scoreInc(Collider2D other) {
		score += 5;
		PlayerPrefs.SetInt("score", score);
		PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore") + 5);
		canvaUI.transform.GetChild(4).transform.GetComponent<Text>().text = score.ToString();
		if (score == 25)
			canvaUI.transform.GetChild(4).transform.GetComponent<Text>().color = new Color(11, 170, 0, 255);
		ending.transform.GetComponent<SpriteRenderer>().color += modifTrans;
		PlayerPrefs.SetInt("mapSauv", PlayerPrefs.GetInt("mapSauv") + Mathf.RoundToInt(Mathf.Pow(2, int.Parse(other.transform.name))));
		Destroy(other.gameObject);
	}

	public void	changeScene() {
		score = 0;
		playerHP = playerHPMax;
		PlayerPrefs.SetInt("HP", playerHP);
		PlayerPrefs.SetInt("stage", PlayerPrefs.GetInt("stage") + 1);
		PlayerPrefs.SetInt("score", score);
		PlayerPrefs.SetInt("mapSauv", 0);
		if (SceneManager.GetActiveScene().buildIndex == 4)
			Destroy(GameManager.Instance.gameObject);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
