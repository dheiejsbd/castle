using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FrameWork.FSM
{
    public class StateMachine
    {
        public int ActivatedStateId { get { return activatedState.Id; } }
        public string ActivatedStateName { get { return activatedState.ToString(); } }

        Dictionary<int, IState> states = new Dictionary<int, IState>();
        IState activatedState;


        public void Switch(int id)
        {
            Debug.Log("Change " + id);
            activatedState?.Exit();
            activatedState = states[id];
            activatedState.Enter();
        }

        public void Add(IState state)
        {
            states.Add(state.Id, state);
        }

        public void Update()
        {
            activatedState?.Execute();
        }
    }
}