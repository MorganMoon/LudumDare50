using LudumDare50.Client.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace LudumDare50.Client.Infrastructure
{
    public class ViewManager : MonoBehaviour
    {
        [SerializeField]
        private View[] _viewPrefabs;

        private IInstantiator _instantiator;
        private TickableManager _tickableManager;

        private Dictionary<Type, IView> _availableTemplates;
        private Dictionary<Type, IView> AvailableTemplates
        {
            get
            {
                if (_availableTemplates == null)
                {
                    CacheTemplates();
                }
                return _availableTemplates;
            }
        }

        private readonly IDictionary<ViewModel, IView> _instantiatedTemplates = new Dictionary<ViewModel, IView>();
        private readonly HashSet<ViewModel> _activeViewModels = new HashSet<ViewModel>();

        [Inject]
        public void Inject(IInstantiator instantiator, TickableManager tickableManager)
        {
            _instantiator = instantiator;
            _tickableManager = tickableManager;
        }

        public void SetActiveViewModels(IEnumerable<ViewModel> viewModels)
        {
            var viewModelsToRemove = new List<ViewModel>();
            foreach (var activeViewModel in _activeViewModels)
            {
                if(!viewModels.Contains(activeViewModel))
                {
                    viewModelsToRemove.Add(activeViewModel);
                }
            }

            foreach(var viewModelToRemove in viewModelsToRemove)
            {
                _activeViewModels.Remove(viewModelToRemove);
                if(_instantiatedTemplates.TryGetValue(viewModelToRemove, out var view))
                {
                    _instantiatedTemplates.Remove(viewModelToRemove);
                    var animator = view.TransitionAnimator;
                    if(animator != null || !animator.Equals(null))
                    {
                        StartCoroutine(ExitAnimation(animator, view.GameObject, viewModelToRemove));
                    }
                    else
                    {
                        TryRemoveAsTickable(viewModelToRemove);
                        viewModelToRemove.Dispose();
                        Destroy(view.GameObject);
                    }
                }
                else
                {
                    TryRemoveAsTickable(viewModelToRemove);
                    viewModelToRemove.Dispose();
                }
            }

            foreach(var viewModel in viewModels)
            {
                if(viewModel == null)
                {
                    continue;
                }

                if(!_activeViewModels.Contains(viewModel))
                {
                    _activeViewModels.Add(viewModel);
                    TryAddAsTickable(viewModel);
                    var prefab = AvailableTemplates[viewModel.GetType()];
                    var newView = _instantiator.InstantiatePrefabForComponent<View>(prefab.GameObject, transform, new object[] { viewModel });
                    _instantiatedTemplates.Add(viewModel, newView);

                    newView.GameObject.SetActive(true);
                    var animator = newView.TransitionAnimator;
                    if(animator != null && !animator.Equals(null))
                    {
                        animator.SetTrigger("Menu_Enter");
                    }
                }
            }
        }

        private IEnumerator ExitAnimation(Animator animator, GameObject instantiatedTemplate, ViewModel viewModelToRemove)
        {
            animator.SetTrigger("Menu_Exit");
            yield return null;
            while (animator.IsInTransition(0))
            {
                yield return null;
            }

            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length * (1 - animator.GetCurrentAnimatorStateInfo(0).normalizedTime));
            yield return null;
            TryRemoveAsTickable(viewModelToRemove);
            viewModelToRemove.Dispose();
            Destroy(instantiatedTemplate);
        }

        private void TryAddAsTickable(object viewModelInstance)
        {
            if (viewModelInstance is ITickable tickableViewModel)
            {
                _tickableManager.Add(tickableViewModel);
            }
        }

        private void TryRemoveAsTickable(object viewModelInstance)
        {
            if (viewModelInstance is ITickable tickableViewModel)
            {
                _tickableManager.Remove(tickableViewModel);
            }
        }

        private void CacheTemplates()
        {
            _availableTemplates = new Dictionary<Type, IView>();
            foreach (var template in _viewPrefabs)
            {
                var viewModelType = template.ViewModelType;

                if (!_availableTemplates.ContainsKey(viewModelType))
                {
                    _availableTemplates.Add(viewModelType, template);
                }
            }
        }
    }
}