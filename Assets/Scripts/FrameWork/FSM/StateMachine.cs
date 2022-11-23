using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FrameWork.FSM
{
    public class StateMachine
    {
        public StateID ActivatedStateId { get { return activatedState.Id; } }
        public string ActivatedStateName { get { return activatedState.ToString(); } }

        Dictionary<StateID, IState> states = new Dictionary<StateID, IState>();
        IState activatedState;


        public void Switch(StateID id)
        {
            activatedState?.Exit();
            activatedState = states[id];
            activatedState.Enter();
        }

        public void Add(IState state)
        {
            if(states.ContainsKey(state.Id))
            {
                Debug.LogWarning(state + " Fail to Add State");
                return; 
            }
            states.Add(state.Id, state);
        }

        public void Update()
        {
            activatedState?.Execute();
        }
    }
}