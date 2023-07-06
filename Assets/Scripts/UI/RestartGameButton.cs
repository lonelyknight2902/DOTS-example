using Unity.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class RestartGameButton : MonoBehaviour
    {
        private Button _button;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(RestartGame);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(RestartGame);
        }

        private void RestartGame()
        {
            Debug.Log("Restart game");
            // World world = World.DefaultGameObjectInjectionWorld;
            // var entityManager = world.EntityManager;
            // entityManager.DestroyEntity(entityManager.UniversalQuery);
            // SceneManager.LoadSceneAsync("Scenes/SampleScene");
        }
    }
}