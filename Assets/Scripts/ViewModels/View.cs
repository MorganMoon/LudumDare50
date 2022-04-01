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
        private readonly Dictionary<string, List<Action<object>>> _bindings = new Dictionary<string, List<Action<object>>>();

        private T _viewModel;
        public T ViewModel
        {
            get => _viewModel;
            set
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
            _bindings.Clear();
            try
            {
                _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
            }
            catch { }
        }

        protected virtual void SetBindings()
        {

        }

        protected void Bind<PropertyType>(Action<PropertyType> binding, string propertyName)
        {
#if UNITY_EDITOR
            if (!IsPropertyExpectedType<PropertyType>(propertyName))
            {
                Debug.LogError($"Binding to wrong type '{typeof(PropertyType)}' for property {propertyName}");
                return;
            }
#endif

            if (!_bindings.TryGetValue(propertyName, out var bindings))
            {
                bindings = new List<Action<object>>();
                _bindings[propertyName] = bindings;
            }

            binding.Invoke(GetPropertyValue<PropertyType>(propertyName));
            bindings.Add((obj) => binding.Invoke((PropertyType)obj));
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var value = GetPropertyValue<object>(e.PropertyName);
            if(_bindings.TryGetValue(e.PropertyName, out var bindings))
            {
                foreach(var binding in bindings)
                {
                    binding?.Invoke(value);
                }
            }
        }

        private bool IsPropertyExpectedType<PropertyType>(string propertyName)
        {
            var property = ViewModel.GetType().GetProperty(propertyName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if(property == null)
            {
                return false;
            }
            var propertyType = property.PropertyType;
            return propertyType == typeof(PropertyType);
        }

        private PropertyType GetPropertyValue<PropertyType>(string propertyName)
        {
            return (PropertyType)ViewModel.GetType().GetProperty(propertyName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).GetValue(ViewModel);
        }
    }
}
