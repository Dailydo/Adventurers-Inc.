using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_CharacterHeader : MonoBehaviour
{
    public GameObject _name_GO;          //Reference to the gameobjects containing the texts
    public GameObject _title_GO;
    public GameObject _level_GO;

    //Quick access to text script
    private TextMeshProUGUI _name;
    private TextMeshProUGUI _title;
    private TextMeshProUGUI _level;


    void Awake()
    {
        //Reference quick access to text script
        _name = _name_GO.GetComponent<TextMeshProUGUI>();
        _title = _title_GO.GetComponent<TextMeshProUGUI>();
        _level = _level_GO.GetComponent<TextMeshProUGUI>();
    }

    //Replaces the values from the card with new ones
    public void UpdateHeaderValues(Character character)
    {
        _name.text = character._name;
        _title.text = character._title;
        _level.text = character._level.ToString();

        gameObject.name = "CharacterHeader_" + character.GetCharacterDescription();
    }
}

