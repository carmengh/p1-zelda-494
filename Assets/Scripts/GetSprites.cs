using UnityEngine;

public class GetSprites : MonoBehaviour
{
    public Sprite[] sprites;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("Zelda/link_sprites");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
