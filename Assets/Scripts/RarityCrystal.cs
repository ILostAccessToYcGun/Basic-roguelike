using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BuffManager;

public class RarityCrystal : Crystals
{

    private void Start()
    {
        TransferStatsFromBuffManagerToPreview();
        UpdateRarityChanceUI();
        cam = FindAnyObjectByType<CameraMovement>();
    }
    public void UpdateRarityChanceUI()
    {
        if (shopKeeper.purchase == ShopKeeper.ShopPurchase.None)
        {
            //TODO: optimize this code later
            if (psGradeCommonChance > buffManager.GradeCommonChance)
                percentages[0].color = moreColour;
            else if (psGradeCommonChance < buffManager.GradeCommonChance)
                percentages[0].color = lessColour;
            else
                percentages[0].color = Color.black;

            if (psGradeUncommonChance > buffManager.GradeUncommonChance)
                percentages[1].color = moreColour;
            else if (psGradeUncommonChance < buffManager.GradeUncommonChance)
                percentages[1].color = lessColour;
            else
                percentages[1].color = Color.black;

            if (psGradeRareChance > buffManager.GradeRareChance)
                percentages[2].color = moreColour;
            else if (psGradeRareChance < buffManager.GradeRareChance)
                percentages[2].color = lessColour;
            else
                percentages[2].color = Color.black;

            if (psGradeEpicChance > buffManager.GradeEpicChance)
                percentages[3].color = moreColour;
            else if (psGradeEpicChance < buffManager.GradeEpicChance)
                percentages[3].color = lessColour;
            else
                percentages[3].color = Color.black;

            if (psGradeLegendaryChance > buffManager.GradeLegendaryChance)
                percentages[4].color = moreColour;
            else if (psGradeLegendaryChance < buffManager.GradeLegendaryChance)
                percentages[4].color = lessColour;
            else
                percentages[4].color = Color.black;
        }
        else
        {
            for (int i = 0; i < percentages.Count; i++)
            {
                percentages[i].color = Color.black;
            }
        }
            
        for (int i = 0; i < percentages.Count; i++)
        {
            float chance = 0f;
            switch (i)
            {
                case 0:
                    chance = (float)psGradeCommonChance;
                    break;
                case 1:
                    chance = (float)psGradeUncommonChance;
                    break;
                case 2:
                    chance = (float)psGradeRareChance;
                    break;
                case 3:
                    chance = (float)psGradeEpicChance;
                    break;
                case 4:
                    chance = (float)psGradeLegendaryChance;
                    break;
            }
            percentages[i].text = Mathf.Floor(chance / (float)psGradeTotalChance * 1000f) / 10 + "%";
        }
    }

    private void ChangeRarity(BuffManager.Grade grade, int modifier) 
    {
        bool allowStatChange = false;
        switch (grade)
        {
            case BuffManager.Grade.Common:
                if (shopKeeper.GetTotalStatDifference() != shopKeeper.allocationLimit)
                    allowStatChange = true;
                else
                {
                    if (!((shopKeeper.saCommon < 0 && modifier < 0) || (shopKeeper.saCommon > 0 && modifier > 0) || shopKeeper.saCommon == 0))
                        allowStatChange = true;
                }
                break;
            case BuffManager.Grade.Uncommon:
                if (shopKeeper.GetTotalStatDifference() != shopKeeper.allocationLimit)
                    allowStatChange = true;
                else
                {
                    if (!((shopKeeper.saUncommon < 0 && modifier < 0) || (shopKeeper.saUncommon > 0 && modifier > 0) || shopKeeper.saUncommon == 0))
                        allowStatChange = true;
                }
                break;
            case BuffManager.Grade.Rare:
                if (shopKeeper.GetTotalStatDifference() != shopKeeper.allocationLimit)
                    allowStatChange = true;
                else
                {
                    if (!((shopKeeper.saRare < 0 && modifier < 0) || (shopKeeper.saRare > 0 && modifier > 0) || shopKeeper.saRare == 0))
                        allowStatChange = true;
                }
                break;
            case BuffManager.Grade.Epic:
                if (shopKeeper.GetTotalStatDifference() != shopKeeper.allocationLimit)
                    allowStatChange = true;
                else
                {
                    if (!((shopKeeper.saEpic < 0 && modifier < 0) || (shopKeeper.saEpic > 0 && modifier > 0) || shopKeeper.saEpic == 0))
                        allowStatChange = true;
                }
                break;
            case BuffManager.Grade.Legendary:
                if (shopKeeper.GetTotalStatDifference() != shopKeeper.allocationLimit)
                    allowStatChange = true;
                else
                {
                    if (!((shopKeeper.saLegendary < 0 && modifier < 0) || (shopKeeper.saLegendary > 0 && modifier > 0) || shopKeeper.saLegendary == 0))
                        allowStatChange = true;
                }
                break;
        }
        if (allowStatChange)
        {
            PSChangeGradeChance(grade, modifier);
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


    public void ConvertPreviewToActual()
    {
        ReverseTransferFromPreviewToBuffManager();
        UpdateRarityChanceUI();
    }

    public void RevertToActual()
    {
        TransferStatsFromBuffManagerToPreview();
        UpdateRarityChanceUI();
    }

    #region Crystal UI Methods
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
    #endregion
}
