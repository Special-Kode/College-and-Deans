using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class play : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{

    GameObject pencil;
    Button button;
    Transform posPen, posBut;
    public Text title;


    // Start is called before the first frame update
    void Start()
    {
        pencil = GameObject.Find("Pencil");
        button = this.GetComponent<Button>();
        posPen = pencil.transform;
        posBut = button.transform;
        traduce();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pencil.transform.parent = posBut;
        pencil.transform.localPosition = Vector3.zero;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //SceneManager.LoadScene("SampleScene");
        FindObjectOfType<GameLoader>().StartGame();
    }

    void traduce()
    {
        if (PlayerPrefs.GetString("language", "e") == "e")
        {
            title.text = "Play";
        }
        else
        {
            title.text = "Jugar";
        }
    }
}
