using UnityEngine;
using System;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float _playerHealth = 10;
    [SerializeField] private float _playerEnergy = 100;
    [SerializeField] private float _playerHunger = 100;
    [SerializeField] private float _speedEnergyRecovery = 0.1f;
    [SerializeField] private float _speedStarvation = 0.005f;

    private float _healthMax = 10;
    private float _energyMax = 100;
    private float _hungerMax = 100;
    private float _startTime = 5;
    private float _currentTime;
    private bool _readyToRecoverEnergy = false;
    private bool _timerIsActive = false;

    private void FixedUpdate()
    {
        PlayerWasteHunger(_speedStarvation);
        EnergyRecovery();
        Timer();
    }

    public void PlayerTakeDamage(float damage)
    {
        _playerHealth -= damage;
        Debug.LogFormat("HP: {0}", _playerHealth);

        if( _playerHealth <= 0)
        {
            Debug.Log("Loh");
        }
    }

    public void PlayerWasteEnergy(float amount)
    {
        _timerIsActive = false;
        _readyToRecoverEnergy = false;
        _currentTime = _startTime;
        _playerEnergy -= amount;
        Debug.LogFormat("Energy: {0}", _playerEnergy);

        if (_playerEnergy < 0)
        {
            _playerEnergy = 0;
        }

        _timerIsActive = true;
    }

    public void PlayerWasteHunger(float amount)
    {
        _playerHunger -= amount;

        if (_playerHunger < 0)
        {
            _playerHunger = 0;
        }
    }

    public void AddPlayerHealth(float amount)
    {
        if (_playerHealth < _healthMax)
            _playerHealth += amount;

        if (_playerHealth > _healthMax)
            _playerHealth = _healthMax;
    }
    public void AddPlayerEnergy(float amount)
    {
        if (_playerEnergy < _energyMax)
            _playerEnergy += amount;

        if (_playerEnergy > _energyMax)
            _playerEnergy = _energyMax;
    }
    public void AddPlayerHunger(float amount)
    {
        if (_playerHunger < _hungerMax)
            _playerHunger += amount;

        if (_playerHunger > _hungerMax)
            _playerHunger = _hungerMax;
    }

    public float GetPlayerHealth()
    {
        return _playerHealth;
    }

    public float GetPlayerEnergy()
    {
        return _playerEnergy;
    }

    public float GetPlayerHunger()
    {
        return _playerHunger;
    }

    private void EnergyRecovery()
    {
        if (_readyToRecoverEnergy && _energyMax > _playerEnergy)
            _playerEnergy += 0.1f;
    }

    private void Timer()
    {
        if (_timerIsActive)
        {
            _currentTime -= Time.deltaTime;

            if (_currentTime <= 0)
            {
                _readyToRecoverEnergy = true;
            }
        }
    }
}

