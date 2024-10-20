using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FillingInventory : MonoBehaviour
{
    [SerializeField] private GameObject _uiTimer;
    [SerializeField] private TMP_Text _timer;
    [SerializeField] private TMP_Text _hp10;
    [SerializeField] private TMP_Text _hp50;
    [SerializeField] private Animator _animator;
    
    private bool _haveUlt;
    private float _cooldown;
    private bool _isBlock;

    public bool IsBlock
    {
        get => _isBlock;
        set
        {
            _isBlock = value;
            _animator.SetBool(AnimationStrings.isBlock, value);
        }
    }

    private void Start()
    {
        var hp10 = Storage.PlayerInventory.PlayerItems.FirstOrDefault(x => x.Id == "HP10");
        if (hp10 != null)
        {
            _hp10.text = hp10.Amount.ToString();
        }

        var hp50 = Storage.PlayerInventory.PlayerItems.FirstOrDefault(x => x.Id == "HP50");
        if (hp50 != null)
        {
            _hp50.text = hp50.Amount.ToString();
        }

        var ult = Storage.PlayerInventory.PlayerItems.FirstOrDefault(x => x.Id == "SpAttack");
        if (ult != null)
        {
            _haveUlt = true;
            _uiTimer.SetActive(true);
        }
    }

    public IEnumerator BlockUlt()
    {
        _cooldown = 15;
        IsBlock = true;

        while (_cooldown > 0)
        {
            _cooldown -= Time.deltaTime;
            _timer.text = _cooldown.ToString("0");
            yield return null;
        }

        _cooldown = 15;
        IsBlock = false;
    }

    public void ReductionPotions()
    {
        var hp10 = Storage.PlayerInventory.PlayerItems.FirstOrDefault(x => x.Id == "HP10");
        var hp50 = Storage.PlayerInventory.PlayerItems.FirstOrDefault(x => x.Id == "HP50");
        _hp10.text = hp10.Amount.ToString();
        _hp50.text = hp50.Amount.ToString();
    }
    
}