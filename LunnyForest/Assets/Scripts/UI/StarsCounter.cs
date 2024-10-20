using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarsCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _starsValue;
    [SerializeField] private List<Star> _stars = new();
    [SerializeField] private LocalDataProvider _localDataProvider;
    private int _levelNumber;
    private int _starsCount;

    private void Awake()
    {
        _levelNumber = SceneManager.GetActiveScene().buildIndex - 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Star star = collision.GetComponent<Star>();

        if (star != null)
        {
            _starsCount += 1;
            star.IsCollected = true;
            _starsValue.text = _starsCount.ToString();
            star.gameObject.SetActive(false);
        }

        LevelFinish levelFinish = collision.GetComponent<LevelFinish>();
        if (levelFinish != null)
        {
            UpdateStarsCountInPlayerProgress();
        }
    }

    private void UpdateStarsCountInPlayerProgress()
    {
        var level = _localDataProvider.gameProgresses.CompletedLevels.FirstOrDefault(x =>
            x.LevelNumber == _levelNumber);
        if (level == null) return;

        foreach (var star in _stars.Where(star => star.IsCollected)
                     .Where(star => !level.CollectedStarsId.Contains(star.Id)))
        {
            _localDataProvider.gameProgresses.CompletedLevels.FirstOrDefault(x => x.LevelNumber == _levelNumber)
                .CollectedStarsId.Add(star.Id);
        }

        _localDataProvider.SavePlayerProgress();
        StartCoroutine(SwitchSceneAfterDelay(1f));
    }

    private IEnumerator SwitchSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        }
    }
}