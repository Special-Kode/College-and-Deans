using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skill : MonoBehaviour
{

    public Text name, text;
    public static char idiom;

    // Update is called once per frame
    void Update()
    {
        switch (name.text)
        {
            case "Alejandro":
                if (idiom == 's')
                {
                    text.text = "Diseñador de IU \n" + "Compositor \n" + "Community Manager \n";
                } else
                {
                    text.text = "UI Designer \n" + "Composer \n" + "Community Manager \n";
                }
                break;
            case "Aless":
                if (idiom == 's')
                {
                    text.text = "Programador \n" + "Diseñador de Mecánicas";
                }
                else
                {
                    text.text = "Programmer \n" + "Mechanics Designer";
                }
                break;
            case "Adrián":
                if (idiom == 's')
                {
                    text.text = "Programador Líder\n" + "Diseñador de IU";
                }
                else
                {
                    text.text = "Lead Programmer\n" + "UI Designer";
                }
                break;
            case "Mario":
                if (idiom == 's')
                {
                    text.text = "Diseñador de Niveles \n" + "Artista \n";
                }
                else
                {
                    text.text = "Level Designer \n" + "Artist \n";
                }
                break;
            case "David":
                if (idiom == 's')
                {
                    text.text = "Programador \n" + "Diseñador de IA";
                }
                else
                {
                    text.text = "Programmer \n" + "IA Designer";
                }
                break;
            case "Borja":
                if (idiom == 's')
                {
                    text.text = "Diseñador de Juego y Niveles \n" + "Programador Web ";
                }
                else
                {
                    text.text = "Game and Level Designer \n" + "Web Programmer"; 
                }
                break;
            case "Jonás":
                if (idiom == 's')
                {
                    text.text = "Artista Líder";
                }
                else
                {
                    text.text = "Lead Artist";
                }
                break;
            default:
                break;
        }
    }
}
