using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LineRenderer catapultLineFront;
    public LineRenderer catapultLineBack;

    private Ray leftCatapultToProjectileRay;
    private SpringJoint2D spring;
    private Transform rock;
    private float circleRadius;
    private bool clickedOn;
    private Rigidbody2D rb2d;
    private CircleCollider2D circle;
    private Vector2 previousVelocity;
    private float originCatapultX = -8.52f;
    private float originaCatapultY = -3.28f;

    private void Awake()
    {
        spring = GetComponent<SpringJoint2D>();
        rb2d = GetComponent<Rigidbody2D>();
        rock = GetComponent<Transform>();
        circle = GetComponent<CircleCollider2D>();
        catapultLineFront.widthMultiplier = 0.2f;
        catapultLineBack.widthMultiplier = 0.2f;
    }



    // Start is called before the first frame update
    void Start()
    {
        LineRendererSetup();
        circleRadius = circle.radius;
        leftCatapultToProjectileRay = new Ray(catapultLineFront.transform.position, Vector3.zero);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (clickedOn)
        {
            print("Dragging - clicedOn = True");
            Dragging();
        }

        if (spring != null)
        {
            LineRendererUpdate();
        }
        else
        {
            print("Destroy Spring");
            catapultLineFront.enabled = false;
            catapultLineBack.enabled = false;
            Destroy (spring);
        }
    }

    void OnMouseDown()
    {
        print("On Mouse Up");
        spring.enabled = false;
        clickedOn = true;
    }

    private void OnMouseUp()
    {
        spring.enabled = true;
        rb2d.isKinematic = false;
        clickedOn = false;
    }

    void Dragging()
    {
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 catapultToMouse = mouseWorldPoint - new Vector3(originCatapultX, originaCatapultY, 0);
        mouseWorldPoint.z = 0f;
        transform.position = mouseWorldPoint;
    }
    void LineRendererSetup ()
    {
        catapultLineFront.SetPosition(0,new Vector3 (-8.52f, -3.01f));
        catapultLineBack.SetPosition(0, new Vector3(-7.49f, -2.88f));

        catapultLineFront.SetPosition(1, new Vector3(-9.2f, -3.86f));
        catapultLineBack.SetPosition(1, new Vector3(-9.2f, -3.86f));

        catapultLineFront.sortingLayerName = "Foreground";
        catapultLineFront.sortingLayerName = "Foreground";

        catapultLineFront.sortingOrder = 2;
        catapultLineBack.sortingOrder = 1;
    }
    void LineRendererUpdate()
    {
        Vector2 catapultToProjectile = transform.position - catapultLineFront.transform.position;
        leftCatapultToProjectileRay.direction = catapultToProjectile;
        Vector3 holdPoint = leftCatapultToProjectileRay.GetPoint(catapultToProjectile.magnitude + circleRadius);

        catapultLineFront.SetPosition(1, holdPoint);
        catapultLineBack.SetPosition(1, holdPoint);

    }
}
