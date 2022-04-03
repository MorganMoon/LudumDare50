using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LudumDare50.Client.ViewModels.Money
{
    public class MoneyView : View<MoneyViewModel>
    {
        [SerializeField]
        private TMP_Text _moneyText;

        protected override void SetBindings()
        {
            _moneyText.text = "$0";
            Bind<float>((moneyCount) => _moneyText.text = $"${moneyCount}", nameof(ViewModel.MoneyCount));
        }
    }
}
