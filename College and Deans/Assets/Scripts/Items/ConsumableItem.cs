using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class ConsumableItem : MonoBehaviour
{
    public Enhancer enhancer;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        LoadEnhancer();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = enhancer.sprite;

        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadEnhancer()
    {
        int rand = UnityEngine.Random.Range(0, 5);
        //rand = 1;
        switch (rand) {
            case 0:
                enhancer = Resources.Load<Enhancer>("ScriptableObjects/Pressonesso");
                break;
            case 1:
                enhancer = Resources.Load<Enhancer>("ScriptableObjects/GoodPlanning");
                break;
            case 2:
                enhancer = Resources.Load<Enhancer>("ScriptableObjects/BadPlanning");
                break;
            case 3:
                enhancer = Resources.Load<Enhancer>("ScriptableObjects/Ualium");
                break;
            case 4:
                enhancer = Resources.Load<Enhancer>("ScriptableObjects/StayUpLate");
                break;
        }
    }

    void CheckEffect()
    {
        Debug.Log("Item " + enhancer.name + " buffing " + enhancer.affected.ToString() + " by " + enhancer.amount);

        switch (enhancer.affected)
        {
            case Enhancer.AffectedStat.Speed:
                //Edit player speed
                break;
            case Enhancer.AffectedStat.Damage:
                //Edit player damage
                break;
            case Enhancer.AffectedStat.TimeScale:
                //Edit player timescale
                FindObjectOfType<ExternMechanicsPlayer>().ScaleTime(enhancer.amount);
                break;
            case Enhancer.AffectedStat.Berserk:
                //Edit player speed
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            CheckEffect();
            Destroy(gameObject);
        }
    }
}
