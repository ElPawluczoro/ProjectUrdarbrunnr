using Scripts.Build;
using Scripts.Controllers;
using Scripts.Enums;
using System;
using Unity.Burst.CompilerServices;
using UnityEngine;

namespace Scripts.UI
{
    public class MainUIController : MonoBehaviour
    {
        [SerializeField] private GameObject buildPanel;

        [SerializeField] private GameObject foodMaker;
        [SerializeField] private GameObject woodcutter;
        [SerializeField] private GameObject foodcollector;

        private Camera mainCamera;

        private GameObject currentOpenPanel;

        private BuildLogic buildLogic;

        private CharactersManager characterManager;


        private void Start()
        {
            mainCamera = Camera.main;
            buildLogic = FindObjectOfType<BuildLogic>();
            characterManager = FindObjectOfType<CharactersManager>();
        }

        private void Update()
        {
            if (currentOpenPanel != null)
            {
                var mousePosition = Input.mousePosition;
                Ray ray = mainCamera.ScreenPointToRay(mousePosition);
                RaycastHit hit;

                if (!Input.GetMouseButtonDown(0)) return;

                if (Physics.Raycast(ray, out hit))
                {
                    ClosePanelIf(hit);
                }
                else
                {
                    ClosePanel();
                }
            }
        }

        public void ClosePanel()
        {
            currentOpenPanel.SetActive(false);
            currentOpenPanel = null;
        }

        public void OpenBuildPanel()
        {
            if (currentOpenPanel != null)
            {
                ClosePanel();
            }
                    
            currentOpenPanel = buildPanel;
            currentOpenPanel.SetActive(true);
        }

        public void ClosePanelIf(RaycastHit hit)
        {
            if (hit.transform.CompareTag("UI")) return;

            ClosePanel();
        }

        private void ChooseBuildding(Enums.Buildings building)
        {
            switch (building)
            {
                case Enums.Buildings.FOOD_MAKER:
                    buildLogic.StartBuilding(foodMaker);
                    break;
                case Enums.Buildings.WOODCUTTER:
                    buildLogic.StartBuilding(woodcutter);
                    break;
                case Enums.Buildings.FOOD_COLLECTOR:
                    buildLogic.StartBuilding(foodcollector);
                    break;
                default:
                    Debug.LogWarning($"Building: {building} not found");
                    break;
            }

            ClosePanel();
        }

        public void ChooseFoodMaker()
        {
            ChooseBuildding(Enums.Buildings.FOOD_MAKER);
        }

        public void ChooseWoodcutter()
        {
            ChooseBuildding(Enums.Buildings.WOODCUTTER);
        }

        public void ChooseFoodCollector()
        {
            ChooseBuildding(Enums.Buildings.FOOD_COLLECTOR);
        }

        public void AddCharacter()
        {
            characterManager.AddCharacter();
        }

    }
}
