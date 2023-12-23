using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{

    // AbilityManager manages the abilities of the player


    #region VARIABLES


    [SerializeField] private Weapon[] acquirableWeaponPrefabs;
    [SerializeField] private List<Shadowmancy> acquirableShadowmancyPrefabs = new List<Shadowmancy>();
    private Weapon equippedWeapon = null;
    private List<Shadowmancy> equippedShadowmancies = new List<Shadowmancy>();

    private int maxShadowmancies = 8;
    private int maxShadowmancyLevel = 8;


    #endregion


    #region GET ABILITY


    // Gets a given weapon by id
    //--------------------------------------//
    public Weapon GetWeaponById(int weaponId)
    //--------------------------------------//
    {
        foreach (Weapon w in acquirableWeaponPrefabs)
        {
            if (w.id == weaponId)
            {
                return w;
            }
        }

        return null;

    } // END GetWeaponById


    // Gets a random weapon from list
    //--------------------------------------//
    public Weapon GetRandomWeapon()
    //--------------------------------------//
    {
        return acquirableWeaponPrefabs[Random.Range(0, acquirableWeaponPrefabs.Length)];

    } // END GetWeaponById


    // Gets an upgradable shadowmancy from lists
    //--------------------------------------//
    public List<Shadowmancy> GetLevelUpShadowmancies(int countToGet)
    //--------------------------------------//
    {
        List<Shadowmancy> ret = new List<Shadowmancy>();
        List<Shadowmancy> acquirablePoolShadowmancies = new List<Shadowmancy>();

        // If can acquire new shadowmancies, randomize between all acquirable shadowmancies
        if (equippedShadowmancies.Count < maxShadowmancies)
        {
            for (int i = 0; i < equippedShadowmancies.Count; i++)
            {
                if (equippedShadowmancies[i].abilityLevel < maxShadowmancyLevel)
                {
                    acquirablePoolShadowmancies.Add(equippedShadowmancies[i]);
                }
            }

            for (int i = 0; i < acquirableShadowmancyPrefabs.Count; i++)
            {
                acquirablePoolShadowmancies.Add(acquirableShadowmancyPrefabs[i]);
            }
        }
        // If shadowmancy limit has been reached, randomize between acquired shadowmancies
        else
        {
            for (int i = 0; i < equippedShadowmancies.Count; i++)
            {
                if (equippedShadowmancies[i].abilityLevel < maxShadowmancyLevel)
                {
                    acquirablePoolShadowmancies.Add(equippedShadowmancies[i]);
                }
            }
        }

        for (int i = 0; i < countToGet; i++)
        {
            if (acquirablePoolShadowmancies.Count == 0)
            {
                if (ret.Count > 0)
                {
                    ret.Add(ret[Random.Range(0, ret.Count)]);
                }
                else
                {
                    Debug.LogError("No shadowmancies present to choose from");
                }
            }
            else
            {
                Shadowmancy proposedShad = acquirablePoolShadowmancies[Random.Range(0, acquirablePoolShadowmancies.Count)];
                acquirablePoolShadowmancies.Remove(proposedShad);
                ret.Add(proposedShad);
            }
        }

        return ret;

    } // END GetLevelUpShadowmancy


    #endregion


    #region LEVEL ABILITIES


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


    // Levels / acquires a shadowmancy from given id
    //--------------------------------------//
    public void LevelShadowmancy(int shadowmancyId)
    //--------------------------------------//
    {
        // Check in acquirable shadowmancies
        foreach (Shadowmancy s in acquirableShadowmancyPrefabs)
        {
            if (s.id == shadowmancyId)
            {
                Shadowmancy newS = GameObject.Instantiate(s);
                newS.transform.SetParent(transform);
                newS.transform.localPosition = Vector3.zero;

                equippedShadowmancies.Add(newS);
                acquirableShadowmancyPrefabs.Remove(s);

                newS.OnAcquire();

                return;
            }
        }

        // Check in equipped shadowmancies
        foreach (Shadowmancy s in equippedShadowmancies)
        {
            if (s.id == shadowmancyId)
            {
                if (s.abilityLevel < maxShadowmancyLevel)
                {
                    s.OnLevelUp();
                }
                else
                {
                    Debug.LogError("Attempting to level a max-level shadowmancy");
                }

                return;
            }
        }

        Debug.LogError("No shadowmancy found to acquire / level up");

    } // END LevelShadowmancy


    #endregion


} // END AbilityManager.cs
