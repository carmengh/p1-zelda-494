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
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            obj1.position = Vector3.Lerp(start1, end1, t);
            obj2.position = Vector3.Lerp(start2, end2, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        obj1.position = end1;
        obj2.position = end2;
    }
}