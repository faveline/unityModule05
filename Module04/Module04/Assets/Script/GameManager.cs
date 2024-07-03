using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	void Start() {
		playerHPMax = playerHP;
		vectCamera = new Vector3(0, 2, -5);
		animPlayer = player.transform.GetChild(0).GetComponent<Animator>();
		leaf.transform.position = new Vector3(Random.Range(-7f, 14f), 10, 0);
		moveTime = 0.001f;
		nextMove = 0f;
		moveLeaf = new Vector3(0, 0.06f, 0);
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
		animPlayer.SetTrigger("takeDamage");
		if (playerHP <= 0) {
			animPlayer.SetTrigger("isDead");
			alive = false;
		}
	}

	public int	getHpMax() {
		return (playerHPMax);
	}
}
