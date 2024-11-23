using System; 
using UnityEngine;

namespace Scripts.Build
{
    public class BuildLogic : MonoBehaviour
    {
        private GameObject currentlyBuildingObject;

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
            currentlyBuildingObject.GetComponent<SpriteRenderer>().color = Color.green;
        }

        public void PlaceBuilding()
        {
            currentlyBuildingObject.GetComponent<SpriteRenderer>().color = Color.white;
            currentlyBuildingObject.GetComponent<BoxCollider2D>().isTrigger = false;
            Destroy(currentlyBuildingObject.GetComponent<CollisonDetector>());
            Destroy(currentlyBuildingObject.GetComponent<Rigidbody2D>());
            currentlyBuildingObject = null;
        }

        public void CancelBuilding()
        {
            Destroy(currentlyBuildingObject);
            currentlyBuildingObject = null;
        }

    }
}
