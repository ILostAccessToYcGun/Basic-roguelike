using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Buff : HoverableInteractables
{
    private BuffManager buffManager;
    private Characters player;

    private UIManager uiManager;
    public Image BuffDescriptionUI;
    public TextMeshProUGUI RarityText;
    public TextMeshProUGUI BuffStatsText;

    public BuffManager.Grade rarity;
    public bool cursed;
    private List<BuffManager.BuffStat> buffStats = new List<BuffManager.BuffStat>();
    //public int chance;

    [SerializeField] string outputText;
    private string gradeText = "";
    private string buffText = "";

    private void RandomizeBuffRarity()
    {
        int chance = Random.Range(0, buffManager.GradeCommonChance + buffManager.GradeUncommonChance + buffManager.GradeRareChance + buffManager.GradeEpicChance + buffManager.GradeLegendaryChance);

        //giving a random rarity based on how rare it is ( buff manager for chance )
        if (chance < buffManager.GradeCommonChance)
        {
            rarity = BuffManager.Grade.Common;
            RarityText.text = "Common";
            BuffDescriptionUI.color = new Color(210f / 255f, 210f / 255f, 210f / 255f, 255f / 255f);
            //BuffDescriptionUI.color = Color.white;
        }
        else if (chance < buffManager.GradeUncommonChance + buffManager.GradeCommonChance)
        {
            rarity = BuffManager.Grade.Uncommon;
            RarityText.text = "Uncommon";
            BuffDescriptionUI.color = new Color(147f / 255f, 255f / 255f, 129f / 255f, 255f / 255f);
            //BuffDescriptionUI.color = Color.green;
        }
        else if (chance < buffManager.GradeRareChance + buffManager.GradeCommonChance + buffManager.GradeUncommonChance)
        {
            rarity = BuffManager.Grade.Rare;
            RarityText.text = "Rare";
            BuffDescriptionUI.color = new Color(100f / 255f, 205f / 255f, 255f / 255f, 255f / 255f);
            //BuffDescriptionUI.color = Color.blue;
        }
        else if (chance < buffManager.GradeEpicChance + buffManager.GradeCommonChance + buffManager.GradeUncommonChance + buffManager.GradeRareChance)
        {
            rarity = BuffManager.Grade.Epic;
            RarityText.text = "Epic";
            BuffDescriptionUI.color = new Color(200f / 255f, 100f / 255f, 255f / 255f, 255f / 255f);
            //BuffDescriptionUI.color = Color.magenta;
        }
        else if (chance < buffManager.GradeLegendaryChance + buffManager.GradeCommonChance + buffManager.GradeUncommonChance + buffManager.GradeRareChance + buffManager.GradeEpicChance)
        {
            rarity = BuffManager.Grade.Legendary;
            RarityText.text = "Legendary";
            BuffDescriptionUI.color = new Color(255f / 255f, 255f / 255f, 100f / 255f, 255f / 255f);
            //BuffDescriptionUI.color = Color.yellow;
        }
        else
        {
            RarityText.text = "Glitched";
            BuffDescriptionUI.color = new Color(255f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            //BuffDescriptionUI.color = Color.red;
        }
    }

    private void GenerateBuff(BuffManager.Grade rarity)
    {
        switch (rarity)
        {
            case BuffManager.Grade.Common:
            case BuffManager.Grade.Uncommon:
                AddStatsToBuff(1);
                break;
            case BuffManager.Grade.Rare:
            case BuffManager.Grade.Epic:
                AddStatsToBuff(2);
                break;
            case BuffManager.Grade.Legendary:
                AddStatsToBuff(3);
                break;
        }
    }

    private void AddStatsToBuff(int num)
    {
        BuffManager.BuffStat buffStatistic = new BuffManager.BuffStat();
        buffStatistic.buffValue = 0;
        buffStatistic.unitStat = BuffManager.UnitStats.MaxHP;
        for (int i = 0; i < num; i++)
        {
            if (i != 0)
            {
                BuffStatsText.text += "<br>";
            }
            GenerateBuffStatistic(buffStatistic);
            buffStats.Add(buffStatistic);
        }
    }

    private void AssignBuffValue(BuffManager.BuffStat stat, List<int> ranges)
    {
        switch (rarity)
        {
            case BuffManager.Grade.Common:
                stat.buffValue = Random.Range(ranges[0], ranges[1]);
                break;
            case BuffManager.Grade.Uncommon:
                stat.buffValue = Random.Range(ranges[2], ranges[3]);
                break;
            case BuffManager.Grade.Rare:
                stat.buffValue = Random.Range(ranges[4], ranges[5]);
                break;
            case BuffManager.Grade.Epic:
                stat.buffValue = Random.Range(ranges[6], ranges[7]);
                break;
            case BuffManager.Grade.Legendary:
                stat.buffValue = Random.Range(ranges[8], ranges[9]);
                break;
        }
    }

    private void RandomizeStatValue (BuffManager.Grade rarity, BuffManager.BuffStat stat)
    {
        switch (stat.unitStat)
        {
            case BuffManager.UnitStats.MaxHP:
                AssignBuffValue(stat, new List<int> { 1, 6, 6, 11, 11, 21, 21, 31, 31, 51 });
                if (stat.buffValue == 0)
                    break;
                BuffStatsText.text += "MaxHP +" + stat.buffValue;
                break;
            case BuffManager.UnitStats.ATK:
                AssignBuffValue(stat, new List<int> { 1, 3, 2, 4, 3, 5, 4, 7, 7, 11 });
                if (stat.buffValue == 0)
                    break;
                BuffStatsText.text += "ATK +" + stat.buffValue;
                break;
            case BuffManager.UnitStats.SPD:
                AssignBuffValue(stat, new List<int> { 1, 3, 2, 4, 3, 5, 5, 8, 8, 11 });
                if (stat.buffValue == 0)
                    break;
                BuffStatsText.text += "SPD +" + stat.buffValue;
                break;
            case BuffManager.UnitStats.DEF:
                AssignBuffValue(stat, new List<int> { 1, 3, 2, 4, 3, 5, 4, 7, 7, 11 });
                if (stat.buffValue == 0)
                    break;
                BuffStatsText.text += "DEF +" + stat.buffValue;
                break;
            case BuffManager.UnitStats.ATKSPD:
                AssignBuffValue(stat, new List<int> { 1, 2, 1, 3, 2, 3, 2, 4, 3, 5 });
                if (stat.buffValue == 0)
                    break;
                BuffStatsText.text += "ATKSPD +" + stat.buffValue;
                break;
            case BuffManager.UnitStats.JUMP:
                AssignBuffValue(stat, new List<int> { 0, 1, 1, 2, 1, 3, 2, 3, 2, 4 });
                if (stat.buffValue == 0)
                    break;
                BuffStatsText.text += "Jumps +" + stat.buffValue;
                break;
            case BuffManager.UnitStats.REG:
                AssignBuffValue(stat, new List<int> { 0, 2, 0, 2, 1, 3, 1, 4, 2, 5 });
                if (stat.buffValue == 0)
                    break;
                BuffStatsText.text += "Health Regen +" + stat.buffValue;
                break;
        }
    }

    private void GenerateBuffStatistic(BuffManager.BuffStat buffStatistic)
    {
        do
        {
            buffManager.PickBuffStat(buffStatistic);
            RandomizeStatValue(rarity, buffStatistic);
        }
        while (buffStatistic.buffValue == 0);//loop if the value is zero
    }

    public void GivePlayerBuff() 
    {
        //TODO: There is an issue here, when there are 2 or more stats present, it specs multiple times into one instead of the listed stats
        for (int i = 0; i < buffStats.Count; i++)
        {
            if (buffStats[i].unitStat == BuffManager.UnitStats.MaxHP)
            {
                player.f_MaxHP += buffStats[i].buffValue;
                player.Heal(player.f_MaxHP - player.CurrentHP);
            }
            else if (buffStats[i].unitStat == BuffManager.UnitStats.ATK)
                player.f_ATK += buffStats[i].buffValue;
            else if (buffStats[i].unitStat == BuffManager.UnitStats.SPD)
                player.f_SPD += buffStats[i].buffValue;
            else if (buffStats[i].unitStat == BuffManager.UnitStats.DEF)
                player.f_DEF += buffStats[i].buffValue;
            else if (buffStats[i].unitStat == BuffManager.UnitStats.ATKSPD)
                player.f_ATKSPD += buffStats[i].buffValue;
            else if (buffStats[i].unitStat == BuffManager.UnitStats.JUMP)
                player.f_JUMP += buffStats[i].buffValue;
            else if (buffStats[i].unitStat == BuffManager.UnitStats.REG)
                player.f_REG += buffStats[i].buffValue;
        }

        //foreach (BuffManager.BuffStat stat in buffStats)
        //{
        //    if (stat.unitStat == BuffManager.UnitStats.MaxHP)
        //    {
        //        player.f_MaxHP += stat.buffValue;
        //        player.Heal(player.f_MaxHP - player.CurrentHP);
        //    }
        //    else if (stat.unitStat == BuffManager.UnitStats.ATK)
        //        player.f_ATK += stat.buffValue;
        //    else if (stat.unitStat == BuffManager.UnitStats.SPD)
        //        player.f_SPD += stat.buffValue;
        //    else if (stat.unitStat == BuffManager.UnitStats.DEF)
        //        player.f_DEF += stat.buffValue;
        //    else if (stat.unitStat == BuffManager.UnitStats.CD)
        //        player.f_CD += stat.buffValue;
        //    else if (stat.unitStat == BuffManager.UnitStats.JUMP)
        //        player.f_JUMP += stat.buffValue;
        //    else if (stat.unitStat == BuffManager.UnitStats.jHGHT)
        //        player.f_jHeight += stat.buffValue;
        //}
        uiManager.PlayerStats_UpdatePlayerStatsUI();
        //buffManager.buffsAlive.Remove(this.gameObject);
        //Destroy(gameObject);
        buffManager.rsManager.IncrementTotalBuffsConsumed();
        buffManager.ClearAllAliveBuffs();
    }


    private void Awake()
    {
        player = FindObjectOfType<Characters>();
        buffManager = FindAnyObjectByType<BuffManager>();
        uiManager = FindAnyObjectByType<UIManager>();
        cam = FindAnyObjectByType<CameraMovement>();
        BuffStatsText.text = "";

        RandomizeBuffRarity();
        GenerateBuff(rarity);
        outputText = gradeText + buffText;
    }
}
