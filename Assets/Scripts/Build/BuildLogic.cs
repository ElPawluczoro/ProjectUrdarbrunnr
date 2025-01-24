using Scripts.Buildings;
using Scripts.Controllers;
using Scripts.Player;
using System;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

namespace Scripts.Build
{
    public class BuildLogic : MonoBehaviour
    {
        private GameObject currentlyBuildingObject;

        private MaterialsManager materialsManager;

        private WorkerAssigner assigner;

        private void Start()
        {
            materialsManager = FindObjectOfType<MaterialsManager>();
            assigner = FindObjectOfType<WorkerAssigner>();
        }

        private void Update()
        {
            if (currentlyBuildingObject == null) return;

            var mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentlyBuildingObject.transform.position = new Vector3(mousePosition.x, mousePosition.y, 1);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Input.GetMouseButtonDown(0)) 
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.CompareTag("UI"))
                    {
                        CancelBuilding();
                        return;
                    }
                }

                if (!currentlyBuildingObject.GetComponent<CollisonDetector>().CanBeBuilt) return;
                PlaceBuilding();
            }
            else if(Input.GetMouseButtonDown(1))
            {
                CancelBuilding();
            }
        }

        public void StartBuilding(GameObject obj)
        {
            var newBuilding = Instantiate(obj);
            currentlyBuildingObject = newBuilding;
            currentlyBuildingObject.GetComponent<BoxCollider2D>().isTrigger = true;
            currentlyBuildingObject.AddComponent<CollisonDetector>();
            currentlyBuildingObject.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            SpriteRenderer spriteRenderer = currentlyBuildingObject.GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.green;
            spriteRenderer.sortingOrder = 75;
        }

        public void PlaceBuilding()
        {
            Materials material = Materials.WOOD;
            int cost = 0;

            int woodCost = currentlyBuildingObject.GetComponent<Workplace>().WoodCost;
            int plankCost = currentlyBuildingObject.GetComponent<Workplace>().PlankCost;
            if (woodCost > 0)
            {
                material = Materials.WOOD;
                cost = woodCost;
            }
            else if (plankCost > 0)
            {
                material = Materials.PLANK;
                cost = plankCost;
            }

            if (!materialsManager.RemoveMaterialIfIsEnough(cost, material)) return;
            SpriteRenderer spriteRenderer = currentlyBuildingObject.GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.white;
            currentlyBuildingObject.GetComponent<BoxCollider2D>().isTrigger = false;
            Destroy(currentlyBuildingObject.GetComponent<CollisonDetector>());
            Destroy(currentlyBuildingObject.GetComponent<Rigidbody2D>());
            assigner.AddWorkplace(currentlyBuildingObject.GetComponent<Workplace>());
            currentlyBuildingObject.GetComponent<Workplace>().OnPlace();
            currentlyBuildingObject = null;
            spriteRenderer.sortingOrder = 50;
        }

        public void CancelBuilding()
        {
            Destroy(currentlyBuildingObject);
            currentlyBuildingObject = null;
        }

    }
}
