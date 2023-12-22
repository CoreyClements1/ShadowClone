using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    // PlayerMovement controls the movement of the player


    #region VARIABLES


    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 moveInputAxes;


    #endregion


    #region MONOBEHAVIOUR


    // FixedUpdate, handle movement
    //--------------------------------------//
    private void FixedUpdate()
    //--------------------------------------//
    {
        MovePlayer();

    } // END FixedUpdate


    #endregion


    #region PLAYER INPUT


    // OnMovement, save input axes to be used for movement
    //--------------------------------------//
    public void OnMovement(Vector2 inpAxes)
    //--------------------------------------//
    {
        moveInputAxes = inpAxes;

    } // END OnMovement


    #endregion


    #region MOVEMENT


    // Moves player based on input axes
    //--------------------------------------//
    private void MovePlayer()
    //--------------------------------------//
    {
        rb.MovePosition(rb.position + moveInputAxes * moveSpeed * Time.fixedDeltaTime);

    } // END MovePlayer


    #endregion


} // END PlayerMovement.cs
