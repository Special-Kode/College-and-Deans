using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject modSlot;

    private bool[] isFull;
    public GameObject[] enhSlots;

    public GameObject item;

    StatsManager m_statsManager;

    // Start is called before the first frame update
    void Start()
    {
        isFull = new bool[enhSlots.Length];

        m_statsManager = FindObjectOfType<StatsManager>();
        SetItemsOnGUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetItemsOnGUI()
    {
        if (m_statsManager.modSprite != null)
            modSlot.GetComponent<Image>().color = Color.white;
        modSlot.GetComponent<Image>().sprite = m_statsManager.modSprite;

        for (uint i = 0; i < enhSlots.Length; i++)
        {
            if (m_statsManager.enhSprites[i] != null)
            {
                isFull[i] = true;
                var uiItem = Instantiate(item, enhSlots[i].transform);
                uiItem.GetComponent<Image>().sprite = m_statsManager.enhSprites[i];
            }
        }
    }

    public void SuitModifier(Sprite modSprite)
    {
        modSlot.GetComponent<Image>().sprite = modSprite;
        modSlot.GetComponent<Image>().color = Color.white;
        m_statsManager.modSprite = modSprite;
    }

    public void SuitEnhancer(Sprite enhSprite)
    {
        for(uint i = 0; i < isFull.Length; i++)
        {
            if (!isFull[i])
            {
                isFull[i] = true;
                var uiItem = Instantiate(item, enhSlots[i].transform);
                uiItem.GetComponent<Image>().sprite = enhSprite;
                m_statsManager.enhSprites[i] = enhSprite;
                break;
            }
        }
    }
}
