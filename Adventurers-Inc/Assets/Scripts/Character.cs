using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //Character key attributes
    public Characters.Gender _gender;
    public string _name = null;
    public string _title = null;
    public Characters.Race _race;
    public Characters.Class _class;
    public int _level = 0;
    public int _XPThreshold = 0;

    public int _currentXP = -1;
    public int _maxHP = 0;
    public int _currentHP = 0;
    public int _strength = 0;
    public int _dexterity = 0;
    public int _intelligence = 0;
    public int _charisma = 0;

    public UI_CharacterHeader _UICharacterHeader;          //Reference to the header displayed over the character when selected 
    public CharacterActivity _characterActivity;           //Reference to the character's activity script


    void Awake()
    {
        _UICharacterHeader = null;
        _characterActivity = gameObject.GetComponent<CharacterActivity>();
    }

    private void Start()
    {
        GenerateCharacterInfo();
        name = "Character_" + GetCharacterDescription();

        SetCharacterOnActivity();
        _characterActivity.MoveToAvailableActivity();
    }

    //Generates a character with randomized traits
    public void GenerateCharacterInfo()
    {
        //Character's info generation and assignation 
        Characters.CharacterInfo characterInfo = Characters.instance.GenerateRandomCharacterInfo();

        _race = characterInfo.race;
        _gender = characterInfo.gender;
        _name = characterInfo.name;
        _class = characterInfo.class_;
        _title = characterInfo.title;

        _level = characterInfo.level;
        _XPThreshold = characterInfo.experienceThreshold;

        _maxHP = characterInfo.maxHP;
        _currentHP = _maxHP;
        _strength = characterInfo.strength;
        _dexterity = characterInfo.dexterity;
        _intelligence = characterInfo.intelligence;
        _charisma = characterInfo.charisma;
    }

    //Sets the character on an initial activity (to be used at instantiation)
    private void SetCharacterOnActivity()
    {
        Activity spawnActivity = Activities.instance.GetRandomAvailableActivity();
        transform.position = spawnActivity.transform.position;
        _characterActivity._targetedActivity = spawnActivity;
        spawnActivity.GetComponent<Activity>()._status = Activities.Status.Occupied;
    }

    //Update the characterCard with current values
    public void UpdateCharacterHeader()
    {
        if (_UICharacterHeader != null)
            _UICharacterHeader.UpdateHeaderValues();
        else
            Debug.Log("No referenced header for " + GetCharacterDescription());
    }

    //Returns a string identifying the character (name  + title)
    public string GetCharacterDescription()
    {
        string result;
        result = _name.ToString() + " " + _title.ToString();
        return result;
    }
}
