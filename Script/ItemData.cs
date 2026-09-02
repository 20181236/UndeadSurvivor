using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/Itemdata")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Melee, Range, Glove, Shoes, Heal}

    [Header("# Main Info")]
    public ItemType itemType;
    public int itemID;
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;

    [Header("# Level Data")]
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;

    [Header("# Weapon")]
    public GameObject projectile;
}
