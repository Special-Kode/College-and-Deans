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
                    text.text = "Dise�ador de IU \n" + "Compositor \n" + "Community Manager \n";
                } else
                {
                    text.text = "UI Designer \n" + "Composer \n" + "Community Manager \n";
                }
                break;
            case "Aless":
                if (idiom == 's')
                {
                    text.text = "Programador \n" + "Dise�ador de Mec�nicas";
                }
                else
                {
                    text.text = "Programmer \n" + "Mechanics Designer";
                }
                break;
            case "Adri�n":
                if (idiom == 's')
                {
                    text.text = "Programador L�der\n" + "Dise�ador de IU";
                }
                else
                {
                    text.text = "Lead Programmer\n" + "UI Designer";
                }
                break;
            case "Mario":
                if (idiom == 's')
                {
                    text.text = "Dise�ador de Niveles \n" + "Artista \n";
                }
                else
                {
                    text.text = "Level Designer \n" + "Artist \n";
                }
                break;
            case "David":
                if (idiom == 's')
                {
                    text.text = "Programador \n" + "Dise�ador de IA";
                }
                else
                {
                    text.text = "Programmer \n" + "IA Designer";
                }
                break;
            case "Borja":
                if (idiom == 's')
                {
                    text.text = "Dise�ador de Juego y Niveles \n" + "Programador Web ";
                }
                else
                {
                    text.text = "Game and Level Designer \n" + "Web Programmer"; 
                }
                break;
            case "Jon�s":
                if (idiom == 's')
                {
                    text.text = "Artista L�der";
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
