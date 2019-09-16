using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class LevelInfo_Classes : ScriptableObject
{
	public List<LevelInfoImporter_Classes> warrior; // Replace 'EntityType' to an actual type that is serializable.
	public List<LevelInfoImporter_Classes> mage; // Replace 'EntityType' to an actual type that is serializable.
	public List<LevelInfoImporter_Classes> rogue; // Replace 'EntityType' to an actual type that is serializable.
	public List<LevelInfoImporter_Classes> hunter; // Replace 'EntityType' to an actual type that is serializable.
	public List<LevelInfoImporter_Classes> merchant; // Replace 'EntityType' to an actual type that is serializable.
}
