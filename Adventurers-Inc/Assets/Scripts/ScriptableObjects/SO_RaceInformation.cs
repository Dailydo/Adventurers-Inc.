using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RaceInformation", menuName = "RaceInformation")]
public class SO_RaceInformation : ScriptableObject
{
    //A struct containing naming information
    [System.Serializable]
    public struct Names
    {
        public string[] maleNames;
        public string[] femaleNames;
        public string[] neutralNames;
    }


    public Characters.Race _race;
    public Characters.Gender[] _genders;
    public Names _names;
    public SO_ClassInformation[] _classes;
    public Characters.LevellingInfo[] _levellingInfos;
}
