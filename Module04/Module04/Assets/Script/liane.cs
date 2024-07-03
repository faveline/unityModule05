using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class liane : MonoBehaviour
{
	private Vector3		extend;
	private Vector3		move;
	public AudioClip	audioSlash;
    
    void Start()
    {
		extend = new Vector3(2.5f, 0f, 0f);
		move = new Vector3(1.25f, 0f, 0f);
    }

	void extendRange() {
		transform.GetChild(1).transform.localScale += extend;
		transform.GetChild(1).transform.position -= move;
		GetComponent<AudioSource>().PlayOneShot(audioSlash);
	}

	void reduceRange() {
		transform.GetChild(1).transform.localScale -= extend;
		transform.GetChild(1).transform.position += move;
	}
}
