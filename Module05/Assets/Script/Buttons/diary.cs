using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class EventTriggerDiary: EventTrigger
{
	public override void OnPointerClick(PointerEventData data)
	{
		SceneManager.LoadScene(1);
	}
}
