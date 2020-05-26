using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This is variation of the UI_Button component (replacing Unity's in-build Button system)
/// This allows a button to be selectable, which means that pressing it may flag that button as selected, deselected, etc.
/// </summary>
public class UI_ButtonSelectable : UI_Button
{
    [Header("ButtonSelectable")]
    public bool isSelected;
    [Tooltip("Works like the ToggleGroup component. But applies to UI_Button components and their companions.")] public UI_ButtonSelectable[] buttonsInGroup;
    public GameObject associatedPanel;
    [Space]
    [Tooltip("Button is deselected whenever the pointer exits its bounds, regardless of other settings.")] public bool deselectOnPointerExit;
    [Tooltip("Don't ignore hover transitions, even if the Button is already selected.")] public bool allowHoverEvenWhenSelected;

    protected virtual void Start ()
    {
        if(isSelected)
        {
            CallCompanionSelect();
        }
        if(deselectOnPointerExit)
        {
            Debug.Log("alwaysDeselectOnPointerExit = true, on: " + gameObject.name);
        }
    }

    /// <summary>
    /// If this button is part of a group (ALA ToggleGroup), force deselect on those other members.
    /// </summary>
    public void SelectThisButtonInGroup ()
    {
        foreach(UI_ButtonSelectable b in buttonsInGroup)
        {
            b.isSelected = false;
            if(b.associatedPanel != null)
            {
                b.associatedPanel.SetActive(false);
            }
            b.CallCompanionDeselect();
        }

        this.isSelected = true;
        if(associatedPanel != null)
        {
            associatedPanel.SetActive(isSelected);
        }

        CallCompanionSelect();
    }

    /// <summary>
    /// Force deselect on this button
    /// </summary>
    public void DeselectThisButtonInGroup ()
    {
        isSelected = false;
        if(associatedPanel != null)
        {
            associatedPanel.SetActive(false);
        }
        CallCompanionDeselect();
    }

    public override void OnPointerEnter (PointerEventData eventData)
    {
        if(!isSelected || allowHoverEvenWhenSelected)
        {
            CallCompanionHover();
        }
    }

    public override void OnPointerDown (PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            CallCompanionPressed();
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

    public override void OnPointerClick (PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(actionOnLMB == ButtonActionType.Toggle)
            {
                DoToggle();
                return;
            }
            if(actionOnLMB == ButtonActionType.OnlySelect)
            {
                DoSelect();
                return;
            }
            if(actionOnLMB == ButtonActionType.OnlyDeselect)
            {
                DoDeselect();
                return;
            }
        }

        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(actionOnRMB == ButtonActionType.Toggle)
            {
                DoToggle();
                return;
            }
            if(actionOnRMB == ButtonActionType.OnlySelect)
            {
                DoSelect();
                return;
            }
            if(actionOnRMB == ButtonActionType.OnlyDeselect)
            {
                DoDeselect();
                return;
            }
        }

        void DoToggle ()
        {
            if(isSelected)
            {
                DoDeselect();
            }
            else
            {
                DoSelect();
            }
        }

        void DoSelect()
        {
            SelectThisButtonInGroup();
        }

        void DoDeselect ()
        {
            DeselectThisButtonInGroup();
            CallCompanionHover();
        }

    }

    public override void OnPointerExit (PointerEventData eventData)
    {
        if(isSelected && !deselectOnPointerExit)
        {
            CallCompanionSelect();
        }
        else
        {
            isSelected = false;
            CallCompanionDeselect();
        }
    }

}
