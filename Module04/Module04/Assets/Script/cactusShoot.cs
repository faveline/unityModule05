using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cactusShoot : MonoBehaviour
{
	public GameObject	boom;
	public AudioClip	audioShoot;

	public void shoot() {
		GameObject	cpy;

		GetComponent<AudioSource>().PlayOneShot(audioShoot);
		cpy = Instantiate(boom, transform.position, transform.rotation);
		cpy.gameObject.transform.parent = transform;
	}
}
