using Scripts.Characters;
using UnityEngine;

namespace Scripts.BehaviourTree
{
    [System.Serializable]
    public class Blackboard
    {
        public Vector3 moveToPosition;
        public GameObject moveToObject;
        public Transform transform;
        public Animator animator;
        public CharacterStatus characterStatus;
    }
}
