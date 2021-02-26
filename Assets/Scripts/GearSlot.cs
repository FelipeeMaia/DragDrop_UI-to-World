using UnityEngine;
using UnityEngine.EventSystems;

public class GearSlot : MonoBehaviour
{
    public GameObject gear;
    protected GearColor gearColor;

    public virtual bool isActive => gear.activeSelf;

    protected virtual void Start()
    {
        gearColor = gear.GetComponent<GearColor>();
    }

    public virtual void GearEnter(Color color)
    {
        gearColor.Color = color;
        gear.SetActive(true);
    }

    public virtual Color GearExit()
    {
        gear.SetActive(false);
        return gearColor.Color;
    }

    public virtual void Reset()
    {

    }
}
