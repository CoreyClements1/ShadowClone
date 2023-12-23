using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // Enemy is the parent of all enemies


    #region VARIABLES


    protected float maxHealth;
    protected float currentHealth;


    #endregion


    #region HEALTH CONTROL


    // Damages the enemy
    //--------------------------------------//
    public void DamageEnemy(float damage)
    //--------------------------------------//
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            KillEnemy();
        } 

    } // END DamageEnemy;


    #endregion


    #region DIE


    // Kills the enemy
    //--------------------------------------//
    public void KillEnemy()
    //--------------------------------------//
    {
        Destroy(this.gameObject);

    } // END KillEnemy


    #endregion


} // END Enemy.cs
