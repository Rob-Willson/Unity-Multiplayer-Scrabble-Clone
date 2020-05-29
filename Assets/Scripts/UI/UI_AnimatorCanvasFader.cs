using System;
using System.Collections;
using UnityEngine;

public enum FadeType { None, FadeIn, FadeOut, FadeInThenOut }

public class UI_AnimatorCanvasFader : MonoBehaviour, IUserInterfaceInteractionCompanion
{
    public CanvasGroup targetCanvasGroup;
    [Space]
    public FadeType fadeTypeOnEnable = FadeType.None;
    public FadeType fadeTypeOnHover = FadeType.None;
    public FadeType fadeTypeOnPressed = FadeType.None;
    public FadeType fadeTypeOnSelect = FadeType.None;
    public FadeType fadeTypeOnClick = FadeType.None;
    public FadeType fadeTypeOnDeselect = FadeType.None;
    [Header("Default FadeIn Behaviour")]
    public float fadeInTime;
    [Header("Default FadeInThenOut Behaviour")]
    public float delayBetweenFadeInAndFadeOut;
    [Header("Default FadeOut Behaviour")]
    public float fadeOutSpeed;
    public float endFadeOutLevel;
    public bool deactivateAfterFade;

    private float startingFadeLevel;
    private Coroutine fadeCoroutine;

    private void Awake ()
    {
        if(targetCanvasGroup == null)
        {
            Debug.Log(this.GetType(), this);
        }
        startingFadeLevel = targetCanvasGroup.alpha;
    }

    protected void OnEnable ()
    {
        TransitionAccordingToComponentSettings(fadeTypeOnEnable);
    }
    protected void OnDisable ()
    {
        StopAllCoroutines();
    }

    // TODO: Probably shouldn't be public
    public void TransitionAccordingToComponentSettings (FadeType fadeType)
    {
        if(targetCanvasGroup != null)
        {
            switch(fadeType)
            {
                case FadeType.None:
                    break;
                case FadeType.FadeIn:
                    FadeInCanvas(false, fadeInTime);
                    break;
                case FadeType.FadeOut:
                    FadeOutCanvas(fadeOutSpeed, endFadeOutLevel, deactivateAfterFade);
                    break;
                case FadeType.FadeInThenOut:
                    FadeInThenOut(true, false, fadeInTime, delayBetweenFadeInAndFadeOut, fadeOutSpeed, endFadeOutLevel, deactivateAfterFade);
                    break;
                default:
                    Debug.Log("Default case called in switch. Bug?");
                    break;
            }
        }
        else
        {
            Debug.LogError("Target canvas group is null in '" + this.name + "'. Did you forget to assign it in the inspector?");
        }
    }

    private bool AllowFade ()
    {
        if(!gameObject.activeInHierarchy) return false;
        if(fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        return true;
    }

    /// <summary> Fade in the target canvas </summary>
    public void FadeInCanvas (bool softenFadeIn, float fadeInTime)
    {
        if(AllowFade())
        {
            fadeCoroutine = StartCoroutine(FadeIn(targetCanvasGroup, softenFadeIn, fadeInTime));
        }
    }
    /// <summary> Fade out the target canvas </summary>
    public void FadeOutCanvas (float fadeOutSpeed, float endFadeOutLevel, bool deactivateAfterFade)
    {
        if(AllowFade())
        {
            fadeCoroutine = StartCoroutine(FadeOut(targetCanvasGroup, fadeOutSpeed, endFadeOutLevel, deactivateAfterFade));
        }
    }
    /// <summary> Fade in the target canvas then after a delay fade it out again </summary>
    public void FadeInThenOut (bool ignoreIfInactive, bool softenFadeIn, float fadeInTime, float delayBetweenFadeInAndFadeOut, float fadeOutSpeed, float endFadeOutLevel, bool deactivateAfterFade)
    {
        if(AllowFade())
        {
            fadeCoroutine = StartCoroutine(FadeInDelayThenOut(targetCanvasGroup, ignoreIfInactive, softenFadeIn, fadeInTime, delayBetweenFadeInAndFadeOut, fadeOutSpeed, endFadeOutLevel, deactivateAfterFade));
        }
    }

    /// <summary>
    /// Fade in the provided CanvasGroup according to provided parameters.
    /// If <param name="softenFadeIn"/> is true, then fading in will start at the current canvas group alpha value (smoothing the fade)
    /// </summary>
    private IEnumerator FadeIn (CanvasGroup canvasGroup, bool softenFadeIn, float fadeInTime)
    {
        float timer = 0;
        if(softenFadeIn) timer = canvasGroup.alpha;
        while(timer < fadeInTime)
        {
            timer += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Clamp01(timer / fadeInTime);
            yield return null;
        }
        fadeCoroutine = null;
    }

    private IEnumerator FadeOut (CanvasGroup canvasGroup, float fadeOutSpeed, float endFadeOutLevel, bool deactivateAfterFade)
    {
        // Fade out (based on speed not time due to need for fade speed to be cumulative)
        float alphaChange = 0;
        float timerOut = canvasGroup.alpha;
        while(timerOut > endFadeOutLevel)
        {
            // TODO: The *0.005 below just makes up for normalising fadeOutSpeed. Useful for testing. Remove later.
            alphaChange += Time.unscaledDeltaTime * fadeOutSpeed * 0.005f;
            timerOut -= alphaChange;
            canvasGroup.alpha = timerOut;
            yield return null;
        }
        // Deactivate (if relevant)
        if(deactivateAfterFade) canvasGroup.gameObject.SetActive(false);
        fadeCoroutine = null;
    }

    private IEnumerator FadeInDelayThenOut (CanvasGroup canvasGroup, bool ignoreIfInactive, bool softenFadeIn, float fadeInTime, float delayBetweenFadeInAndFadeOut, float fadeOutSpeed, float endFadeOutLevel, bool deactivateAfterFade)
    {
        // Fade in
        fadeCoroutine = StartCoroutine(FadeIn(canvasGroup, softenFadeIn, fadeInTime));
        while(fadeCoroutine != null)
        {
            yield return null;
        }
        // Delay
        yield return new WaitForSecondsRealtime(delayBetweenFadeInAndFadeOut);
        // Fade out
        fadeCoroutine = StartCoroutine(FadeOut(canvasGroup, fadeOutSpeed, endFadeOutLevel, deactivateAfterFade));
        while(fadeCoroutine != null)
        {
            yield return null;
        }
        fadeCoroutine = null;
    }

    public void OnHover ()
    {
        TransitionAccordingToComponentSettings(fadeTypeOnHover);
    }

    public void OnPressed ()
    {
        TransitionAccordingToComponentSettings(fadeTypeOnPressed);
    }

    public void OnSelect ()
    {
        TransitionAccordingToComponentSettings(fadeTypeOnSelect);
    }

    public void OnClick ()
    {
        TransitionAccordingToComponentSettings(fadeTypeOnClick);
    }

    public void OnDeselect ()
    {
        TransitionAccordingToComponentSettings(fadeTypeOnDeselect);
    }

    public void Reset ()
    {
        
    }

}