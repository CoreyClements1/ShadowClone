using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{

    // Ability is the parent of all weapons, shadowmancies, and other abilities


    #region VARIABLES


    public string abilityName;
    public int id;
    public Sprite abilityIcon;
    public int abilityLevel = 1;
    protected float damage = 0f;
    protected float attackSpeed = 0;
    protected float projectileSpeed = 0;
    protected int numProjectiles = 0;
    protected int pierce = 1;
    protected float projectileLifetime = 0;
    protected float range = 0f;


    #endregion


    #region FUNCTIONS


    public abstract void OnAcquire();
    public abstract void OnLevelUp();


    #endregion


} // END Ability.cs
