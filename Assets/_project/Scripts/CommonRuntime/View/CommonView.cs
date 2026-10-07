using TMPro;
using UnityEngine;

namespace Clicker.Common.View
{
    public class CommonView : MonoBehaviour
    {
        [field: SerializeField] private TMP_Text _money;

        public void SetMoney(int value)
            => _money.text = $"Current money: {value}$";
    }
}