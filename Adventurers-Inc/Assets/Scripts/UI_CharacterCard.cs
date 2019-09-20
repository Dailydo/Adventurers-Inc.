using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_CharacterCard : MonoBehaviour
{
    public GameObject _name_GO;          //Reference to the gameobjects containing the texts
    public GameObject _title_GO;

    public GameObject _race_GO;
    public GameObject _class_GO;
    public GameObject _level_GO;

    public GameObject _HP_GO;
    public GameObject _strength_GO;
    public GameObject _dexterity_GO;
    public GameObject _intelligence_GO;
    public GameObject _charisma_GO;

    //Quick access to text script
    private TextMeshProUGUI _name;
    private TextMeshProUGUI _title;

    private TextMeshProUGUI _race;
    private TextMeshProUGUI _class;
    private TextMeshProUGUI _level;

    private TextMeshProUGUI _HP;
    private TextMeshProUGUI _strength;
    private TextMeshProUGUI _dexterity;
    private TextMeshProUGUI _intelligence;
    private TextMeshProUGUI _charisma;


    void Awake()
    {
        //Reference quick access to text script
        _name = _name_GO.GetComponent<TextMeshProUGUI>();
        _title = _title_GO.GetComponent<TextMeshProUGUI>();
        _race = _race_GO.GetComponent<TextMeshProUGUI>();
        _class = _class_GO.GetComponent<TextMeshProUGUI>();
        _level = _level_GO.GetComponent<TextMeshProUGUI>();
        _HP = _HP_GO.GetComponent<TextMeshProUGUI>();
        _strength = _strength_GO.GetComponent<TextMeshProUGUI>();
        _dexterity = _dexterity_GO.GetComponent<TextMeshProUGUI>();
        _intelligence = _intelligence_GO.GetComponent<TextMeshProUGUI>();
        _charisma = _charisma_GO.GetComponent<TextMeshProUGUI>();
    }

    //Replaces the values from the card with new ones
    public void UpdateCardValues(Character character)
    {
        _name.text = character._name;
        _title.text = character._title;
        _race.text = character._race.ToString();
        _class.text = character._class.ToString();
        _level.text = character._level.ToString();
        _HP.text = character._currentHP.ToString() + " / " + character._maxHP.ToString();
        _strength.text = character._strength.ToString();
        _dexterity.text = character._dexterity.ToString();
        _intelligence.text = character._intelligence.ToString();
        _charisma.text = character._charisma.ToString();
    }
}

