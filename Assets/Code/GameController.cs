using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] List<GameObject> characters;
    [SerializeField] TextMeshProUGUI textScore;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI gameOverScore;
    [SerializeField] TextMeshProUGUI highScore;
    [SerializeField] GameObject countDown;
    [SerializeField] GameObject coinSound;

    public static bool IsGameOver { get; set; }

    int distance;
    int coinCounter;
    bool isStarted;
    CameraController cameraController;

    private void Awake()
    {
        cameraController = FindObjectOfType<CameraController>();
    }

    private void Start()
    {
        isStarted = false;
        coinCounter = 0;
        int index = PlayerPrefs.GetInt("Character");
        GameObject prefab = Instantiate(characters[index], player);
        prefab.transform.SetParent(player);
        prefab.AddComponent<CharacterMovement>();
        cameraController.SetCamera(prefab);
        StartCoroutine(CountDown());


        gameOverPanel.SetActive(false);
        textScore.gameObject.SetActive(true);
        IsGameOver = false;
    }

    private void Update()
    {
        if (!isStarted)
        {
            player.transform.position = new Vector3(0, player.transform.position.y, -3);
            player.GetChild(0).position = player.transform.position;
        }

        if (IsGameOver)
        {
            GameOver();
        }

        distance = Mathf.RoundToInt(player.GetChild(0).transform.position.z);
        if (distance < 0)
            distance = 0;
        textScore.text = $"Score: {distance + (coinCounter * 10)}";
    }

    IEnumerator CountDown()
    {
        Time.timeScale = 0;
        countDown.SetActive(true);
        float pauseTime = Time.realtimeSinceStartup + 3.2f;

        while (Time.realtimeSinceStartup < pauseTime)
        {
            yield return null;
        }

        isStarted = true;
        countDown.SetActive(false);
        Time.timeScale = 1;
    }

    void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        textScore.gameObject.SetActive(false);        
        gameOverScore.text = textScore.text;

        if (PlayerPrefs.GetInt("Score") < distance)
            PlayerPrefs.SetInt("Score", distance);

        highScore.text = $"High Score: {PlayerPrefs.GetInt("Score", 0)}";        
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync("Game");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void CoinSound()
    {        
        coinSound.GetComponent<AudioSource>().Play();
    }
}
