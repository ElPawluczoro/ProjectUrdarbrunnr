using Assets.Scripts.BehaviourTree.DecoratorNodes;
using Scripts.BehaviourTree.ActionNodes;
using Scripts.BehaviourTree.CompositeNodes;
using Scripts.Characters;
using Unity.VisualScripting;
using UnityEngine;

namespace Scripts.BehaviourTree
{
    public class BehaviourTreeRunner : MonoBehaviour
    {
        public BehaviourTree tree;
        

        void Start()
        {
            tree = tree.Clone(); //tree.Bind(GetComponent<AiAgent>());
            tree.Bind(transform, GetComponent<Animator>(), GetComponent<CharacterStatus>());
        }

        void Update()
        {
            tree.Update();
        }
    }
}
