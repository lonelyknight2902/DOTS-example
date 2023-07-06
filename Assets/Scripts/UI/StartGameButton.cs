using System;
using Component;
using CortexDeveloper.ECSMessages.Service;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class StartGameButton : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(StartGame);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(StartGame);
        }

        private void StartGame()
        {
            Debug.Log("Start");
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            MessageBroadcaster.PrepareMessage().AliveForOneFrame().PostImmediate(entityManager, new StartGameCommand());
            _button.gameObject.SetActive(false);
        }
    }
}