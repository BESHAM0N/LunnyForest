using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthBarText;
    [SerializeField] private Slider _healthSlider;
    private Damaged _playerDamaged;

    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if(player == null)
            Debug.Log("Игрок не найден");
        _playerDamaged = player.GetComponent<Damaged>();
    }

    private void Start()
    {
        _healthSlider.value = CalculatesSliderPercentage(_playerDamaged.Health, _playerDamaged.MaxHealth);
        _healthBarText.text = $"Health: {_playerDamaged.Health}/{_playerDamaged.MaxHealth}";
    }

    private void OnEnable()
    {
        _playerDamaged.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        _playerDamaged.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }
    private float CalculatesSliderPercentage(float playerDamagedHealth, float playerDamagedMaxHealth)
    {
        return playerDamagedHealth / playerDamagedMaxHealth;
    }
    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        _healthSlider.value = CalculatesSliderPercentage(newHealth, maxHealth);
        _healthBarText.text = $"Health: {newHealth}/{maxHealth}";
    }
}
