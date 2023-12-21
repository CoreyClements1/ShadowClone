using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundGenerator : MonoBehaviour
{

    // GroundGenerator generates the ground, along with the various biomes associated


    #region VARIABLES


    [Header("General")]
    [SerializeField] private int chunkSize = 16;
    [SerializeField] private int worldWidthChunks = 5;
    [SerializeField] private Tilemap tilemapHigh;
    [SerializeField] private Tilemap tilemapMid;
    [SerializeField] private Tilemap tilemapLow;
    [SerializeField] private RuleTile ruleTileLowType;
    [SerializeField] private RuleTile ruleTileHighType;
    [SerializeField] private Tile basicTile;
    [SerializeField] private int seed;

    [Header("Land")]
    [SerializeField] private float scale;
    [SerializeField] private float edgeOffsetScale;
    [SerializeField] private float edgeOffsetAmt;

    [Header("Rivers")]
    [SerializeField] private bool generateRivers;
    [SerializeField] private float riverScale;
    [SerializeField] private float riverStartAmt;
    [SerializeField] private float riverEndAmt;
    [SerializeField] private float riverEdgeOffsetScale;
    [SerializeField] private float riverEdgeOffsetAmt;


    #endregion


    #region MONOBEHAVIOUR


    // Start
    //--------------------------------------//
    void Start()
    //--------------------------------------//
    {
        GenerateGround();

    } // END Start


    // Update
    //--------------------------------------//
    void Update()
    //--------------------------------------//
    {


    } // END Update


    #endregion


    #region GROUND GENERATION


    // Generates the ground
    //--------------------------------------//
    private void GenerateGround()
    //--------------------------------------//
    {
        for (int i = 0; i < 500; i++)
        {
            for (int j = 0; j < 500; j++)
            {
                bool genRiver = GetShouldGenRiver(new Vector2(i, j));

                float perlinOffset = NoiseHelper.Perlin2D(new Vector2(i, j), seed * 2, edgeOffsetScale);
                float perlinVal = NoiseHelper.Perlin2D(new Vector2(i + (perlinOffset * edgeOffsetAmt), j + (perlinOffset * edgeOffsetAmt)), seed, scale);

                if (perlinVal < .35f)
                {
                    tilemapLow.SetTile(new Vector3Int(i, j, 0), ruleTileLowType);
                }
                else if (perlinVal < .65f)
                {
                    //groundTilemap.SetTile(new Vector3Int(i, j, 0), basicTile);
                }
                else
                {
                    tilemapHigh.SetTile(new Vector3Int(i, j, 0), ruleTileHighType);
                }

                tilemapMid.SetTile(new Vector3Int(i, j, 0), basicTile);
            }
        }

    } // END GenerateGround


    // Gets whether a river should be generated
    //--------------------------------------//
    private bool GetShouldGenRiver(Vector2 loc)
    //--------------------------------------//
    {
        if (generateRivers)
        {
            float perlinOffset = NoiseHelper.Perlin2D(loc, seed * 2, riverEdgeOffsetScale);
            float riverPerlin = NoiseHelper.Perlin2D(new Vector2(loc.x + (perlinOffset * riverEdgeOffsetAmt), loc.y + (perlinOffset * riverEdgeOffsetAmt)), seed * 3, riverScale);

            if (riverPerlin > riverStartAmt && riverPerlin < riverEndAmt)
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
        

    } // END GetShouldGenRiver


    #endregion


} // END GroundGenerator.cs
