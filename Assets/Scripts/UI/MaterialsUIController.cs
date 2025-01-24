using Scripts.Player;
using System;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class MaterialsUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text woodCounter;
        [SerializeField] private TMP_Text plankCounter;
        [SerializeField] private TMP_Text foodCounter;

        private MaterialsManager materialsManager;

        private void Start()
        {
            materialsManager = FindObjectOfType<MaterialsManager>();

            UpdateUI();

            MaterialsManager.updateMaterialCounterUI += UpdateUI;
        }

        public void UpdateUI()
        {
            woodCounter.text = materialsManager.Wood.ToString();
            plankCounter.text = materialsManager.Plank.ToString();
            foodCounter.text = materialsManager.Food.ToString();
        }


    }
}
