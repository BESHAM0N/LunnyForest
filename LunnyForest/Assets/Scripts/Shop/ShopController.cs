using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] private LocalDataProvider _localDataProvider;
    [SerializeField] private MoneyCounter _moneyCounter;

    public bool Buy(ShopItem shopItem)
    {
        if (Storage.PlayerInventory.Money < shopItem!.BuyPrice)
        {
            Debug.Log("Нужно больше золота");
            return false;
        }

        Spend(shopItem!.BuyPrice);
        AddItemInInventory(shopItem);
        _moneyCounter.ChangeMoneyCount();
        return true;
    }

    private void AddItemInInventory(ShopItem shopItem)
    {
        var itemFromInventory = Storage.PlayerInventory.PlayerItems.FirstOrDefault(x => x.Id == shopItem.Id);
        if (itemFromInventory == null)
        {
            var ownedItem = new OwnedItem
            {
                Id = shopItem.Id,
                Amount = 1,
            };
            Storage.PlayerInventory.PlayerItems.Add(ownedItem);
        }
        else
        {
            itemFromInventory.Amount++;
        }

        _localDataProvider.SavePlayerInventory();
    }

    private void Spend(int coins)
    {
        if (coins < 0)
        {
            Debug.Log("Стоймость товара меньше 0");
            return;
        }

        Storage.PlayerInventory.Money -= coins;
    }
}