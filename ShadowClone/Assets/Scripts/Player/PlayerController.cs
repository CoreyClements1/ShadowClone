using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    // PlayerController controls the player, and sends input to individual components


    #region VARIABLES


    [SerializeField] private PlayerMovement playerMovement;


    #endregion


    #region MONOBEHAVIOUR


    // Start
    //--------------------------------------//
    void Start()
    //--------------------------------------//
    {


    } // END Start


    // Update
    //--------------------------------------//
    void Update()
    //--------------------------------------//
    {


    } // END Update


    #endregion


    #region PLAYER INPUT


    // OnMovement, distribute to movement controller
    //--------------------------------------//
    public void OnMovement(InputAction.CallbackContext value)
    //--------------------------------------//
    {
        playerMovement.OnMovement(value.ReadValue<Vector2>());

    } // END OnMovement


    #endregion


} // END PlayerController.cs
