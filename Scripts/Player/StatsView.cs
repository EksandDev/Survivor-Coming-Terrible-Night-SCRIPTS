using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsView : MonoBehaviour
{
    [SerializeField] private Slider _sliderHealth; 
    [SerializeField] private Slider _sliderEnergy;
    [SerializeField] private Slider _sliderHunger;

    private PlayerStats _playerStats;

    private void Start()
    {
        _playerStats = GetComponent<PlayerStats>();
    }

    private void FixedUpdate()
    {
        _sliderHealth.value = _playerStats.GetPlayerHealth();
        _sliderEnergy.value = _playerStats.GetPlayerEnergy();
        _sliderHunger.value = _playerStats.GetPlayerHunger();
    }
}
