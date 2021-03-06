using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class ModifierItem : MonoBehaviour
{
    public Modifier modifier;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        LoadModifier();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = modifier.sprite;

        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadModifier()
    {
        if (modifier != null) return;

        Modifier[] mods = Resources.LoadAll<Modifier>("Items/Modifiers");

        int rand = UnityEngine.Random.Range(0, mods.Length);
        modifier = mods[rand];
    }

    void ModifyWeapon()
    {
        Debug.Log("Modifier " + modifier.name + " id: " + modifier.modifierId);

        FindObjectOfType<AnimatorPlayerScript>().UpdateWeapon(modifier.modifierId);
        FindObjectOfType<StatsManager>().modifier = modifier.modifierId;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ModifyWeapon();
            FindObjectOfType<SFXManager>().reloadSFX();
            FindObjectOfType<Inventory>().SuitModifier(modifier.sprite);
            Destroy(gameObject);
        }
    }
}
