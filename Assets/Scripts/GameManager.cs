using System.Collections;
using System.Linq;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Referencias as Engrenagens")]
    public int gearCount;
    public GearSlot[] AllGears; 
    private int activeGears;
    public int ActiveGears
    {
        get { return activeGears; }
        set
        {
            activeGears = value;

            nuggetMsg.text = allGearsActive ? msgActive : msgNonActive;
        }
    }
    public bool allGearsActive => (activeGears == gearCount);

    [Header("Mensagens a ser ditas pelo Nugget")]
    public TMP_Text nuggetMsg;
    [TextArea(3,10)]public string msgActive, msgNonActive;

    public void Awake()
    {
        GearSlot[] _worldGears = FindObjectsOfType<WorldGearSlot>();
        GearSlot[] _uiGears = FindObjectsOfType<UIGearSlot>();

        AllGears = _worldGears.Concat(_uiGears).ToArray();
    }

    public void Reset()
    {
        foreach (GearSlot gearSlot in AllGears)
            gearSlot.Reset();

        ActiveGears = 0;
    }
}
