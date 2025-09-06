using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public bool canMove = true;
    Rigidbody rb;
    public float speed = 4;
    public GetSprites zelda;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        Vector2 current_input = GetInput();
        if (!canMove){ 
            rb.linearVelocity = Vector2.zero;
            return;
        }
        rb.linearVelocity = current_input * speed;
    }

    Vector2 GetInput()
    {
        float horizontal_input = Input.GetAxisRaw("Horizontal");
        float vertical_input = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(horizontal_input) > 0.0f)
        {
            vertical_input = 0.0f;
        }

        GridUtils.GridMovement(ref vertical_input, ref horizontal_input, ref rb);

        // set sprite to show character direction
        if (horizontal_input == -1)
        {
            GetComponent<SpriteRenderer>().sprite = zelda.sprites[1];
        }
        else if (horizontal_input == 1)
        {
            GetComponent<SpriteRenderer>().sprite = zelda.sprites[3];
        }
        if (vertical_input == -1)
        {
            GetComponent<SpriteRenderer>().sprite = zelda.sprites[0];
        }
        else if (vertical_input == 1)
        {
            GetComponent<SpriteRenderer>().sprite = zelda.sprites[2];
        }

            return new Vector2(horizontal_input, vertical_input);
    }
}