using UnityEngine;

public class GetSprites : MonoBehaviour
{
    public Sprite[] sprites;
    public Sprite[] boomerangSprites;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("Zelda/link_sprites");
        BoomerangSprites();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BoomerangSprites()
    {
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
