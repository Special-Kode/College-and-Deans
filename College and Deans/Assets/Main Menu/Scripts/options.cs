using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class options : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{

    GameObject pencil, mainMenu, optionsMenu;
    Button button, s, e;
    Vector3 posPen, posBut;


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
        posPen = pencil.transform.position;
        posBut = button.transform.position;
        optionsMenu.SetActive(false);
    }

    private void spanish()
    {
        skill.idiom = 's';
    }

    private void english()
    {
        skill.idiom = 'e';
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        posPen.y = posBut.y;
        pencil.transform.position = posPen;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
}