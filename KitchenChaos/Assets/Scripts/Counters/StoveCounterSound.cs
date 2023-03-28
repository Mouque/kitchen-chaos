using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{

    public static StoveCounterSound Instance { get; private set; }

    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;
    private float warningSoundTimer;
    private bool playWarningSound;


    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        audioSource.volume = SoundManager.Instance.GetVolume();
        KitchenGameManager.Instance.OnGamePaused += KitchenGameManager_OnGamePaused;
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        bool soundWasPlayingBeforePause = stoveCounter.GetState() == StoveCounter.State.Frying || stoveCounter.GetState() == StoveCounter.State.Fried;
        if (soundWasPlayingBeforePause)
        {
            audioSource.Play();
        }
    }

    private void KitchenGameManager_OnGamePaused(object sender, EventArgs e)
    {
        audioSource.Pause();
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangeEventArgs e)
    {
        float burnShowProgressAmount = 0.5f;
        playWarningSound = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;

        if (playSound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }


    }

    public void ChangeVolume()
    {
        audioSource.volume = SoundManager.Instance.GetVolume();
    }

    private void Update()
    {
        if (playWarningSound)
        {
            warningSoundTimer -= Time.deltaTime;

            if (warningSoundTimer <= 0f)
            {
                float warningSoundTimerMax = .2f;
                warningSoundTimer = warningSoundTimerMax;

                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }

    }
}
