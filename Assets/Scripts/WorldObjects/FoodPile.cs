using Scripts.Buildings;
using System; 
using UnityEngine;

namespace Scripts.WorldObjects
{
    public class FoodPile : MonoBehaviour
    {
        [SerializeField] private int nutrition = 5;
        [SerializeField] private int foodLeft = 10;

        private Foodmaker foodmaker;

        public void SetFoodMaker(Foodmaker foodmaker)
        {
            this.foodmaker = foodmaker;
        }

        public int Nutrition => nutrition;

        public void Eat()
        {
            foodLeft -= 1;
            if(foodLeft <= 0)
            {
                foodmaker.ReduceCrates();
                Destroy(gameObject);
            }
        }
    }
}
