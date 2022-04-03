using UnityEngine;

namespace LudumDare50.Client.ViewModels.MiniGame.EnterPassword
{
    public class EnterPasswordView : View<EnterPasswordViewModel>
    {
        public TMPro.TMP_InputField inputField;

        public void OnSubmit()
        {
            Debug.Log("PASSWORD SUBMITTED!");
            ViewModel.OnSubmit(inputField.text);
        }
    }
}

