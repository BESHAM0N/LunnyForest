using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class LocalDataProvider : MonoBehaviour
{
    public GameProgress gameProgresses;
    public PlayerInventory playerInventory;
    
    private const string _gameProgressFileName = "GameProgress";
    private const string _shopFileName = "Shop";
    private const string _inventoryFileName = "PlayerInventory";
    private const string _saveFileExtension = ".json";
    private string _savePath => Application.dataPath;

    private void Awake()
    {
        LoadGameProgress();
        LoadInventory();
    }

    private string GetFullPath(string fileName)
    {
        string fullPath = Path.Combine(_savePath, $"{fileName}{_saveFileExtension}");
        return fullPath;
    }

    private bool IsDataGPFileExist(string fullPath)
    {
        var result = File.Exists(fullPath);
        if (!result)
        {
            Debug.Log($"Файл по пути {fullPath} не найден, создан пустой файл");
            GenerateFirstFile();
        }
        return result;
    }
    
    private bool IsDataPIFileExist(string fullPath)
    {
        var result = File.Exists(fullPath);
        if (!result)
        {
            Debug.Log($"Файл по пути {fullPath} не найден, создан пустой файл");
            GeneratePlayerInventory();
        }
        return result;
    }

    public void GenerateFirstFile()
    {
        gameProgresses = new GameProgress();
        List<Level> levels = new List<Level>
        {
            new Level
            {
                LevelNumber = 1,
                CollectedStarsId = new List<int>(),
                IsCompleted = false
            },
            new Level
            {
                LevelNumber = 2,
                CollectedStarsId = new List<int>(),
                IsCompleted = false
            }
        };
        gameProgresses.CompletedLevels = new List<Level>(levels);
        SavePlayerProgress();
    }

    public void SavePlayerProgress()
    {
        var serializeObject = JsonConvert.SerializeObject(gameProgresses, Formatting.Indented);
        File.WriteAllText(GetFullPath(_gameProgressFileName), serializeObject);
    }

    private bool LoadGameProgress()
    {
        var path = GetFullPath(_gameProgressFileName);
        if (!IsDataGPFileExist(path))
            return false;

        var jsonText = File.ReadAllText(path);
        if (string.IsNullOrEmpty(jsonText))
        {
            Debug.Log("Файл пустой");
            return false;
        }

        try
        {
            gameProgresses = JsonConvert.DeserializeObject<GameProgress>(jsonText);
        }
        catch (Exception ex)
        {
            Debug.Log($"Произошло исключение при чтении json файла: {ex.Message}");
        }
        return true;
    }
    
    public void SavePlayerInventory()
    {
        var serializeObject = JsonConvert.SerializeObject(Storage.PlayerInventory, Formatting.Indented);
        File.WriteAllText(GetFullPath(_inventoryFileName), serializeObject);
    }

    private bool LoadInventory()
    {
        var path = GetFullPath(_inventoryFileName);
        if (!IsDataPIFileExist(path))
            return false;
    
        var jsonText = File.ReadAllText(path);
        if (string.IsNullOrEmpty(jsonText))
        {
            Debug.Log("Файл пустой");
            return false;
        }
    
        try
        {
            Storage.PlayerInventory = JsonConvert.DeserializeObject<PlayerInventory>(jsonText);
            Debug.Log("Инвентарь загружен");
        }
        catch (Exception ex)
        {
            Debug.Log($"Произошло исключение при чтении json файла: {ex.Message}");
        }
        return true;
    }

    private void GeneratePlayerInventory()
    {
        playerInventory = new PlayerInventory();
        List<OwnedItem> ownedItems = new List<OwnedItem>();
        playerInventory.Money = 30;

        playerInventory.PlayerItems = ownedItems;
        var serializeObject = JsonConvert.SerializeObject(playerInventory, Formatting.Indented);
        File.WriteAllText(GetFullPath(_inventoryFileName), serializeObject);
    }

    // private void GenerateShop()
    // {
    //     shopConteiner = new ShopConteiner();
    //     List<ShopItem> shopItems = new List<ShopItem>
    //     {
    //         new ShopItem
    //         {
    //             Id = "HP10",
    //             BuyPrice = 3,
    //             ItemType = ItemType.Potion
    //         },
    //         new ShopItem
    //         {
    //             Id = "HP50",
    //             BuyPrice = 5,
    //             ItemType = ItemType.Potion
    //         },
    //         new ShopItem
    //         {
    //             Id = "SpAttack",
    //             BuyPrice = 7,
    //             ItemType = ItemType.Ability
    //         },
    //         new ShopItem
    //         {
    //             Id = "Roll",
    //             BuyPrice = 4,
    //             ItemType = ItemType.Ability
    //         },
    //         new ShopItem
    //         {
    //             Id = "Bone",
    //             BuyPrice = 4,
    //             ItemType = ItemType.Trifle
    //         },
    //         new ShopItem
    //         {
    //             Id = "Apple",
    //             BuyPrice = 5,
    //             ItemType = ItemType.Trifle
    //         },
    //     };
    //
    //     shopConteiner.ShopItems = shopItems;
    //     var serializeObject = JsonConvert.SerializeObject(shopConteiner, Formatting.Indented);
    //     File.WriteAllText(GetFullPath(_shopFileName), serializeObject);
    // }
}
