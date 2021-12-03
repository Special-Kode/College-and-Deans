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
        if (enhancer != null) return;

        Enhancer[] enhs = Resources.LoadAll<Enhancer>("Items/Enhancers");

        int rand = UnityEngine.Random.Range(0, enhs.Length);
        enhancer = enhs[rand];
    }

    void CheckEffect()
    {
        Debug.Log("Item " + enhancer.name + " buffing " + enhancer.affected.ToString() + " by " + enhancer.amount);

        switch (enhancer.affected)
        {
            case Enhancer.AffectedStat.Speed:
                //Edit player speed
                FindObjectOfType<Movement>().SetSpeedMultiplier(enhancer.amount);
                FindObjectOfType<StatsManager>().SpeedStat = FindObjectOfType<Movement>().GetSpeedMultiplier();
                break;
            case Enhancer.AffectedStat.Damage:
                //Edit player damage
                FindObjectOfType<AttackBehaviour>().getWeapon().SetDamageMultiplier((int)enhancer.amount);
                FindObjectOfType<StatsManager>().DamageStat = FindObjectOfType<AttackBehaviour>().getWeapon().GetDamageMultiplier();
                break;
            case Enhancer.AffectedStat.TimeScale:
                //Edit player timescale
                FindObjectOfType<ExternMechanicsPlayer>().ScaleTime(enhancer.amount);
                FindObjectOfType<StatsManager>().TimeScaleStat = FindObjectOfType<ExternMechanicsPlayer>().GetScaleTime();
                break;
            case Enhancer.AffectedStat.Berserk:
                //Edit player berserk
                FindObjectOfType<ExternMechanicsPlayer>().ScaleDamage((int)enhancer.amount);
                FindObjectOfType<StatsManager>().BerserkStats = FindObjectOfType<ExternMechanicsPlayer>().GetScaleDamage();

                FindObjectOfType<AttackBehaviour>().getWeapon().SetDamageMultiplier((int)enhancer.amount);
                FindObjectOfType<StatsManager>().DamageStat = FindObjectOfType<AttackBehaviour>().getWeapon().GetDamageMultiplier();
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            CheckEffect();
            FindObjectOfType<SFXManager>().powerSFX();
            FindObjectOfType<Inventory>().SuitEnhancer(enhancer.sprite);
            Destroy(gameObject);
        }
    }
}
