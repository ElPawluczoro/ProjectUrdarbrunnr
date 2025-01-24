using Scripts.UI;
using System; 
using UnityEngine;

namespace Scripts.Player
{ 
    public class MaterialsManager : MonoBehaviour
    {
        private int wood = 0;
        private int plank = 0;
        private int food = 0;

        public int Wood => wood;
        public int Plank => plank;
        public int Food => food;


        private int maxMaterials = 100;

        public delegate void UpdateMaterialCounterUI();
        public static event UpdateMaterialCounterUI updateMaterialCounterUI;

        private void Start()
        {
            AddMaterial(100, Materials.WOOD); //TODO relocate
            AddMaterial(10, Materials.FOOD); //TODO relocate
        }

        public void AddMaterial(int count, Materials material)
        {
            switch (material)
            {
                case Materials.WOOD:
                    wood += count;
                    if (wood > maxMaterials)
                    {
                        wood = maxMaterials;
                    }
                    break;
                case Materials.PLANK:
                    plank += count;
                    if (plank > maxMaterials)
                    {
                        plank = maxMaterials;
                    }
                    break;
                case Materials.FOOD:
                    food += count;
                    if (food > maxMaterials)
                    {
                        food = maxMaterials;
                    }
                    break;
            }

            updateMaterialCounterUI.Invoke();
        }

        public void RemoveMaterial(int count, Materials material)
        {
            switch (material)
            {
                case Materials.WOOD:
                    wood -= count;
                    if (wood < 0)
                    {
                        wood = 0;
                    }
                    break;
                case Materials.PLANK:
                    plank -= count;
                    if (plank < 0)
                    {
                        plank = 0;
                    }
                    break;
                case Materials.FOOD:
                    food -= count;
                    if (food < 0)
                    {
                        food = 0;
                    }
                    break;
            }

            updateMaterialCounterUI.Invoke();
        }

        public bool IsMaterialEnough(int count, Materials material)
        {
            switch (material)
            {
                case Materials.WOOD:
                    if (wood >= count) return true;
                    return false;
                case Materials.PLANK:
                    if (plank >= count) return true;
                    return false;
                case Materials.FOOD:
                    if (food >= count) return true;
                    return false;
            }

            return false;
        }

        public bool RemoveMaterialIfIsEnough(int count, Materials material)
        {
            bool result = IsMaterialEnough(count, material);
            if(result) RemoveMaterial(count, material);
            return result;
        }



    }

    public enum Materials
    {
        WOOD, PLANK, FOOD
    }
}
