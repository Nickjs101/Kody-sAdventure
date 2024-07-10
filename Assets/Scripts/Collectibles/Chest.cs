using UnityEngine;

public class Chest : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;

    public void ChangeSprite()
        {
            spriteRenderer.sprite = newSprite; 
        }
}
