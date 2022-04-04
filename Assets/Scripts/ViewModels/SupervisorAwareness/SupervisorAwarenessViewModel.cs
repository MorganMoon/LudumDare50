using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare50.Client.ViewModels.SupervisorAwareness
{
    public class SupervisorAwarenessViewModel : ViewModel<Data.SupervisorAwareness>
    {
        private int _maxFailures;
        public int MaxFailures
        {
            get => _maxFailures;
            set
            {
                if (_maxFailures == value)
                {
                    return;
                }

                _maxFailures = value;
                OnPropertyChanged();
            }
        }

        private int _currentFailures;
        public int CurrentFailures
        {
            get => _currentFailures;
            set
            {
                if (_currentFailures == value)
                {
                    return;
                }

                _currentFailures = value;
                OnPropertyChanged();
            }
        }

        public override void Prepare(Data.SupervisorAwareness parameter)
        {
            MaxFailures = parameter.Max;
            CurrentFailures = parameter.Current;
        }
    }
}
