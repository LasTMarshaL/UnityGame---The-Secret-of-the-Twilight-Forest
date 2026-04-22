using System;
using TMPro;
using UnityEngine;
using YG;

public class ShopMenu : MonoBehaviour // This class manages the shop menu.
{
    private PlayerController _player;

    [SerializeField] private TextMeshProUGUI _healthPriseText;
    [SerializeField] private TextMeshProUGUI _damagePriseText;
    [SerializeField] private TextMeshProUGUI _speedPriseText;

    [SerializeField] private TextMeshProUGUI _healthInfoText;
    [SerializeField] private TextMeshProUGUI _damageInfoText;
    [SerializeField] private TextMeshProUGUI _speedInfoText;

    [SerializeField] private TextMeshProUGUI _textOnCoinsButton;
    [SerializeField] private TextMeshProUGUI _textOnHPButton;

    private float _healthPrise = 35;
    private float _damagePrise = 35;
    private float _speedPrise = 35;

    private int regainHealthPrise = 30;

    [SerializeField] private string _rewardIDHP;
    [SerializeField] private string _rewardIDCoins;

    [SerializeField] private GameSaver _gameSaver;

    private void Start()
    {
        _player = PlayerController.playerInstance;
    }
    private void Update()
    {
        if (YG2.lang == "ru") // Define language from web platform using YG2
        {
            if (_player.Stats.CanUpgradeLevel(_player.Stats.HealthLevel))
            {
                _healthInfoText.text = "Здоровье уровень: " + (_player.Stats.HealthLevel + 1);
                _healthPriseText.text = _healthPrise + " монет";
            }
            else
            {
                _healthInfoText.text = "Максимум";
                _healthPriseText.text = "";
            }

            if (_player.Stats.CanUpgradeLevel(_player.Stats.DamageLevel))
            {
                _damageInfoText.text = "Урон уровень: " + (_player.Stats.DamageLevel + 1);
                _damagePriseText.text = _damagePrise + " монет";
            }
            else
            {
                _damageInfoText.text = "Максимум";
                _damagePriseText.text = "";
            }

            if (_player.Stats.CanUpgradeLevel(_player.Stats.SpeedLevel))
            {
                _speedPriseText.text = _speedPrise + " монет";
                _speedInfoText.text = "Скорость уровень: " + (_player.Stats.SpeedLevel + 1);
            }
            else
            {
                _speedInfoText.text = "Максимум";
                _speedPriseText.text = "";
            }

            if (_player.AddsHealthTimer >= 30)
            {
                _textOnHPButton.text = "Смотреть";
            }
            else
            {
                _textOnHPButton.text = ((float)Math.Round(_player.AddsHealthTimer, 1)).ToString() + " / 30";
            }

            if (_player.AddsCoinsTimer >= 30)
            {
                _textOnCoinsButton.text = "Смотреть";
            }
            else
            {
                _textOnCoinsButton.text = ((float)Math.Round(_player.AddsCoinsTimer, 1)).ToString() + " / 30";
            }
        }
        else if (YG2.lang == "en") // Define language from web platform using YG2 
        {
            if (_player.Stats.CanUpgradeLevel(_player.Stats.HealthLevel))
            {
                _healthInfoText.text = "Health level: " + (_player.Stats.HealthLevel + 1);
                _healthPriseText.text = _healthPrise + " coins";
            }
            else
            {
                _healthInfoText.text = "Maximum";
                _healthPriseText.text = "";
            }

            if (_player.Stats.CanUpgradeLevel(_player.Stats.DamageLevel))
            {
                _damageInfoText.text = "Damage level: " + (_player.Stats.DamageLevel + 1);
                _damagePriseText.text = _damagePrise + " coins";
            }
            else
            {
                _damageInfoText.text = "Maximum";
                _damagePriseText.text = "";
            }

            if (_player.Stats.CanUpgradeLevel(_player.Stats.SpeedLevel))
            {
                _speedInfoText.text = "Speed level: " + (_player.Stats.SpeedLevel + 1);
                _speedPriseText.text = _speedPrise + " coins";
            }
            else
            {
                _speedInfoText.text = "Maximum";
                _speedPriseText.text = "";
            }

            if (_player.AddsHealthTimer >= 30)
            {
                _textOnHPButton.text = "Watch";
            }
            else
            {
                _textOnHPButton.text = ((float)Math.Round(_player.AddsHealthTimer, 1)).ToString() + " / 30";
            }

            if (_player.AddsCoinsTimer >= 30)
            {
                _textOnCoinsButton.text = "Watch";
            }
            else
            {
                _textOnCoinsButton.text = ((float)Math.Round(_player.AddsCoinsTimer, 1)).ToString() + " / 30";
            }
        }
    }


    /// <summary>
    /// Attempts to upgrade the player's health level by spending coins and increasing the upgrade cost.
    /// </summary>
    public void HealthImprove()
    {
        if (_player.Wallet.SendCoins((int)_healthPrise))
        {
            _healthPrise += Mathf.Round(_healthPrise * 0.7f);

            _player.Stats.UpgradeHealthLevel();

            _player.Health.LoadMaxHealth(_player.Stats.Health);
            _player.Health.LoadHealth(_player.Health.MaxHealth);

            _gameSaver.SaveLevels();
        }
    }

    /// <summary>
    /// Attempts to upgrade the player's damage level by spending coins and increasing the upgrade cost.
    /// </summary>
    public void DamageImprove()
    {
        if (_player.Wallet.SendCoins((int)_damagePrise))
        {
            _damagePrise += Mathf.Round(_damagePrise * 0.7f);

            _player.Stats.UpgradeDamageLevel();

            _gameSaver.SaveLevels();
        }
    }

    /// <summary>
    /// Attempts to upgrade the player's speed level by spending coins and increasing the upgrade cost.
    /// </summary>
    public void SpeedImprove()
    {
        if (_player.Wallet.SendCoins((int)_speedPrise))
        {
            _speedPrise += Mathf.Round(_speedPrise * 0.7f);

            _player.Stats.UpgradeSpeedLevel();

            _gameSaver.SaveLevels();
        }
    }

    /// <summary>
    /// Provides health points tp player for coins.
    /// </summary>
    public void HealthForCoins()
    {
        if (_player.Wallet.SendCoins(regainHealthPrise))
        {
            _player.Health.GetHealth(25);

            _gameSaver.SaveGamePlayer();
        }
    }

    /// <summary>
    /// Provides coins to player for adds watching (plugin YG2 is used).
    /// </summary>
    public void CoinsForAdds()
    {
        if (_player.AddsCoinsTimer >= 30)
        {
            YG2.RewardedAdvShow(_rewardIDCoins, () =>
            {
               _player.Wallet.GetCoins(25);
            });

            _player.LoadAddsCoinsTimer(0);

            _gameSaver.SaveGamePlayer();
        }
    }

    /// <summary>
    /// Provides health points to player for adds watching (plugin YG2 is used).
    /// </summary>
    public void HealthForAdds()
    {
        if (_player.AddsHealthTimer >= 30)
        {
            YG2.RewardedAdvShow(_rewardIDHP, () =>
            {
                _player.Health.GetHealth(25);
            });

            _player.LoadHealthCoinsTimer(0);

            _gameSaver.SaveGamePlayer();
        }
    }
}
