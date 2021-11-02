using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class credits : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{

    GameObject pencil;
    Button button;
    Vector3 posPen, posBut;


    // Start is called before the first frame update
    void Start()
    {
        pencil = GameObject.Find("Pencil");
        button = this.GetComponent<Button>();
        posPen = pencil.transform.position;
        posBut = button.transform.position;


    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        posPen.y = posBut.y;
        pencil.transform.position = posPen;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Credits");
    }
}
