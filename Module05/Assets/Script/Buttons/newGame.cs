using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class EventTriggerNewGame: EventTrigger
{
	public override void OnPointerClick(PointerEventData data)
	{
		PlayerPrefs.DeleteAll();
		PlayerPrefs.SetInt("HP", -1);
		PlayerPrefs.SetInt("score", 0);
		PlayerPrefs.SetInt("totalScore", 0);
		PlayerPrefs.SetInt("stage", 1);
 		SceneManager.LoadScene(1);
	}
}
