using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Scripts.BehaviourTree.ActionNodes
{
    public class GoToPosition : ActionNode
    {
        public float x;
        public float y;
        public float speed;

        private Vector3 destinantion;

        protected override void OnStart()
        {
            destinantion = new Vector3 (x, y, 0);
            blackboard.animator.SetBool("Walking", true);
            if(blackboard.transform.position.x < destinantion.x)
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
            destinantion = Vector3.zero;
            blackboard.animator.SetBool("Walking", false);
        }

        protected override State OnUpdate()
        {
            blackboard.transform.position = Vector3.MoveTowards(blackboard.transform.position, destinantion, speed * Time.deltaTime);
            if(blackboard.transform.position == destinantion)
            {
                return State.SUCCESS;
            }

            return State.RUNNING;
        }
    }
}
