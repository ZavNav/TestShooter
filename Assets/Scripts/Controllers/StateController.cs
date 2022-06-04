using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Character;
using Enemy;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class StateController : MonoBehaviour
    {
        public static StateController Instance;
        
        private List<EnemyBehaviour> _enemies = new ();
        private ViewController _view;
        private bool _crossedFinishLine = false;

        private void Awake()
        {
            Instance = this;
            Time.timeScale = 1;
        }

        private void Start()
        {
            Debug.Log($"============\nGame started at {DateTime.Now:dd, MMM, yyy}");
            _view = FindObjectOfType<ViewController>();
        }

        public void AddEnemyToList(EnemyBehaviour enemy)
        {
            _enemies.Add(enemy);
        }
        
        public void RemoveEnemy(EnemyBehaviour enemy)
        {
            _enemies.Remove(enemy);
            Destroy(enemy.gameObject);
            CheckForWin();
        }

        public void CrossTheFinishLine()
        {
            _crossedFinishLine = true;
            CheckForWin();
        }

        private void CheckForWin()
        {
            if (_enemies.Count != 0 || !_crossedFinishLine) return;
            
            _view.ShowResult(true);
            DelayedRestart();
        }

        private async void DelayedRestart()
        {
            await Task.Delay(2000);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void LoseGame(CharacterControl character)
        {
            _view.ShowResult(false);
            character.enabled = false;
            DelayedRestart();
        }
    }
}