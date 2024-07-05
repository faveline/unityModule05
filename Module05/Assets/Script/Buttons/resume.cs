using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class EventTriggerResume: EventTrigger
{
	public override void OnPointerClick(PointerEventData data)
	{
		SceneManager.LoadScene(PlayerPrefs.GetInt("stage"));
	}
}
