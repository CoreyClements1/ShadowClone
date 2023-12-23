using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpCanvas : MonoBehaviour
{

    // LevelUpCanvas handles the canvas which provides level up options


    #region VARIABLES


    [SerializeField] private LevelUpUpgradeChoice[] levelUpChoices;
    [SerializeField] private Canvas levelUpCanvas;
    [SerializeField] private CanvasGroup canvasGroup;


    #endregion


    #region INITIATE LEVEL UP


    // Starts coroutine
    //--------------------------------------//
    public void InitiateLevelUp()
    //--------------------------------------//
    {
        StartCoroutine(InitiateLevelUpCoroutine());

    } // END InitiateLevelUP


    // Initiates level up and displays options
    //--------------------------------------//
    private IEnumerator InitiateLevelUpCoroutine()
    //--------------------------------------//
    {
        levelUpCanvas.gameObject.SetActive(true);
        canvasGroup.alpha = 0f;
        canvasGroup.LeanAlpha(1f, .5f);

        List<Shadowmancy> abilitiesToShow = PlayerController.Instance.abilityManager.GetLevelUpShadowmancies(3);

        for (int i = 0; i < 3; i++)
        {
            levelUpChoices[i].SetupForShow(abilitiesToShow[i]);
        }

        yield return new WaitForSeconds(.5f);
        
        foreach(LevelUpUpgradeChoice c in levelUpChoices) 
        {
            yield return new WaitForSeconds(.25f);

            c.ShowItemAndFade();
        }

        yield return new WaitForSeconds(levelUpChoices[0].fadeTime);

        foreach (LevelUpUpgradeChoice c in levelUpChoices)
        {
            c.AllowSelect();
        }

    } // END InitiateLevelUp


    #endregion


    #region END LEVEL UP


    // Starts coroutine
    //--------------------------------------//
    public void OnItemSelected(Ability selectedAbility)
    //--------------------------------------//
    {
        PlayerController.Instance.abilityManager.LevelShadowmancy(selectedAbility.id);

        StartCoroutine(OnItemSelectedCoroutine());

    } // END OnItemSelected


    // On item selected, hide other items
    //--------------------------------------//
    private IEnumerator OnItemSelectedCoroutine()
    //--------------------------------------//
    {
        canvasGroup.alpha = 1f;
        canvasGroup.LeanAlpha(0f, .5f);

        foreach (LevelUpUpgradeChoice c in levelUpChoices)
        {
            c.OnOtherItemSelected();
        }

        yield return new WaitForSeconds(.5f);

        levelUpCanvas.gameObject.SetActive(false);

    } // END OnItemSelectedCoroutine


    #endregion


} // END LevelUpCanvas.cs
