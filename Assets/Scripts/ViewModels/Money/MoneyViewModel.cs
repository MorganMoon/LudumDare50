using LudumDare50.Client.Data;
using LudumDare50.Client.Game;
using LudumDare50.Client.ViewModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MoneyViewModel : ViewModel
{
    private readonly IInventory _inventory;

    private float _moneyCount;
    public float MoneyCount
    {
        get => _moneyCount;
        set
        {
            if(_moneyCount == value)
            {
                return;
            }

            _moneyCount = value;
            OnPropertyChanged();
        }
    }

    [Inject]
    public MoneyViewModel(IInventory inventory)
    {
        _inventory = inventory;

        _inventory.OnInventoryUpdated += InInventoryUpdated;
    }

    private void InInventoryUpdated()
    {
        MoneyCount = _inventory.GetItem(InventoryItemType.Money).Count;
    }
}
