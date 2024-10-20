using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory
{
    public List<OwnedItem> PlayerItems { get; set; } = new();
    
    private int _money;
       
    public int Money
    {
        get => _money;
        set
        {
            if (value < 0)
            {
                Debug.Log("Количество монет не может быть отрицательным");
                return;
            }
            _money = value;
        }
    }
}
