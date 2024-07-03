using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float			speed;
	public float			jump;
	private float			moveTime;
	private float			nextMove;
	private bool			isGrounded;
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
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
			GetComponent<Rigidbody2D>().AddForce(Vector2.up * jump, ForceMode2D.Impulse);
			anim.SetTrigger("isJumping");
			isGrounded = false;
		}
    }

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.layer == 6) {
			isGrounded = true;
		}
		if (!GameManager.Instance.alive)
			return;		
		if (other.gameObject.layer == 7 && Time.time > invulTime) {
			GameManager.Instance.atkPlayer(1);
			invulTime = Time.time + invulnerability;
		}
 	}

	public Vector3	getPosIni() {
		return (posIni);
	}
}
