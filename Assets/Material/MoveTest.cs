using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    private Rigidbody2D rb;
    //[SerializeField] private GameObject groundCheck;
    //[SerializeField] private float groundDistance;
    [SerializeField] private LayerMask layerMask;
    public float speed;
    public KeyCode jumpInput;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //groundCheck = ReturnDecendantOfParent(transform.root.gameObject, "groundCheck");
    }

    void Update()
    {
        Move();
        //Jump();
    }

    public static GameObject descendant = null;
    public static GameObject ReturnDecendantOfParent(GameObject parent, string descendantName)
    {

        foreach (Transform child in parent.transform)
        {
            if (child.name == descendantName)
            {
                descendant = child.gameObject;
                break;
            }
            else
            {
                ReturnDecendantOfParent(child.gameObject, descendantName);
            }
        }
        return descendant;
    }
    void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        Vector3 temp = transform.position;
        temp.x += moveX * speed * Time.deltaTime;

        transform.position = temp;
    }
    //void Jump()
    //{
    //    if(Input.GetKeyDown(jumpInput) && IsGrounded())
    //    {
    //        rb.velocity = new Vector2(rb.velocity.x, 7f);
    //    }
    //}

    //private bool IsGrounded() => Physics2D.Raycast(groundCheck.transform.position, Vector2.down, groundDistance, layerMask);

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawLine(groundCheck.transform.position, new Vector3(groundCheck.transform.position.x , groundCheck.transform.position.y - groundDistance));
    //}
}
