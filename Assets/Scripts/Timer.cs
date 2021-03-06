﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text uiText;
    [SerializeField] private float maxTimer;
    [SerializeField] private GameObject GameOverScreen;

    private float timer;
    private bool selected = false;

    private Button firstSelected;

    // Start is called before the first frame update
    void Start()
    {
        timer = maxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0.5f)
        {
            timer -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(timer / 60F);
            int seconds = Mathf.FloorToInt(timer - minutes * 60);
            string readableTime = string.Format("{0:0}:{1:00}", minutes, seconds);
            uiText.text = readableTime;
        }

        // Displays Game Over screen when timer goes to 0
        else if(timer <= 0.5f)
        {
            for (int i = 0; i < ReInput.players.playerCount; i++)
            {
                Player player = ReInput.players.Players[i];
                player.controllers.maps.LoadMap(ControllerType.Joystick, player.id, "GameOver", "default", true);
                player.controllers.maps.LoadMap(ControllerType.Joystick, player.id, "default", "default", false);
            }

            GameOverScreen.SetActive(true);

            if(selected == false){
                firstSelected = GameOverScreen.GetComponentsInChildren<Button>()[0];
                firstSelected.Select();
                selected = true;
            }

            uiText.text = "0:00";
            timer = 0.0f;
        }
    }
}
