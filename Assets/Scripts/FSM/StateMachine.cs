using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class StateMachine<T>
    {
        protected Dictionary<T, State<T>> mStates;
        protected State<T> mCurrentState;

        public StateMachine()
        {
            mStates = new Dictionary<T, State<T>>();
        }

        public void Add(State<T> state)
        {
            mStates.Add(state.ID, state);
        }

        public void Add(T stateID, State<T> state)
        {
            mStates.Add(stateID, state);
        }

        public State<T> GetState(T stateID)
        {
            if (mStates.ContainsKey(stateID))
            {
                return mStates[stateID];
            }

            return null;
        }

        public void SetCurrentState(State<T> state)
        {
            if (mCurrentState == state)
            {
                return;
            }
            if (mCurrentState != null)
            {
                mCurrentState.Exit();
            }

            mCurrentState = state;
            if (mCurrentState != null)
            {
                mCurrentState.Enter();
            }
        }

        public void Update()
        {
            if (mCurrentState != null)
            {
                mCurrentState.Update();
            }
        }

        public void FixedUpdate()
        {
            if (mCurrentState != null)
            {
                mCurrentState.FixedUpdate();
            }
        }
    }
}