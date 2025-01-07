using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RarityCrystal : Crystals
{

    private void Start()
    {
        UpdateRarityChanceUI();
        cam = FindAnyObjectByType<CameraMovement>();
    }
    public void UpdateRarityChanceUI()
    {
        percentages.text = Mathf.Floor((float)buffManager.GradeCommonChance / (float)buffManager.GradeTotalChance * 1000f) / 10 + "%<br>" +
            Mathf.Floor((float)buffManager.GradeUncommonChance / (float)buffManager.GradeTotalChance * 1000f) / 10 + "%<br>" +
            Mathf.Floor((float)buffManager.GradeRareChance / (float)buffManager.GradeTotalChance * 1000f) / 10 + "%<br>" +
            Mathf.Floor((float)buffManager.GradeEpicChance / (float)buffManager.GradeTotalChance * 1000f) / 10 + "%<br>" +
            Mathf.Floor((float)buffManager.GradeLegendaryChance / (float)buffManager.GradeTotalChance * 1000f) / 10 + "%";
    }

    public void CommonPlus()
    {
        buffManager.ChangeGradeChance(BuffManager.Grade.Common, 1);
        UpdateRarityChanceUI();
    }
    public void CommonMinus()
    {
        buffManager.ChangeGradeChance(BuffManager.Grade.Common, -1);
        UpdateRarityChanceUI();
    }
    public void UncommonPlus()
    {
        buffManager.ChangeGradeChance(BuffManager.Grade.Uncommon, 1);
        UpdateRarityChanceUI();
    }
    public void UncommonMinus()
    {
        buffManager.ChangeGradeChance(BuffManager.Grade.Uncommon, -1);
        UpdateRarityChanceUI();
    }

    public void RarePlus()
    {
        buffManager.ChangeGradeChance(BuffManager.Grade.Rare, 1);
        UpdateRarityChanceUI();
    }
    public void RareMinus()
    {
        buffManager.ChangeGradeChance(BuffManager.Grade.Rare, -1);
        UpdateRarityChanceUI();
    }

    public void EpicPlus()
    {
        buffManager.ChangeGradeChance(BuffManager.Grade.Epic, 1);
        UpdateRarityChanceUI();
    }
    public void EpicMinus()
    {
        buffManager.ChangeGradeChance(BuffManager.Grade.Epic, -1);
        UpdateRarityChanceUI();
    }

    public void LegendaryPlus()
    {
        buffManager.ChangeGradeChance(BuffManager.Grade.Legendary, 1);
        UpdateRarityChanceUI();
    }
    public void LegendaryMinus()
    {
        buffManager.ChangeGradeChance(BuffManager.Grade.Legendary, -1);
        UpdateRarityChanceUI();
    }
}
