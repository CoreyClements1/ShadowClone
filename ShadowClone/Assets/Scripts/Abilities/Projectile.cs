using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    // Projectile is the parent of all shot projectiles


    #region VARIABLES


    [SerializeField] protected Rigidbody2D rb;

    protected float projSpeed;
    protected int pierce;
    protected int damage;
    protected float lifetime;

    protected Vector3 fireDirection;


    #endregion


    #region MONOBEHAVIOUR


    // Update
    //--------------------------------------//
    protected virtual void FixedUpdate()
    //--------------------------------------//
    {
        HandleLifetime();
        MoveProjectile();

    } // END Update


    #endregion


    #region FIRE


    // Fire the projectile in a given direction
    //--------------------------------------//
    public virtual void FireProjectile(Vector3 _fireDirection, float _projSpeed, int _pierce, int _damage, float _lifetime)
    //--------------------------------------//
    {
        projSpeed = _projSpeed;
        pierce = _pierce;
        damage = _damage;
        fireDirection = _fireDirection;
        lifetime = _lifetime;

    } // END FireProjectile


    #endregion


    #region MOVE


    // Moves the projectile according to speed and direction
    //--------------------------------------//
    protected virtual void MoveProjectile()
    //--------------------------------------//
    {
        rb.MovePosition(transform.position + fireDirection * projSpeed * Time.fixedDeltaTime);

    } // END MoveProjectile


    #endregion


    #region HIT


    // OnTriggerEnter, check if hit an enemy and hit them if so
    //--------------------------------------//
    private void OnTriggerEnter2D(Collider2D other)
    //--------------------------------------//
    {
        Transform otherTrans = other.transform;

        Enemy otherEnemy = otherTrans.GetComponent<Enemy>();
        if (otherEnemy != null)
        {
            OnProjectileHit(otherEnemy);
        }

    } // END OnTriggerEnter2D


    // Called when projectile has hit an enemy
    //--------------------------------------//
    protected virtual void OnProjectileHit(Enemy hitEnemy)
    //--------------------------------------//
    {
        hitEnemy.DamageEnemy(damage);
        pierce -= 1;

        if (pierce <= 0)
        {
            KillProjectile();
        }

    } // END OnProjectileHit


    #endregion


    #region DIE


    // Handles the lifetime of the projectile
    //--------------------------------------//
    protected void HandleLifetime()
    //--------------------------------------//
    {
        if (lifetime <= 0)
        {
            KillProjectile();
        }
        else
        {
            lifetime -= Time.fixedDeltaTime;
        }

    } // END HandleLifetime


    // Kills the projectile
    //--------------------------------------//
    public void KillProjectile()
    //--------------------------------------//
    {
        Destroy(this.gameObject);

    } // END KillProjectile


    #endregion


} // END Projectile.cs
