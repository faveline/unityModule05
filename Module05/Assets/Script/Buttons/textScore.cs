using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textScore : MonoBehaviour
{
    void Start()
    {
        transform.GetComponent<Text>().text = PlayerPrefs.GetInt("totalScore").ToString();
    }
}
