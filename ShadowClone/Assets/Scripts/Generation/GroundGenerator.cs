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
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tile[] tiles;
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
                    groundTilemap.SetTile(new Vector3Int(i, j, 0), tiles[2]);
                }
                else if (perlinVal < .65f)
                {
                    if (genRiver)
                    {
                        groundTilemap.SetTile(new Vector3Int(i, j, 0), tiles[2]);
                    }
                    else
                    {
                        if (Random.Range(0f, 1f) > .9f)
                        {
                            groundTilemap.SetTile(new Vector3Int(i, j, 0), tiles[1]);
                        }
                        else
                        {
                            groundTilemap.SetTile(new Vector3Int(i, j, 0), tiles[0]);
                        }
                    }
                }
                else
                {
                    if (genRiver)
                    {
                        groundTilemap.SetTile(new Vector3Int(i, j, 0), tiles[5]);
                    }
                    else
                    {
                        if (Random.Range(0f, 1f) > .9f)
                        {
                            groundTilemap.SetTile(new Vector3Int(i, j, 0), tiles[4]);
                        }
                        else
                        {
                            groundTilemap.SetTile(new Vector3Int(i, j, 0), tiles[3]);
                        }
                    }
                }
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
