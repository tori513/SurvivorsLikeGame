using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    WeaponBase gunPrefab;
    [SerializeField]
    WeaponBase rocketPrefab;
    [SerializeField]
    WeaponBase shieldPrefab;
    [SerializeField]
    WeaponBase stickPrefab;

    Dictionary<WeaponType, WeaponData> weaponData = new Dictionary<WeaponType, WeaponData>();
    Dictionary<WeaponType, WeaponBase> activeWeapons = new Dictionary<WeaponType, WeaponBase>();

    [SerializeField]
    Transform shieldTransform;

    [SerializeField]
    Transform stickTransform;

    [SerializeField]
    WeaponUI[] weaponUI;

    [SerializeField]
    Rocket rocket;
    [SerializeField]
    Slash slash;
    [SerializeField]
    WeaponShield shield;

    void Start()
    {

        weaponData = new Dictionary<WeaponType, WeaponData>()
        {
            { WeaponType.Gun, new WeaponData(gunPrefab, weaponUI[0], 0, 0f) },
            { WeaponType.Rocket, new WeaponData(rocketPrefab, weaponUI[1], 0, 10f) },
            { WeaponType.Shield, new WeaponData(shieldPrefab, weaponUI[2], 0, 5f) },
            { WeaponType.Stick, new WeaponData(stickPrefab, weaponUI[3], 0, 15f) }
        };  

        ActiveWeapon(WeaponType.Gun);
        IncreaseWeaponLevel(WeaponType.Gun);
    }

    void Update()
    {
        if (GameManager.Instance.isStop) return;

        foreach (KeyValuePair<WeaponType, WeaponBase> weaponPair in activeWeapons)
        {
            WeaponType type = weaponPair.Key;
            WeaponBase weapon = weaponPair.Value;

            weapon.WeaponUpdate();
            float timer = weapon.GetTimer();
            float coolTime = weapon.GetCoolTime();
            int level = weaponData[type].level;

            weaponData[type].ui.Set(timer, coolTime, level);
        }
    }

    public void ActiveWeapon(WeaponType type)
    {

        if (activeWeapons.ContainsKey(type))
            return;

        WeaponData data = weaponData[type];
        WeaponBase weapon = null;

        switch (type)
        {
            case WeaponType.Shield:
                weapon = Instantiate(data.prefab, shieldTransform.position, Quaternion.identity);
                break;

            case WeaponType.Stick:
                weapon = Instantiate(data.prefab, stickTransform.position, Quaternion.identity);
                break;

            default:
                weapon = Instantiate(data.prefab, transform.position, Quaternion.identity);
                break;
        }

        weapon.transform.SetParent(transform);
        activeWeapons[type] = weapon;
        data.ui.gameObject.SetActive(true); 
    }

    public void IncreaseWeaponLevel(WeaponType type)
    {
        weaponData[type].level++;
    }

    public int GetWeaponLevel(WeaponType type)
    {
        return weaponData[type].level;
    }

    public void DamageUp(WeaponType type, float damage)
    {
        switch (type)
        {
            case WeaponType.Rocket:
                rocket.DamageUp(damage);
                break;
            case WeaponType.Shield:
                shield.DamageUp(damage);
                break;
            case WeaponType.Stick:
                slash.DamageUp(damage);
                break;
        }
    }
}
