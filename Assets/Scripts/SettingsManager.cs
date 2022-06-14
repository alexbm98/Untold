using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    public TMPro.TMP_Dropdown resolution;
    public TMPro.TMP_Dropdown health;
    public Toggle tPrincipiante;
    public Toggle tNormal;
    public Toggle tDificil;
    public Slider sMusica;
    public Slider sSonido;
    public Button bGuardar;
    public Button bResetear;

    public void Awake()
    {
        LoadData();
    }

    public void Update()
    {
        HandleExit();
    }

    public void HandleResolution()
    {
        switch (resolution.value)
        {
            case 0:

                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;

            case 1:

                Screen.SetResolution(1280, 720, Screen.fullScreen);
                break;

            case 2:

                Screen.SetResolution(640, 480, Screen.fullScreen);
                break;
        }
    }

    public void HandleHealth()
    {
        
    }

    public void HandleTP()
    {
        if (tPrincipiante.isOn)
        {
            tNormal.isOn = false;
            tDificil.isOn = false;
        }
    }

    public void HandleTN()
    {
        if (tNormal.isOn)
        {
            tPrincipiante.isOn = false;
            tDificil.isOn = false;
        }
    }

    public void HandleTD()
    {
        if (tDificil.isOn)
        {
            tNormal.isOn = false;
            tPrincipiante.isOn = false;
        }
    }

    public void HandleMusic()
    {
        AudioManager.Instance.musicPlayer.volume = sMusica.value;
    }

    public void HandleSound()
    {
        AudioManager.Instance.soundPlayer.volume = sSonido.value;
    }

    public void HandleSave()
    {
        //Resolución

        PlayerPrefs.SetInt("resolutionDBValue", resolution.value);

        switch (resolution.value)
        {
            case 0:

                PlayerPrefs.SetInt("widthScreen", 1920);
                PlayerPrefs.SetInt("heightScreen", 1080);
                break;

            case 1:

                PlayerPrefs.SetInt("widthScreen", 1280);
                PlayerPrefs.SetInt("heightScreen", 720);
                break;

            case 2:

                PlayerPrefs.SetInt("widthScreen", 640);
                PlayerPrefs.SetInt("heightScreen", 480);
                break;
        }

        //Dificultad

        if (tPrincipiante.isOn)
        {
            PlayerPrefs.SetString("Difficulty", "Facil");
        }
        else if (tNormal.isOn)
        {
            PlayerPrefs.SetString("Difficulty", "Normal");
        }
        else if (tDificil.isOn)
        {
            PlayerPrefs.SetString("Difficulty", "Dificil");
        }

        //Volumen

        PlayerPrefs.SetFloat("musicVolume", sMusica.value);
        PlayerPrefs.SetFloat("soundVolume", sSonido.value);

        //Vidas

        switch (health.value)
        {
            case 0: PlayerPrefs.SetInt("health", 5);
                break;
            case 1: PlayerPrefs.SetInt("health", 4);
                break;
            case 2: PlayerPrefs.SetInt("health", 3);
                break;
            case 3: PlayerPrefs.SetInt("health", 2);
                break;
            case 4: PlayerPrefs.SetInt("health", 1);
                break;
        }

        SceneManager.LoadScene("Title");
    }

    public void HandleReset()
    {
        //Resolución
        resolution.value = 0;

        //Dificultad
        tPrincipiante.isOn = false;
        tNormal.isOn = true;
        tDificil.isOn = false;

        //Volumen
        sMusica.value = 0.5f;
        sSonido.value = 0.5f;

        //Vidas
        health.value = 2;
    }

    public void HandleExit()
    {
        if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene("Title");
        }
    }

    public void LoadData()
    {
        int width = PlayerPrefs.GetInt("widthScreen", 1920);
        int height = PlayerPrefs.GetInt("heightScreen", 1080);
        string difficulty = PlayerPrefs.GetString("Difficulty", "Normal");
        float musicVolume = PlayerPrefs.GetFloat("musicVolume", 0.5f);
        float soundVolume = PlayerPrefs.GetFloat("soundVolume", 0.5f);
        int lifes = PlayerPrefs.GetInt("health", 3);

        //Dropbox Resolución

        Screen.SetResolution(width, height, true);
        resolution.value = PlayerPrefs.GetInt("resolutionDBValue", 1);

        //Toggle Dificultad

        switch (difficulty)
        {
            case "Facil":
                tPrincipiante.isOn = true;
                tNormal.isOn = false;
                tDificil.isOn = false;

                break;

            case "Normal":
                tPrincipiante.isOn = false;
                tNormal.isOn = true;
                tDificil.isOn = false;

                break;

            case "Dificil":
                tPrincipiante.isOn = false;
                tNormal.isOn = false;
                tDificil.isOn = true;

                break;
        }

        //Sliders Música y Sonido

        sMusica.value = musicVolume;
        sSonido.value = soundVolume;
        AudioManager.Instance.musicPlayer.volume = musicVolume;
        AudioManager.Instance.soundPlayer.volume = soundVolume;

        //Dropbox Vidas

        switch (lifes)
        {
            case 1: health.value = 4;
                break;
            case 2: health.value = 3;
                break;
            case 3: health.value = 2;
                break;
            case 4: health.value = 1;
                break;
            case 5: health.value = 0;
                break;
        }
    }
}
