using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class UnitInfoView : MonoBehaviour
    {
        public const string HP_TEMPLATE = "HP: {0}";
        public const string ARMOR_TEMPLATE = "Armor: {0}";

        [SerializeField] private TMP_Text _hpText;
        [SerializeField] private TMP_Text _armorText;
        [SerializeField] private Slider _healthSlider;

        public TMP_Text HpText => _hpText;

        public TMP_Text ArmorText => _armorText;

        public Slider HealthSlider => _healthSlider;
    }
}