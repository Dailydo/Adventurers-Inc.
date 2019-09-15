using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterCard : MonoBehaviour
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
    private TextMeshPro _name;          
    private TextMeshPro _title;

    private TextMeshPro _race;
    private TextMeshPro _class;
    private TextMeshPro _level;

    private TextMeshPro _HP;
    private TextMeshPro _strength;
    private TextMeshPro _dexterity;
    private TextMeshPro _intelligence;
    private TextMeshPro _charisma;


    void Awake()
    {
        //Reference quick access to text script
        _name = _name_GO.GetComponent<TextMeshPro>();
        _title = _title_GO.GetComponent<TextMeshPro>();
        _race = _race_GO.GetComponent<TextMeshPro>();
        _class = _class_GO.GetComponent<TextMeshPro>();
        _level = _level_GO.GetComponent<TextMeshPro>();
        _HP = _HP_GO.GetComponent<TextMeshPro>();
        _strength = _strength_GO.GetComponent<TextMeshPro>();
        _dexterity = _dexterity_GO.GetComponent<TextMeshPro>();
        _intelligence = _intelligence_GO.GetComponent<TextMeshPro>();
        _charisma = _charisma_GO.GetComponent<TextMeshPro>();
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
