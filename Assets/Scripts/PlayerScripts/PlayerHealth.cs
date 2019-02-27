﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField]
    private Color FullHealthColor;

    [SerializeField]
    private Color ZeroHealthColor;

    [SerializeField]
    private GameObject deathPanel;

    public Image FillImage;
    private Slider Slider;

    public float StartingHealth = 100f;
    private int currentHealth;
    private int fullHealth;

    private void Start()
    {
        Slider = GetComponentInChildren<Slider>();

        fullHealth = 100;
        currentHealth = fullHealth;

        ChangeHealthColor();
    }

    public void HurtPlayer(int amount)
    {
        currentHealth -= amount;//take away our health
        ChangeHealthColor();
        Debug.Log("OW! Player was hurt. Health left: " + currentHealth);

    }

    private void Update()
    {
        if (PlayerIsDead())
        {
            DoDeath();
        }
    }

    private void ChangeHealthColor()
    {
        Slider.value = currentHealth;
        FillImage.color = Color.Lerp(ZeroHealthColor, FullHealthColor, currentHealth / 100);//change the color depending on how much health we have
    }

    private bool PlayerIsDead()
    {
        if (currentHealth <= 0)
            return true;          
        else
            return false;
    }

    private void DoDeath()
    {
        Time.timeScale = 0f;
        deathPanel.gameObject.SetActive(true);
    }
}