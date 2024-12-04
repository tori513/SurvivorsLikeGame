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

    Dictionary<WeaponType, WeaponBase> weaponPrefabList;
    Dictionary<WeaponType, int> weaponLevels = new Dictionary<WeaponType, int>();

    Dictionary<WeaponType, WeaponBase> weapons = new Dictionary<WeaponType, WeaponBase>();
    Dictionary<WeaponType, WeaponUI> weaponUIs = new Dictionary<WeaponType, WeaponUI>();
    Dictionary<WeaponType, float> weaponDamages = new Dictionary<WeaponType, float>()
    {
        { WeaponType.Rocket, 10f },
        { WeaponType.Shield, 5f },
        { WeaponType.Stick, 15f },
    };

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
        weaponPrefabList = new Dictionary<WeaponType, WeaponBase>()
        {
            {WeaponType.Gun,gunPrefab },
            {WeaponType.Rocket, rocketPrefab},
            {WeaponType.Shield, shieldPrefab},
            {WeaponType.Stick, stickPrefab}
        };

        foreach (WeaponType type in weaponPrefabList.Keys)
        {
            if (!weaponLevels.ContainsKey(type))
            {
                weaponLevels[type] = 0;
            }
        }

        ActiveWeapon(WeaponType.Gun);
        IncreaseWeaponLevel(WeaponType.Gun);
    }

    void Update()
    {
        if (GameManager.Instance.isStop) return;

        foreach (KeyValuePair<WeaponType, WeaponBase> weapon in weapons)
        {
            WeaponType type = weapon.Key;
            WeaponBase weapon_ = weapon.Value;

            weapon_.WeaponUpdate();
            float timer = weapon_.GetTimer();
            float coolTime = weapon_.GetCoolTime();
            int skillLevel = weaponLevels[type];

            weaponUIs[type].Set(timer, coolTime, skillLevel);
        }
    }

    public void ActiveWeapon(WeaponType type)
    {

        if (weapons.ContainsKey(type))
            return;

        WeaponBase weapon = null;

        switch (type)
        {
            case WeaponType.Shield:
                weapon = Instantiate(weaponPrefabList[type], shieldTransform.position, Quaternion.identity);
                break;

            case WeaponType.Stick:
                weapon = Instantiate(weaponPrefabList[type], stickTransform.position, Quaternion.identity);
                break;

            default:
                weapon = Instantiate(weaponPrefabList[type], transform.position, Quaternion.identity);
                break;
        }

        weapon.transform.SetParent(transform);
        weapons[type] = weapon;
        weaponUI[(int)type].gameObject.SetActive(true);
        weaponUIs[type] = weaponUI[(int)type];
    }

    public void IncreaseWeaponLevel(WeaponType type)
    {
        weaponLevels[type]++;
    }

    public int GetWeaponLevel(WeaponType type)
    {
        return weaponLevels[type];
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
