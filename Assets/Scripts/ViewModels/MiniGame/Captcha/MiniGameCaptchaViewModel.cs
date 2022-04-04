using Cerberus;
using LudumDare50.Client.States.MiniGame;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LudumDare50.Client.ViewModels.Captcha
{
    public class MiniGameCaptchaViewModel : ViewModel
    {
        private readonly IStateController<MiniGameStateEvent> _miniGameStateController;
        private List<bool> _neededSelections = new List<bool>();

        public List<Toggle> Toggles { get; set; }

        private List<GameObject> _blueSquareGameObjects = new List<GameObject>();
        

        [Inject]
        public MiniGameCaptchaViewModel(IStateController<MiniGameStateEvent> miniGameStateController)
        {
            _miniGameStateController = miniGameStateController;
        }

        public void Startup()
        {
            //Randomize which buttons need to be selected
            if (_blueSquareGameObjects.Count <= 0)
            {
                foreach (var toggle in Toggles)
                {
                    _blueSquareGameObjects.Add(toggle.gameObject.transform.GetChild(toggle.gameObject.transform.childCount - 1).gameObject);
                }
            }
            else
            {
                foreach(var toggle in Toggles)
                {
                    toggle.isOn = false;
                }
                _neededSelections.Clear();
            }

            var random = new System.Random();
            foreach(var square in _blueSquareGameObjects)
            {
                if(random.Next() % 2 == 1)
                {
                    square.SetActive(true);
                    _neededSelections.Add(true);
                }
                else
                {
                    square.SetActive(false);
                    _neededSelections.Add(false);
                }
            }
        }

        public void OnNotARobotButtonPressed(List<bool> playerSelections)
        {
            bool success = true;
            for (int i = 0; i < playerSelections.Count; i++)
            {
                if(_neededSelections[i] != playerSelections[i])
                {
                    success = false;
                }
            }

            if(success)
            {
                _miniGameStateController.TriggerEvent(MiniGameStateEvent.Success);
            }
            else
            {
                Startup();
            }
        }
    }
}
