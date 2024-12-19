using Scripts.BehaviourTree;
using Scripts.Characters;
using System;
using System.Collections.Generic;
using UnityEngine;
using Blackboard = Scripts.BehaviourTree.Blackboard;

namespace Scripts.Buildings
{
    public abstract class Workplace : MonoBehaviour
    {
        [SerializeField] protected int maxAssignee = 1;

        [SerializeField] protected List<CharacterStatus> workers = new List<CharacterStatus>();

        public virtual void AssignWorker(CharacterStatus character)
        {
            if (workers.Count >= maxAssignee) 
            {
                Debug.LogError($"Something was trying to assign worker to full workplace: {this}");
                return;
            }

            if(character.work != null)
            {
                character.work.UnasignWorker(character);
            }

            workers.Add(character);
            character.AssignToWork(this);
        }

        public virtual void UnasignWorker(CharacterStatus character) 
        {
            workers.Remove(character);
            character.work = null;
        }

        public abstract Node.State Work(Blackboard blackboard);
    }
}
