using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LudumDare50.Client.Game
{
    public class Coworker : MonoBehaviour
    {
        [SerializeField]
        [Min(0)]
        private float standByTime;

        [SerializeField]
        [Min(0)]
        private float greetWaitTime;

        [SerializeField]
        [Min(0)]
        private float watchTime;

        private Animator _animator;

        private ISleepService _sleepService;

        private float _elapsedTime;

        private bool _standingBy;
        private bool _waitingToGreet;
        private bool _watching;

        [Inject]
        private void Inject(ISleepService sleepService)
        {
            _sleepService = sleepService;
        }

        void Start()
        {
            _animator = GetComponent<Animator>();
            _standingBy = true;
        }

        void Update()
        {
            if (_standingBy)
            {
                _elapsedTime += Time.deltaTime;
                if (_elapsedTime >= standByTime)
                {
                    _animator.SetTrigger("Approach");
                    _standingBy = false;
                }
            }

            if (_waitingToGreet)
            {
                _elapsedTime += Time.deltaTime;
                if (_elapsedTime >= greetWaitTime)
                {
                    _waitingToGreet = false;
                    _animator.SetTrigger("Greet");
                }
            }

            if (_watching)
            {
                _elapsedTime += Time.deltaTime;
                _sleepService.WakeUp();
                if (_elapsedTime >= watchTime)
                {
                    _watching = false;
                    _animator.SetTrigger("Leave");
                    _elapsedTime = 0;
                    _standingBy = true;
                }
            }
        }

        public void StartWaitingToGreet()
        {
            _waitingToGreet = true;
            _elapsedTime = 0;
        }

        public void StartWatching()
        {
            _watching = true;
            _elapsedTime = 0;
        }
    }
}

