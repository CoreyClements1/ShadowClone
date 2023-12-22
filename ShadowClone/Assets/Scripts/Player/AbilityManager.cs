using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{

    // AbilityManager manages the abilities of the player


    #region VARIABLES


    [SerializeField] private Weapon[] acquirableWeaponPrefabs;
    private Weapon equippedWeapon = null;


    #endregion


    #region EQUIP ABILITIES


    // Equips a weapon and spawns it on the player by a given weapon id
    //--------------------------------------//
    public void EquipWeapon(int weaponId)
    //--------------------------------------//
    {
        foreach (Weapon w in acquirableWeaponPrefabs)
        {
            if (w.id == weaponId)
            {
                Weapon newW = GameObject.Instantiate(w);
                newW.transform.SetParent(transform);

                equippedWeapon = newW;
                equippedWeapon.OnAcquire();

                break;
            }
        }

    } // END EquipWeapon


    #endregion


} // END AbilityManager.cs
