using Scripts.Buildings;

namespace Scripts.Buildings
{
    public class FoodCollector : Collector
    {
        private new void Start()
        {
            objectTag = "bush";
            materialCollected = Player.Materials.FOOD;
            base.Start();
        }
    }
}
