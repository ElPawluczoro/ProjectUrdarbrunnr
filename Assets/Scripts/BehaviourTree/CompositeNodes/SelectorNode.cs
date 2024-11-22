using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.BehaviourTree.CompositeNodes
{
    public class SelectorNode : CompositeNode
    {
        int current;

        protected override void OnStart()
        {
            current = 0;
        }

        protected override void OnStop()
        {
            
        }

        protected override State OnUpdate()
        {
            var child = children[current];
            switch (child.Update())
            {
                case State.RUNNING:
                    return State.RUNNING;
                case State.FALIURE:
                    current++;
                    return State.RUNNING;
                case State.SUCCESS:
                    current++;
                    break;
            }

            return current == children.Count ? State.SUCCESS : State.RUNNING;
        }
    }
}
