using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterCard : MonoBehaviour
{
    private TextMeshPro _textNameScript;          //Reference to the TextMeshPro script that displays the character's name
    private TextMeshPro _textTitleScript;
    private TextMeshPro _textRaceScript;
    private TextMeshPro _textClassScript;
    private TextMeshPro _textLevelScript;


    void Awake()
    {
        _textNameScript = transform.Find("Name").GetComponent<TextMeshPro>();
        _textTitleScript = transform.Find("Title").GetComponent<TextMeshPro>();
        _textRaceScript = transform.Find("Race").GetComponent<TextMeshPro>();
        _textClassScript = transform.Find("Class").GetComponent<TextMeshPro>();
        _textLevelScript = transform.Find("Level").GetComponent<TextMeshPro>();
    }

    //Replaces the values from the card with new ones
    public void UpdateCardValues(string name, string title, Characters.Race race, Characters.Class class_, int level)
    {
        _textNameScript.text = name;
        _textTitleScript.text = title;
        _textRaceScript.text = "Race: " + race.ToString();
        _textClassScript.text = "Class: " + class_.ToString();
        _textLevelScript.text = "Level: " + level.ToString();
    }
}
