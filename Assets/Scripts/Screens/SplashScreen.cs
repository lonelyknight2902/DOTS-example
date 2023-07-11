using System;
using FSM;
using FSM.State;
using UnityEngine;

namespace Screens
{
    public class SplashScreen : MonoBehaviour
    {
        public GameObject spriteLogo;
        public AudioClip audioLogo;
        
        public enum SplashStates
        {
            FADE_IN = 0,
            PLAY_AUDIO,
            FADE_OUT,
        }

        private StateMachine<int> m_fsm = new StateMachine<int>();

        private void Start()
        {
            m_fsm.Add((int)SplashStates.FADE_IN, new Fade(m_fsm, this));
            m_fsm.Add((int)SplashStates.PLAY_AUDIO, new PlayAudio(m_fsm, this));
            m_fsm.Add((int)SplashStates.FADE_OUT, new Fade(m_fsm, this, Fade.FadeType.FADE_OUT));
            
            m_fsm.SetCurrentState(m_fsm.GetState((int)SplashStates.FADE_IN));
            Debug.Log("starting");
        }

        private void Update()
        {
            if (m_fsm != null)
            {
                m_fsm.Update();
            }
        }

        public void Exit()
        {
            Debug.Log("Splash screen with FSM has exited");
            m_fsm = null;
        }
        
        
    }
}