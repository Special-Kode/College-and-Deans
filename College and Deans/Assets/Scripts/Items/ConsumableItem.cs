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

        if ((enhancer.affected & Enhancer.AffectedStat.Speed) != 0)
        {
            //Edit player speed
            EditPlayerSpeed();
        }
        if ((enhancer.affected & Enhancer.AffectedStat.Damage) != 0)
        {
            //Edit player damage
            EditPlayerDamage();
        }
        if ((enhancer.affected & Enhancer.AffectedStat.Timescale) != 0)
        {
            //Edit player timescale
            EditPlayerTimescale();
        }
        if ((enhancer.affected & Enhancer.AffectedStat.Resistance) != 0)
        {
            //Edit player resistance
            EditPlayerResistance();
        }
        if ((enhancer.affected & Enhancer.AffectedStat.Berserk) != 0)
        {
            //Edit player berserk
            EditPlayerBerserk();
        }
    }

    private void EditPlayerSpeed()
    {
        FindObjectOfType<Movement>().MultiplySpeed(enhancer.amount);
        FindObjectOfType<StatsManager>().SpeedStat = FindObjectOfType<Movement>().GetSpeedMultiplier();
    }

    private void EditPlayerDamage()
    {
        FindObjectOfType<AttackBehaviour>().getWeapon().MultiplyDamage(enhancer.amount);
        FindObjectOfType<StatsManager>().DamageStat = FindObjectOfType<AttackBehaviour>().getWeapon().GetDamageMultiplier();
    }

    private void EditPlayerTimescale()
    {
        FindObjectOfType<ExternMechanicsPlayer>().ScaleTime(enhancer.amount);
        FindObjectOfType<StatsManager>().TimeScaleStat = FindObjectOfType<ExternMechanicsPlayer>().GetTimescale();
    }

    private void EditPlayerResistance()
    {
        FindObjectOfType<ExternMechanicsPlayer>().ScaleResistance(enhancer.amount);
        FindObjectOfType<StatsManager>().ResistanceStat = FindObjectOfType<ExternMechanicsPlayer>().GetResistanceScaler();
    }

    private void EditPlayerBerserk()
    {
        FindObjectOfType<ExternMechanicsPlayer>().ScaleResistance(1.0f / enhancer.amount);
        FindObjectOfType<StatsManager>().ResistanceStat = FindObjectOfType<ExternMechanicsPlayer>().GetResistanceScaler();

        FindObjectOfType<AttackBehaviour>().getWeapon().MultiplyDamage(enhancer.amount);
        FindObjectOfType<StatsManager>().DamageStat = FindObjectOfType<AttackBehaviour>().getWeapon().GetDamageMultiplier();
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
