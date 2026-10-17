using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Clicker.Business.View
{
    public class BusinessView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _income;
        [SerializeField] private Button _levelUp;
        [SerializeField] private Button _upgrade1;
        [SerializeField] private Button _upgrade2;
        [SerializeField] private TMP_Text _levelUp1Name;
        [SerializeField] private TMP_Text _upgrade1Name;
        [SerializeField] private TMP_Text _upgrade2Name;
        [SerializeField] private Image _process;
        
        private bool _isLevelUp;
        private bool _isGrade1Up;
        private bool _isGrade2Up;

        public void Awake()
        {
            _levelUp.onClick.AddListener(LevelUpClick);   
            _upgrade1.onClick.AddListener(Upgrade1Click);
            _upgrade2.onClick.AddListener(Upgrade2Click);
        }

        public void OnDestroy()
        {
            _levelUp.onClick.RemoveListener(LevelUpClick);
            _upgrade1.onClick.RemoveListener(Upgrade1Click);
            _upgrade2.onClick.RemoveListener(Upgrade2Click);
        }

        public void SetLevel(int value) => _level.text = "Level: " + value;
        public void SetName(string value) => _name.text = value;
        public void SetIncome(int value) => _income.text = "Income: " + value + "$";
        public void SetInteractableLevelUp(bool value) => _levelUp.interactable = value;
        public void SetInteractableGrade1(bool value) => _upgrade1.interactable = value;
        public void SetInteractableGrade2(bool value) => _upgrade2.interactable = value;
        public void SetNameLevelUp(int cost) => _levelUp1Name.text = $"LevelUp \n Cost: {cost}$";
        public void SetNameGrade1(float cost, float income) 
            => _upgrade1Name.text = $"Grade1 \n Cost: {cost}$, Income: {income}%";
        public void SetNameGrade2(float cost, float income) 
            => _upgrade2Name.text = $"Grade2 \n Cost: {cost}$, Income: {income}%";
        public void SetNameGrade1Bought() =>  _upgrade1Name.text = $"Bought";
        public void SetNameGrade2Bought() =>  _upgrade2Name.text = $"Bought";
        
        public void SetProcess(float value) => _process.fillAmount = value;
        
        public bool ConsumeLevelUpClick()
        {
            if (!_isLevelUp) return false;
            _isLevelUp = false;
            return true;
        }

        public bool ConsumeUpgrade1Click()
        {
            if (!_isGrade1Up) return false;
            _isGrade1Up = false;
            return true;
        }

        public bool ConsumeUpgrade2Click()
        {
            if (!_isGrade2Up) return false;
            _isGrade2Up = false;
            return true;
        }

        private void LevelUpClick() => _isLevelUp = true;
        private void Upgrade1Click() => _isGrade1Up = true;
        private void Upgrade2Click() => _isGrade2Up = true;
    }
}