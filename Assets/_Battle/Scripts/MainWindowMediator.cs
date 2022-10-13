using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScripts
{
    internal class MainWindowMediator : MonoBehaviour
    {
        [Header("Player Stats")]
        [SerializeField] private TMP_Text _countMoneyText;
        [SerializeField] private TMP_Text _countHealthText;
        [SerializeField] private TMP_Text _countPowerText;
        [SerializeField] private TMP_Text _countCrimeRateText;

        [Header("Enemy Stats")]
        [SerializeField] private TMP_Text _countPowerEnemyText;

        [Header("Money Buttons")]
        [SerializeField] private Button _addMoneyButton;
        [SerializeField] private Button _minusMoneyButton;

        [Header("Health Buttons")]
        [SerializeField] private Button _addHealthButton;
        [SerializeField] private Button _minusHealthButton;

        [Header("Power Buttons")]
        [SerializeField] private Button _addPowerButton;
        [SerializeField] private Button _minusPowerButton;

        [Header("Crime rate Buttons")]
        [SerializeField] private Button _minusCrimeRateButton;

        [Header("Other Buttons")]
        [SerializeField] private Button _fightButton;
        [SerializeField] private Button _avoidButton;

        private PlayerData _money;
        private PlayerData _heath;
        private PlayerData _power;
        private PlayerData _crimeRate;

        private Enemy _enemy;

        private ValueDataManager _manager;

        private void Start()
        {
            _manager = Resources.Load<ValueDataManager>("ValueDataManager");

            _enemy = new Enemy("Enemy Flappy");

            _money = CreatePlayerData(DataType.Money);
            _heath = CreatePlayerData(DataType.Health);
            _power = CreatePlayerData(DataType.Power);
            _crimeRate = CreatePlayerData(DataType.Crime);

            _crimeRate.Value = _manager.CrimeRate;

            Subscribe();

            _avoidButton.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            DisposePlayerData(ref _money);
            DisposePlayerData(ref _heath);
            DisposePlayerData(ref _power);
            DisposePlayerData(ref _crimeRate);

            Unsubscribe();
        }


        private PlayerData CreatePlayerData(DataType dataType)
        {
            PlayerData playerData = new PlayerData(dataType);
            playerData.Attach(_enemy);

            return playerData;
        }

        private void DisposePlayerData(ref PlayerData playerData)
        {
            playerData.Detach(_enemy);
            playerData = null;
        }


        private void Subscribe()
        {
            _addMoneyButton.onClick.AddListener(IncreaseMoney);
            _minusMoneyButton.onClick.AddListener(DecreaseMoney);

            _addHealthButton.onClick.AddListener(IncreaseHealth);
            _minusHealthButton.onClick.AddListener(DecreaseHealth);

            _addPowerButton.onClick.AddListener(IncreasePower);
            _minusPowerButton.onClick.AddListener(DecreasePower);

            _minusCrimeRateButton.onClick.AddListener(DecreaseCrimeRate);

            _fightButton.onClick.AddListener(Fight);

            _avoidButton.onClick.AddListener(AvoidFight);
        }

        private void Unsubscribe()
        {
            _addMoneyButton.onClick.RemoveListener(IncreaseMoney);
            _minusMoneyButton.onClick.RemoveListener(DecreaseMoney);

            _addHealthButton.onClick.RemoveListener(IncreaseHealth);
            _minusHealthButton.onClick.RemoveListener(DecreaseHealth);

            _addPowerButton.onClick.RemoveListener(IncreasePower);
            _minusPowerButton.onClick.RemoveListener(DecreasePower);

            _minusCrimeRateButton.onClick.RemoveListener(DecreaseCrimeRate);

            _fightButton.onClick.RemoveListener(Fight);

            _avoidButton.onClick.RemoveListener(AvoidFight);
        }


        private void IncreaseMoney() => IncreaseValue(_money);
        private void DecreaseMoney() => DecreaseValue(_money);

        private void IncreaseHealth() => IncreaseValue(_heath);
        private void DecreaseHealth() => DecreaseValue(_heath);

        private void IncreasePower() => IncreaseValue(_power);
        private void DecreasePower() => DecreaseValue(_power);

        private void DecreaseCrimeRate()
        {
            //- 1 Для сравнения условия ==, т.к. в CheckDecreaseValueCondition строго >
            if (CheckDecreaseValueCondition(_money, _manager.ConditionRoDecreaseCrimeRate - 1))
            {
                DecreaseValue(_crimeRate);
                //Временный глупый способ на коленке, чтобы не менять код
                //Единственное решение которое пришло в голову - писать DecreaseValue отдельно для DecreaseMoney
                for (int i = 0; i < _manager.ConditionRoDecreaseCrimeRate; i++)
                {
                    DecreaseValue(_money);
                }
                CheckAvoidFightCondition();
                _enemy.IncreaseKCrime();

            }
        }


        private void IncreaseValue(PlayerData playerData) => AddToValue(playerData);
        private void DecreaseValue(PlayerData playerData) => RemoveFromValue(playerData);

        private void AddToValue(PlayerData playerData)
        {
            playerData.Value += 1;
            ChangeDataWindow(playerData);
        }
        private void RemoveFromValue(PlayerData playerData)
        {
            if (CheckDecreaseValueCondition(playerData, 0))
            {
                playerData.Value -= 1;
                ChangeDataWindow(playerData);
            }
        }
        private bool CheckDecreaseValueCondition(PlayerData curentValue, int conditionValue)
        {
            return curentValue.Value > conditionValue ? true : false;
        }

        private void CheckAvoidFightCondition()
        {
            if (_crimeRate.Value < 3)
            {
                _avoidButton.gameObject.SetActive(true);
            }
        }

        private void ChangeDataWindow(PlayerData playerData)
        {
            int value = playerData.Value;
            DataType dataType = playerData.DataType;
            TMP_Text textComponent = GetTextComponent(dataType);
            textComponent.text = $"Player {dataType:F} {value}";

            int enemyPower = _enemy.CalcPower();
            _countPowerEnemyText.text = $"Enemy Power {enemyPower}";
        }

        private TMP_Text GetTextComponent(DataType dataType) =>
            dataType switch
            {
                DataType.Money => _countMoneyText,
                DataType.Health => _countHealthText,
                DataType.Power => _countPowerText,
                DataType.Crime => _countCrimeRateText,
                _ => throw new ArgumentException($"Wrong {nameof(DataType)}")
            };


        private void Fight()
        {
            int enemyPower = _enemy.CalcPower();
            bool isVictory = _power.Value >= enemyPower;

            string color = isVictory ? "#07FF00" : "#FF0000";
            string message = isVictory ? "Win" : "Lose";

            Debug.Log($"<color={color}>{message}!!!</color>");
        }
        private void AvoidFight()
        {
            Debug.Log("Avoid was Success");
        }
    }
}
