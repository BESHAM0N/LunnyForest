using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
   [SerializeField] private Vector3 _moveSpeed = new Vector3(0, 75, 0);
   [SerializeField] private float _timeToFade = 1f;
   private RectTransform _rectTransform;
   private TextMeshProUGUI _textMeshPro;
   private float _timeElapsed;
   private Color _startColor;

   private void Awake()
   {
      _rectTransform = GetComponent<RectTransform>();
      _textMeshPro = GetComponent<TextMeshProUGUI>();
      _startColor = _textMeshPro.color;
   }
   private void Update()
   {
      _rectTransform.position += _moveSpeed * Time.deltaTime;
      _timeElapsed += Time.deltaTime;
      if (_timeElapsed < _timeToFade)
      {
         var fadeAlpha = _startColor.a * (1 - (_timeElapsed / _timeToFade));
         _textMeshPro.color = new Color(_startColor.r, _startColor.g, _startColor.b, fadeAlpha);
      }
      else
      {
         Destroy(gameObject);
      }
   }
}
