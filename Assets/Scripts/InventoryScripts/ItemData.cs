using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    //setting the size of the item icon
    public int width = 1;
    public int height = 1;

    public Sprite itemIcon;
}
