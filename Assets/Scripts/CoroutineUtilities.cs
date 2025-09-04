using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoroutineUtilities
{
    public static IEnumerator MoveTwoObjectsOverTime(
        Transform obj1, Vector3 start1, Vector3 end1,
        Transform obj2, Vector3 start2, Vector3 end2,
        float duration)
    {
        // Set starting positions
        obj1.position = start1;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            obj1.position = Vector3.Lerp(start1, end1, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        obj1.position = end1;
        yield return new WaitForSeconds(0.5f);
        obj2.position = end2;

    }
}
