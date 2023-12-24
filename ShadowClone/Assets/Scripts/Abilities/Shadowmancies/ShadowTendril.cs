using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ShadowTendril : Shadowmancy
{

    // ShadowAura is a shadowmancy which creates a damaging aura around the player


    #region VARIABLES


    [SerializeField] protected ShadowTendrilIndividual tendrilPrefab;
    protected List<ShadowTendrilIndividual> activeTendrils = new List<ShadowTendrilIndividual>();


    #endregion


    #region ABSTRACT IMPLIMENTS


    // When acquired, begin firing bullets in direction of mouse
    //--------------------------------------//
    public override void OnAcquire()
    //--------------------------------------//
    {
        damage = .5f;
        attackSpeed = .5f;
        range = 10f;
        numProjectiles = 1;

        AddTendril();

        StartCoroutine(FireCoroutine());

    } // END OnAcquire


    // Level up: increase damage and range
    //--------------------------------------//
    public override void OnLevelUp()
    //--------------------------------------//
    {
        abilityLevel += 1;

        numProjectiles += 1;
        attackSpeed = attackSpeed / 1.5f;
        damage += .25f;

        AddTendril();

    } // END OnLevelUp


    #endregion


    #region TENDRIL DISPLAYS


    // Adds a tendril to the total number of tendrils
    //--------------------------------------//
    protected void AddTendril()
    //--------------------------------------//
    {
        ShadowTendrilIndividual newTendril = GameObject.Instantiate(tendrilPrefab);
        newTendril.transform.SetParent(transform);
        newTendril.transform.localPosition = Vector3.zero;
        newTendril.SetupTendril(this);
        activeTendrils.Add(newTendril);

    } // END AddTendril


    #endregion


    #region FIRE


    // Fire bullets according to fire rate in direction of reticle
    //--------------------------------------//
    private IEnumerator FireCoroutine()
    //--------------------------------------//
    {
        while (PlayerController.Instance != null && PlayerController.Instance.canUseAbilities)
        {
            foreach(ShadowTendrilIndividual tendril in activeTendrils)
            {
                if (tendril.trackedEnemy != null)
                {
                    tendril.trackedEnemy.DamageEnemy(damage);
                }
            }

            yield return new WaitForSeconds(attackSpeed);
        }

    } // END FireCoroutine


    #endregion


    #region GET ENEMIES


    // Gets closest non-attached enemy
    //--------------------------------------//
    protected Enemy GetClosestNonAttachedEnemy()
    //--------------------------------------//
    {
        List<Enemy> candidateEnemies = new List<Enemy>();

        // Get enemies within range
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range / 2f);
        foreach (Collider2D hitCollider in hitColliders)
        {
            Transform otherTrans = hitCollider.transform;

            Enemy otherEnemy = otherTrans.GetComponent<Enemy>();
            if (otherEnemy != null)
            {
                // Confirm not already locked via other tendril
                bool alreadyLocked = false;
                foreach (ShadowTendrilIndividual tendril in activeTendrils)
                {
                    if (tendril.trackedEnemy != null && tendril.trackedEnemy == otherEnemy)
                    {
                        alreadyLocked = true;
                        break;
                    }
                }

                if (!alreadyLocked)
                    candidateEnemies.Add(otherEnemy);
            }
        }

        // Get closest via linq, from https://forum.unity.com/threads/clean-est-way-to-find-nearest-object-of-many-c.44315/
        Enemy ret = candidateEnemies.OrderBy(t => (t.transform.position - transform.position).sqrMagnitude).FirstOrDefault();

        return ret;

    } // END GetClosestEnemies


    // Requests a new enemy for a tendril
    //--------------------------------------//
    public void RequestNewEnemy(ShadowTendrilIndividual tendrilToGive)
    //--------------------------------------//
    {
        Enemy newEnemy = GetClosestNonAttachedEnemy();

        if (newEnemy != null)
        {
            tendrilToGive.SetTrackedEnemy(newEnemy);
        }

    } // END RequestNewEnemy


    // Gets range
    //--------------------------------------//
    public float GetRange()
    //--------------------------------------//
    {
        return range;
        
    } // END GetRange


    #endregion


} // END ShadowAura.cs
