using System.Collections;
using UnityEngine;

public class BreakingBlock : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D boxColliderFirst;
    [SerializeField] private BoxCollider2D boxColliderSecond;
    private Vector2 startBlockPosition;
    private SpriteRenderer spriterenderer;


    private void Start()
    {
        startBlockPosition = transform.position;
        spriterenderer = GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            StartCoroutine(SimulateBreakingBlock());
    }

    

    IEnumerator SimulateBreakingBlock()
    {
        yield return new WaitForSeconds(1);
        rb.bodyType = RigidbodyType2D.Dynamic;
        boxColliderFirst.enabled = false;
        boxColliderSecond.enabled = false;
    }

    public void SetStartBlockPosition()
    {
        StopCoroutine(SimulateBreakingBlock());
        rb.bodyType = RigidbodyType2D.Static;
        transform.position = startBlockPosition;
        spriterenderer.enabled = true;
        boxColliderFirst.enabled = true;
        boxColliderSecond.enabled = true;
    }
}


