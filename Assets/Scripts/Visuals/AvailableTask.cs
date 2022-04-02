using System;
using Cerberus;
using LudumDare50.Client.Data;
using LudumDare50.Client.Game;
using LudumDare50.Client.States.OfficeState;
using UnityEngine;
using Zenject;

namespace LudumDare50.Client.Visuals
{
    public class AvailableTask : MonoBehaviour
    {
        [Inject]
        private ITaskService _taskService;
        [Inject]
        private IStateController<OfficeStateEvent> _officeStateController;
        [SerializeField]
        private Animator _animator;

        private GameTask _selectedTask;

        private void Awake()
        {
            _taskService.OnTaskAvailabilityChanged += TaskService_OnTaskAvailabilityChanged;
        }

        private void Update()
        {
            if(_selectedTask.Equals(default))
            {
                return;
            }

            if(IsSelectedTaskAvailable())
            {
                _animator.SetBool("IsVisible", true);
            }
            else
            {
                _animator.SetBool("IsVisible", false);
            }
        }

        public void OnClicked()
        {
            if (!IsSelectedTaskAvailable())
            {
                return;
            }

            if(_officeStateController.TriggerEvent(OfficeStateEvent.StartMiniGame))
            {
                _taskService.AcceptTask();
            }
        }

        private bool IsSelectedTaskAvailable()
        {
            var now = DateTime.UtcNow;
            return !_selectedTask.Accepted && _selectedTask.AvailableAt <= now && _selectedTask.AvailableUntil > now;
        }

        private void TaskService_OnTaskAvailabilityChanged()
        {
            _selectedTask = _taskService.GetTask();
        }
    }
}