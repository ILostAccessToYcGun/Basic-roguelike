using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

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

    public Grade rarity;
    public bool cursed;
    public List<BuffStat> buffStats;

    public int chance;

    public void RandomizeBuffRarity()
    {
        chance = Random.Range(0, GradeCommonChance + GradeUncommonChance + GradeRareChance + GradeEpicChance + GradeLegendaryChance);

        //giving a random rarity based on how rare it is ( buff manager for chance )
        if (chance < GradeCommonChance)
        {
            rarity = Grade.Common;
        }
        else if (chance < GradeUncommonChance + GradeCommonChance)
        {
            rarity = Grade.Uncommon;
        }
        else if (chance < GradeRareChance + GradeCommonChance + GradeUncommonChance)
        {
            rarity = Grade.Rare;
        }
        else if (chance < GradeEpicChance + GradeCommonChance + GradeUncommonChance + GradeRareChance)
        {
            rarity = Grade.Epic;
        }
        else if (chance < GradeLegendaryChance + GradeCommonChance + GradeUncommonChance + GradeRareChance + GradeEpicChance)
        {
            rarity = Grade.Legendary;
        }
    }

    public void GenerateBuff(Grade rarity)
    {
        switch (rarity)
        {
            case Grade.Common:
            case Grade.Uncommon:
                AddStatsToBuff(1);
                break;
            case Grade.Rare:
            case Grade.Epic:
                AddStatsToBuff(2);
                break;
            case Grade.Legendary:
                AddStatsToBuff(3);
                break;
        }
    }

    private void AddStatsToBuff(int num)
    {
        BuffStat buffStatistic = new BuffStat();
        buffStatistic.buffValue = 0;
        buffStatistic.unitStat = UnitStats.MaxHP;
        for (int i = 0; i <  num; i++)
        {
            GenerateBuffStatistic(buffStatistic);
            buffStats.Add(buffStatistic);
        }
        
    }

    //I will use this later, for now for the sake of getting it working i will unga bunga
    private void AssignBuffValue(BuffStat stat, List<int> ranges)
    {
        switch (rarity)
        {
            case Grade.Common:
                stat.buffValue = Random.Range(ranges[0], ranges[1]);
                break;
            case Grade.Uncommon:
                stat.buffValue = Random.Range(ranges[2], ranges[3]);
                break;
            case Grade.Rare:
                stat.buffValue = Random.Range(ranges[4], ranges[5]);
                break;
            case Grade.Epic:
                stat.buffValue = Random.Range(ranges[6], ranges[7]);
                break;
            case Grade.Legendary:
                stat.buffValue = Random.Range(ranges[8], ranges[9]);
                break;
        }
    }

    //Next what I need to do is finish the RandomizeStatValue method and hard code pretty much all the stats
    public void RandomizeStatValue (Grade rarity, BuffStat stat)
    {
        switch (stat.unitStat)
        {
            case UnitStats.MaxHP:
                switch (rarity)
                {
                    case Grade.Common:
                        stat.buffValue = Random.Range(1, 6);
                        break;
                    case Grade.Uncommon:
                        stat.buffValue = Random.Range(6, 11);
                        break;
                    case Grade.Rare:
                        stat.buffValue = Random.Range(11, 21);
                        break;
                    case Grade.Epic:
                        stat.buffValue = Random.Range(21, 31);
                        break;
                    case Grade.Legendary:
                        stat.buffValue = Random.Range(31, 51);
                        break;
                }
                break;
            case UnitStats.ATK:
                switch (rarity)
                {
                    case Grade.Common:
                        stat.buffValue = Random.Range(1, 3);
                        break;
                    case Grade.Uncommon:
                        stat.buffValue = Random.Range(2, 4);
                        break;
                    case Grade.Rare:
                        stat.buffValue = Random.Range(3, 5);
                        break;
                    case Grade.Epic:
                        stat.buffValue = Random.Range(4, 7);
                        break;
                    case Grade.Legendary:
                        stat.buffValue = Random.Range(7, 11);
                        break;
                }
                break;
            case UnitStats.SPD:
                switch (rarity)
                {
                    case Grade.Common:
                        stat.buffValue = Random.Range(1, 3);
                        break;
                    case Grade.Uncommon:
                        stat.buffValue = Random.Range(2, 4);
                        break;
                    case Grade.Rare:
                        stat.buffValue = Random.Range(3, 5);
                        break;
                    case Grade.Epic:
                        stat.buffValue = Random.Range(5, 8);
                        break;
                    case Grade.Legendary:
                        stat.buffValue = Random.Range(8, 11);
                        break;
                }
                break;
            case UnitStats.DEF:
                switch (rarity)
                {
                    case Grade.Common:
                        stat.buffValue = Random.Range(1, 3);
                        break;
                    case Grade.Uncommon:
                        stat.buffValue = Random.Range(2, 4);
                        break;
                    case Grade.Rare:
                        stat.buffValue = Random.Range(3, 5);
                        break;
                    case Grade.Epic:
                        stat.buffValue = Random.Range(4, 7);
                        break;
                    case Grade.Legendary:
                        stat.buffValue = Random.Range(7, 11);
                        break;
                }
                break;
            case UnitStats.CD:
                switch (rarity)
                {
                    case Grade.Common:
                    case Grade.Uncommon:
                    case Grade.Rare:
                    case Grade.Epic:
                    case Grade.Legendary:
                        stat.buffValue = 1; //TODO: this needs to change once we rework CD
                        break;
                }
                break;
            case UnitStats.JUMP:
                switch (rarity)
                {
                    case Grade.Common:
                        stat.buffValue = 0;
                        break;
                    case Grade.Uncommon:
                        stat.buffValue = 1;
                        break;
                    case Grade.Rare:
                        stat.buffValue = Random.Range(1, 3);
                        break;
                    case Grade.Epic:
                        stat.buffValue = 2;
                        break;
                    case Grade.Legendary:
                        stat.buffValue = Random.Range(2, 4);
                        break;
                }
                break;
            case UnitStats.jHGHT:
                switch (rarity)
                {
                    case Grade.Common:
                        stat.buffValue = Random.Range(0, 2);
                        break;
                    case Grade.Uncommon:
                        stat.buffValue = Random.Range(0, 2);
                        break;
                    case Grade.Rare:
                        stat.buffValue = Random.Range(1, 3);
                        break;
                    case Grade.Epic:
                        stat.buffValue = Random.Range(1, 4);
                        break;
                    case Grade.Legendary:
                        stat.buffValue = Random.Range(2, 5);
                        break;
                }
                break;
        }
    }

    private void GenerateBuffStatistic(BuffStat buffStatistic)
    {
        do
        {
            buffStatistic.PickStat();
            RandomizeStatValue(rarity, buffStatistic);
        }
        while (buffStatistic.buffValue == 0);//loop if the value is zero
    }



    private void GivePlayerBuff()
    {
        //loop through the buff stat list. and do an if statement to check for each stat
        foreach (BuffStat stat in buffStats)
        {
            if (stat.unitStat == UnitStats.MaxHP)
            {
                player.f_MaxHP = stat.buffValue;
            }
            else if (stat.unitStat == UnitStats.ATK)
            {
                player.f_ATK = stat.buffValue;
            }
            else if (stat.unitStat == UnitStats.SPD)
            {
                player.f_SPD = stat.buffValue;
            }
            else if (stat.unitStat == UnitStats.DEF)
            {
                player.f_DEF = stat.buffValue;
            }
            else if (stat.unitStat == UnitStats.CD)
            {
                player.f_CD = stat.buffValue;
            }
            else if (stat.unitStat == UnitStats.JUMP)
            {
                player.f_JUMP = stat.buffValue;
            }
            else if (stat.unitStat == UnitStats.jHGHT)
            {
                player.f_jHeight = stat.buffValue;
            }
        }
    }


    private void Awake()
    {
        player = FindObjectOfType<Characters>();


        //TODO: TEMPORARY
        GradeCommonChance = 45;
        GradeUncommonChance = 25;
        GradeRareChance = 15;
        GradeEpicChance = 10;
        GradeLegendaryChance = 5;
        //TEMPORARY


        RandomizeBuffRarity();
        GenerateBuff(rarity); //TODO: Error here, you need to run a debug. something is wrong with the BuffStat class, null ref excepection
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GivePlayerBuff();
            Destroy(gameObject);
        }
        
    }
}
