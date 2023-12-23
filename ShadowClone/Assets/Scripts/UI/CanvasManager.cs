using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{

    // CanvasManager manages all canvases


    #region VARIABLES


    public static CanvasManager Instance;
    public LevelUpCanvas levelUpCanvas;


    #endregion


    #region SETUP


    // Start
    //--------------------------------------//
    private void Start()
    //--------------------------------------//
    {
        InitSingleton();

    } // END Start


    // Initializes this as a singleton
    //--------------------------------------//
    private void InitSingleton()
    //--------------------------------------//
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }

    } //END InitSingleton


    #endregion


} // END CanvasManager.cs
