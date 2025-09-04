using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 4;

    private int floor_length = 16;
    private int floor_height = 7;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 current_input = GetInput();
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

        GridMovement(ref vertical_input, ref horizontal_input, ref rb);

        return new Vector2(horizontal_input, vertical_input);
    }

    void GridMovement(ref float vertical_input, ref float horizontal_input, ref Rigidbody rb)
    {
        // establish vertical movement
        if (vertical_input != 0.0f && rb.position.x % 0.5f != 0)
        {
            // check distance to line, warp player onto it once they are close enough
            vertical_input = 0.0f;
            float dir1 = 0.5f - (rb.position.x % 0.5f);
            float dir2 = rb.position.x % 0.5f;

            if (dir1 < dir2)
            {
                if (dir1 < 0.05f)
                {
                    rb.transform.position = rb.position + new Vector3(dir1, 0, 0);
                }
                else
                {
                    horizontal_input = 1;
                }
            }
            else
            {
                if (dir2 < 0.05f)
                {
                    rb.transform.position = rb.position - new Vector3(dir2, 0, 0);
                }
                else
                {
                    horizontal_input = -1;
                }
            }
        }

        // establish horizontal movement
        if (horizontal_input != 0.0f && rb.position.y % 0.5f != 0)
        {
            // check distance to line, warp player onto it once they are close enough
            horizontal_input = 0.0f;
            float dir1 = 0.5f - (rb.position.y % 0.5f);
            float dir2 = rb.position.y % 0.5f;

            if (dir1 < dir2)
            {
                if (dir1 < 0.05f)
                {
                    rb.transform.position = rb.position + new Vector3(0, dir1, 0);
                }
                else
                {
                    vertical_input = 1;
                }
            }
            else
            {
                if (dir2 < 0.05f)
                {
                    rb.transform.position = rb.position - new Vector3(0, dir2, 0);
                }
                else
                {
                    vertical_input = -1;
                }
            }
        }
    }
}
