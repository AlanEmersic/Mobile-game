using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(Swipe))]
public class CharacterSelection : MonoBehaviour
{
    [SerializeField] List<GameObject> characters;
    [SerializeField] TextMeshProUGUI nameText;

    Swipe swipe;
    int index;

    private void Awake()
    {
        swipe = GetComponent<Swipe>();
    }

    private void Start()
    {
        nameText.text = characters[index].name;

        foreach (GameObject obj in characters)
        {
            obj.SetActive(false);
        }

        characters[index].SetActive(true);
    }

    private void Update()
    {
        if (swipe.SwipeRight)
            Previous();
        if (swipe.SwipeLeft)
            Next();
    }

    public void Next()
    {
        characters[index].SetActive(false);
        index = (index + 1) % characters.Count;
        characters[index].SetActive(true);
        nameText.text = characters[index].name;
    }

    public void Previous()
    {
        characters[index].SetActive(false);
        index--;

        if (index < 0)
            index += characters.Count;

        characters[index].SetActive(true);
        nameText.text = characters[index].name;
    }

    public void Select()
    {
        PlayerPrefs.SetInt("Character", index);
        SceneManager.LoadScene("Menu");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
