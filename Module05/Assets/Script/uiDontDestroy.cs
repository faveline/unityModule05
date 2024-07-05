using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiDontDestroy : MonoBehaviour
{
	private static uiDontDestroy instance = null;
    public static uiDontDestroy Instance => instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

	private GameObject	canvaUI;


	void Start() {
	}

	void OnLevelWasLoaded(){
		canvaUI = GameObject.FindGameObjectsWithTag("canvaUI")[0];
		for (int i = 0; i < GameManager.Instance.getHpMax(); i++) {
			if (PlayerPrefs.GetInt("HP") > i)
				GameManager.Instance.canvaUI.transform.GetChild(i).gameObject.SetActive(true);
			else
				GameManager.Instance.canvaUI.transform.GetChild(i).gameObject.SetActive(false);
		}
		canvaUI.transform.GetChild(4).transform.GetComponent<Text>().text = "0";
	}
}
