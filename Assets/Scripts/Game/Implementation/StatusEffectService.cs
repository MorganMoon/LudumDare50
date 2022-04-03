using LudumDare50.Client.Data;
using LudumDare50.Client.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LudumDare50.Client.Game.Implementation
{
    public class StatusEffectService : IStatusEffectService
    {
        private readonly StatusEffectSettings _statusEffectSettings;
        private readonly ISleepService _sleepService;

        [Inject]
        public StatusEffectService(StatusEffectSettings statusEffectSettings, ISleepService sleepService)
        {
            _statusEffectSettings = statusEffectSettings;
            _sleepService = sleepService;
        }

        public void TryApplyEffect(InventoryItemType inventoryItemType)
        {
            switch(inventoryItemType)
            {
                case InventoryItemType.Coffee:
                    TryApply_Coffee();
                    break;
                case InventoryItemType.Money:
                default:
                    return;
            }
        }

        private void TryApply_Coffee()
        {
            var updatedCurrentEnergy = _sleepService.Energy.Current + _statusEffectSettings.CoffeeEnergyBoost;
            if(updatedCurrentEnergy > _sleepService.Energy.Max)
            {
                updatedCurrentEnergy = _sleepService.Energy.Max;
            }
            _sleepService.Energy = new Energy(_sleepService.Energy.Max, updatedCurrentEnergy);
        }
    }
}
