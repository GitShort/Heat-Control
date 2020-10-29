using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    GameObject[] Fires;
    GameObject[] Vegetation;

    [SerializeField] GameObject finishedLevelMenu;
    [SerializeField] GameObject lostLevelMenu;
    [SerializeField] TextMeshProUGUI extinguished;
    [SerializeField] GameObject mainGameMenu;
    [SerializeField] GameObject playerHealthUI;
    [SerializeField] GameObject winScreen;

    public int burntTreesCount = 0;
    public int ExtinguishedTreesCount = 0;

    public static GameManager instance = null;

    [SerializeField] float duration = 0f;
    float totalTime;
    PlayerHealth pHealth;

    private void Start()
    {
        finishedLevelMenu.SetActive(false);
        lostLevelMenu.SetActive(false);
        winScreen.SetActive(false);
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            mainGameMenu.SetActive(true);
            playerHealthUI.SetActive(false);
        }
        else
            mainGameMenu.SetActive(false);

        pHealth = GetComponent<PlayerHealth>();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        Fires = GameObject.FindGameObjectsWithTag("Fire");
        Vegetation = GameObject.FindGameObjectsWithTag("Vegetation");

        Timer();
        if (totalTime >= duration)
        {
            if (Vegetation.Length == burntTreesCount || pHealth.health <= 0)
            {
                enableLostLevelMenu();
            }
            else if (Fires.Length <= 0)
            {
                enableFinishedLevelMenu();
            }
            else if (Fires.Length <= 0 && SceneManager.GetActiveScene().buildIndex == 5)
            {
                enableWinMenu();
            }
        }

    }

    void Timer()
    {
        if (totalTime <= duration)
        {
            totalTime += Time.deltaTime;
        }
    }

    void enableFinishedLevelMenu()
    {
        extinguishedTreesMessage();
        finishedLevelMenu.SetActive(true);
    }
    void enableWinMenu()
    {
        winScreen.SetActive(true);
    }
    void enableLostLevelMenu()
    {
        lostLevelMenu.SetActive(true);
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void extinguishedTreesMessage()
    {
        extinguished.text = "You have put out " + ExtinguishedTreesCount.ToString() + " fires!";
    }

    public void closeGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void loseHealth()
    {
        pHealth.health = pHealth.health - 1;
    }
}
