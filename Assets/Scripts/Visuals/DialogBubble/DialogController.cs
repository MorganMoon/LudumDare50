using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LudumDare50.Client.Visuals.Dialog
{
    public class DialogController : MonoBehaviour
    {
        [SerializeField]
        private List<string> DialogOptions = new List<string>();

        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        [Range(0.01f, 0.2f)]
        private float _letterRenderTime = 0.05f;

        private void OnEnable()
        {
            StartCoroutine(GenerateMessage());
        }

        private IEnumerator GenerateMessage()
        {
            var random = new System.Random();

            var messageToDisplay = DialogOptions[random.Next(0, DialogOptions.Count)];

            WaitForSeconds wait = new WaitForSeconds(_letterRenderTime);

            _text.text = "";

            foreach (char letter in messageToDisplay)
            {
                _text.text = _text.text + letter;
                if(letter != ' ')
                    yield return wait;
            }
        }
    }
}
