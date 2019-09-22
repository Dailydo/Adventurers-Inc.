using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{

    //SINGLETON------------------------------
    public static Characters instance = null;       //singleton's instance

    //CUSTOM CLASSES & STRUCTS----------------
    //A class containing leveling information
    [System.Serializable]
    public class LevelInfo
    {
        public int _referenceLevel = -1;          //Specify here the level to which the data refers to
        public int _XPThreshold = -1;             //Amount of XP to reach to level up

        public int _currentXP = -1;
        public int _minHP = -1;
        public int _maxHP = -1;
        public int _minStrength = -1;
        public int _maxStrength = -1;
        public int _minDexterity = -1;
        public int _maxDexterity = -1;
        public int _minIntelligence = -1;
        public int _maxIntelligence = -1;
        public int _minCharisma = -1;
        public int _maxCharisma = -1;
        
        //Class constructor, created from a LevelInfoImporter item
        public LevelInfo (LevelInfoImporter levelInfoImporter)
        {
            _referenceLevel = levelInfoImporter.referenceLevel;
            _XPThreshold = levelInfoImporter.XPThreshold;
            _currentXP = 0;
            _minHP = levelInfoImporter.minHP;
            _maxHP = levelInfoImporter.maxHP;
            _minStrength = levelInfoImporter.minStrength;
            _maxStrength = levelInfoImporter.maxStrength;
            _minDexterity = levelInfoImporter.minDexterity;
            _maxDexterity = levelInfoImporter.maxDexterity;
            _minIntelligence = levelInfoImporter.minIntelligence;
            _maxIntelligence = levelInfoImporter.maxIntelligence;
            _minCharisma = levelInfoImporter.minCharisma;
            _maxCharisma = levelInfoImporter.maxCharisma;
        }

        //Returns an HP value between _minHP and _maxHP
        public int GetRandomizedHP()
        {
            return Random.Range(_minHP, _maxHP);
        }

        //Returns a strength value between _minStrength and _maxStrength
        public int GetRandomizedStrength()
        {
            return Random.Range(_minStrength, _maxStrength);
        }

        //Returns a dexterity value between _minDexterity and _maxDexterity
        public int GetRandomizedDexterity()
        {
            return Random.Range(_minDexterity, _maxDexterity);
        }

        //Returns a intelligence value between _minIntelligence and _maxIntelligence
        public int GetRandomizedIntelligence()
        {
            return Random.Range(_minIntelligence, _maxIntelligence);
        }

        //Returns a charisma value between _minCharisma and _maxCharisma
        public int GetRandomizedCharisma()
        {
            return Random.Range(_minCharisma, _maxCharisma);
        }
    }

    public struct CharacterInfo         //Data package to be communicated to a character for initialization following generation
    {
        public Race race;
        public Gender gender;
        public string name;
        public string title; 
        public Class class_;

        public int level;
        public int experienceThreshold;

        public int maxHP;
        public int strength;
        public int dexterity;
        public int intelligence;
        public int charisma;
    }

    public enum Gender { Unset, Male, Female, Neutral };
    public enum Race { Unset, Human, Dwarf, Elf, Orc, Halfling, Troll, StonePerson };
    public enum Class { Unset, Warrior, Mage, Rogue, Hunter, Gunslinger, Paladin, Merchant, Seer };


    //VARIABLES-------------------------------

    public SO_Race[] _races;           //References the races available for character generation
    public Titles _titlesContainer;
    public Names _namesContainer;
    public LevelInfo_Races _racesLevelInfoContainer;
    public LevelInfo_Classes _classesLevelInfoContainer;
    //Add gameplay traits at some point...

    public GameObject _characterPrefab;
    public int _maxCharactersNumber = 2;            //Character generation cap so they don't exceed the scene's capacity
    public List<GameObject> _charactersList;        //List of all the characters currently present in the scene
    public int _charactersToSpawn = 1;              //Number of characters to spawn on play



    //BASE METHODS-----------------------------

    public void Awake()
    {
        //singleton definition
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void Start()
    {
        SpawnCharacters(_charactersToSpawn);
        UpdateCharactersReference();
    }


    //CLASS METHODS-----------------------------

    //Spawns a number of characters in the scene
    public void SpawnCharacters(int number)
    {
        //Cap characters number based on max allowed number
        if (number > _maxCharactersNumber)
        {
            number = _maxCharactersNumber;
            Debug.Log("Requested characters number exceeds scene's capacity");
        }

        //Spawn characters 
        int i = 0;
        while (i < number)
        {
            SpawnAndGenerateCharacter();
            i++;
        }
    }

    //Spawns a character on an available activity and generate it
    public void SpawnAndGenerateCharacter()
    {
        //Spawn the character and assign it to the "Characters" gameobject
        GameObject character = Instantiate(_characterPrefab, Vector3.zero, Quaternion.identity);
        character.transform.parent = transform;
    }

    //Returns a character (informations, not instantiation)
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

        //Collect levellingInfo for the race and class/level
        LevelInfo raceLevelInfo = GetRaceLevelInfo(characterInfo.race);
        LevelInfo classLevelInfo = GetClassLevelInfo(characterInfo.class_, 1);

        characterInfo.experienceThreshold = classLevelInfo._XPThreshold;

        //Set attributes by cumulating level info stats, made at least positive
        characterInfo.maxHP = EnsurePositiveValue(raceLevelInfo.GetRandomizedHP() + classLevelInfo.GetRandomizedHP());
        characterInfo.strength = EnsurePositiveValue(raceLevelInfo.GetRandomizedStrength() + classLevelInfo.GetRandomizedStrength());
        characterInfo.dexterity = EnsurePositiveValue(raceLevelInfo.GetRandomizedDexterity() + classLevelInfo.GetRandomizedDexterity());
        characterInfo.intelligence = EnsurePositiveValue(raceLevelInfo.GetRandomizedIntelligence() + classLevelInfo.GetRandomizedIntelligence());
        characterInfo.charisma = EnsurePositiveValue(raceLevelInfo.GetRandomizedCharisma() + classLevelInfo.GetRandomizedCharisma());

        return characterInfo;
    }
    
    //Returns the value or '1' if the value is inferior to 1
    public int EnsurePositiveValue(int value)
    {
        int result = value;

        if (value <= 0)
            value = 1;

        return result;
    }

    //Returns a LevelInfo class instance corresponding to the specified race
    public LevelInfo GetRaceLevelInfo(Race race)
    {
        //Find the proper LevelInfoImporter based on the race
        LevelInfoImporter levelInfoImporter = new LevelInfoImporter();
        switch (race)
        {
            case Race.Human:
                levelInfoImporter = _racesLevelInfoContainer.human[0];     //Only one entry for human, levelling is set in classes
                break;
            case Race.Dwarf:
                levelInfoImporter = _racesLevelInfoContainer.dwarf[0];
                break;
            case Race.Elf:
                levelInfoImporter = _racesLevelInfoContainer.elf[0];
                break;
            default:
                Debug.Log("Race level info acquisition defaulted in switch");
                break;
        }

        //Create and return a LevelInfo item based on the levelInfoImporter
        LevelInfo result = new LevelInfo(levelInfoImporter);
        return result;
    }

    //Returns a LevelInfo class instance corresponding to the class and level
    public LevelInfo GetClassLevelInfo(Class class_, int level)
    {
        //Find the LevelInfoImporter list corresponding to the specified class
        List<LevelInfoImporter> levelInfoImporterList = new List<LevelInfoImporter>();
        switch (class_)
        {
            case Class.Warrior:
                levelInfoImporterList = _classesLevelInfoContainer.warrior;    
                break;
            case Class.Mage:
                levelInfoImporterList = _classesLevelInfoContainer.mage;
                break;
            case Class.Rogue:
                levelInfoImporterList = _classesLevelInfoContainer.rogue;
                break;
            case Class.Hunter:
                levelInfoImporterList = _classesLevelInfoContainer.hunter;
                break;
            case Class.Merchant:
                levelInfoImporterList = _classesLevelInfoContainer.merchant;
                break;
            default:
                Debug.Log("Class levelInfoImporter list acquisition defaulted in switch");
                break;
        }

        //Find the list entry corresponding to the specified level
        LevelInfoImporter levelInfoImporter = new LevelInfoImporter();
        foreach (LevelInfoImporter item in levelInfoImporterList)
        {
            if (item.referenceLevel == level)
                levelInfoImporter = item;
        }

        //Create and return a LevelInfo item based on the levelInfoImporter
        LevelInfo result = new LevelInfo(levelInfoImporter);
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

    //Updates the list containing the number of characters
    public void UpdateCharactersReference()
    {
        _charactersList.Clear();

        foreach (Transform child in transform)
        {
            if (child.tag == "Character")
                _charactersList.Add(child.gameObject);
        }
    }

    //Destroys a character and removes its dependencies
    public void DestroyCharacter(Character character)
    {
        Destroy(character._UICharacterHeader.gameObject);                             //Remove header
        character._characterActivity._targetedActivity.ResetSelf();                   //Reset current activity 
        UIManager.instance._CharacterPanel.gameObject.SetActive(false);                 //Disable character panel
        Destroy(character.gameObject);

        UpdateCharactersReference();
    }
}
