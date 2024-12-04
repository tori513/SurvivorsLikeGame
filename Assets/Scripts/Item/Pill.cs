using System.Collections;
using UnityEngine;

public class Pill : MonoBehaviour
{
    [SerializeField]
    int healPoint;

    [SerializeField]
    AudioClip clip;

    ObjectPool pool;

    public void Set(ObjectPool pool)
    {
        this.pool = pool;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Heal(healPoint);
            PlaySound();
            StartCoroutine(WaitReturnRoutine());
        }
    }

    IEnumerator WaitReturnRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        pool.ReturnObject(gameObject);
    }

    void PlaySound()
    {
        SoundManager.Instance.PlaySound(clip, 1f);
    }
}
