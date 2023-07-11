using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class State<T>
    {
        public delegate void DelegateNoArg();

        public DelegateNoArg OnEnter;
        public DelegateNoArg OnExit;
        public DelegateNoArg OnUpdate;
        public DelegateNoArg OnFixedUpdate;
        public string Name { get; set; }
        
        public T ID { get; private set; }

        public State(T id)
        {
            ID = id;
        }

        public State(T id, string name) : this(id)
        {
            Name = name;
        }

        public State(T id, DelegateNoArg onEnter, DelegateNoArg onExit = null, DelegateNoArg onUpdate = null,
            DelegateNoArg onFixedUpdate = null) : this(id)
        {
            OnEnter = onEnter;
            OnExit = onExit;
            OnUpdate = onUpdate;
            OnFixedUpdate = onFixedUpdate;
        }
        
        public State(T id, string name, DelegateNoArg onEnter, DelegateNoArg onExit = null, DelegateNoArg onUpdate = null,
            DelegateNoArg onFixedUpdate = null) : this(id, name)
        {
            OnEnter = onEnter;
            OnExit = onExit;
            OnUpdate = onUpdate;
            OnFixedUpdate = onFixedUpdate;
        }

        virtual public void Enter()
        {
            OnEnter?.Invoke();
        }

        virtual public void Exit()
        {
            OnExit?.Invoke();
        }

        virtual public void Update()
        {
            OnUpdate?.Invoke();
        }

        virtual public void FixedUpdate()
        {
            OnFixedUpdate?.Invoke();
        }
        
        
    }
}
