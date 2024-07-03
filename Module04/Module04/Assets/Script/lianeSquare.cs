using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lianeSquare : MonoBehaviour
{
	private Animator	anim;

    void Start() {
		anim = transform.parent.transform.GetComponent<Animator>();
    }

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == 8) {
			anim.SetTrigger("atk");
		}
	}
}
