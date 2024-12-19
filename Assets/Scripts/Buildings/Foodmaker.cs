using UnityEngine;
using Blackboard = Scripts.BehaviourTree.Blackboard;
using Node = Scripts.BehaviourTree.Node;

namespace Scripts.Buildings
{
    public class Foodmaker : Workplace
    {
        public override Node.State Work(Blackboard blackboard)
        {
            var destinantion = this.transform.position;
            if (!(Vector3.Distance(blackboard.transform.position, transform.position) <= 1))
            {
                if (blackboard.transform.position.x < destinantion.x)
                {
                    blackboard.transform.rotation = new Quaternion(0, 0, 0, 0);
                }
                else
                {
                    blackboard.transform.rotation = new Quaternion(0, 180, 0, 0);
                }

                blackboard.animator.SetBool("Walking", true);

                blackboard.transform.position = 
                    Vector3.MoveTowards(blackboard.transform.position, destinantion, blackboard.characterStatus.Speed * Time.deltaTime);
                return Node.State.RUNNING;
            }

            blackboard.animator.SetBool("Walking", false);

            return Node.State.SUCCESS;


        }
    }
}
