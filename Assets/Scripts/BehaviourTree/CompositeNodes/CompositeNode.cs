using Scripts.BehaviourTree.DecoratorNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Scripts.BehaviourTree.CompositeNodes
{
    public abstract class CompositeNode : Node
    {
        [HideInInspector] public List<Node> children = new List<Node>();

        public override Node Clone()
        {
            CompositeNode node = Instantiate(this);
            node.children = children.ConvertAll(c => c.Clone());
            return node;
        }
    }
}
