using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShadowTendrilIndividual : MonoBehaviour
{

    // ShadowTendrilIndividual controls an individual shadow tendril to lock on to an enemy


    #region VARIABLES


    [SerializeField] private RectTransform rectTrans;
    public Enemy trackedEnemy;
    private ShadowTendril tendrilManager;


    #endregion


    #region MONOBEHAVIOUR


    // Setup
    //--------------------------------------//
    public void SetupTendril(ShadowTendril _tendrilManager)
    //--------------------------------------//
    {
        tendrilManager = _tendrilManager;
        tendrilManager.RequestNewEnemy(this);

    } // END SetupTendril


    // Update
    //--------------------------------------//
    void Update()
    //--------------------------------------//
    {
        TrackEnemy();

    } // END Update


    #endregion


    #region ENEMY TRACK


    // Sets tracked enemy
    //--------------------------------------//
    public void SetTrackedEnemy(Enemy enemy)
    //--------------------------------------//
    {
        trackedEnemy = enemy;
        enemy.OnEnemyDie += OnEnemyDie;

    } // END SetTrackedEnemy


    // Rotates and scales to track enemy
    //--------------------------------------//
    private void TrackEnemy()
    //--------------------------------------//
    {
        if (trackedEnemy == null)
        {
            tendrilManager.RequestNewEnemy(this);
        }
        else
        {
            // Look at enemy
            Vector3 relativePos = trackedEnemy.transform.position - transform.position;
            Vector2 relativePos2d = new Vector2(relativePos.x, relativePos.y);
            relativePos2d.Normalize();

            float angle = Vector2.Angle(Vector2.up, relativePos2d);
            if (trackedEnemy.transform.position.x > transform.position.x)
            {
                angle = 180f - angle;
                angle = 180f + angle;
            }

            rectTrans.rotation = Quaternion.Euler(0f, 0f, angle);

            //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.left);
            //rectTrans.rotation = rotation;

            // Set length
            rectTrans.localScale = new Vector3(rectTrans.localScale.x, (trackedEnemy.transform.position - transform.position).magnitude, rectTrans.localScale.z);

            // Check if enemy too far
            if ((trackedEnemy.transform.position - transform.position).magnitude > tendrilManager.GetRange())
            {
                UntrackEnemy();
                tendrilManager.RequestNewEnemy(this);
            }
        }

    } // END TrackEnemy


    #endregion


    #region UNTRACK


    // On subscribed enemy death, reassign connected tendril
    //--------------------------------------//
    public void OnEnemyDie(object sender, EventArgs e)
    //--------------------------------------//
    {
        UntrackEnemy();

    } // END OnEnemyDie


    // Untrack an enemy
    //--------------------------------------//
    private void UntrackEnemy()
    //--------------------------------------//
    {
        // Unsubscribe
        if (trackedEnemy != null)
        {
            trackedEnemy.OnEnemyDie -= OnEnemyDie;
        }

        // Hide tendril
        rectTrans.localScale = new Vector3(rectTrans.localScale.x, 0f, rectTrans.localScale.z);

        // Set null and request new
        trackedEnemy = null;
        tendrilManager.RequestNewEnemy(this);

    } // END UntrackEnemy


    #endregion


} // END ShowTendrilIndividual.cs
