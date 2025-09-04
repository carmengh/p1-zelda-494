using UnityEngine;

public static class GridUtils
{
    public static void GridMovement(ref float vertical_input, ref float horizontal_input, ref Rigidbody rb)
    {
        // Snap to X grid if moving vertically
        if (vertical_input != 0.0f && rb.position.x % 0.5f != 0)
        {
            vertical_input = 0.0f;
            float dir1 = 0.5f - (rb.position.x % 0.5f);
            float dir2 = rb.position.x % 0.5f;

            if (dir1 < dir2)
            {
                if (dir1 < 0.05f)
                    rb.transform.position = rb.position + new Vector3(dir1, 0, 0);
                else
                    horizontal_input = 1;
            }
            else
            {
                if (dir2 < 0.05f)
                    rb.transform.position = rb.position - new Vector3(dir2, 0, 0);
                else
                    horizontal_input = -1;
            }
        }

        // Snap to Y grid if moving horizontally
        if (horizontal_input != 0.0f && rb.position.y % 0.5f != 0)
        {
            horizontal_input = 0.0f;
            float dir1 = 0.5f - (rb.position.y % 0.5f);
            float dir2 = rb.position.y % 0.5f;

            if (dir1 < dir2)
            {
                if (dir1 < 0.05f)
                    rb.transform.position = rb.position + new Vector3(0, dir1, 0);
                else
                    vertical_input = 1;
            }
            else
            {
                if (dir2 < 0.05f)
                    rb.transform.position = rb.position - new Vector3(0, dir2, 0);
                else
                    vertical_input = -1;
            }
        }
    }
}