using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class UnitInfoView : MonoBehaviour
    {
        public readonly string hpTemplate = "HP: {0}";
        public readonly string armorTemplate = "Armor: {0}";

        [SerializeField] private TMP_Text _hpText;
        [SerializeField] private TMP_Text _armorText;
        [SerializeField] private Slider _healthSlider;

        public TMP_Text HpText => _hpText;

        public TMP_Text ArmorText => _armorText;

        public Slider HealthSlider => _healthSlider;
    }
}