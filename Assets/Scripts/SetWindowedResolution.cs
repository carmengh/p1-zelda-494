using UnityEngine;

public class SetWindowedResolution : MonoBehaviour
{
    public static bool God_Mode = false;
    void Start()
    {
        God_Mode = false;
        // Set resolution to 1024x960 in windowed mode
        Screen.SetResolution(1024, 960, false);
    }
}