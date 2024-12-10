using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData
{
    public WeaponBase prefab;
    public WeaponUI ui;
    public int level;
    public float damage;

    public WeaponData(WeaponBase prefab, WeaponUI ui, int level, float damage)
    {
        this.prefab = prefab;
        this.ui = ui;
        this.level = level;
        this.damage = damage;
    }
}
