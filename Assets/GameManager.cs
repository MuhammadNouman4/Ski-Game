using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject Player;

    [SerializeField] public GameObject MenuPanel, HelpPanel, QuitConfirmation, AreYouSure, BoostButton;
    [SerializeField] public GameObject estEgg1, estEgg2;
    public Button Heading;

    public static GameManager Instance { get; private set; }

    void awake()
    {
        //       instance = this;
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        Player.SetActive(false);
        BoostButton.SetActive(false);
        estEgg1.SetActive(true);
        estEgg2.SetActive(false);

    }

    void Update()
    {
        Heading.onClick.AddListener(CheckInput);
        if (count == 1)
        {
            Debug.Log("Stop");
            CounDownText.SetActive(false);
            count = 0;
            StopAllCoroutines();
            PlayButton();
        }

    }
    [SerializeField] private TextMeshProUGUI CountDownTex;
    [SerializeField] private GameObject CounDownText;
    int count;
    IEnumerator Countdown(int secons)
    {
        count = secons;
        while (count > 0)
        {
            yield return new WaitForSeconds(1);
            CounDownText.SetActive(true);
            CountDownTex.text = count.ToString();
            count--;
        }
        StartGame();
    }

    public void StartGame()
    {
        StartCoroutine(Countdown(4));
    }
    public void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            estEgg1.SetActive(false);
            estEgg2.SetActive(true);
        }

    }
    public void Quit()
    {
        Application.Quit();
    }

    public void MenuOptionsPress()
    {
        Player.SetActive(false);
        BoostButton.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        Player.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PlayButton()
    {
        Player.SetActive(true);
        BoostButton.SetActive(true);
    }

    public void HelpButton()
    {
        HelpPanel.SetActive(true);
    }

    public void closeHelpButton()
    {
        HelpPanel.SetActive(false);
    }

    public void OpenMenuPanel()
    {
        MenuPanel.SetActive(true);
    }

    public void closeMenuPanel()
    {
        MenuPanel.SetActive(false);
    }


    public void QuitConfirm()
    {
        QuitConfirmation.SetActive(true);
    }
    public void CloseQuitConfirm()
    {
        QuitConfirmation.SetActive(false);
    }


    public void AreYouSureQuit()
    {
        AreYouSure.SetActive(true);
    }
    public void closeAreYouSureQuit()
    {
        AreYouSure.SetActive(false);
    }

    public void NectLevel()
    {
        SceneManager.LoadScene(1);
    }



}
