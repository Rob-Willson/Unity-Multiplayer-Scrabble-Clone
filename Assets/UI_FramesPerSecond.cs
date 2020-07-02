using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Text;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UI_FramesPerSecond : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsText;
    [SerializeField] private int numberOfReadingsToAverage = 30;
    [SerializeField] private List<float> previousXNumberOfReadings = new List<float>();
    [SerializeField] private float averageFPSFromPreviousXReadings;
    [SerializeField] private float averageMsecFromPreviousXReadings;
    [SerializeField] private bool showFPS;

    private float framesPerSecond;
    private float frameDelayMillis;
    private float timeUntilNextTextUpdateMillis;
    private StringBuilder sb = new StringBuilder(50);

    private void Update ()
    {
        if(!showFPS)
        {
            ClearFPSText();
            return;
        }

        framesPerSecond = 1f / Time.unscaledDeltaTime;
        frameDelayMillis = Time.unscaledDeltaTime * 1000f;

        timeUntilNextTextUpdateMillis -= frameDelayMillis;
        if(timeUntilNextTextUpdateMillis > 0)
        {
            return;
        }

        UpdateFPSText();
        timeUntilNextTextUpdateMillis = 500f;
    }

    private void UpdateFPSText ()
    {
        sb.Clear();
        sb.AppendFormat("{0:0.} ({1:0.0} ms)", framesPerSecond, frameDelayMillis);
        previousXNumberOfReadings.Add(framesPerSecond);
        if(previousXNumberOfReadings.Count > numberOfReadingsToAverage)
        {
            previousXNumberOfReadings.RemoveAt(0);
        }

        averageFPSFromPreviousXReadings = 0;
        for(int i = 0; i < previousXNumberOfReadings.Count; i++)
        {
            averageFPSFromPreviousXReadings += previousXNumberOfReadings[i];
        }
        averageFPSFromPreviousXReadings = averageFPSFromPreviousXReadings / previousXNumberOfReadings.Count;
        averageMsecFromPreviousXReadings = 1000f / averageFPSFromPreviousXReadings;

        sb.AppendFormat("\nAVG: {0:0.} ({1:0.0} ms) [{2}]", averageFPSFromPreviousXReadings, averageMsecFromPreviousXReadings, previousXNumberOfReadings.Count);
        fpsText.SetText(sb);
    }

    private void ClearFPSText ()
    {
        if(fpsText.text == "")
        {
            return;
        }
        sb.Clear();
        fpsText.SetText(sb);
    }

}
