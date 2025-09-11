using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public bool canMove = true;
    private Rigidbody rb;
    public float speed = 4f;

    public GetSprites zelda;
    private SpriteRenderer sr;

    private Vector2 current_input;
    private float animTimer = 0f;
    private float frameDuration = 0.15f;
    private int frameIndex = 0;

    private enum Direction { Down = 0, Left = 1, Up = 2, Right = 3 }
    private Direction facing = Direction.Down;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!canMove)
        {
            if (!GetComponent<HasHealth>().knocked_back)
            {
                rb.linearVelocity = Vector2.zero;
            }
            return;
        }

        current_input = GetInput();

        if (current_input != Vector2.zero)
        {
            AnimateWalking();
        }
        else
        {
            ShowIdleFrame();
        }

        rb.linearVelocity = current_input * speed;
    }

    Vector2 GetInput()
    {
        float horizontal_input = Input.GetAxisRaw("Horizontal");
        float vertical_input = Input.GetAxisRaw("Vertical");

        // NES-style: horizontal has priority
        if (Mathf.Abs(horizontal_input) != 0f)
            vertical_input = 0f;

        GridUtils.GridMovement(ref vertical_input, ref horizontal_input, ref rb);

        if (horizontal_input == -1) facing = Direction.Left;
        else if (horizontal_input == 1) facing = Direction.Right;
        else if (vertical_input == -1) facing = Direction.Down;
        else if (vertical_input == 1) facing = Direction.Up;

        return new Vector2(horizontal_input, vertical_input);
    }

    void AnimateWalking()
    {
        animTimer += Time.deltaTime;

        if (animTimer >= frameDuration)
        {
            animTimer = 0f;
            frameIndex = (frameIndex + 1) % 3; // Cycle through 0,1,2
        }

        int spriteIndex = GetSpriteIndex(facing, frameIndex);
        sr.sprite = zelda.sprites[spriteIndex];
    }

    void ShowIdleFrame()
    {
        frameIndex = 1; // Middle frame is idle
        int spriteIndex = GetSpriteIndex(facing, frameIndex);
        sr.sprite = zelda.sprites[spriteIndex];
    }

    int GetSpriteIndex(Direction direction, int frame)
    {
        // Base index for frame (0 = first row, 1 = second row, 2 = third row)
        // Direction is offset horizontally
        // Final index = direction + (frame * 12)
        return (int)direction + (frame * 12);
    }
}
