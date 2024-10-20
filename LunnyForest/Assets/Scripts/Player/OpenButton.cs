using UnityEngine;

public class OpenButton : MonoBehaviour
{
   [SerializeField] private GameObject _openShopButton;
   [SerializeField] private GameObject _openChallengeButton;
   private void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision == null) return;
      _openChallengeButton.SetActive(collision.GetComponent<Challenge>());
      _openShopButton.SetActive(collision.GetComponent<Shop>());
   }
}

