using System;
using Component;
using CortexDeveloper.ECSMessages.Service;
using TMPro;
using Unity.Entities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseGameButton : MonoBehaviour
    {
        private Button _button;
        private TextMeshProUGUI _text;

    
        private void Awake()
        {
            _button = GetComponent<Button>();
            _text = GetComponentInChildren<TextMeshProUGUI>();
            _button.onClick.AddListener(PauseGame);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(PauseGame);
        }

        private void PauseGame()
        {
            World world = World.DefaultGameObjectInjectionWorld;
            // EntityCommandBufferSystem ecbSystem = world.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
            // EntityCommandBuffer ecb = ecbSystem.CreateCommandBuffer();
            var entityManager = world.EntityManager;
            Debug.Log("Game paused");
            if (_text.text == "Pause")
            {
                _text.text = "Resume";
                MessageBroadcaster.PrepareMessage().AliveForUnlimitedTime().PostImmediate(entityManager, new PauseGameCommand());
                MessageBroadcaster.RemoveAllMessagesWith<ResumeGameCommand>(entityManager);
            }
            else
            {
                _text.text = "Pause";
                MessageBroadcaster.RemoveAllMessagesWith<PauseGameCommand>(entityManager);
                MessageBroadcaster.PrepareMessage().AliveForUnlimitedTime().PostImmediate(entityManager, new ResumeGameCommand());
            }
        }
    }
}