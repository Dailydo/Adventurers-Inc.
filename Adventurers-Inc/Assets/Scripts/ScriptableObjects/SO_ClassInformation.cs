using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ClassInformation", menuName = "ClassInformation")]
public class SO_ClassInformation : ScriptableObject
{
    public Characters.Class _class;
    public Characters.LevellingInfo _levellingInfos;
}
