using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class LevelInfo_Races : ScriptableObject
{
	public List<LevelInfoImporter_Races> human; // Replace 'EntityType' to an actual type that is serializable.
	public List<LevelInfoImporter_Races> dwarf; // Replace 'EntityType' to an actual type that is serializable.
	public List<LevelInfoImporter_Races> elf; // Replace 'EntityType' to an actual type that is serializable.
}
