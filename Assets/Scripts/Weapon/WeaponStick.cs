using System.Collections;
using UnityEngine;

public class WeaponStick : WeaponBase
{
    [SerializeField]
    ObjectPool slashPool;

    protected override void Attack()
    {
        GameObject slashObj = slashPool.GetObject();
        Slash slash = slashObj.GetComponent<Slash>();
        slash.Attack();
        PlaySound();
        StartCoroutine(ShowAniRoutine(slashObj));
    }

    IEnumerator ShowAniRoutine(GameObject obj)
    {
        yield return new WaitForSeconds(0.25f);
        slashPool.ReturnObject(obj);
    }
}
