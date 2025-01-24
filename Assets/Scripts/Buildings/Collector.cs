using Scripts.BehaviourTree;
using Scripts.BehaviourTree.CommonBehaviour;
using Scripts.Player;
using UnityEngine;

namespace Scripts.Buildings
{
    public abstract class Collector : Workplace
    {
        private GameObject[] trees;
        private GameObject closestTree;

        private bool chopped = false;

        protected string objectTag = "";
        protected Materials materialCollected;

        protected override void Start()
        {
            base.Start();
            trees = GameObject.FindGameObjectsWithTag(objectTag);
            //closestTree = FindClosestTree();
        }

        public override void OnPlace()
        {
            closestTree = FindClosestTree();
        }

        public override Node.State Work(Blackboard blackboard)
        {
            if (chopped)
            {
                if (CommonBehaviour.GoToPosition(transform.position, blackboard) == Node.State.RUNNING)
                {
                    return Node.State.RUNNING;
                }
            }
            else if (CommonBehaviour.GoToPosition(closestTree.transform.position, blackboard) == Node.State.RUNNING)
            {
                return Node.State.RUNNING;
            }

            blackboard.animator.SetBool("Walking", false);

            if (!chopped)
            {
                if (workStartTime == 0)
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
                        chopped = true;
                        return Node.State.RUNNING;
                    }
                    return Node.State.RUNNING;
                }
                return Node.State.RUNNING;
            }

            materialsManager.AddMaterial(1, materialCollected);
            chopped = false;
            return Node.State.SUCCESS;

        }

        public GameObject FindClosestTree()
        {
            GameObject closest = trees[0];
            float distance = Mathf.Infinity;

            foreach (var tree in trees)
            {
                float currentDisctance = Vector3.Distance(this.transform.position, tree.transform.position);
                if (currentDisctance < distance)
                {
                    closest = tree;
                    distance = currentDisctance;
                }
            }

            return closest;
        }

    }
}
