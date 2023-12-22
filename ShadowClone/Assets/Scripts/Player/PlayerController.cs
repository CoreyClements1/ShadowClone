using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    // PlayerController controls the player, and sends input to individual components


    #region VARIABLES


    public static PlayerController Instance { get; private set; }
    public Reticle reticle { get; private set; }
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private AbilityManager abilityManager;
    public Transform projectileParent;

    public bool canMove { get; private set; }
    public bool canUseAbilities { get; private set; }


    #endregion


    #region INIT


    // Start
    //--------------------------------------//
    void Start()
    //--------------------------------------//
    {
        InitSingleton();

        canMove = true;
        canUseAbilities = true;

        reticle = GetComponent<Reticle>();
        abilityManager.EquipWeapon(0);

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
