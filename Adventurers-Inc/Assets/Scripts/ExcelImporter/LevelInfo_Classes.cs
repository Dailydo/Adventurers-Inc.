using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class LevelInfo_Classes : ScriptableObject
{
	public List<LevelInfoImporter> warrior; // Replace 'EntityType' to an actual type that is serializable.
	public List<LevelInfoImporter> mage; // Replace 'EntityType' to an actual type that is serializable.
	public List<LevelInfoImporter> rogue; // Replace 'EntityType' to an actual type that is serializable.
	public List<LevelInfoImporter> hunter; // Replace 'EntityType' to an actual type that is serializable.
	public List<LevelInfoImporter> merchant; // Replace 'EntityType' to an actual type that is serializable.
}
