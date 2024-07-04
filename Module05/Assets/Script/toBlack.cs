using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toBlack : MonoBehaviour
{
	public void changePos() {
		GameManager.Instance.player.transform.position = GameManager.Instance.player.gameObject.GetComponent<Player>().getPosIni();
	}

	public void	toAlive() {
		GameManager.Instance.player.transform.GetChild(0).transform.GetComponent<Animator>().SetTrigger("isAlive");
	}
}
