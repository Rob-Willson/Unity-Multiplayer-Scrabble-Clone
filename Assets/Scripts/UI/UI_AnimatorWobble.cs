using System.Collections;
using UnityEngine;

public class UI_AnimatorWobble : MonoBehaviour, IUserInterfaceInteractionCompanion
{
    public enum WobbleType { None, Start, Stop }
    public RectTransform targetRect;
    [Space]
    public WobbleType wobbleOnEnable = WobbleType.None;
    public WobbleType wobbleOnHover = WobbleType.Start;
    public WobbleType wobbleOnPressed = WobbleType.None;
    public WobbleType wobbleOnSelect = WobbleType.None;
    public WobbleType wobbleOnDeselect = WobbleType.Stop;
    [Space]
    [Tooltip("The speed that the animation occurs")] public float animateSpeed = 1f;
    [Range(0f, 89f)] public float wobbleAngle;
    private Coroutine wobbleAnimationCoroutine;
    private Quaternion startingRotation;

    private void Awake ()
    {
        if(targetRect == null)
        {
            Debug.LogError("Unassigned element in inspector: '" + this.transform.name + "'");
        }
        startingRotation = targetRect.localRotation;

    }

    private void OnEnable()
    {
        TransitionAccordingToComponentSettings(wobbleOnEnable);
    }
    public void OnHover ()
    {
        TransitionAccordingToComponentSettings(wobbleOnHover);
    }
    public void OnPressed ()
    {
        TransitionAccordingToComponentSettings(wobbleOnPressed);
    }
    public void OnClick () { }
    public void OnSelect ()
    {
        TransitionAccordingToComponentSettings(wobbleOnSelect);
    }
    public void OnDeselect ()
    {
        TransitionAccordingToComponentSettings(wobbleOnDeselect);
    }
    public void Reset ()
    {
        targetRect.localRotation = startingRotation;
    }

    private void TransitionAccordingToComponentSettings (WobbleType wobbleType)
    {
        if(targetRect != null)
        {
            switch(wobbleType)
            {
                case WobbleType.None:
                    break;
                case WobbleType.Start:
                    StartWobble();
                    break;
                case WobbleType.Stop:
                    StopWobble();
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

    public void StartWobble ()
    {
        if(wobbleAnimationCoroutine != null) StopCoroutine(wobbleAnimationCoroutine);
        wobbleAnimationCoroutine = StartCoroutine(AnimateWobble());
    }

    public void StopWobble ()
    {
        if(wobbleAnimationCoroutine != null) StopCoroutine(wobbleAnimationCoroutine);
        wobbleAnimationCoroutine = StartCoroutine(AnimateWobbleSlowToStartingPosition());
    }

    private IEnumerator AnimateWobble ()
    {
        float percentage;
        Quaternion targetQuaternionRotation;
        Quaternion startQuaternionRotation;
        bool wobbleClockwise = false;
        while(true)
        {
            // Define direction of next wobble
            percentage = 0;
            wobbleClockwise = !wobbleClockwise;
            if(wobbleClockwise)
            {
                targetQuaternionRotation = Quaternion.Euler(startingRotation.eulerAngles + new Vector3(0, 0, wobbleAngle));
            }
            else
            {
                targetQuaternionRotation = Quaternion.Euler(startingRotation.eulerAngles + new Vector3(0, 0, -wobbleAngle));
            }
            startQuaternionRotation = Quaternion.Euler(targetRect.localEulerAngles);
            // Apply animation
            while(percentage < 1f)
            {
                targetRect.localRotation = Quaternion.Lerp(startQuaternionRotation, targetQuaternionRotation, percentage);
                percentage += Time.unscaledDeltaTime * animateSpeed;
                yield return null;
            }
            yield return null;
        }
    }

    private IEnumerator AnimateWobbleSlowToStartingPosition ()
    {
        float percentage = 0;
        Quaternion targetQuaternionRotation = startingRotation;
        Quaternion startQuaternionRotation = Quaternion.Euler(targetRect.localEulerAngles);
        while(percentage < 1f)
        {
            targetRect.localRotation = Quaternion.Lerp(startQuaternionRotation, targetQuaternionRotation, percentage);
            percentage += Time.unscaledDeltaTime * animateSpeed;
            yield return null;
        }
    }

}