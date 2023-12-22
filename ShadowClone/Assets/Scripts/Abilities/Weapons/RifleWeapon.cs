using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RifleWeapon : Weapon
{

    // RifleWeapon is a weapon which fires individual bullets in direction of look


    #region VARIABLES


    [SerializeField] protected Projectile bulletPrefab;


    #endregion


    #region ABSTRACT IMPLIMENTS


    // When acquired, begin firing bullets in direction of mouse
    //--------------------------------------//
    public override void OnAcquire()
    //--------------------------------------//
    {
        damage = 1;
        attackSpeed = .5f;
        projectileSpeed = 20f;
        numProjectiles = 1;
        pierce = 1;
        projectileLifetime = 5f;

        StartCoroutine(FireCoroutine());

    } // END OnAcquire


    public override void OnActivate()
    {
        throw new System.NotImplementedException();
    }


    public override void OnLevelUp()
    {
        throw new System.NotImplementedException();
    }


    #endregion


    #region FIRE


    // Fire bullets according to fire rate in direction of reticle
    //--------------------------------------//
    private IEnumerator FireCoroutine()
    //--------------------------------------//
    {
        while (PlayerController.Instance != null && PlayerController.Instance.canUseAbilities)
        {
            // Spawn bullet
            Projectile newBullet = GameObject.Instantiate(bulletPrefab, PlayerController.Instance.transform.position, bulletPrefab.transform.rotation);
            newBullet.transform.SetParent(PlayerController.Instance.projectileParent);

            // Get fire direction, account for mag = 0
            Vector2 fireDir = PlayerController.Instance.reticle.reticlePos - new Vector2(PlayerController.Instance.transform.position.x, PlayerController.Instance.transform.position.y);
            fireDir = fireDir.normalized;

            if (fireDir == Vector2.zero)
            {
                fireDir = Vector2.up;
            }

            // Fire bullet
            newBullet.FireProjectile(fireDir, projectileSpeed, pierce, damage, projectileLifetime);

            yield return new WaitForSeconds(attackSpeed);
        }

    } // END FireCoroutine


    #endregion


} // END RifleWeapon.cs
