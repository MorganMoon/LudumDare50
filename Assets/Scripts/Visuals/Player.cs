using LudumDare50.Client.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LudumDare50.Client.Visuals
{
    public class Player : MonoBehaviour
    {
        [Inject]
        private ISleepService _sleepService;
        [SerializeField]
        private Animator _playerAnimator;

        private void LateUpdate()
        {
            _playerAnimator.SetBool("IsAsleep", _sleepService.IsAsleep);
        }
    }
}