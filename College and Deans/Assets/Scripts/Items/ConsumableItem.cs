using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class ConsumableItem : MonoBehaviour
{
    public Enhancer enhancer;
    public bool isFood;
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

        Enhancer[] enhs;

        if (isFood)
            enhs = Resources.LoadAll<Enhancer>("Items/CafeItems");
        else
            enhs = Resources.LoadAll<Enhancer>("Items/Enhancers");

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
        if ((enhancer.affected & Enhancer.AffectedStat.SeeFullMinimap) != 0)
        {
            //Edit player minimap vision
            EditPlayerSeeMinimap();
        }
        if ((enhancer.affected & Enhancer.AffectedStat.Cheatsheet) != 0)
        {
            //Edit player cheatsheet
            EditPlayerCheatsheet();
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

    private void EditPlayerSeeMinimap()
    {
        var stats = FindObjectOfType<StatsManager>();

        if (stats.SeeFullMinimap == 1.0f) return;

        stats.SeeFullMinimap = enhancer.amount;
    }

    private void EditPlayerCheatsheet()
    {
        FindObjectOfType<AttackBehaviour>().getWeapon().MultiplyDamage(enhancer.amount + 1);
        FindObjectOfType<StatsManager>().DamageStat = FindObjectOfType<AttackBehaviour>().getWeapon().GetDamageMultiplier();

        FindObjectOfType<ExternMechanicsPlayer>().ScaleTime(1.0f / enhancer.amount);
        FindObjectOfType<StatsManager>().TimeScaleStat = FindObjectOfType<ExternMechanicsPlayer>().GetTimescale();
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
