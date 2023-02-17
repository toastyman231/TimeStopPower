using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class TimeStop : MonoBehaviour
{
    public static event EventHandler TimeStopped;

    public static event EventHandler StartCountdown;

    public static event EventHandler TimeStarted;

    [SerializeField] private float timeLimit;

    [SerializeField] private float coolDown;

    [SerializeField] private Slider powerSlider;

    [SerializeField] private AudioClip stopSound;

    [SerializeField] private AudioClip startSound;

    private AudioSource _audioSource;

    private bool _canStopTime = true;

    private bool _timeStopped;

    // Start is called before the first frame update
    void Start()
    {
        TimeStopped += ToggleTime;
        TimeStarted += ToggleTime;
        StartCountdown += OnCountdownStart;

        _audioSource = GetComponent<AudioSource>();
    }

    void OnDestroy()
    {
        TimeStopped -= ToggleTime;
        TimeStarted -= ToggleTime;
        StartCountdown -= OnCountdownStart;
    }

    public void OnStopTime()
    {
        if (!_canStopTime) return;
        
        if (!_timeStopped) StopTime(); else StartTime();
    }

    private void ToggleTime(object sender, EventArgs e)
    {
        Time.timeScale = 1 - Time.timeScale;
        _timeStopped = !_timeStopped;

        if (!_timeStopped)
        {
            Debug.Log("Time started!");
            _audioSource.clip = startSound;
            _audioSource.Play();
            _canStopTime = false;
            StopAllCoroutines();
            powerSlider.value = 0f;
            powerSlider.interactable = false;
            StartCoroutine(StartTimeLimit(coolDown, () =>
            {
                _canStopTime = true;
                powerSlider.value = 1f;
                powerSlider.interactable = true;
            }, true));
        }
        else
        {
            Debug.Log("Time stopped!");
            _audioSource.clip = stopSound;
            _audioSource.Play();
        }
    }

    private void OnCountdownStart(object sender, EventArgs e)
    {
        // Start time stop time limit, then restart time when it runs out
        StartCoroutine(StartTimeLimit(timeLimit, StartTime));
    }

    private IEnumerator StartTimeLimit(float timeLimit, Action RunAfterTimeLimit, bool reverseBar = false)
    {
        float normalizedTime = 0f;
        while (normalizedTime <= 1f)
        {
            powerSlider.value = reverseBar ? normalizedTime : 1 - normalizedTime;
            normalizedTime += Time.unscaledDeltaTime / timeLimit;
            yield return null;
        }

        RunAfterTimeLimit();
    }

    private static void StopTime()
    {
        TimeStopped?.Invoke(null, EventArgs.Empty);
    }

    private static void StartTime()
    {
        TimeStarted?.Invoke(null, EventArgs.Empty);
    }

    public static void InvokeStartCountdown()
    {
        StartCountdown?.Invoke(null, EventArgs.Empty);
    }
}
