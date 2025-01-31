using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.BehaviourTree
{
    public abstract class Node : ScriptableObject
    {
        public enum State
        {
            RUNNING, FALIURE, SUCCESS
        }

        [HideInInspector] public State state = State.RUNNING;
        [HideInInspector] public bool started = false;
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;
        [HideInInspector] public Blackboard blackboard;
        //[HideInInspector] public AiAgent agent;
        [TextArea] public string description;

        public State Update()
        {
            if (!started)
            {
                OnStart();
                started = true;
            }

            state = OnUpdate();

            if(state == State.FALIURE || state == State.SUCCESS)
            {
                OnStop();
                started = false;
            }

            return state;
        }

        public virtual Node Clone()
        {
            return Instantiate(this);
        }

        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract State OnUpdate();
    }
}
