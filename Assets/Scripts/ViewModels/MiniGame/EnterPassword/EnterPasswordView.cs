using UnityEngine;

namespace LudumDare50.Client.ViewModels.MiniGame.EnterPassword
{
    public class EnterPasswordView : View<EnterPasswordViewModel>
    {
        [SerializeField]
        private TMPro.TMP_InputField _inputField;

        [SerializeField]
        private TMPro.TMP_Text _stickyNoteText;

        [SerializeField]
        private GameObject _passwordWrongIndicator;

        protected override void SetBindings()
        {
            _stickyNoteText.text = ViewModel.Password;
            _inputField.onSubmit.AddListener(OnSubmit);
        }

        public void OnSubmit(string submittedText)
        {
            if (submittedText == _stickyNoteText.text)
            {
                ViewModel.OnCorrectSubmission();
                _passwordWrongIndicator.SetActive(false);
            }
            else
            {
                _inputField.ActivateInputField();
                _passwordWrongIndicator.SetActive(true);
            }
        }
    }
}

