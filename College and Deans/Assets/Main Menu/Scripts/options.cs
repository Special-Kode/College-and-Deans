using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class options : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{

    GameObject pencil, mainMenu, optionsMenu;
    Button button, s, e;
    Transform posPen, posBut;
    public Text textbutton, title, music, language; 


    // Start is called before the first frame update
    void Start()
    {
        pencil = GameObject.Find("Pencil");
        mainMenu = GameObject.Find("Menu");
        optionsMenu = GameObject.Find("Options");
        button = this.GetComponent<Button>();
        s = GameObject.Find("Spanish").GetComponent<Button>();
        s.onClick.AddListener(spanish);
        e = GameObject.Find("English").GetComponent<Button>();
        e.onClick.AddListener(english);
        posPen = pencil.transform;
        posBut = button.transform;
        traduce();
        optionsMenu.SetActive(false);
    }

    private void spanish()
    {
        skill.idiom = "s";
    }

    private void english()
    {
        skill.idiom = "e";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pencil.transform.parent = posBut;
        pencil.transform.localPosition = Vector3.zero;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    void traduce()
    {
        if (PlayerPrefs.GetString("language", "e") == "e")
        {
            title.text = "Options";
            textbutton.text = "Options";
            music.text = "Music";
            language.text = "Language";
        }
        else
        {
            title.text = "Opciones";
            textbutton.text = "Opciones";
            music.text = "M?sica";
            language.text = "Idioma";
        }
    }
}