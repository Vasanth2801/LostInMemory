using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Singleton Instance")]
    public static GameManager Instance;

    [Header("Graphics Settings")]
    [SerializeField] private int qualityIndex;
    [SerializeField] private bool isFullScreen;

    [Header("Resolution Settings")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    [Header("Save Settings")]
    [SerializeField] private GameObject saveDataFile;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " X " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int _qualityIndex)
    {
        qualityIndex = _qualityIndex;
    }

    public void SetFullScreen(bool _isFullScreen)
    {
        isFullScreen = _isFullScreen;
    }

    public void ApplyGraphics()
    {
        PlayerPrefs.SetInt("QualityIndex", qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);

        PlayerPrefs.SetInt("IsFullScreen", isFullScreen ? 1 : 0);
        Screen.fullScreen = isFullScreen;
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}