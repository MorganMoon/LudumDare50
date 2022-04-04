using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare50.Client.ViewModels.Energy
{
    public class EnergyViewModel : ViewModel<Data.Energy>
    {
        private float _maxEnergy;
        public float MaxEnergy
        {
            get => _maxEnergy;
            set
            {
                if(_maxEnergy == value)
                {
                    return;
                }

                _maxEnergy = value;
                OnPropertyChanged();
            }
        }

        private float _currentEnergy;
        public float CurrentEnergy
        {
            get => _currentEnergy;
            set
            {
                if(_currentEnergy == value)
                {
                    return;
                }

                _currentEnergy = value;
                OnPropertyChanged();
            }
        }

        private bool _canSleep = true;
        public bool CanSleep
        {
            get => _canSleep;
            set
            {
                if(_canSleep == value)
                {
                    return;
                }

                _canSleep = value;
                OnPropertyChanged();
            }
        }

        public override void Prepare(Data.Energy parameter)
        {
            MaxEnergy = parameter.Max;
            CurrentEnergy = parameter.Current;
        }
    }
}