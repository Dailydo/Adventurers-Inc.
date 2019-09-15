using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ClassInformation", menuName = "ClassInformation")]
public class SO_Class : ScriptableObject
{
    public Characters.Class _class;
    public Characters.LevellingInfo[] _levellingInfo;
}
