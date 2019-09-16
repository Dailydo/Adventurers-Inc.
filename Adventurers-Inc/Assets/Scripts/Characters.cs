using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{

    public static Characters instance = null;       //singleton's instance

    //A struct containing leveling information
    [System.Serializable]
    public struct LevelInfo
    {
        public int referenceLevel;          //Specify here the level to which the data refers to
        public int XPThreshold;             //Amount of XP to reach to level up

        public int currentXP;
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
    public enum Race { Unset, Human, Dwarf, Elf, Orc, Halfling, Troll, StonePerson };
    public enum Class { Unset, Warrior, Mage, Rogue, Hunter, Gunslinger, Paladin, Merchant, Seer };

    public SO_Race[] _races;           //References the races available for character generation
    public Titles _titlesContainer;
    public Names _namesContainer;
    public LevelInfo_Races _racesLevelInfoContainer;
    public LevelInfo_Classes _classesLevelInfoContainer;
    //Add gameplay traits at some point...

    public GameObject _characterCard_UI;



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
        SO_Race referenceRace = _races[Random.Range(0, _races.Length)];
        SO_Class referenceClass = referenceRace._classes[Random.Range(0, referenceRace._classes.Length)];

        characterInfo.race = referenceRace._race;
        characterInfo.gender = referenceRace._genders[Random.Range(0, referenceRace._genders.Length)];
        characterInfo.name = GetRandomName(characterInfo.gender, referenceRace);
        characterInfo.title = GetRandomTitle();
        characterInfo.class_ = referenceClass._class;

        characterInfo.level = 1;

        //LevelInfo raceLevelInfo = GetRaceLevelInfo(characterInfo.race);



        //Set attributes by cumulating stats from Race and Class for level 1        //Could be later extracted as "Stats gain per level"

        /*
        LevellingInfo raceLevellingInfo = referenceRace._levellingInfo;
        LevellingInfo classLevellingInfo = referenceClass._levellingInfo[0];

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
        */

        return characterInfo;
    }

    //Returns the LevelInfo struct corresponding to the specified race
    public LevelInfo GetRaceLevelInfo(Race race)
    {
        LevelInfo result = new LevelInfo();

        //Values that needs to be initialized to 0 as they make no sense for in race
        result.referenceLevel = 0;
        result.XPThreshold = 0;
        result.currentXP = 0;

        //Find the proper LevelInfoImporter based on the race
        LevelInfoImporter_Races levelInfoContainer = new LevelInfoImporter_Races();

        switch (race)
        {
            case Race.Human:
                levelInfoContainer = _racesLevelInfoContainer.human[0];     //Only one entry for human, levelling is set in classes
                break;
            case Race.Dwarf:
                levelInfoContainer = _racesLevelInfoContainer.dwarf[0];
                break;
            case Race.Elf:
                levelInfoContainer = _racesLevelInfoContainer.elf[0];
                break;
            default:
                Debug.Log("Race level info acquisition defaulted in switch");
                break;
        }

        result.minHP = levelInfoContainer.minHP;
        result.maxHP = levelInfoContainer.maxHP;
        result.minStrength = levelInfoContainer.minStrength;
        result.maxStrength = levelInfoContainer.maxStrength;
        result.minDexterity = levelInfoContainer.minDexterity;
        result.maxDexterity = levelInfoContainer.maxDexterity;
        result.minIntelligence = levelInfoContainer.minIntelligence;
        result.maxIntelligence = levelInfoContainer.maxIntelligence;
        result.minCharisma = levelInfoContainer.minCharisma;
        result.maxCharisma = levelInfoContainer.maxCharisma;

        return result;
    }

    //Returns a random name based on a Gender, within a RaceInfo data set
    public string GetRandomName(Gender gender, SO_Race raceInfo)
    {
        string result;

        List<NamesImporter> nameList = GetNameList(raceInfo._race, gender);
        result = nameList[Random.Range(0, nameList.Count)].name_;

        return result;
    }
        
    //Returns the NamesImporter list corresponding to the specified race and gender
    public List<NamesImporter> GetNameList (Race race, Gender gender)
    {
        List<NamesImporter> result = new List<NamesImporter>();

        switch (race)
        {
            case Race.Human:
                switch (gender)
                {
                    case Gender.Male:
                        result = _namesContainer.human_male;
                        break;
                    case Gender.Female:
                        result = _namesContainer.human_female;
                        break;
                    default:
                        Debug.Log("Name assignation defaulted in Race.Human");
                        break;
                }
                break;
            case Race.Dwarf:
                switch (gender)
                {
                    case Gender.Male:
                        result = _namesContainer.dwarf_male;
                        break;
                    case Gender.Female:
                        result = _namesContainer.dwarf_female;
                        break;
                    default:
                        Debug.Log("Name assignation defaulted in Race.Dwarf");
                        break;
                }
                break;
            case Race.Elf:
                switch (gender)
                {
                    case Gender.Male:
                        result = _namesContainer.elf_male;
                        break;
                    case Gender.Female:
                        result = _namesContainer.elf_female;
                        break;
                    case Gender.Neutral:
                        result = _namesContainer.elf_neutral;
                        break;
                    default:
                        Debug.Log("Name assignation defaulted in Race.Elf");
                        break;
                }
                break;
            default:
                Debug.Log("Name assignation defaulted in Race");
                break;
        }

        return result;
    }

    //Returns a random title in the list _titles
    public string GetRandomTitle()
    {
        string title = _titlesContainer.Entities[Random.Range(0, _titlesContainer.Entities.Count)].title;
        return title;
    }
}
