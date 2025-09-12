using UnityEngine;

public class GoriyaSprite : MonoBehaviour
{
    public Sprite[] sprites;
    public Sprite[] boomerangSprites;

    void Awake()
    {
        LoadSprites();
    }

    public void LoadSprites()
    {
        sprites = new Sprite[4];

        for (int i = 0; i < 4; i++)
        {
            string spriteName = "Zelda/goriya" + (i + 1);
            sprites[i] = Resources.Load<Sprite>(spriteName);

            if (sprites[i] == null)
            {
                Debug.LogWarning("Failed to load sprite: " + spriteName);
            }
            else
            {
                Debug.Log("Loaded sprite: " + sprites[i].name);
            }
        }
        boomerangSprites = new Sprite[3];

        for (int i = 0; i < 3; i++)
        {
            string spriteName = "Zelda/boomerang" + (i + 1);
            boomerangSprites[i] = Resources.Load<Sprite>(spriteName);

            if (sprites[i] == null)
            {
                Debug.LogWarning("Failed to load sprite: " + spriteName);
            }
            else
            {
                Debug.Log("Loaded sprite: " + sprites[i].name);
            }
        }
    }
}