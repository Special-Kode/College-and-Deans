using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class credits : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{

    GameObject pencil, mainMenu, creditsMenu;
    Button button;
    Transform posPen, posBut;
    public Text title, textbutton;


    // Start is called before the first frame update
    void Start()
    {
        pencil = GameObject.Find("Pencil");
        mainMenu = GameObject.Find("Menu");
        creditsMenu = GameObject.Find("Credits");
        button = this.GetComponent<Button>();
        posPen = pencil.transform;
        posBut = button.transform;
        traduce();
        creditsMenu.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pencil.transform.parent = posBut;
        pencil.transform.localPosition = Vector3.zero;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    void traduce()
    {
        if (PlayerPrefs.GetString("language", "e") == "e")
        {
            title.text = "Credits and Acknoledgement";
            textbutton.text = "credits";
        }
        else
        {
            title.text = "Creditos y Reconocimientos";
            textbutton.text = "Créditos";
        }
    }
}
