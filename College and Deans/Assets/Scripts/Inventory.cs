using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject modSlot;

    public bool[] isFull;
    public GameObject[] enhSlots;

    public GameObject item;

    StatsManager m_statsManager;

    // Start is called before the first frame update
    void Start()
    {
        m_statsManager = FindObjectOfType<StatsManager>();
        SetItemsOnGUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetItemsOnGUI()
    {
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
