using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Scripts.BehaviourTree.ActionNodes
{
    internal class WaitNode : ActionNode
    {
        public float duration = 1;
        float startTime;

        protected override void OnStart()
        {
            startTime = Time.time;
        }

        protected override void OnStop()
        {
            
        }

        protected override State OnUpdate()
        {
            if(Time.time - startTime > duration)
            {
                return State.SUCCESS;
            }

            return State.RUNNING;
        }
    }
}
