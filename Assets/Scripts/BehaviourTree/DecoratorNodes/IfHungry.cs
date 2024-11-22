using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.BehaviourTree.DecoratorNodes
{
    public class IfHungry : DecoratorNode
    {
        public int minHunger = 25;

        protected override void OnStart()
        {
            if(blackboard.characterStatus.Hunger <= minHunger)
            {
                state = State.SUCCESS;
            }
            else
            {
                state = State.FALIURE;
            }
        }

        protected override void OnStop()
        {
           
        }

        protected override State OnUpdate()
        {
            if (state == State.SUCCESS || state == State.RUNNING)
            {
                state = child.Update();
            }
            return state;
        }
    }
}
