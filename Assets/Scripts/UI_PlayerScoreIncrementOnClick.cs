using UnityEngine;
using UnityEngine.EventSystems;

public class UI_PlayerScoreIncrementOnClick : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private int index;
    [SerializeField] private UI_PlayerScores playerScoresUIComponent = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            playerScoresUIComponent.IncrementScore(index, 1);
        }
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            playerScoresUIComponent.IncrementScore(index, -1);
        }
    }
}
