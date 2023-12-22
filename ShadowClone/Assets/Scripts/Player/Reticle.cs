using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Reticle : MonoBehaviour
{

    // Reticle handles the reticle and its position


    #region VARIABLES


    public Vector2 reticlePos { get; private set; }
    [SerializeField] private Transform reticleTransform;


    #endregion


    #region MONOBEHAVIOUR


    // Update
    //--------------------------------------//
    private void Update()
    //--------------------------------------//
    {
        TrackMouse();

    } // END Update


    #endregion


    #region MOUSE TRACK


    // Tracks the mouse on screen and updates reticle
    //--------------------------------------//
    private void TrackMouse()
    //--------------------------------------//
    {
        
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        reticlePos = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

        reticleTransform.position = reticlePos;

    } // END Update


    #endregion


} // END Reticle.cs
