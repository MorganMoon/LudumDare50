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
        private float _selectedStandByTime;

        [SerializeField]
        [Min(0)]
        private float greetWaitTime;
        private float _selectedGreetWaitTime;

        [SerializeField]
        [Min(0)]
        private float watchTime;
        private float _selectedWatchTime;

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

        private void Awake()
        {
            _selectedStandByTime = standByTime;
            _selectedGreetWaitTime = greetWaitTime;
            _selectedWatchTime = watchTime;
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
                if (_elapsedTime >= _selectedStandByTime)
                {
                    _animator.SetTrigger("Approach");
                    _standingBy = false;
                    _selectedStandByTime = standByTime + Random.Range(-0.5f, 0.1f);
                }
            }

            if (_waitingToGreet)
            {
                _elapsedTime += Time.deltaTime;
                if (_elapsedTime >= _selectedGreetWaitTime)
                {
                    _waitingToGreet = false;
                    _animator.SetTrigger("Greet");
                    _selectedGreetWaitTime = greetWaitTime + Random.Range(-0.3f, 0.3f);
                }
            }

            if (_watching)
            {
                _elapsedTime += Time.deltaTime;
                _sleepService.WakeUp();
                if (_elapsedTime >= _selectedWatchTime)
                {
                    _watching = false;
                    _animator.SetTrigger("Leave");
                    _elapsedTime = 0;
                    _standingBy = true;
                    _selectedWatchTime = watchTime + Random.Range(-0.1f, 0.5f);
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

