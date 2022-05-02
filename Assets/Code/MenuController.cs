using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{    
    [SerializeField] List<GameObject> characters;


    private void Start()
    {
        Time.timeScale = 1; 
        int index = PlayerPrefs.GetInt("Character");
        Instantiate(characters[index], new Vector3(-0.76f, 0, -5), Quaternion.Euler(new Vector3(0, 144, 0)));
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Select()
    {
        SceneManager.LoadScene("Characters");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
