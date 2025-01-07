using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Buff : HoverableInteractables
{
    private BuffManager bm;
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
        int chance = Random.Range(0, bm.GradeCommonChance + bm.GradeUncommonChance + bm.GradeRareChance + bm.GradeEpicChance + bm.GradeLegendaryChance);

        //giving a random rarity based on how rare it is ( buff manager for chance )
        if (chance < bm.GradeCommonChance)
        {
            rarity = BuffManager.Grade.Common;
            RarityText.text = "Common";
            BuffDescriptionUI.color = new Color(210f / 255f, 210f / 255f, 210f / 255f, 255f / 255f);
            //BuffDescriptionUI.color = Color.white;
        }
        else if (chance < bm.GradeUncommonChance + bm.GradeCommonChance)
        {
            rarity = BuffManager.Grade.Uncommon;
            RarityText.text = "Uncommon";
            BuffDescriptionUI.color = new Color(147f / 255f, 255f / 255f, 129f / 255f, 255f / 255f);
            //BuffDescriptionUI.color = Color.green;
        }
        else if (chance < bm.GradeRareChance + bm.GradeCommonChance + bm.GradeUncommonChance)
        {
            rarity = BuffManager.Grade.Rare;
            RarityText.text = "Rare";
            BuffDescriptionUI.color = new Color(100f / 255f, 205f / 255f, 255f / 255f, 255f / 255f);
            //BuffDescriptionUI.color = Color.blue;
        }
        else if (chance < bm.GradeEpicChance + bm.GradeCommonChance + bm.GradeUncommonChance + bm.GradeRareChance)
        {
            rarity = BuffManager.Grade.Epic;
            RarityText.text = "Epic";
            BuffDescriptionUI.color = new Color(200f / 255f, 100f / 255f, 255f / 255f, 255f / 255f);
            //BuffDescriptionUI.color = Color.magenta;
        }
        else if (chance < bm.GradeLegendaryChance + bm.GradeCommonChance + bm.GradeUncommonChance + bm.GradeRareChance + bm.GradeEpicChance)
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
                BuffStatsText.text += "MaxHP +" + stat.buffValue;
                break;
            case BuffManager.UnitStats.ATK:
                AssignBuffValue(stat, new List<int> { 1, 3, 2, 4, 3, 5, 4, 7, 7, 11 });
                BuffStatsText.text += "ATK +" + stat.buffValue;
                break;
            case BuffManager.UnitStats.SPD:
                AssignBuffValue(stat, new List<int> { 1, 3, 2, 4, 3, 5, 5, 8, 8, 11 });
                BuffStatsText.text += "SPD +" + stat.buffValue;
                break;
            case BuffManager.UnitStats.DEF:
                AssignBuffValue(stat, new List<int> { 1, 3, 2, 4, 3, 5, 4, 7, 7, 11 });
                BuffStatsText.text += "DEF +" + stat.buffValue;
                break;
            case BuffManager.UnitStats.CD:
                //AssignBuffValue(stat, new List<int> {  });
                switch (rarity)
                {
                    case BuffManager.Grade.Common:
                    case BuffManager.Grade.Uncommon:
                    case BuffManager.Grade.Rare:
                    case BuffManager.Grade.Epic:
                    case BuffManager.Grade.Legendary:
                        stat.buffValue = 1; //TODO: this needs to change once we rework CD
                        break;
                }
                BuffStatsText.text += "CD +" + stat.buffValue;
                break;
            case BuffManager.UnitStats.JUMP:
                AssignBuffValue(stat, new List<int> { 0, 1, 1, 2, 1, 3, 2, 3, 2, 4 });
                BuffStatsText.text += "Jumps +" + stat.buffValue;
                break;
            case BuffManager.UnitStats.jHGHT:
                AssignBuffValue(stat, new List<int> { 0, 2, 0, 2, 1, 3, 1, 4, 2, 5 });
                BuffStatsText.text += "Jump Height +" + stat.buffValue;
                break;
        }
    }

    private void GenerateBuffStatistic(BuffManager.BuffStat buffStatistic)
    {
        do
        {
            bm.PickBuffStat(buffStatistic);
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
            else if (buffStats[i].unitStat == BuffManager.UnitStats.CD)
                player.f_CD += buffStats[i].buffValue;
            else if (buffStats[i].unitStat == BuffManager.UnitStats.JUMP)
                player.f_JUMP += buffStats[i].buffValue;
            else if (buffStats[i].unitStat == BuffManager.UnitStats.jHGHT)
                player.f_jHeight += buffStats[i].buffValue;
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
        uiManager.UpdatePlayerStatsUI();
        Destroy(gameObject);
    }


    private void Awake()
    {
        player = FindObjectOfType<Characters>();
        bm = FindAnyObjectByType<BuffManager>();
        uiManager = FindAnyObjectByType<UIManager>();
        cam = FindAnyObjectByType<CameraMovement>();
        BuffStatsText.text = "";

        RandomizeBuffRarity();
        GenerateBuff(rarity);
        outputText = gradeText + buffText;
    }
}
