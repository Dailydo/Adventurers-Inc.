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

    public int _currentXP = 0;
    public int _maxHP = 0;
    public int _currentHP = 0;
    public int _strength = 0;
    public int _dexterity = 0;
    public int _intelligence = 0;
    public int _charisma = 0;

    private CharacterCard _characterCard;          //Reference to the characterCard displaying the character's information


    void Awake()
    {
        _characterCard = transform.Find("CharacterCard").GetComponent<CharacterCard>();
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

        _maxHP = characterInfo._maxHP;
        _currentHP = _maxHP;
        _strength = characterInfo._strength;
        _dexterity = characterInfo._dexterity;
        _intelligence = characterInfo._intelligence;
        _charisma = characterInfo._charisma;
    }

    //Update the characterCard with current values
    public void UpdateCharacterCard()
    {
        _characterCard.UpdateCardValues(this);
        Characters.instance._characterCard_UI.GetComponent<CharacterCard_UI>().UpdateCardValues(this);
    }
}
