using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LudumDare50.Client.ViewModels
{
    public interface IView : IDisposable
    {
        Animator TransitionAnimator { get; }
        Type ViewModelType { get; }
        GameObject GameObject { get; }
    }

    public abstract class View : MonoBehaviour, IView
    {
        public abstract Animator TransitionAnimator { get; }

        public abstract Type ViewModelType { get; }

        public GameObject GameObject => gameObject;

        public abstract void Dispose();
    }

    public abstract class View<T> : View where T : ViewModel
    {
        private readonly Dictionary<string, List<Action<string>>> _stringBindings = new Dictionary<string, List<Action<string>>>();

        private T _viewModel;
        public T ViewModel
        {
            get => _viewModel;
            private set
            {
                if(_viewModel == value)
                {
                    return;
                }

                Dispose();
                _viewModel = value;
                _viewModel.PropertyChanged += ViewModel_PropertyChanged;
                SetBindings();
            }
        }

        public override Type ViewModelType => typeof(T);

        [SerializeField]
        private Animator _animator;
        public override Animator TransitionAnimator => _animator;

        [Inject]
        private void Inject(T viewModel)
        {
            ViewModel = viewModel;
        }

        public override void Dispose()
        {
            _stringBindings.Clear();
            try
            {
                _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
            }
            catch { }
        }

        protected virtual void SetBindings()
        {

        }

        protected void Bind(Action<string> binding, string propertyName)
        {
            if(!_stringBindings.TryGetValue(propertyName, out var bindings))
            {
                bindings = new List<Action<string>>();
            }

            bindings.Add(binding);
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var value = sender.GetType().GetProperty(e.PropertyName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).GetValue(sender);
            if(value is string stringValue && _stringBindings.TryGetValue(e.PropertyName, out var bindings))
            {
                foreach(var binding in bindings)
                {
                    binding?.Invoke(stringValue);
                }
            }
        }
    }
}
