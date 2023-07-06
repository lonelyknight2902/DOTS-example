using System;
using System.Collections;
using Component;
using TMPro;
using Unity.Entities;
using UnityEngine;

namespace UI
{
    public class Scoring : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _score;
        private Entity _playerEntity;
        private EntityManager _entityManager;

        private void Start()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            // yield return new WaitForSeconds(1f);
            _playerEntity = _entityManager.CreateEntityQuery(typeof(Player)).GetSingletonEntity();
        }

        private void Update()
        {
            var scoreValue = _entityManager.GetComponentData<Player>(_playerEntity).score;
            _score.text = $"Score: {scoreValue}";
        }
    }
}