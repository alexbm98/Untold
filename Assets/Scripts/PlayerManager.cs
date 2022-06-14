using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public Camera camera;
    public Image health;
    public Image stamina;
    public Rigidbody player;
    public Animator animatorP;
    private Vector3 offset;
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public float maxStamina = 100f;
    public float currentStamina = 100f;
    private int recargaStamina = 0;
    public int enemigosDerrotados = 0;
    public TMP_Text TEnemigosDerrotados;

    private void Awake()
    {
        Instance = this;
        LoadData();
    }

    // Start is called before the first frame update
    void Start()
    {
        tag = "Player";
        offset = new Vector3(0f, 1.3f, -2.38f);
        SpawnManager.Instance.StartSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject != null)
        {
            HandleCameraMovement();
            HandlePlayerMovement();
            HandleHealth();
            HandleStamina();
            HandleDie();
            HandleScore();
        }
    }

    public void HandleScore()
    {
        TEnemigosDerrotados.text = enemigosDerrotados.ToString();
    }

    public void HandleHealth()
    {
        health.fillAmount = currentHealth / maxHealth;
    }

    public void HandleStamina()
    {
        if (recargaStamina == 0 && currentStamina < maxStamina)
        {
            currentStamina += 0.25f;
        }
        
        if (recargaStamina == 1 && currentStamina > 0)
        {
            currentStamina -= 0.5f;
        }

        stamina.fillAmount = currentStamina / maxStamina;
    }

    public void HandleCameraMovement()
    {
        if (Input.GetKey("a"))
        {
            camera.transform.LookAt(player.transform.position);
            camera.transform.Rotate(-24.5f, 0f, 0f);
            player.transform.Rotate(-0.9f * Vector3.up);
        }

        if (Input.GetKey("d"))
        {
            camera.transform.LookAt(player.transform.position);
            camera.transform.Rotate(-24.5f, 0f, 0f);
            player.transform.Rotate(0.9f * Vector3.up);
        }
    }

    public void HandlePlayerMovement()
    {
        if (!animatorP.GetBool("muere"))
        {
            if (Input.GetKey("w") && !animatorP.GetBool("defender") && !animatorP.GetBool("atacar01"))
            {
                if (Input.GetKey("left shift"))
                {
                    if (currentStamina > 0)
                    {
                        animatorP.SetBool("corre", true);
                        animatorP.SetBool("seMueveAdelante", true);
                        player.transform.Translate(0, 0, 0.05f);
                        recargaStamina = 1;
                    }
                    else
                    {
                        animatorP.SetBool("corre", false);
                        animatorP.SetBool("seMueveAdelante", true);
                        player.transform.Translate(0, 0, 0.025f);
                    }
                }
                else
                {
                    animatorP.SetBool("seMueveAdelante", true);
                    player.transform.Translate(0, 0, 0.025f);
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                animatorP.SetBool("atacar01", true);
                recargaStamina = 2;
            }

            if (Input.GetMouseButtonUp(0))
            {
                animatorP.SetBool("atacar01", false);
                recargaStamina = 0;
            }

            if (Input.GetMouseButtonDown(1))
            {
                animatorP.SetBool("defender", true);
                recargaStamina = 2;
            }

            if (Input.GetMouseButtonUp(1))
            {
                animatorP.SetBool("defender", false);
                recargaStamina = 0;
            }

            if (Input.GetKeyUp("left shift"))
            {
                animatorP.SetBool("corre", false);
                recargaStamina = 0;
            }

            if (Input.GetKeyUp("w"))
            {
                animatorP.SetBool("seMueveAdelante", false);
                recargaStamina = 0;
            }

            if (Input.GetKey("s") && !animatorP.GetBool("defender") && !animatorP.GetBool("atacar01"))
            {
                animatorP.SetBool("seMueveAtras", true);
                player.transform.Translate(0, 0, -0.025f);
                recargaStamina = 0;
            }

            if (Input.GetKeyUp("s"))
            {
                animatorP.SetBool("seMueveAtras", false);
                recargaStamina = 0;
            }
        }
    }

    public void HandleDie()
    {
        if (currentHealth <= 0)
        {
            animatorP.SetBool("muere", true);
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (TurtleShellController.Instance != null)
        {
            if (collision.CompareTag("Enemy") && !animatorP.GetBool("defender") && !animatorP.GetBool("esGolpeado") && !animatorP.GetBool("atacar01") && !TurtleShellController.Instance.animator.GetBool("muere") && currentHealth > 0)
            {
                animatorP.SetBool("esGolpeado", true);
            }
        }
    }

    public void OnTriggerStay(Collider collision)
    {
        if (TurtleShellController.Instance != null)
        {
            if (collision.CompareTag("Enemy") && !animatorP.GetBool("defender") && !animatorP.GetBool("esGolpeado") && !animatorP.GetBool("atacar01") && !TurtleShellController.Instance.animator.GetBool("muere") && currentHealth > 0)
            {
                animatorP.SetBool("esGolpeado", true);
            }
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (TurtleShellController.Instance != null)
        {
            if (collision.CompareTag("Enemy") && !TurtleShellController.Instance.animator.GetBool("muere"))
            {
                animatorP.SetBool("esGolpeado", false);
            }
        }
    }

    public void LoadData()
    {
        int width = PlayerPrefs.GetInt("widthScreen", 1920);
        int height = PlayerPrefs.GetInt("heightScreen", 1080);
        string difficulty = PlayerPrefs.GetString("Difficulty", "Normal");
        float musicVolume = PlayerPrefs.GetFloat("musicVolume", 0.5f);
        float soundVolume = PlayerPrefs.GetFloat("soundVolume", 0.5f);

        //Resolución

        Screen.SetResolution(width, height, true);

        //Dificultad

        switch (difficulty)
        {
            case "Facil":
                currentHealth = 150f;
                maxHealth = 150f;
                currentStamina = 150f;

                break;

            case "Normal":
                currentHealth = 100f;
                maxHealth = 100f;
                currentStamina = 100f;

                break;

            case "Dificil":
                currentHealth = 50f;
                maxHealth = 50f;
                currentStamina = 50f;

                break;
        }

        //Música y Sonido

        AudioManager.Instance.musicPlayer.volume = musicVolume;
        //AudioManager.Instance.soundPlayer.volume = soundVolume;
    }
}