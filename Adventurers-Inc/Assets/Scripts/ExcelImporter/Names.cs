using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class Names : ScriptableObject
{
	public List<NamesImporter> human_male; // Replace 'EntityType' to an actual type that is serializable.
    public List<NamesImporter> human_female; // Replace 'EntityType' to an actual type that is serializable.
    public List<NamesImporter> dwarf_male; // Replace 'EntityType' to an actual type that is serializable.
    public List<NamesImporter> dwarf_female; // Replace 'EntityType' to an actual type that is serializable.
    public List<NamesImporter> elf_male; // Replace 'EntityType' to an actual type that is serializable.
    public List<NamesImporter> elf_female; // Replace 'EntityType' to an actual type that is serializable.
    public List<NamesImporter> elf_neutral; // Replace 'EntityType' to an actual type that is serializable.
}
