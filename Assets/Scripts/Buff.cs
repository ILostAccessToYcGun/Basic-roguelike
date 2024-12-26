using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : BuffManager
{
    /*
    The range of the stat buff value (based on rarity)
        So buffs will have the following member variables
        Rarity --
        Stat Value -- 
        Stats affected <list>
        Stat Value range (?)
     */



    public Grade Rarity;
    public bool cursed;
    public int buffValue;
    public List<UnitStats> buffStats;

    //the stats will have an equal chance of rolling, but I want to make a system where we can change the odds.

    public void RandomizeRarity()
    {
        Rarity = (Grade)Random.Range(0, 5);
    }

    public void RandomizeStats(Grade rarity)
    {
        switch (rarity)
        {
            case Grade.Common:
            case Grade.Uncommon:
                AddStats(1);
                break;
            case Grade.Rare:
            case Grade.Epic:
                AddStats(2);
                break;
            case Grade.Legendary:
                AddStats(3);
                break;
        }
    }

    private void AddStats(int num)
    {
        for (int i = 0; i <  num; i++)
        {
            buffStats.Add((UnitStats)Random.Range(0, 7));
        }
    }

    //Next what I need to do is finish the RandomizeStatValue method and hard code pretty much all the stats
    public void RandomizeStatValue (Grade rarity, UnitStats stat)
    {
        switch (rarity)
        {
            case Grade.Common:
                break;
            case Grade.Uncommon:
                break;
            case Grade.Rare:
                break;
            case Grade.Epic:
                break;
            case Grade.Legendary:
                break;

        }

    }

    private void Awake()
    {
        Rarity = Grade.Common;
        cursed = false;
        buffValue = 0;
        buffStats.Add(UnitStats.MaxHP);
    }
}
