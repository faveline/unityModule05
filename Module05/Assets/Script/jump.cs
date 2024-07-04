using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
	private bool		isGrounded;
	private float		jumpVar;
	private Animator	anim;

    void Start()
    {
		jumpVar = transform.parent.transform.GetComponent<Player>().jump;
		anim = transform.parent.transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
       	if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
			transform.parent.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpVar, ForceMode2D.Impulse);
			anim.SetTrigger("isJumping");
			isGrounded = false;
		} 
    }

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.layer == 6) {
			isGrounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.layer == 6) {
			isGrounded = false;
		}
	}
}
