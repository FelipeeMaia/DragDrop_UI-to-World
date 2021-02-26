using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGearSlot : GearSlot
{
    private GameManager _gm;
    public int rotationDirection;

    protected override void Start()
    {
        base.Start();
        _gm = FindObjectOfType<GameManager>();

        gear.SetActive(false);
    }

    void Update()
    {
        if (_gm.allGearsActive)
            gear.transform.Rotate(Vector3.forward * rotationDirection);
    }

    public override void GearEnter(Color color)
    {
        _gm.ActiveGears++;

        base.GearEnter(color);
    }

    public override Color GearExit()
    {
        _gm.ActiveGears--;

        return base.GearExit();
    }

    public override void Reset()
    {
        gear.SetActive(false);
    }
}
