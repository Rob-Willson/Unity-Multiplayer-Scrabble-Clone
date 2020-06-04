using System.Collections;
using UnityEngine;

public class UI_AnimatorSlide : MonoBehaviour, IUserInterfaceInteractionCompanion
{
    public enum SlideType { None, SlideToEnd, SlideToStart, SlideEndToStart, Reset };
    public RectTransform targetRect;
    [Space]
    public SlideType slideOnEnable = SlideType.None;
    public SlideType slideOnHover = SlideType.None;
    public SlideType slideOnPressed = SlideType.None;
    public SlideType slideOnSelect = SlideType.SlideToEnd;
    public SlideType slideOnDeselect = SlideType.SlideToStart;
    [Space]

    [Tooltip("The vector used to calculate the end location of the target RectTransform")] public Vector2 animateMoveVector;
    [Tooltip("The speed that the animation occurs")] public float animateSpeed = 1f;
    public float delay;

    private Coroutine slideAnimationCoroutine;
    private Vector2 startAnchoredPos;
    private Vector2 endAnchoredPos;

    private void Awake ()
    {
        if(targetRect == null)
        {
            Debug.LogError("Unassigned element in inspector: '" + this.transform.name + "'");
        }
        startAnchoredPos = targetRect.anchoredPosition;
        endAnchoredPos = startAnchoredPos + animateMoveVector;
    }

    private void OnEnable ()
    {
        TransitionAccordingToComponentSettings(slideOnEnable);
    }
    private void OnDisable ()
    {
        StopAllCoroutines();
    }

    public void OnHover ()
    {
        TransitionAccordingToComponentSettings(slideOnHover);
    }
    public void OnPressed ()
    {
        TransitionAccordingToComponentSettings(slideOnPressed);
    }
    public void OnSelect ()
    {
        TransitionAccordingToComponentSettings(slideOnSelect);
    }
    public void OnDeselect ()
    {
        TransitionAccordingToComponentSettings(slideOnDeselect);
    }
    public void OnClick () { }

    private void TransitionAccordingToComponentSettings (SlideType slideType)
    {
        if(targetRect != null)
        {
            switch(slideType)
            {
                case SlideType.None:
                    break;
                case SlideType.SlideToEnd:
                    SlideToEnd();
                    break;
                case SlideType.SlideToStart:
                    SlideToStart();
                    break;
                case SlideType.SlideEndToStart:
                    SlideEndToStart();
                    break;
                case SlideType.Reset:
                    Reset();
                    break;
                default:
                    Debug.Log("Default case called in switch. Bug?");
                    break;
            }
        }
        else
        {
            Debug.LogError("Target RectTransform is null in '" + this.name + "'. Did you forget to assign it in the inspector?");
        }
    }

    public void Reset ()
    {
        targetRect.anchoredPosition = startAnchoredPos;
    }

    public void SlideToEnd ()
    {
        if(slideAnimationCoroutine != null) StopCoroutine(slideAnimationCoroutine);
        slideAnimationCoroutine = StartCoroutine(AnimateSlide(endAnchoredPos));
    }

    public void SlideToStart ()
    {
        if(slideAnimationCoroutine != null) StopCoroutine(slideAnimationCoroutine);
        slideAnimationCoroutine = StartCoroutine(AnimateSlide(startAnchoredPos + animateMoveVector));
    }

    public void SlideEndToStart ()
    {
        targetRect.anchoredPosition = endAnchoredPos;
        SlideToStart();
    }

    /// <summary>
    /// Animate the sliding of the target rect transform. 
    /// Applies a minor dampening effect at the end of the animation.
    /// </summary>
    private IEnumerator AnimateSlide (Vector2 targetPos)
    {
        Vector2 startPos = targetRect.anchoredPosition;
        float transitionPercentage = 0f;

        if(delay > 0)
        {
            yield return new WaitForSecondsRealtime(delay);
        }

        while(!Calculations.Approximately(targetRect.anchoredPosition, targetPos, 0.5f))
        {
            targetRect.anchoredPosition = Vector2.Lerp(startPos, targetPos, transitionPercentage += Time.unscaledDeltaTime * animateSpeed * (1.1f - transitionPercentage));
            yield return null;
        }
        targetRect.anchoredPosition = targetPos;
    }

}