using UnityEngine;

public class GelAnimator : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;
    public float switchInterval = 0.3f;  // seconds between sprite switches

    private SpriteRenderer sR;
    private float timer = 0f;
    private bool usingFirstSprite = true;

    void Start()
    {
        sR = GetComponent<SpriteRenderer>();

        if (sprite1 != null)
            sR.sprite = sprite1;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= switchInterval)
        {
            timer = 0f;
            usingFirstSprite = !usingFirstSprite;

            sR.sprite = usingFirstSprite ? sprite1 : sprite2;
        }
    }
}