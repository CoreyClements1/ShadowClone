using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "BiomeType", menuName = "Shadow Clone/Biome Type")]
public class BiomeType : ScriptableObject
{

    // BiomeType contains info regarding a certain biome, and how to build it


    #region VARIABLES


    [Header("Ground")]
    public Tile[] groundTiles;
    public float[] groundRandomCutoffs;


    #endregion


} // END BiomeType.cs
