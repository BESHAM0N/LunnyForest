using TMPro;
using UnityEngine;

public class MoneyCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _count;
    [SerializeField] private LocalDataProvider _localDataProvider;

    private void Start()
    {
        // _count.text = _localDataProvider.gameProgresses.Money.ToString();
        _count.text = Storage.PlayerInventory.Money.ToString();
    }
    public void MoneyIncrease(int money)
    {
        var currentMoney = Storage.PlayerInventory.Money;
        currentMoney += money;
        Storage.PlayerInventory.Money = currentMoney;
        _localDataProvider.SavePlayerProgress();
        _localDataProvider.SavePlayerInventory();
        ChangeMoneyCount();
    }

    public void ChangeMoneyCount()
    {
        _count.text = Storage.PlayerInventory.Money.ToString();
    }
}
