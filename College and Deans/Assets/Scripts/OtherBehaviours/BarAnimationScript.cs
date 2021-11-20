using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarAnimationScript : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Text time;

    [SerializeField] private bool useGradient;
    [SerializeField] private Color defaultColor;

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;

        time.text = maxHealth.ToString();

        fill.color = useGradient ? gradient.Evaluate(1.0f) : defaultColor;
        time.color = useGradient ? gradient.Evaluate(1.0f) : defaultColor;
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        if (health < 0) health = 0;
        time.text = (health).ToString();

        fill.color = useGradient ? gradient.Evaluate(slider.normalizedValue) : defaultColor;
        time.color = useGradient ? gradient.Evaluate(slider.normalizedValue) : defaultColor;
    }

    // Update is called once per frame
    void Update()
    {

        //TODO edit this when editing UI
        this.GetComponentInChildren<UnityEngine.UI.Text>().text= GameObject.FindGameObjectWithTag("Player").GetComponent<ExternMechanicsPlayer>().CurrentHealth.ToString();
    }
}
