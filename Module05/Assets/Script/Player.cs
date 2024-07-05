using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	public float			speed;
	public float			jump;
	private float			moveTime;
	private float			nextMove;
	private Animator		anim;
	private SpriteRenderer	sRender;
	private Vector3			sauvPos;
	private Vector3			posIni;
	private float			invulnerability;
	private float			invulTime;

    void Start()
    {
		moveTime = 0.001f;
		nextMove = 0f;
		anim = transform.GetChild(0).GetComponent<Animator>();
		sRender = transform.GetChild(0).GetComponent<SpriteRenderer>();
		sauvPos = transform.GetChild(0).transform.position;
		posIni = transform.position;
		invulnerability = 2f;
		invulTime = 0f;
    }

    void Update()
    {
		if (Time.time < nextMove || !GameManager.Instance.alive)
			return;
		nextMove = Time.time + moveTime;
		if (Input.GetKey(KeyCode.A)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
			if (sRender.flipX == false) {
				sRender.flipX = true;
			}
			anim.SetFloat("moveX", -2);
			anim.SetFloat("isLeft", 1);
		}
		else if (Input.GetKey(KeyCode.D)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
			if (sRender.flipX == true) {
				sRender.flipX = false;
			}
			anim.SetFloat("moveX", 2);
			anim.SetFloat("isLeft", 0);
		}
		else {
			if (sRender.flipX == true)
				anim.SetFloat("moveX", -1);
			else if (sRender.flipX == false)
				anim.SetFloat("moveX", 1);
		}
		if (Input.GetKeyDown(KeyCode.R)) {
			GameManager.Instance.deathPlayer();
		}
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Destroy(GameManager.Instance.gameObject);
			SceneManager.LoadScene(0);
		}
    }

	void OnCollisionEnter2D(Collision2D other) {
		if (!GameManager.Instance.alive)
			return;		
		if (other.gameObject.layer == 7 && Time.time > invulTime) {
			GameManager.Instance.atkPlayer(1);
			invulTime = Time.time + invulnerability;
		}
 	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == 9) {	
			GameManager.Instance.scoreInc(other);	
		}
		if (other.gameObject.layer == 10 && GameManager.Instance.getScore() >= 25) {
			GameManager.Instance.changeScene();
		}
	}

	public Vector3	getPosIni() {
		return (posIni);
	}
}
