using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{

    public static Characters instance = null;       //singleton's instance

    public enum Gender { Unset, Male, Female, Neutral };
    public string[] _maleNames;
    public string[] _femaleNames;
    public string[] _neutralNames;
    public string[] _titles;
    public enum Race { Unset, Human, Dwarf, Elve, Orc, Halfling, Troll, StonePerson };
    public enum Class { Unset, Warrior, Mage, Rogue, Hunter, Gunslinger, Paladin, Merchant, Seer };
    //Add gameplay traits at some point...


    void Awake()
    {
        //singleton definition
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    //Returns a random gender from the Gender enum
    public Gender GetRandomGender()
    {
        Gender gender;
        gender = (Gender)Random.Range(1, System.Enum.GetValues(typeof(Gender)).Length);
        return gender;
    }

    //Returns a random name based on a Gender
    public string GetRandomName(Gender gender)
    {
        string name;

        //Define the list to pick from
        string[] list;      //list to pick from
        string[] fallbackList = new string[] { "None"};

        switch (gender)
        {
            case Gender.Female:
                list = _femaleNames;
                break;
            case Gender.Male:
                list = _maleNames;
                break;
            case Gender.Neutral:
                list = _neutralNames;
                break;
            case Gender.Unset:
                list = fallbackList;
                Debug.Log("Name assignation impossible for Gender.Unset value");
                break;
            default:
                list = fallbackList;
                Debug.Log("Name assignation switch fell back on Default option");
                break;
        }

        name = list[Random.Range(0, (list.Length))];

        return name;
    }

    //Returns a random race from the Race enum
    public Race GetRandomRace()
    {
        Race race;
        race = (Race)Random.Range(1, System.Enum.GetValues(typeof(Race)).Length);
        return race;
    }

    //Returns a random class from the Class enum
    public Class GetRandomClass()
    {
        Class class_;
        class_ = (Class)Random.Range(1, System.Enum.GetValues(typeof(Class)).Length);
        return class_;
    }

    //Returns a random title in the list _titles
    public string GetRandomTitle()
    {
        string title = _titles[Random.Range(0, _titles.Length)];
        return title;
    }
}
