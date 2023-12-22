using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityInfo", menuName = "Shadow Clone/Ability Info")]
public class AbilityInfo : ScriptableObject
{

    // AbilityInfo contains necessary display and prefab info regarding an ability


    #region VARIABLES


    public string abilityName;
    public Ability abilityPrefab;


    #endregion


} // END AbilityInfo
