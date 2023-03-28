using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System;

public class AudioManager : MonoBehaviour
{
    public AudioSource BurstAudio;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnEnable()
    {
        Ball.BurstSound += PlayBurstAudio;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBurstAudio()
    {
        BurstAudio.Play();
    }

    private void OnDisable()
    {
        Ball.BurstSound -= PlayBurstAudio;
    }
}
