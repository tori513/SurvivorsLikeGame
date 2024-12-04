using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    PlayerController controller;

    Rigidbody2D rb;

    [SerializeField]
    float speed;
    float initSpeed;

    AudioSource audioSource;

    void Start()
    {
        controller = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        initSpeed = speed;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.isStop) return;

        rb.velocity = controller.GetValue() * speed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void SpeedUp()
    {
        StopAllCoroutines();
        speed *= 1.4f;
        StartCoroutine(ResetSpeed());
    }

    IEnumerator ResetSpeed()
    {
        audioSource.Play();
        yield return new WaitForSeconds(5f);
        audioSource.Stop();
        speed = initSpeed;
    }
}
