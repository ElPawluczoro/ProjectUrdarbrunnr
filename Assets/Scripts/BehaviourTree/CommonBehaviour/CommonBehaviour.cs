using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Scripts.BehaviourTree.CommonBehaviour
{
    public static class CommonBehaviour
    {
        public static Node.State GoToPosition(Vector3 destination, Blackboard blackboard)
        {
            if (!(Vector3.Distance(blackboard.transform.position, destination) <= 0.5f))
            {
                if (blackboard.transform.position.x < destination.x)
                {
                    blackboard.transform.rotation = new Quaternion(0, 0, 0, 0);
                }
                else
                {
                    blackboard.transform.rotation = new Quaternion(0, 180, 0, 0);
                }

                blackboard.animator.SetBool("Walking", true);

                blackboard.transform.position =
                    Vector3.MoveTowards(blackboard.transform.position, destination, blackboard.characterStatus.Speed * Time.deltaTime);
                return Node.State.RUNNING;
            }
            return Node.State.SUCCESS;
        }
    }
}
