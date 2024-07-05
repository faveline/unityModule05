using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stageColor : MonoBehaviour
{
	public int	nbrStage;

    void Start() {
		if (PlayerPrefs.GetInt("stage") < nbrStage + 1) {
			Debug.Log(nbrStage);
        	transform.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 0.4f);
		}
    }
}
