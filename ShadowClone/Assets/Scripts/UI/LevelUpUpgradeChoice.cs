using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelUpUpgradeChoice : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    // LevelUpUpgradeChoice handles an individual choice displayed when choosing upgrades


    #region VARIABLES


    private Ability slottedAbility;
    [SerializeField] private RectTransform mainTransform;
    [SerializeField] private CanvasGroup canvGroup;
    [SerializeField] private Image slotImg;
    [SerializeField] private Sprite slotImgOff;
    [SerializeField] private Sprite slotImgOn;
    [SerializeField] private Image itemImg;

    public float fadeTime { get; private set; } = .5f;
    private float fadeDist = 50f;

    private bool canSelect = false;


    #endregion


    #region SHOW ITEM


    // Sets up slot to be shown
    //--------------------------------------//
    public void SetupForShow(Ability abilityToShow)
    //--------------------------------------//
    {
        canSelect = false;

        slottedAbility = abilityToShow;

        slotImg.sprite = slotImgOff;
        itemImg.sprite = slottedAbility.abilityIcon;
        canvGroup.alpha = 0f;

        mainTransform.localScale = Vector3.one;

    } // END SetupForShow


    // Shows item in slot and fades in
    //--------------------------------------//
    public void ShowItemAndFade()
    //--------------------------------------//
    {
        mainTransform.localPosition = new Vector3(mainTransform.localPosition.x, fadeDist, 0f);
        canvGroup.LeanAlpha(1f, fadeTime);
        mainTransform.LeanMoveLocalY(0f, fadeTime).setEaseOutQuad();

    } // END ShowItemAndFade


    // Allows selection of item
    //--------------------------------------//
    public void AllowSelect()
    //--------------------------------------//
    {
        canSelect = true;

    } // END AllowSelect


    #endregion


    #region POINTER ENTER / EXIT


    // OnPointerEnter, highlight slot
    //--------------------------------------//
    public void OnPointerEnter(PointerEventData eventData)
    //--------------------------------------//
    {
        slotImg.sprite = slotImgOn;

    } // END OnPointerEnter


    // OnPointerEnter, highlight slot
    //--------------------------------------//
    public void OnPointerExit(PointerEventData eventData)
    //--------------------------------------//
    {
        slotImg.sprite = slotImgOff;

    } // END OnPointerEnter


    #endregion


    #region CLICK


    // OnPointerClick, attempt to select item
    //--------------------------------------//
    public void OnPointerClick(PointerEventData eventData)
    //--------------------------------------//
    {
        if (canSelect)
        {
            canSelect = false;
            CanvasManager.Instance.levelUpCanvas.OnItemSelected(slottedAbility);

            mainTransform.LeanScale(1.5f * Vector3.one, fadeTime).setEaseOutQuad();
        }
        
    } // END OnPointerClick


    // OnOtherItemSelected, fade out
    //--------------------------------------//
    public void OnOtherItemSelected()
    //--------------------------------------//
    {
        if (canSelect)
        {
            canSelect = false;

            mainTransform.LeanMoveLocalY(-fadeDist, fadeTime).setEaseOutQuad();
        }

    } // END OnOtherItemSelected


    #endregion


} // END LevelUpUpgradeChoice.cs
