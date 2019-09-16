using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RaceInformation", menuName = "RaceInformation")]
public class SO_Race : ScriptableObject
{
    public Characters.Race _race;
    public Characters.Gender[] _genders;
    public SO_Class[] _classes;
}
