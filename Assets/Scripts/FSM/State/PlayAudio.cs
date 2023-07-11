using Screens;
using UnityEngine;

namespace FSM.State
{
    public class PlayAudio : State<int>
    {
        public float Duration { get; set; } = 5.0f;
        private float deltaTime = 0.0f;
        private SplashScreen m_splash;
        private StateMachine<int> m_fsm;

        public PlayAudio(StateMachine<int> stateMachine, SplashScreen splashScreen) : base((int)SplashScreen.SplashStates.PLAY_AUDIO)
        {
            m_splash = splashScreen;
            m_fsm = stateMachine;
        }

        public override void Enter()
        {
            deltaTime = Time.deltaTime;
            m_splash.GetComponent<AudioSource>().PlayOneShot(m_splash.audioLogo);
            base.Enter();
            Debug.Log("Entering: PlayAudio State");
        }

        public override void Update()
        {
            deltaTime += Time.deltaTime;
            if (deltaTime > Duration)
            {
                int nextId = (int)SplashScreen.SplashStates.FADE_OUT;
                Debug.Log("done playing");
                m_splash.GetComponent<AudioSource>().Stop();
                State<int> nextState = m_fsm.GetState(nextId);
                m_fsm.SetCurrentState(nextState);
            }
        }
    }
}