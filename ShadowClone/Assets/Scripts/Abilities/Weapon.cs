using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Ability
{

    // Weapon is the parent of any weapon


    #region VARIABLES


    protected float activateCharges;
    protected float activateCooldown;


    #endregion


    #region FUNCTIONS


    public abstract void OnActivate();


    #endregion


} // END Weapon.cs
