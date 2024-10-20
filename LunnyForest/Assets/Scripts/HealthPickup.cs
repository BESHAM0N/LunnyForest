using System;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int _healthRestore = 20;
    private AudioSource _audioSource;
    private Vector3 _spinRotationSpeed = new Vector3(0, 180, 0);

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.eulerAngles += _spinRotationSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damaged damaged = collision.GetComponent<Damaged>();

        if (damaged && damaged.Health < damaged.MaxHealth)
        {
            bool wasHealed = damaged.Heal(_healthRestore);
            if (wasHealed)
            {
                if (_audioSource)
                    AudioSource.PlayClipAtPoint(_audioSource.clip, gameObject.transform.position, _audioSource.volume);
                Destroy(gameObject);
            }
        }
    }
}