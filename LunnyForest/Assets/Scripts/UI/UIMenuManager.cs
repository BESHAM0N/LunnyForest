using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuManager : MonoBehaviour
{
    [SerializeField] private Button _settingsMenuButton;
    [SerializeField] private Button _exitSettingsMenuButton;
    [SerializeField] private Button _openChallengesButton;
    [SerializeField] private Button _exitChallengesButton;
    [SerializeField] private Button _openShopButton;
    [SerializeField] private Button _exitShopButton;
    [SerializeField] private Button _levelOneButton;
    [SerializeField] private List<GameObject> _starsLevelOne;
    [SerializeField] private Button _levelTwoButton;
    [SerializeField] private List<GameObject> _starsLevelTwo;

    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private Toggle _soundToggle;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _challengesMenu;
    [SerializeField] private LocalDataProvider _localDataProvider;
    [SerializeField] private TMP_Text _moneyValue;
    [SerializeField] private MoneyCounter _moneyCounter;

    [SerializeField] private GameObject _shop;
    private void Start()
    {
        Cursor.visible = true;
        _settingsMenuButton.onClick.AddListener(OpenSettingsMenu);
        _exitSettingsMenuButton.onClick.AddListener(CloseSettingsMenu);
        _openChallengesButton.onClick.AddListener(OpenChallengesMenu);
        _exitChallengesButton.onClick.AddListener(CloseChallengesMenu);
        // _levelOneButton.onClick.AddListener(ToLevelOveScene);
        // _levelTwoButton.onClick.AddListener(ToLevelTwoScene);
        _openShopButton.onClick.AddListener(OpenShop);
        _exitShopButton.onClick.AddListener(CloseShop);
        // _moneyValue.text = _localDataProvider.gameProgresses.Money.ToString();
        _moneyValue.text = Storage.PlayerInventory.Money.ToString();

        foreach (var level in _localDataProvider.gameProgresses.CompletedLevels)
        {
            foreach (var star in level.CollectedStarsId)
            {
                switch (level.LevelNumber)
                {
                    case 1:
                        foreach (var starMenu in _starsLevelOne.Where(starMenu =>
                                     star == starMenu.GetComponent<Star>().Id))
                        {
                            starMenu.SetActive(true);
                        }

                        if (_starsLevelOne.All(x => x.activeSelf) &&
                            !_localDataProvider.gameProgresses.CompletedLevels[0].IsCompleted)
                        {
                            _localDataProvider.gameProgresses.CompletedLevels[0].IsCompleted = true;
                            _moneyCounter.MoneyIncrease(50);
                        }

                        break;
                    case 2:
                        foreach (var starMenu in _starsLevelTwo.Where(starMenu =>
                                     star == starMenu.GetComponent<Star>().Id))
                        {
                            starMenu.SetActive(true);
                        }

                        if (_starsLevelTwo.All(x => x.activeSelf) &&
                            !_localDataProvider.gameProgresses.CompletedLevels[1].IsCompleted)
                        {
                            _localDataProvider.gameProgresses.CompletedLevels[1].IsCompleted = true;
                            _moneyCounter.MoneyIncrease(100);
                        }

                        break;
                }
            }
        }
    }
    private void OpenSettingsMenu()
    {
        _settingsMenu.gameObject.SetActive(true);
    }

    private void CloseSettingsMenu()
    {
        _settingsMenu.gameObject.SetActive(false);
    }

    private void OpenChallengesMenu()
    {
        _challengesMenu.gameObject.SetActive(true);
    }

    private void CloseChallengesMenu()
    {
        _challengesMenu.gameObject.SetActive(false);
    }

    private void OpenShop()
    {
        _shop.gameObject.SetActive(true);
    }

    private void CloseShop()
    {
        _shop.gameObject.SetActive(false);
    }

    // private void ToLevelOveScene()
    // {
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    // }
}