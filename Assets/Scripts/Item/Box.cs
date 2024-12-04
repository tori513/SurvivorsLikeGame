using System.Collections;
using UnityEngine;

public class Box : MonoBehaviour
{
    ObjectPool pool;

    ObjectPool pillPool;
    ObjectPool coinPool;

    Animator animator;
    AudioSource source;

    bool isOpen;

    void Start()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();

        pillPool = GameObject.FindGameObjectWithTag("PillPool").GetComponent<ObjectPool>();
        coinPool = GameObject.FindGameObjectWithTag("CoinPool").GetComponent<ObjectPool>();
    }

    public void Set(ObjectPool pool)
    {
        this.pool = pool;
    }

    public void TakeDamage()
    {
        animator.SetBool("isOpen", true);
        PlaySound();
        StartCoroutine(MakeItemRoutine());
    }

    IEnumerator MakeItemRoutine()
    {
        yield return new WaitForSeconds(0.4f);
        MakeItem();
    }

    void MakeItem()
    {
        int random = Random.Range(0, 3);

        if (random == 0)
        {
            Pill pill = pillPool.GetObject().GetComponent<Pill>();
            pill.Set(pillPool);
            pill.transform.position = transform.position;

        }
        else
        {
            Shoes coin = coinPool.GetObject().GetComponent<Shoes>();
            coin.Set(coinPool);
            coin.transform.position = transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !isOpen)
        {
            isOpen = true;

            TakeDamage();
            StartCoroutine(DisappearBoxRoutine());
        }
    }

    IEnumerator DisappearBoxRoutine()
    {
        yield return new WaitForSeconds(5f);
        animator.SetBool("isReturn", true);
        yield return new WaitForSeconds(2f);
        ResetBox();
        pool.ReturnObject(gameObject);
    }

    void ResetBox()
    {
        animator.SetBool("isOpen", false);
        animator.SetBool("isReturn", false);
        animator.Play("Box_Idle", 0); 

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white; 
        }
    }

    void PlaySound()
    {
        source.Play();
    }

    void OnEnable()
    {
        isOpen = false;
    }
}
