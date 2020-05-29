using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_AnimatorRecolourer : MonoBehaviour, IUserInterfaceInteractionCompanion
{
    public enum ColourPreset {
        None,
        DarkRedHighlight,
        BlackToYellow_Transparent,
        BlackYellowBlack,
        YellowHighlight,
        BlackBlue_Transparent,
        WhiteBlue,
        WhiteHighlight,
        WhiteHighlight_Transparent,
        DarkGreenHighlight,
        GreenHighlight,
        RedHighlight,
        BlueHighlight,
        BlackHighlight,
        DarkBlueHighlight
    };

    public MaskableGraphic targetGraphic;
    [Space]
    [Tooltip("Overrides the colours below")] public ColourPreset colourPreset = ColourPreset.None;
    public Color32 hoverColor = new Color32(255, 0, 255, 255);
    public Color32 pressedColor = new Color32(255, 0, 255, 255);
    public Color32 selectColor = new Color32(255, 0, 255, 255);
    private Color32 startingColor;
    [Space]
    [Tooltip("The speed that the animation occurs")] public float animateSpeed = 10f;

    private Coroutine recolourAnimationCoroutine;

    private void Awake ()
    {
        if(targetGraphic == null)
        {
            Debug.LogError("Unassigned element in inspector: '" + this.transform.name + "'");
            return;
        }
        startingColor = targetGraphic.color;

        AssignColoursFromPreset();
    }

    public void OnHover ()
    {
        RecolourToHover();
    }
    public void OnPressed ()
    {
        RecolourToPress();
    }
    public void OnClick () { }
    public void OnSelect ()
    {
        RecolourToSelect();
    }
    public void OnDeselect ()
    {
        RecolourToDeselect();
    }

    public void Reset ()
    {
        targetGraphic.color = startingColor;
    }

    public void RecolourToHover ()
    {
        if(recolourAnimationCoroutine != null) StopCoroutine(recolourAnimationCoroutine);
        recolourAnimationCoroutine = StartCoroutine(AnimateRecolour(hoverColor, animateSpeed));
    }
    public void RecolourToPress ()
    {
        if(recolourAnimationCoroutine != null) StopCoroutine(recolourAnimationCoroutine);
        recolourAnimationCoroutine = StartCoroutine(AnimateRecolour(pressedColor, animateSpeed));
    }
    public void RecolourToSelect ()
    {
        if(recolourAnimationCoroutine != null) StopCoroutine(recolourAnimationCoroutine);
        recolourAnimationCoroutine = StartCoroutine(AnimateRecolour(selectColor, animateSpeed));
    }
    public void RecolourToDeselect ()
    {
        if(recolourAnimationCoroutine != null) StopCoroutine(recolourAnimationCoroutine);
        if(this.isActiveAndEnabled) recolourAnimationCoroutine = StartCoroutine(AnimateRecolour(startingColor, animateSpeed));
    }
    /// <summary>
    /// Stops any coroutines and returns colour to start colour, fast.
    /// </summary>
    public void RecolourReset ()
    {
        if(recolourAnimationCoroutine != null) StopCoroutine(recolourAnimationCoroutine);
        recolourAnimationCoroutine = StartCoroutine(AnimateRecolour(startingColor, 6f));
    }
    /// <summary>
    /// Ping-pongs between two colours.
    /// </summary>
    public void RecolourPingPong (float pingAnimateSpeed, float pongAnimateSpeed, bool doLoop)
    {
        if(recolourAnimationCoroutine != null) StopCoroutine(recolourAnimationCoroutine);
        recolourAnimationCoroutine = StartCoroutine(AnimateRecolourPingPong(hoverColor, pingAnimateSpeed, selectColor, pongAnimateSpeed, doLoop));
    }

    private IEnumerator AnimateRecolourPingPong (Color32 startColor, float pingAnimateSpeed, Color32 endColor, float pongAnimateSpeed, bool doLoop)
    {
        do
        {
            // Ping!
            float t = 0f;
            Color32 actualStartColor = targetGraphic.color;
            while(targetGraphic.color != endColor)
            {
                Color32 newColor = Color32.Lerp(actualStartColor, endColor, t);
                targetGraphic.color = newColor;
                t += Time.unscaledDeltaTime * pingAnimateSpeed;
                yield return null;
            }
            // Pong!
            t = 0f;
            while(targetGraphic.color != startColor)
            {
                Color32 newColor = Color32.Lerp(endColor, startColor, t);
                targetGraphic.color = newColor;
                t += Time.unscaledDeltaTime * pongAnimateSpeed;
                yield return null;
            }
        }
        while(doLoop);
    }

    private IEnumerator AnimateRecolour (Color32 targetColor, float speed)
    {
        Color32 startColor = targetGraphic.color;
        float t = 0f;
        while(targetGraphic.color != targetColor)
        {
            Color32 newColor = Color32.Lerp(startColor, targetColor, t);
            targetGraphic.color = newColor;
            t += Time.unscaledDeltaTime * speed;
            yield return null;
        }
    }

    private void AssignColoursFromPreset ()
    {
        switch(colourPreset)
        {
            case ColourPreset.None:
                break;
            case ColourPreset.DarkRedHighlight:
                hoverColor = new Color32(85, 35, 0, 255);
                pressedColor = new Color32(120, 30, 10, 255);
                selectColor = new Color32(60, 20, 0, 255);
                break;
            case ColourPreset.BlackToYellow_Transparent:
                hoverColor = new Color32(100, 100, 100, 128);
                pressedColor = new Color32(185, 165, 75, 128);
                selectColor = new Color32(120, 120, 50, 128);
                break;
            case ColourPreset.BlackYellowBlack:
                hoverColor = new Color32(50, 50, 50, 255);
                pressedColor = new Color32(100, 80, 35, 255);
                selectColor = new Color32(0, 0, 0, 255);
                break;
            case ColourPreset.YellowHighlight:
                hoverColor = new Color32(255, 240, 190, 255);
                pressedColor = new Color32(255, 225, 110, 255);
                selectColor = new Color32(255, 220, 125, 255);
                break;
            case ColourPreset.BlackBlue_Transparent:
                hoverColor = new Color32(70, 70, 70, 128);
                pressedColor = new Color32(0, 100, 150, 128);
                selectColor = new Color32(0, 70, 100, 128);
                break;
            case ColourPreset.WhiteBlue:
                hoverColor = new Color32(160, 204, 255, 255);
                pressedColor = new Color32(85, 170, 245, 255);
                selectColor = new Color32(70, 140, 225, 255);
                break;
            case ColourPreset.WhiteHighlight:
                hoverColor = new Color32(255, 255, 255, 180);
                pressedColor = new Color32(255, 255, 255, 255);
                selectColor = new Color32(255, 255, 255, 225);
                break;
            case ColourPreset.WhiteHighlight_Transparent:
                hoverColor = new Color32(255, 255, 255, 3);
                pressedColor = new Color32(255, 255, 255, 5);
                selectColor = new Color32(255, 255, 255, 2);
                break;
            case ColourPreset.DarkGreenHighlight:
                hoverColor = new Color32(0, 75, 25, 255);
                pressedColor = new Color32(45, 130, 65, 255);
                selectColor = new Color32(0, 60, 20, 255);
                break;
            case ColourPreset.GreenHighlight:
                hoverColor = new Color32(115, 190, 120, 255);
                pressedColor = new Color32(120, 230, 125, 255);
                selectColor = new Color32(115, 190, 120, 255);
                break;
            case ColourPreset.RedHighlight:
                hoverColor = new Color32(225, 80, 75, 255);
                pressedColor = new Color32(255, 75, 65, 255);
                selectColor = new Color32(225, 80, 75, 255);
                break;
            case ColourPreset.BlueHighlight:
                hoverColor = new Color32(75, 140, 225, 255);
                pressedColor = new Color32(85, 170, 245, 255);
                selectColor = new Color32(75, 140, 225, 255);
                break;
            case ColourPreset.BlackHighlight:
                hoverColor = new Color32(60, 60, 60, 255);
                pressedColor = new Color32(90, 90, 90, 255);
                selectColor = new Color32(0, 0, 0, 255);
                break;
            case ColourPreset.DarkBlueHighlight:
                hoverColor = new Color32(0, 50, 90, 255);
                pressedColor = new Color32(0, 90, 150, 255);
                selectColor = new Color32(0, 50, 90, 255);
                break;
            default:
                Debug.LogError("Missing a definition for the ColourPreset: '" + colourPreset + "'");
                break;
        }
    }

}