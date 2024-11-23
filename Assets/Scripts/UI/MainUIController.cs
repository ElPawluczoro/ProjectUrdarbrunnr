using Scripts.Build;
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

        private Camera mainCamera;

        private GameObject currentOpenPanel;

        private BuildLogic buildLogic;


        private void Start()
        {
            mainCamera = Camera.main;
            buildLogic = FindObjectOfType<BuildLogic>();
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

        private void ChooseBuildding(Buildings building)
        {
            switch (building)
            {
                case Buildings.FOOD_MAKER:
                    buildLogic.StartBuilding(foodMaker);
                    break;
                default:
                    Debug.LogWarning($"Building: {building} not found");
                    break;
            }

            ClosePanel();
        }

        public void ChooseFoodMaker()
        {
            ChooseBuildding(Buildings.FOOD_MAKER);
        }


    }
    }
