using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Buff : BuffManager
{
    public BuffManager bm;

    public Grade rarity;
    public bool cursed;
    public List<BuffStat> buffStats = new List<BuffStat>();
    //public int chance;

    public string outputText;
    private string gradeText = "";
    private string buffText = "";

    public void RandomizeBuffRarity()
    {
        int chance = Random.Range(0, GradeCommonChance + GradeUncommonChance + GradeRareChance + GradeEpicChance + GradeLegendaryChance);

        //giving a random rarity based on how rare it is ( buff manager for chance )
        if (chance < GradeCommonChance)
        {
            rarity = Grade.Common;
            gradeText = "Common ";
        }
        else if (chance < GradeUncommonChance + GradeCommonChance)
        {
            rarity = Grade.Uncommon;
            gradeText = "Uncommon ";
        }
        else if (chance < GradeRareChance + GradeCommonChance + GradeUncommonChance)
        {
            rarity = Grade.Rare;
            gradeText = "Rare ";
        }
        else if (chance < GradeEpicChance + GradeCommonChance + GradeUncommonChance + GradeRareChance)
        {
            rarity = Grade.Epic;
            gradeText = "Epic ";
        }
        else if (chance < GradeLegendaryChance + GradeCommonChance + GradeUncommonChance + GradeRareChance + GradeEpicChance)
        {
            rarity = Grade.Legendary;
            gradeText = "Legendary ";
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

    public void RandomizeStatValue (Grade rarity, BuffStat stat)
    {
        switch (stat.unitStat)
        {
            case UnitStats.MaxHP:
                AssignBuffValue(stat, new List<int> { 1, 6, 6, 11, 11, 21, 21, 31, 31, 51 });
                buffText += " MaxHP: " + stat.buffValue;
                break;
            case UnitStats.ATK:
                AssignBuffValue(stat, new List<int> { 1, 3, 2, 4, 3, 5, 4, 7, 7, 11 });
                buffText += " ATK: " + stat.buffValue;
                break;
            case UnitStats.SPD:
                AssignBuffValue(stat, new List<int> { 1, 3, 2, 4, 3, 5, 5, 8, 8, 11 });
                buffText += " SPD: " + stat.buffValue;
                break;
            case UnitStats.DEF:
                AssignBuffValue(stat, new List<int> { 1, 3, 2, 4, 3, 5, 4, 7, 7, 11 });
                buffText += " DEF: " + stat.buffValue;
                break;
            case UnitStats.CD:
                //AssignBuffValue(stat, new List<int> {  });
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
                buffText += " CD: " + stat.buffValue;
                break;
            case UnitStats.JUMP:
                AssignBuffValue(stat, new List<int> { 0, 1, 1, 2, 1, 3, 2, 3, 2, 4 });
                buffText += " JUMP: " + stat.buffValue;
                break;
            case UnitStats.jHGHT:
                AssignBuffValue(stat, new List<int> { 0, 2, 0, 2, 1, 3, 1, 4, 2, 5 });
                buffText += " jHGHT: " + stat.buffValue;
                break;
        }
    }

    private void GenerateBuffStatistic(BuffStat buffStatistic)
    {
        do
        {
            PickBuffStat(buffStatistic);
            RandomizeStatValue(rarity, buffStatistic);
        }
        while (buffStatistic.buffValue == 0);//loop if the value is zero
    }

    private void GivePlayerBuff()
    {
        foreach (BuffStat stat in buffStats)
        {
            if (stat.unitStat == UnitStats.MaxHP)
                player.f_MaxHP += stat.buffValue;
            else if (stat.unitStat == UnitStats.ATK)
                player.f_ATK += stat.buffValue;
            else if (stat.unitStat == UnitStats.SPD)
                player.f_SPD += stat.buffValue;
            else if (stat.unitStat == UnitStats.DEF)
                player.f_DEF += stat.buffValue;
            else if (stat.unitStat == UnitStats.CD)
                player.f_CD += stat.buffValue;
            else if (stat.unitStat == UnitStats.JUMP)
                player.f_JUMP += stat.buffValue;
            else if (stat.unitStat == UnitStats.jHGHT)
                player.f_jHeight += stat.buffValue;
        }
    }


    private void Awake()
    {
        player = FindObjectOfType<Characters>();
        bm = FindAnyObjectByType<BuffManager>();
        //TODO: TEMPORARY
        GradeCommonChance = bm.GradeCommonChance;
        GradeUncommonChance = bm.GradeUncommonChance;
        GradeRareChance = bm.GradeRareChance;
        GradeEpicChance = bm.GradeEpicChance;
        GradeLegendaryChance = bm.GradeLegendaryChance;

        MaxHPChance = bm.MaxHPChance;
        ATKChance = bm.ATKChance;
        SPDChance = bm.SPDChance;
        DEFChance = bm.DEFChance;
        CDChance = bm.CDChance;
        JUMPChance = bm.JUMPChance;
        jHGHTChance = bm.jHGHTChance;
        //TEMPORARY

        RandomizeBuffRarity();
        GenerateBuff(rarity);//IM WIDADWUWY DA GOAT
        outputText = gradeText + buffText;
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
