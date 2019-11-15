using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDamage : MonoBehaviour
{
    public int hitPoints = 2;
    public Sprite damageSprite;
    public float damageImpactSpeed;

    private int currentHitpoints;
    private float damageImpactSpeedSqr;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;
    private Collider2D collider2d;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D> ();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHitpoints = hitPoints;
        damageImpactSpeedSqr = damageImpactSpeed * damageImpactSpeed;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Damager")
            return;

        if (collision.relativeVelocity.sqrMagnitude < damageImpactSpeedSqr)
            return;

        spriteRenderer.sprite = damageSprite;
        currentHitpoints -= 1;
    }

    void Kill()
    {
       spriteRenderer.enabled = false;
        collider2d.enabled = false;
        rb2d.isKinematic = true;

    }
}
