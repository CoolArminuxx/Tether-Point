using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private const float MaxHealth = 100f;
    public float health = MaxHealth;
    public float AlphaValue;
    [SerializeField] OnDeath DeathRef;
    public Image RedPanel;
    public float DecreaseAlpha;

    void Start()
    {
        
    }

    void Update()
    {
        if (health < 100)//If health is less than 100 / Then gradually decrease the alpha on the damage effect panel
        {
            AlphaValue -= Time.deltaTime / DecreaseAlpha;
            RedPanel.GetComponent<CanvasGroup>().alpha = AlphaValue;
        }
        if (AlphaValue <= 0)//If Alpha is less than or equal to 0 / Then Set health back to full and alpha to 0
        {
            AlphaValue = 0;
            RedPanel.GetComponent<CanvasGroup>().alpha = AlphaValue;
            health = 100;
        }
        if (health <= 0)//If health is less than or equal to 0 / Player dies, spawn at active checkpoint
        {
            DeathRef.Checkpoint();
            health = 100;
            AlphaValue = 0;
        }
    }
}
