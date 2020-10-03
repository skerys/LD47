﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(OverlapTrigger))]
public class Button : MonoBehaviour
{

    public event System.Action OnActivate = delegate { };
    public event System.Action OnDeactivate = delegate { };

    OverlapTrigger trigger;

    // number of players required to activate
    [SerializeField] int numOfPlayerReq = 0; // number of players required to activate


    private int playersOnButton = 0;

    SpriteRenderer sprite;

    [Header("Sprites")]
    [SerializeField] Sprite activated;
    [SerializeField] Sprite deactivated;

    [Header("Audio Clips")]
    [SerializeField] AudioClip onActivate;
    [SerializeField] AudioClip onDeactivate;
    
    AudioSource audio;

    private void Awake()
    {
        trigger = GetComponent<OverlapTrigger>();
        sprite = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
        trigger.OnTriggerEnter += Enter;
        trigger.OnTriggerExit += Exit;
    }
        
    void Enter()
    {
        playersOnButton++;
        if (playersOnButton == numOfPlayerReq)
        {
            sprite.sprite = activated;
            if (onActivate)
            {
                audio.clip = onActivate;
                audio.Play();
            }  
            OnActivate();
        }
    }

    private void Exit()
    {
        playersOnButton--;
        if (playersOnButton < numOfPlayerReq)
        {
            sprite.sprite = deactivated;
            if (onDeactivate)
            {
                audio.clip = onDeactivate;
                audio.Play();
            }
            OnDeactivate();
        }

    }
}