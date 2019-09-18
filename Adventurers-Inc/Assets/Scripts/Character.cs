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

    private CharacterCard _3DCharacterCard;          //Reference to the characterCard displaying the character's information
    private UICharacterCardClamp _UICharacterCardClamp;
    private GameObject _UICharacterCard = null;


    void Awake()
    {
        _3DCharacterCard = transform.Find("3DCharacterCard").GetComponent<CharacterCard>();
        _UICharacterCardClamp = transform.Find("UICharacterCard_AnchorPoint").GetComponent<UICharacterCardClamp>();
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
    }

    //Update the characterCard with current values
    public void UpdateCharacterCard()
    {
        if (_3DCharacterCard.isActiveAndEnabled)    _3DCharacterCard.UpdateCardValues(this);

        if (_UICharacterCard == null)
            _UICharacterCard = _UICharacterCardClamp._UICharacterCard;
        _UICharacterCard.GetComponent<CharacterCard_UI>().UpdateCardValues(this);
    }
}
