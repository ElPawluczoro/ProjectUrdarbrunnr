using Scripts.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.BehaviourTree.ActionNodes
{
    public class WorkNode : ActionNode
    {
        private Workplace workplace;

        protected override void OnStart()
        {
            workplace = blackboard.characterStatus.work;
            if (workplace == null) state = State.FALIURE;
            else state = State.RUNNING;
        }

        protected override void OnStop()
        {
            
        }

        protected override State OnUpdate()
        {
            if (state == State.FALIURE) return State.FALIURE;

            state = workplace.Work(blackboard);

            return state;
        }
    }
}
