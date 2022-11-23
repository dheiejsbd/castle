using UnityEditor;
using UnityEngine;

namespace FrameWork.FSM
{
    public interface IState
    {
        StateID Id { get; }

        void Enter();
        void Execute();
        void Exit();
    }
}
