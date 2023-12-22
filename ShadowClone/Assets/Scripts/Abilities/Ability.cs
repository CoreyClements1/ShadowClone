using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{

    // Ability is the parent of all weapons, shadowmancies, and other abilities


    #region VARIABLES


    protected string abilityName;
    public int id;
    protected int abilityLevel = 1;
    protected int damage = 0;
    protected float attackSpeed = 0;
    protected float projectileSpeed = 0;
    protected int numProjectiles = 0;
    protected int pierce = 1;
    protected float projectileLifetime = 0;


    #endregion


    #region FUNCTIONS


    public abstract void OnAcquire();
    public abstract void OnLevelUp();


    #endregion


} // END Ability.cs
