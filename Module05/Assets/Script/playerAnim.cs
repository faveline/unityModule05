using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnim : MonoBehaviour
{
	public AudioClip jump;
	public AudioClip damage;
	public AudioClip alive;
	public AudioClip death;

	public void	soundJump() {
		GetComponent<AudioSource>().PlayOneShot(jump);
	}

	public void	soundDamage() {
		GetComponent<AudioSource>().PlayOneShot(damage);
	}

	public void	soundAlive() {
		GetComponent<AudioSource>().PlayOneShot(alive);
	}

	public void	soundDeath() {
		GetComponent<AudioSource>().PlayOneShot(death);
	}

	public void	deathToAlive() {
		GameManager.Instance.toBlack = true;
	}

	public void aliveToIdle() {
		for (int i = 0; i < GameManager.Instance.getHpMax(); i++) {
			GameManager.Instance.canvaUI.transform.GetChild(i).gameObject.SetActive(true);
		}
		GameManager.Instance.playerHP = GameManager.Instance.getHpMax();
		GameManager.Instance.alive = true;
	}
}
