using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private GameObject pauseMenu;
    public static bool isPaused = false;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void SaveData()
    {
        if(DataManager.instance != null)
        {
            DataManager.instance.SaveFromJson();
        }
        else
        {
            Debug.LogWarning("DataManager instance not found!");
        }
    }

    public void LoadData()
    {
        if(DataManager.instance != null)
        {
            DataManager.instance.LoadFromJson();
            Debug.Log("Data Loaded via UIManager");
        }
        else
        {
            Debug.LogWarning("DataManager instance not found!");
        }
    }

    public void DeleteData()
    {
        if(DataManager.instance != null)
        {
            DataManager.instance.DeleteFromJson();
        }
        else
        {
            Debug.LogWarning("DataManager instance not found!");
        }
    }
   
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}