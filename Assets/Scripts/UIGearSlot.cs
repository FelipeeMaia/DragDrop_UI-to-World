using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGearSlot : GearSlot
{
    public Color initialColor;

    protected override void Start()
    {
        base.Start();
        initialColor = gearColor.Color;
    }

    public override void Reset()
    {
        gearColor.Color = initialColor;
        gear.SetActive(true);
    }
}
