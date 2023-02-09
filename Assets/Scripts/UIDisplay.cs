using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    Health playerHealth;
    ScoreKeeper scoreKeeper;
    float initialHealth;
    float currentHealth;
    [SerializeField] Slider healthBar;
    [SerializeField] TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        playerHealth = GameObject.Find("player").GetComponent<Health>();
    }
    void Start()
    {
        initialHealth = playerHealth.GetHealth();
    }

    void Update()
    {
        HealthBarChange();
        ShowScore();
    }

    void HealthBarChange()
    {
        currentHealth = playerHealth.GetHealth();
        float healthBarRatio = currentHealth / initialHealth;
        healthBar.value = healthBarRatio;
    }

    void ShowScore()
    {
        scoreText.text = scoreKeeper.GetScore().ToString("0000000");
    }
}
