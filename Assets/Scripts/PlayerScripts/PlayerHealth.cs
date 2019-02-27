using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField]
    private Color fullHealthColor, zeroHealthColor, semiFullHealthColor;

    [SerializeField]
    private GameObject deathPanel;

    [SerializeField]
    private int blinkAmount;

    [SerializeField]
    private GameObject player;

    public Image FillImage;
    private Slider Slider;
    private Renderer[] renderers;

    public float StartingHealth = 100f;
    private int currentHealth;
    private int fullHealth;
    private bool playerIsInvincible;

    private void Start()
    {
        Slider = GetComponentInChildren<Slider>();
        renderers = player.GetComponentsInChildren<Renderer>();

        fullHealth = 100;
        currentHealth = fullHealth;

        ChangeHealthColor();
    }

    public void HurtPlayer(int amount)
    {
        if (!playerIsInvincible)
        {
            StartCoroutine(PlayerInvincibility());
            currentHealth -= amount;//take away our health
            ChangeHealthColor();
        }
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
        if (currentHealth > 80)
        {
            FillImage.color = fullHealthColor;
        }
        else if (currentHealth > 30)
        {
            FillImage.color = semiFullHealthColor;
        }
        else if(currentHealth < 30)
        {
            FillImage.color = zeroHealthColor;
        }
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

    private IEnumerator PlayerInvincibility()
    {
        playerIsInvincible = true;

        for (int i = 0; i < blinkAmount; i++)
        {
            ToggleRenderers();
            yield return new WaitForSeconds(.1f);
        }
        MakeSurePlayerIsVisible();

        playerIsInvincible = false;
    }

    void ToggleRenderers()
    {
        foreach(Renderer r in renderers)
        {
            if (r.enabled == true)
                r.enabled = false;
            else if (r.enabled == false)
                r.enabled = true;
        }
    }

    void MakeSurePlayerIsVisible()
    {
        foreach (Renderer r in renderers)
        {
            if (r.enabled == false)
                r.enabled = true;
        }
    }
}
