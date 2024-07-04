using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class EventTriggerExit: EventTrigger
{
	public override void OnPointerClick(PointerEventData data)
	{
		Debug.Log("Application Quit");
		Application.Quit();	
	}
}
