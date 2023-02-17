using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessController : MonoBehaviour
{
    private ColorAdjustments _colorAdjustments;
    private CustomEffectComponent _customEffectComponent;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Volume>().profile.TryGet(out _colorAdjustments);
        GetComponent<Volume>().profile.TryGet(out _customEffectComponent);
        _customEffectComponent.intensity.Override(0f);
        _colorAdjustments.saturation.Override(0f);
        TimeStop.TimeStopped += OnTimeStopped;
        TimeStop.TimeStarted += OnTimeStarted;
    }

    void OnDestroy()
    {
        TimeStop.TimeStopped -= OnTimeStopped;
        TimeStop.TimeStarted -= OnTimeStarted;
    }

    private IEnumerator AnimateTimeStop(float timeStopLength, Action RunAfterAnimation)
    {
        float normalizedTime = 0f;
        int direction = 1;
        while (normalizedTime <= 1f)
        {
            normalizedTime += direction * Time.unscaledDeltaTime / timeStopLength;
            float value = Mathf.Clamp(map(normalizedTime, 0f, 1f, 0f, 1.5f), 0, 1.5f);
            _customEffectComponent.intensity.Override(value);
            if (normalizedTime >= 1f)
            {
                _colorAdjustments.saturation.Override(-80f);
                direction *= -1;
                normalizedTime = 1f;
            }

            if (value == 0f) break;
            yield return null;
        }

        RunAfterAnimation();
    }

    private float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }


    private void OnTimeStopped(object sender, EventArgs e)
    {
        StartCoroutine(AnimateTimeStop(1.7f, TimeStop.InvokeStartCountdown));
    }

    private void OnTimeStarted(object sender, EventArgs e)
    {
        StopAllCoroutines();
        _customEffectComponent.intensity.Override(0f);
        _colorAdjustments.saturation.Override(0f);
    }
}
