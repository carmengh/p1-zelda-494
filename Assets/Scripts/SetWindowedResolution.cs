using UnityEngine;

public class SetWindowedResolution : MonoBehaviour
{
    void Start()
    {
        // Set resolution to 1024x960 in windowed mode
        Screen.SetResolution(1024, 960, false);
    }
}