using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boom : MonoBehaviour
{
	public float	speed;
	private	float	nextTime;
	private Vector3	vectSpeed;

	void Start() {
		nextTime = 0f;
		vectSpeed = new Vector3(0.2f, 0, 0);
	}

	void Update() {
		if (Time.time < nextTime)
			return;
		nextTime = Time.time + speed;
		transform.position += vectSpeed;
	}

	public void selfDestruct() {
		Destroy(gameObject);
	}
}
