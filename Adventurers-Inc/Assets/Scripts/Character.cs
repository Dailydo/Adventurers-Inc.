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

    public GameObject _currentActivity;                 //The activity currently occupied by the character

    private UI_CharacterHeader _UICharacterHeader;          //Reference to the header displayed over the character when selected 


    void Awake()
    {
        _UICharacterHeader = null;
    }

    //Generates a character with randomized traits
    public void GenerateCharacter()
    {
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

        UpdateCharacterHeader();
    }

    //Update the characterCard with current values
    public void UpdateCharacterHeader()
    {
        //Reference the character header on first use
        if (_UICharacterHeader == null)
            _UICharacterHeader = transform.Find("UIHeaderAnchorPoint").GetComponent<ClampUIOverObject>()._UICharacterHeader.GetComponent<UI_CharacterHeader>();

        _UICharacterHeader.UpdateHeaderValues(this);
    }
}
