using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundGenerator : MonoBehaviour
{

    // GroundGenerator generates the ground, along with the various biomes associated


    #region VARIABLES


    [Header("General")]
    [SerializeField] private int viewDistance;
    [SerializeField] private int seed;
    [SerializeField] private Transform playerTransform;
    private Vector2Int lastPlayerLoc;

    [Header("Tiles")]
    [SerializeField] private Tilemap tilemapHigh;
    [SerializeField] private Tilemap tilemapMid;
    [SerializeField] private Tilemap tilemapLow;
    [SerializeField] private RuleTile ruleTileLowType;
    [SerializeField] private RuleTile ruleTileHighType;
    [SerializeField] private Tile basicTile;

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
        GenInitialGround();

    } // END Start


    // Update
    //--------------------------------------//
    void Update()
    //--------------------------------------//
    {
        CheckNeedGeneration();

    } // END Update


    #endregion


    #region PLAYER CHECK


    // Checks if generation is required based on player movements
    //--------------------------------------//
    private void CheckNeedGeneration()
    //--------------------------------------//
    {
        Vector2Int playerLoc = new Vector2Int(Mathf.FloorToInt(playerTransform.position.x), Mathf.FloorToInt(playerTransform.position.y));
        int halfView = Mathf.FloorToInt((float)viewDistance / 2f);

        // If player has entered new grid segment...
        if (playerLoc != lastPlayerLoc)
        {
            // Render columns to the right
            if (playerLoc.x > lastPlayerLoc.x)
            {
                // Render all columns between player last loc and player new loc
                int remainingRenders = Mathf.Abs(playerLoc.x - lastPlayerLoc.x);
                for (int r = 0; r < remainingRenders; r++)
                {
                    int i = playerLoc.x + halfView - r;
                    for (int j = playerLoc.y - halfView; j < playerLoc.y + halfView + 1; j++)
                    {
                        GenerateGroundAtLoc(new Vector2Int(i, j));
                    }
                }
            }
            // Render column to the left
            else if (playerLoc.x < lastPlayerLoc.x)
            {
                // Render all columns between player last loc and player new loc
                int remainingRenders = Mathf.Abs(playerLoc.x - lastPlayerLoc.x);
                for (int r = 0; r < remainingRenders; r++)
                {
                    int i = playerLoc.x - halfView + r;
                    for (int j = playerLoc.y - halfView; j < playerLoc.y + halfView + 1; j++)
                    {
                        GenerateGroundAtLoc(new Vector2Int(i, j));
                    }
                }
            }

            // Render row above
            if (playerLoc.y > lastPlayerLoc.y)
            {
                // Render all rows between player last loc and player new loc
                int remainingRenders = Mathf.Abs(playerLoc.y - lastPlayerLoc.y);
                for (int r = 0; r < remainingRenders; r++)
                {
                    int j = playerLoc.y + halfView - r;
                    for (int i = playerLoc.x - halfView; i < playerLoc.x + halfView + 1; i++)
                    {
                        GenerateGroundAtLoc(new Vector2Int(i, j));
                    }
                }
            }
            // Render row below
            else if (playerLoc.y < lastPlayerLoc.y)
            {
                // Render all rows between player last loc and player new loc
                int remainingRenders = Mathf.Abs(playerLoc.y - lastPlayerLoc.y);
                for (int r = 0; r < remainingRenders; r++)
                {
                    int j = playerLoc.y - halfView + r;
                    for (int i = playerLoc.x - halfView; i < playerLoc.x + halfView + 1; i++)
                    {
                        GenerateGroundAtLoc(new Vector2Int(i, j));
                    }
                }
            }
        }

        lastPlayerLoc = playerLoc;
    
    } // END CheckNeedGeneration


    #endregion


    #region GROUND GENERATION


    // Generates the initial ground around player
    //--------------------------------------//
    private void GenInitialGround()
    //--------------------------------------//
    {
        Vector2Int playerLoc = new Vector2Int(Mathf.FloorToInt(playerTransform.position.x), Mathf.FloorToInt(playerTransform.position.y));
        int halfView = Mathf.FloorToInt((float)viewDistance / 2f);

        for (int i = playerLoc.x - halfView; i < playerLoc.x + halfView + 1; i++)
        {
            for (int j = playerLoc.y - halfView; j < playerLoc.y + halfView + 1; j++)
            {
                GenerateGroundAtLoc(new Vector2Int(i, j));
            }
        }

    } // END GenInitialGround


    // Generates the ground
    //--------------------------------------//
    private void GenerateGroundAtLoc(Vector2Int loc)
    //--------------------------------------//
    {
        bool genRiver = GetShouldGenRiver(new Vector2(loc.x, loc.y));

        float perlinOffset = NoiseHelper.Perlin2D(new Vector2(loc.x, loc.y), seed * 2, edgeOffsetScale);
        float perlinVal = NoiseHelper.Perlin2D(new Vector2(loc.x + (perlinOffset * edgeOffsetAmt), loc.y + (perlinOffset * edgeOffsetAmt)), seed, scale);

        if (perlinVal < .35f)
        {
            tilemapLow.SetTile(new Vector3Int(loc.x, loc.y, 0), ruleTileLowType);
        }
        else if (perlinVal < .65f)
        {
            //groundTilemap.SetTile(new Vector3Int(i, j, 0), basicTile);
        }
        else
        {
            tilemapHigh.SetTile(new Vector3Int(loc.x, loc.y, 0), ruleTileHighType);
        }

        tilemapMid.SetTile(new Vector3Int(loc.x, loc.y, 0), basicTile);

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
