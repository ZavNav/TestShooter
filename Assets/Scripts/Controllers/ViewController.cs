using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class ViewController : MonoBehaviour
    {
        [SerializeField] private GameObject resultPanel;
        
        public void ShowResult(bool won)
        {
            Debug.Log("Player " + (won ? "won" : "loose"));
            Time.timeScale = 0;
            resultPanel.SetActive(true);
            resultPanel.transform.GetChild(0).GetComponent<Text>().text = won ? "You win!" : "You lose :(";
        }
    }
}