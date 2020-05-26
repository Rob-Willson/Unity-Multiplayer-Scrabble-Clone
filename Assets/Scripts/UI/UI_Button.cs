using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This is one of the base custom UI Button components, replacing Unity's in-build Button system
/// It may be inherited to extend and override various behaviours.
/// It is primarily designed for handling visual transitions via 'Interaction Companion' components (UI_Recolour, UI_AnimatorSlide, etc.)
/// On Awake these are collected up, and on different input types they may be called as required.
/// </summary>

public class UI_Button : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerDownHandler, IPointerExitHandler
{
    protected List<IUserInterfaceInteractionCompanion> interactionCompanions = new List<IUserInterfaceInteractionCompanion>();

    public enum ButtonActionType { None, Toggle, OnlySelect, OnlyDeselect }
    public ButtonActionType actionOnLMB = ButtonActionType.Toggle;
    public ButtonActionType actionOnRMB = ButtonActionType.None;

    protected virtual void Awake ()
    {
        interactionCompanions.AddRange(GetComponents<IUserInterfaceInteractionCompanion>());
    }

    protected void OnDisable ()
    {
        CallCompanionReset();
    }

    public virtual void OnPointerEnter (PointerEventData eventData)
    {
        CallCompanionHover();
    }

    public virtual void OnPointerDown (PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(actionOnLMB != ButtonActionType.None)
            {
                CallCompanionPressed();
            }
            return;
        }
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(actionOnRMB != ButtonActionType.None)
            {
                CallCompanionPressed();
            }
            return;
        }
    }

    public virtual void OnPointerClick (PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(actionOnLMB != ButtonActionType.None)
            {
                CallCompanionHover();
            }
            return;
        }
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(actionOnRMB != ButtonActionType.None)
            {
                CallCompanionHover();
            }
            return;
        }
    }

    public virtual void OnPointerExit (PointerEventData eventData)
    {
        CallCompanionDeselect();
    }

    /// <summary>
    /// Notify all UI transition companion components that the target UI element has been HOVERED.
    /// </summary>
    public void CallCompanionHover ()
    {
        foreach(IUserInterfaceInteractionCompanion companion in interactionCompanions)
        {
            companion.OnHover();
        }
    }

    /// <summary>
    /// Notify all UI transition companion components that the target UI element has been PRESSED (but not necessarily selected).
    /// </summary>
    public void CallCompanionPressed ()
    {
        foreach(IUserInterfaceInteractionCompanion companion in interactionCompanions)
        {
            companion.OnPressed();
        }
    }

    /// <summary>
    /// Notify all UI transition companion components that the target UI element has been SELECTED.
    /// </summary>
    public void CallCompanionSelect ()
    {
        foreach(IUserInterfaceInteractionCompanion companion in interactionCompanions)
        {
            companion.OnSelect();
        }
    }

    /// <summary>
    /// Notify all UI transition companion components that the target UI element has been DESELECTED.
    /// </summary>
    public void CallCompanionDeselect ()
    {
        foreach(IUserInterfaceInteractionCompanion companion in interactionCompanions)
        {
            companion.OnDeselect();
        }
    }

    public void CallCompanionReset ()
    {
        foreach(IUserInterfaceInteractionCompanion companion in interactionCompanions)
        {
            companion.Reset();
        }
    }

}
