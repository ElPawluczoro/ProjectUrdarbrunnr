using Scripts.BehaviourTree;
using Scripts.BehaviourTree.CommonBehaviour;
using UnityEngine;

namespace Scripts.Buildings
{
    public class Woodcutter : Collector
    {
        private new void Start()
        {
            objectTag = "Tree";
            materialCollected = Player.Materials.WOOD;
            base.Start();
        }
    }
}
