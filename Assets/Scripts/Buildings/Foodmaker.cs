using Scripts.BehaviourTree.ActionNodes;
using Scripts.BehaviourTree.CommonBehaviour;
using Scripts.WorldObjects;
using UnityEngine;
using Blackboard = Scripts.BehaviourTree.Blackboard;
using Node = Scripts.BehaviourTree.Node;

namespace Scripts.Buildings
{
    public class Foodmaker : Workplace
    {
        [SerializeField] private GameObject foodCrate;

        private int crates = 0;
        private int maxCrates = 3;

        private GameObject leftCrate;
        private GameObject middleCrate;
        private GameObject rightCrate;

        private bool spent;

        public override Node.State Work(Blackboard blackboard)
        {
            if (crates == maxCrates) return Node.State.FALIURE;
            if (!spent)
            {
                if (!materialsManager.RemoveMaterialIfIsEnough(1, Player.Materials.FOOD)) return Node.State.FALIURE;
                else spent = true;
            }

            var destination = this.transform.position;

            if(CommonBehaviour.GoToPosition(destination, blackboard) == Node.State.RUNNING)
            {
                return Node.State.RUNNING;
            }

            blackboard.animator.SetBool("Walking", false);
            if(workStartTime == 0)
            {
                workStartTime = Time.time;
            }

            if (Time.time - workStartTime > workTime)
            {
                workProgress += 1;
                workStartTime = Time.time;

                if (workProgress >= workMaxProgress)
                {
                    workProgress = 0;
                    workStartTime = 0;

                    var newFoodCrate = Instantiate(foodCrate);

                    float x = 0;
                    if (middleCrate == null) 
                    {
                        x = 0;
                        middleCrate = newFoodCrate;
                    }
                    else if(leftCrate == null)
                    {
                        x = -0.5f;
                        leftCrate = newFoodCrate;
                    }
                    else if(rightCrate == null)
                    {
                        x = 0.5f;
                        rightCrate = newFoodCrate;
                    }

                    newFoodCrate.transform.position = transform.position + new Vector3(x, -0.5f, 0);
                    newFoodCrate.GetComponent<FoodPile>().SetFoodMaker(this);
                    crates++;
                    spent = false;

                    return Node.State.SUCCESS;
                }

                return Node.State.RUNNING;
            }

            return Node.State.RUNNING;

        }

        public override void OnPlace()
        {
            throw new System.NotImplementedException();
        }

        public void ReduceCrates()
        {
            crates--;
        }
    }
}
