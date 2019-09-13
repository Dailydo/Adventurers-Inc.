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

    private CharacterCard _characterCard;          //Reference to the characterCard displaying the character's information


    void Awake()
    {
        _characterCard = transform.Find("CharacterCard").GetComponent<CharacterCard>();
    }

    //Update the characterCard with current values
    public void UpdateCharacterCard()
    {
        _characterCard.UpdateCardValues(_name, _title, _race, _class, _level);
    }

    //Generates a character with randomized traits
    public void GenerateCharacter()
    {
        _gender = Characters.instance.GetRandomGender();
        _name = Characters.instance.GetRandomName(_gender);
        _race = Characters.instance.GetRandomRace();
        _class = Characters.instance.GetRandomClass();
        _level = 1;
        _title = Characters.instance.GetRandomTitle();
    }
}
