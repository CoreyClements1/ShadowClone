using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowAura : Shadowmancy
{

    // ShadowAura is a shadowmancy which creates a damaging aura around the player


    #region VARIABLES


    [SerializeField] protected Transform auraTransform;
    [SerializeField] protected SpriteRenderer auraImg;


    #endregion


    #region ABSTRACT IMPLIMENTS


    // When acquired, begin firing bullets in direction of mouse
    //--------------------------------------//
    public override void OnAcquire()
    //--------------------------------------//
    {
        damage = .5f;
        attackSpeed = 1f;
        range = 3f;
        auraImg.color = new Color(1, 1, 1, 0);

        StartCoroutine(FireCoroutine());

    } // END OnAcquire


    // Level up: increase damage and range
    //--------------------------------------//
    public override void OnLevelUp()
    //--------------------------------------//
    {
        abilityLevel += 1;

        range += (abilityLevel / 2f);
        damage += .25f;

    } // END OnLevelUp


    #endregion


    #region FIRE


    // Fire bullets according to fire rate in direction of reticle
    //--------------------------------------//
    private IEnumerator FireCoroutine()
    //--------------------------------------//
    {
        while (PlayerController.Instance != null && PlayerController.Instance.canUseAbilities)
        {
            yield return new WaitForSeconds(attackSpeed / 2f);

            // Show aura
            auraTransform.localScale = Vector3.one * (range / 2.5f);
            auraTransform.LeanScale(Vector3.one * (range / 2f), attackSpeed / 2f).setEaseOutQuad();
            
            LeanTween.value(auraImg.gameObject, new Color(1, 1, 1, 0), new Color(1, 1, 1, .5f), attackSpeed / 4f).setOnUpdate((value) =>
            {
                auraImg.GetComponent<SpriteRenderer>().color = value;
            });

            yield return new WaitForSeconds(attackSpeed / 8f);

            // Hurt overlapped
            List<Enemy> overlappedEnemies = GetOverlappedEnemies();
            foreach (Enemy e in overlappedEnemies)
            {
                e.DamageEnemy(damage);
            }

            yield return new WaitForSeconds(attackSpeed / 8f);

            // Hide aura
            LeanTween.value(auraImg.gameObject, new Color(1, 1, 1, .5f), new Color(1, 1, 1, 0), attackSpeed / 4f).setOnUpdate((value) =>
            {
                auraImg.GetComponent<SpriteRenderer>().color = value;
            });

            yield return new WaitForSeconds(attackSpeed / 4f);
        }

    } // END FireCoroutine


    #endregion


    #region COLLISIONS


    // Gets colliding enemies
    //--------------------------------------//
    protected List<Enemy> GetOverlappedEnemies()
    //--------------------------------------//
    {
        List<Enemy> ret = new List<Enemy>();

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range / 2f);
        foreach (Collider2D hitCollider in hitColliders)
        {
            Transform otherTrans = hitCollider.transform;

            Enemy otherEnemy = otherTrans.GetComponent<Enemy>();
            if (otherEnemy != null)
            {
                ret.Add(otherEnemy);
            }
        }

        return ret;

    } // END GetOverlappedEnemies


    #endregion


} // END ShadowAura.cs
