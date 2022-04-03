using LudumDare50.Client.ViewModels;
using System;
using System.Collections.Generic;
using Zenject;

namespace LudumDare50.Client.Infrastructure.Implementation
{
    public class ScreenService : IScreenService
    {
        private readonly DiContainer _diContainer;
        private readonly Dictionary<Type, ViewModel> _activeViewModels = new Dictionary<Type, ViewModel>();
        private readonly ViewManager _viewManager;

        [Inject]
        public ScreenService(DiContainer diContainer, ViewManager viewManager)
        {
            _diContainer = diContainer;
            _viewManager = viewManager;
        }

        public bool TryGetViewModel<T>(out T viewModel) where T : ViewModel
        {
            if (_activeViewModels.TryGetValue(typeof(T), out var instance))
            {
                viewModel = (T)instance;
                return true;
            }
            viewModel = null;
            return false;
        }

        public void ClearScreen()
        {
            _activeViewModels.Clear();
            UpdateViewManager();
        }

        public void RemoveFromScreen<T>() where T : ViewModel
        {
            _activeViewModels.Remove(typeof(T));
            UpdateViewManager();
        }

        public T AddToScreen<T>() where T : ViewModel
        {
            var viewModelType = typeof(T);
            if(_activeViewModels.TryGetValue(viewModelType, out var viewModel))
            {
                return (T)viewModel;
            }

            var newInstance = Resolve<T>();
            newInstance.Prepare();
            _activeViewModels.Add(viewModelType, newInstance);
            UpdateViewManager();
            return newInstance;
        }

        public T AddToScreen<T, TParameter>(TParameter parameter) where T : ViewModel<TParameter>
        {
            var viewModelType = typeof(T);
            if (_activeViewModels.TryGetValue(viewModelType, out var viewModel))
            {
                ((T)viewModel).Prepare(parameter);
                return (T)viewModel;
            }

            var newInstance = Resolve<T>();
            newInstance.Prepare();
            newInstance.Prepare(parameter);
            _activeViewModels.Add(viewModelType, newInstance);
            UpdateViewManager();
            return newInstance;
        }

        public T SetActiveScreen<T>() where T : ViewModel
        {
            var viewModelType = typeof(T);
            if (_activeViewModels.TryGetValue(viewModelType, out var instance))
            {
                _activeViewModels.Clear();
                _activeViewModels.Add(viewModelType, instance);
                UpdateViewManager();
                return (T)instance;
            }

            _activeViewModels.Clear();
            var newInstance = Resolve<T>();
            newInstance.Prepare();
            _activeViewModels.Add(viewModelType, newInstance);
            UpdateViewManager();
            return newInstance;
        }

        public T SetActiveScreen<T, TParameter>(TParameter parameter) where T : ViewModel<TParameter>
        {
            var viewModelType = typeof(T);
            if (_activeViewModels.TryGetValue(viewModelType, out var instance))
            {
                _activeViewModels.Clear();
                _activeViewModels.Add(viewModelType, instance);
                ((T)instance).Prepare(parameter);
                UpdateViewManager();
                return (T)instance;
            }

            _activeViewModels.Clear();
            var newInstance = Resolve<T>();
            newInstance.Prepare();
            newInstance.Prepare(parameter);
            _activeViewModels.Add(viewModelType, newInstance);
            UpdateViewManager();
            return newInstance;
        }

        public IScreenBuilder SetActiveScreen()
        {
            void OnScreenBuilderBuild(ScreenBuilder screenBuilder)
            {
                _activeViewModels.Clear();
                foreach(var activeViewModel in screenBuilder.ActiveViewModels)
                {
                    var viewModel = activeViewModel.Value;
                    viewModel.Prepare();
                    if (screenBuilder.Parameters.TryGetValue(activeViewModel.Key, out var parameter))
                    {
                        viewModel.GetType().GetMethod(nameof(viewModel.Prepare), System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance, null, new Type[] { parameter.GetType() }, null).Invoke(viewModel, new object[] { parameter });
                    }
                    _activeViewModels.Add(activeViewModel.Key, viewModel);
                }
                UpdateViewManager();
            }

            return new ScreenBuilder(this, OnScreenBuilderBuild);
        }

        private void UpdateViewManager()
        {
            _viewManager.SetActiveViewModels(_activeViewModels.Values);
        }

        private T Resolve<T>() where T : class
        {
            var result = _diContainer.TryResolve<T>();

            if (result == null)
            {
                result = _diContainer.Instantiate<T>();
            }

            return result;
        }

        private class ScreenBuilder : IScreenBuilder
        {
            private readonly ScreenService _screenService;
            private readonly Action<ScreenBuilder> _onBuild;

            public Dictionary<Type, object> Parameters { get; } = new Dictionary<Type, object>();
            public Dictionary<Type, ViewModel> ActiveViewModels { get; } = new Dictionary<Type, ViewModel>();

            public ScreenBuilder(ScreenService screenService, Action<ScreenBuilder> onBuild)
            {
                _screenService = screenService;
                _onBuild = onBuild;
            }

            public void Build()
            {
                _onBuild?.Invoke(this);
            }

            public IScreenBuilder With<T>() where T : ViewModel
            {
                return InternalWith<T>();
            }

            public IScreenBuilder With<T, TParameter>(TParameter parameter) where T : ViewModel<TParameter>
            {
                var viewModelType = typeof(T);
                Parameters[viewModelType] = parameter;
                return InternalWith<T>();
            }

            private IScreenBuilder InternalWith<T>(int priority = int.MaxValue) where T : ViewModel
            {
                var viewModelType = typeof(T);
                if (ActiveViewModels.ContainsKey(viewModelType))
                {
                    return this;
                }

                if (_screenService._activeViewModels.TryGetValue(viewModelType, out var instance))
                {
                    ActiveViewModels[viewModelType] = instance;
                    return this;
                }

                ActiveViewModels[viewModelType] = _screenService.Resolve<T>();
                return this;
            }
        }
    }
}
