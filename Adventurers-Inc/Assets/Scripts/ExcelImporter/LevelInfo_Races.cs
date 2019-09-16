using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class LevelInfo_Races : ScriptableObject
{
	public List<LevelInfoImporter> human; // Replace 'EntityType' to an actual type that is serializable.
	public List<LevelInfoImporter> dwarf; // Replace 'EntityType' to an actual type that is serializable.
	public List<LevelInfoImporter> elf; // Replace 'EntityType' to an actual type that is serializable.
}
