using UnityEditor;
using UnityEngine;

namespace FrameWork.FSM
{
    public interface IState
    {
        int Id { get; }

        void Enter();
        void Execute();
        void Exit();
    }
}
