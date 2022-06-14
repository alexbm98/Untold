using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public AudioManager aM;
    public TMP_Text[] Puestos;

    void Awake()
    {
        LoadData();
    }

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        int nivel = PlayerPrefs.GetInt("Level", 1);

        GameObject canvas = GameObject.Find("Canvas");
        ArrayList puntuaciones = new ArrayList();
        ArrayList nombres = new ArrayList();

        int n = PlayerPrefs.GetInt("numPuntuaciones", 0);

        for (int i = 0; i < n; i++)
        {
            puntuaciones.Add(PlayerPrefs.GetInt("puntuacion" + i, 0));
        }

        for (int i = 0; i < n; i++)
        {
            nombres.Add(PlayerPrefs.GetString("nombre" + i, ""));
        }

        ArrayList puntuacionesOrd = new ArrayList(puntuaciones);
        puntuacionesOrd.Sort();
        object[] a = puntuacionesOrd.ToArray();

        float lineaActualX = 336.2749f;
        float lineaActualY = 42.96978f;
        float lineaActualZ = 323.2047f;
        float sepLineaX = 0.0294f;
        float sepLineaY = -1.0756f;
        float sepLineaZ = 0.0068f;
        float espacioX = 2.71f;
        float espacioY = 0.16f;
        float espacioZ = 1.08f;

        int puesto = 1;

        for (int i = a.Length - 1; i >= 0; i--)
        {
            object p = a[i];

            int cont = 0;

            for (int z = 0; z < puntuaciones.Capacity; z++)
            {
                if (puntuaciones[z] == p)
                {
                    break;
                }
                else
                {
                    cont++;
                }
            }

            string nombre = (string)nombres[cont];
            int puntuacion = (int)puntuaciones[cont];
            char[] letras = nombre.ToCharArray();

            if (letras.Length < 10)
            {
                Puestos[puesto - 1].text = puesto + "                 ";

                for (int j = 0; j < 10; j++)
                {
                    if (j < letras.Length)
                    {
                        Puestos[puesto - 1].text += letras[j];
                    }
                    else
                    {
                        Puestos[puesto - 1].text += "  ";
                    }
                }

                Puestos[puesto - 1].text += "               " + puntuacion;
            }
            else
            {
                Puestos[puesto - 1].text = puesto + "                 " + nombre + "               " + puntuacion;
            }

            //Posición
            /*GameObject pos = new GameObject();
            //RectTransform rPosP = Puesto.GetComponent<RectTransform>();
            pos.AddComponent<CanvasRenderer>();
            pos.AddComponent<RectTransform>();
            RectTransform rPos = pos.GetComponent<RectTransform>();
            rPos.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 160f);
            rPos.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 30f);
            rPos.SetPositionAndRotation(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f));
            /*print(rPosP.position.x);
            rPos.Translate(rPosP.position.x, rPosP.position.y, rPosP.position.z);*/
            /*rPos.Rotate(5.76f, 515.437f, 0.234f);
            TextMeshProUGUI tPos = pos.AddComponent<TextMeshProUGUI>();
            pos.transform.Translate(new Vector3(lineaActualX + sepLineaX, lineaActualY + sepLineaY, lineaActualZ + sepLineaZ));
            pos.transform.SetParent(canvas.transform);

            Font fo = new Font("Assets/TextMesh Pro/Examples & Extras/Fonts/Bangers.ttf");
            TMP_FontAsset fontAsset = TMP_FontAsset.CreateFontAsset(fo);

            tPos.font = fontAsset;
            tPos.color = Color.black;
            tPos.fontSize = 18;
            tPos.text = puesto.ToString();*/

            //Nombre
            /*GameObject name = new GameObject();
            name.AddComponent<CanvasRenderer>();
            name.AddComponent<RectTransform>();
            RectTransform r = name.GetComponent<RectTransform>();
            r.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 410.9007f);
            r.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 33.792f);
            r.position = new Vector3(193.5f, -38.404f, 0);
            TextMeshProUGUI t = name.AddComponent<TextMeshProUGUI>();
            name.transform.Translate(new Vector3(lineaActual + sepLineaX + espacioX, lineaActual + sepLineaY + espacioY, lineaActual + sepLineaZ + espacioZ));
            name.transform.SetParent(canvas.transform);

            t.font = fontAsset;
            t.color = Color.black;
            t.fontSize = 18;
            t.text = nombre;

            //Puntuación
            GameObject points = new GameObject();
            points.AddComponent<CanvasRenderer>();
            points.AddComponent<RectTransform>();
            RectTransform rP = points.GetComponent<RectTransform>();
            rP.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 410.9007f);
            rP.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 33.792f);
            rP.position = new Vector3(193.5f, -38.404f, 0);
            TextMeshProUGUI tP = points.AddComponent<TextMeshProUGUI>();
            points.transform.Translate(new Vector3(lineaActual + sepLineaX + espacioX*2, lineaActual + sepLineaY + espacioY*2, lineaActual + sepLineaZ + espacioZ*2));
            points.transform.SetParent(canvas.transform);

            tP.font = fontAsset;
            tP.color = Color.black;
            tP.fontSize = 18;
            tP.text = puntuacion.ToString();*/

            lineaActualX += sepLineaX;
            lineaActualY += sepLineaY;
            lineaActualZ += sepLineaZ;

            puesto++;

            if (puesto > 10)
            {
                break;
            }
        }
    }

    public void LoadData()
    {
        int width = PlayerPrefs.GetInt("widthScreen", 1920);
        int height = PlayerPrefs.GetInt("heightScreen", 1080);
        float musicVolume = PlayerPrefs.GetFloat("musicVolume", 0.5f);
        float soundVolume = PlayerPrefs.GetFloat("soundVolume", 0.5f);

        //Resolución

        Screen.SetResolution(width, height, true);

        //Música y Sonido

        AudioManager.Instance.musicPlayer.volume = musicVolume;
        AudioManager.Instance.soundPlayer.volume = soundVolume;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }
    }
}
