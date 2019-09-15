using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{

    public static Characters instance = null;       //singleton's instance

    //A struct containing leveling information
    [System.Serializable]
    public struct LevellingInfo
    {
        public int referenceLevel;          //Specify here the level to which the data refers to
        public int experienceToNextLevel;

        //The following values are modifiers that add to each other to define min/max values between which pick
        public int minHP;
        public int maxHP;
        public int minStrength;
        public int maxStrength;
        public int minDexterity;
        public int maxDexterity;
        public int minIntelligence;
        public int maxIntelligence;
        public int minCharisma;
        public int maxCharisma;       
    }

    public struct CharacterInfo         //Data package to be communicated to a character for initialization following generation
    {
        public Race race;
        public Gender gender;
        public string name;
        public string title; 
        public Class class_;

        public int level;
        public int _maxHP;
        public int _strength;
        public int _dexterity;
        public int _intelligence;
        public int _charisma;
    }

    public enum Gender { Unset, Male, Female, Neutral };
    public enum Race { Unset, Human, Dwarf, Elve, Orc, Halfling, Troll, StonePerson };
    public enum Class { Unset, Warrior, Mage, Rogue, Hunter, Gunslinger, Paladin, Merchant, Seer };

    public SO_RaceInformation[] _races;           //References the races available for character generation
    public string[] _titles;
    //Add gameplay traits at some point...


    public void Awake()
    {
        //singleton definition
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public CharacterInfo GenerateRandomCharacterInfo()
    {
        CharacterInfo characterInfo = new CharacterInfo();

        //Reference key-generation data for comfort
        SO_RaceInformation referenceRace = _races[Random.Range(0, _races.Length)];
        SO_ClassInformation referenceClass = referenceRace._classes[Random.Range(0, referenceRace._classes.Length)];


        characterInfo.race = referenceRace._race;
        characterInfo.gender = referenceRace._genders[Random.Range(0, referenceRace._genders.Length)];
        characterInfo.name = GetRandomName(characterInfo.gender, referenceRace);
        characterInfo.title = GetRandomTitle();
        characterInfo.class_ = referenceClass._class;

        characterInfo.level = 1;
        
        //Set attributes by cumulating stats from Race and Class for level 1        //Could be later extracted as "Stats gain per level"
        LevellingInfo raceLevellingInfo = referenceRace._levellingInfos[0];
        LevellingInfo classLevellingInfo = referenceClass._levellingInfos;

        characterInfo._maxHP = Random.Range(raceLevellingInfo.minHP, raceLevellingInfo.maxHP) + Random.Range(classLevellingInfo.minHP, classLevellingInfo.maxHP);
        characterInfo._strength = Random.Range(raceLevellingInfo.minStrength, raceLevellingInfo.maxStrength) + Random.Range(classLevellingInfo.minStrength, classLevellingInfo.maxStrength);
        characterInfo._dexterity = Random.Range(raceLevellingInfo.minDexterity, raceLevellingInfo.maxDexterity) + Random.Range(classLevellingInfo.minDexterity, classLevellingInfo.maxDexterity);
        characterInfo._intelligence = Random.Range(raceLevellingInfo.minIntelligence, raceLevellingInfo.maxIntelligence) + Random.Range(classLevellingInfo.minIntelligence, classLevellingInfo.maxIntelligence);
        characterInfo._charisma = Random.Range(raceLevellingInfo.minCharisma, raceLevellingInfo.maxCharisma) + Random.Range(classLevellingInfo.minCharisma, classLevellingInfo.maxCharisma);

        //Cap values so they are not negative
        characterInfo._maxHP = (characterInfo._maxHP <= 0) ? 1 : characterInfo._maxHP;
        characterInfo._strength = (characterInfo._strength <= 0) ? 1 : characterInfo._strength;
        characterInfo._dexterity = (characterInfo._dexterity <= 0) ? 1 : characterInfo._dexterity;
        characterInfo._intelligence = (characterInfo._intelligence <= 0) ? 1 : characterInfo._intelligence;
        characterInfo._charisma = (characterInfo._charisma <= 0) ? 1 : characterInfo._charisma;

        return characterInfo;
    }

    //Returns a random name based on a Gender, within a RaceInformation data set
    public string GetRandomName(Gender gender, SO_RaceInformation raceInfo)
    {
        string name;

        string[] list;      

        switch (gender)
        {
            case Gender.Female:
                list = raceInfo._names.femaleNames;
                break;
            case Gender.Male:
                list = raceInfo._names.maleNames;
                break;
            case Gender.Neutral:
                list = raceInfo._names.neutralNames;
                break;
            default:
                list = new string[] { "Empty name" };
                Debug.Log("Name assignation switch fell back on Default option");
                break;
        }

        name = list[Random.Range(0, (list.Length))];

        return name;
    }

    //Returns a random title in the list _titles
    public string GetRandomTitle()
    {
        string title = _titles[Random.Range(0, _titles.Length)];
        return title;
    }
}
