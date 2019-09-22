using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_CharacterHeader : MonoBehaviour
{
    public Character _character = null;       //The character the header is attached to

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

    private void Start()
    {
        UpdateHeaderValues();
        _character._UICharacterHeader = this;
    }

    //Updates card values from _character's data
    public void UpdateHeaderValues()
    {
        _name.text = _character._name;
        _title.text = _character._title;
        _level.text = _character._level.ToString();

        gameObject.name = "CharacterHeader_" + _character.GetCharacterDescription();
    }

    //Signals the UI manager details on the character are requested
    public void CharacterDetailsRequested()
    {
        UIManager.instance.LogClickedItem(_character.gameObject);
    }
}

