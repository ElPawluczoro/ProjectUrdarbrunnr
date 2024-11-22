using Scripts.WorldObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Scripts.BehaviourTree.ActionNodes
{
    internal class LookForFood : ActionNode
    {
        private Vector3 destinantion;
        [SerializeField] private float speed = 1;
        [SerializeField] private float eatCooldown = 1;
        private float timeScienceEat;
        private FoodPile foodPile;

        protected override void OnStart()
        {
            timeScienceEat = Mathf.Infinity;

            FindFood();

            state = State.RUNNING;
        }

        private void GoToPosition(Vector3 pos)
        {
            destinantion = pos;
            blackboard.animator.SetBool("Walking", true);
            if (blackboard.transform.position.x < destinantion.x)
            {
                blackboard.transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else
            {
                blackboard.transform.rotation = new Quaternion(0, 180, 0, 0);
            }
        }

        protected override void OnStop()
        {
            
        }

        protected override State OnUpdate()
        {
            if (blackboard.transform.position != destinantion)
            { 
                blackboard.transform.position = Vector3.MoveTowards(blackboard.transform.position, destinantion, speed * Time.deltaTime);
                return state;
            }
            
            if (blackboard.animator.GetBool("Walking"))
            {
                blackboard.animator.SetBool("Walking", false);
            }

            if(timeScienceEat >= eatCooldown)
            {
                foodPile.Eat();
                blackboard.characterStatus.AddHunger(foodPile.Nutrition);
                timeScienceEat = 0;
            }

            if(foodPile == null)
            {
                FindFood();
            }

            if(blackboard.characterStatus.Hunger >= 80)
            {
                state = State.SUCCESS;
            }
            

            timeScienceEat += Time.deltaTime;

            return state;

        }

        public void FindFood()
        {
            List<FoodPile> foodPiles = GameObject.FindObjectsOfType<FoodPile>().ToList();
            if (foodPiles.Count == 0)
            {
                state = State.FALIURE;
                return;
            }
            else
            {
                foodPile = GetClosestFoodPile(foodPiles);
                GoToPosition(foodPile.transform.position);
            }
        }

        public FoodPile GetClosestFoodPile(List<FoodPile> fp)
        {
            FoodPile closestPile = fp[0];
            float distance = Vector3.Distance(blackboard.transform.position, closestPile.transform.position);
            if (fp.Count == 1) return closestPile;
            foreach (FoodPile p in fp) 
            {
                float newDistance = Vector3.Distance(blackboard.transform.position, p.transform.position);
                if (newDistance < distance)
                {
                    distance = newDistance;
                    closestPile = p;
                }
            }

            return closestPile;

        }
    }
}
