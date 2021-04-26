using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public enum ItemType
    {
        MedKit,
        ShieldPack,
        PistolBulletPack,
    }
    public ItemType itemType;

    public void RemoveItem()
    {
        Destroy(gameObject);
    }

    public int healing(int health)
    {
        if (health <= 175)
        {
           return health += 25;
        }
        else 
        {
           return 200;
        }

    }
    public int shielding(int shield)
    {
        if (shield <= 90)
        {
            return shield += 10;
        }
        else
        {
            return 100;
        }
    }
    public int getPistolBullet(int pistolBullet)
    {
        if (pistolBullet <= 262)
        {
            return pistolBullet += 10;
        }
        else
        {
            return 272;
        }
    }
}
