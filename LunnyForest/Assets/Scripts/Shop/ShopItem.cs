using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public string Id;
    public int BuyPrice;
    public ItemType ItemType;

    [SerializeField] private ShopController _shopController;
    [SerializeField] private Button _buyButton;

    private void Start()
    {
        _buyButton.onClick.AddListener(SendItem);
        HideBuyButton();
    }

    private void HideBuyButton()
    {
        switch (Id)
        {
            case "Roll" when Storage.PlayerInventory.PlayerItems.Any(x=>x.Id == "Roll"):
            case "SpAttack" when Storage.PlayerInventory.PlayerItems.Any(x=>x.Id == "SpAttack"):
                gameObject.GetComponent<Button>().interactable = false;
                _buyButton.interactable = false;
                break;
        }
    }
    private void SendItem()
    {
        Debug.Log("Пробую купить");
        var tryBuy = _shopController.Buy(gameObject.GetComponent<ShopItem>());
        if (tryBuy)
        {
            HideBuyButton();
        }
    }
}
