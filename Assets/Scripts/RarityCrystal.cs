using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BuffManager;

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

    private void ChangeRarity(BuffManager.Grade grade, int modifier) 
    {
        //TODO: THIS NEEDS A LOT OF WORK
        int check = 0;
        switch (grade)
        {
            case BuffManager.Grade.Common:
                check = shopKeeper.saCommon + modifier;
                break;
            case BuffManager.Grade.Uncommon:
                check = shopKeeper.saUncommon + modifier;
                break;
            case BuffManager.Grade.Rare:
                check = shopKeeper.saRare + modifier;
                break;
            case BuffManager.Grade.Epic:
                check = shopKeeper.saEpic + modifier;
                break;
            case BuffManager.Grade.Legendary:
                check = shopKeeper.saLegendary + modifier;
                break;
        }


        if (Mathf.Abs(check) < shopKeeper.allocationLimit)
        {
            buffManager.ChangeGradeChance(grade, modifier);
            UpdateRarityChanceUI();
            switch (grade)
            {
                case BuffManager.Grade.Common:
                    shopKeeper.saCommon += modifier;
                    break;
                case BuffManager.Grade.Uncommon:
                    shopKeeper.saUncommon += modifier;
                    break;
                case BuffManager.Grade.Rare:
                    shopKeeper.saRare += modifier;
                    break;
                case BuffManager.Grade.Epic:
                    shopKeeper.saEpic += modifier;
                    break;
                case BuffManager.Grade.Legendary:
                    shopKeeper.saLegendary += modifier;
                    break;
            }
            //TODO: add a way to select your upgrades before locking them in?
        }
    }

    public void CommonPlus()
    {
        ChangeRarity(BuffManager.Grade.Common, 1);
    }
    public void CommonMinus()
    {
        ChangeRarity(BuffManager.Grade.Common, -1);
    }
    public void UncommonPlus()
    {
        ChangeRarity(BuffManager.Grade.Uncommon, 1);
    }
    public void UncommonMinus()
    {
        ChangeRarity(BuffManager.Grade.Uncommon, -1);
    }

    public void RarePlus()
    {
        ChangeRarity(BuffManager.Grade.Rare, 1);
    }
    public void RareMinus()
    {
        ChangeRarity(BuffManager.Grade.Rare, -1);
    }

    public void EpicPlus()
    {
        ChangeRarity(BuffManager.Grade.Epic, 1);
    }
    public void EpicMinus()
    {
        ChangeRarity(BuffManager.Grade.Epic, -1);
    }

    public void LegendaryPlus()
    {
        ChangeRarity(BuffManager.Grade.Legendary, 1);
    }
    public void LegendaryMinus()
    {
        ChangeRarity(BuffManager.Grade.Legendary, -1);
    }
}
