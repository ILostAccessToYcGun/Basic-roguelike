using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatCrystal : Crystals
{
    
    private void Start()
    {
        TransferStatsFromBuffManagerToPreview();
        UpdateStatChanceUI();
        cam = FindAnyObjectByType<CameraMovement>();
    }
    public void UpdateStatChanceUI()
    {
        if (shopKeeper.purchase == ShopKeeper.ShopPurchase.None)
        {

            //TODO: optimize this code later
            if (psMaxHPChance > buffManager.MaxHPChance)
                percentages[0].color = moreColour;
            else if (psMaxHPChance < buffManager.MaxHPChance)
                percentages[0].color = lessColour;
            else
                percentages[0].color = Color.black;

            if (psATKChance > buffManager.ATKChance)
                percentages[1].color = moreColour;
            else if (psATKChance < buffManager.ATKChance)
                percentages[1].color = lessColour;
            else
                percentages[1].color = Color.black;

            if (psSPDChance > buffManager.SPDChance)
                percentages[2].color = moreColour;
            else if (psSPDChance < buffManager.SPDChance)
                percentages[2].color = lessColour;
            else
                percentages[2].color = Color.black;

            if (psDEFChance > buffManager.DEFChance)
                percentages[3].color = moreColour;
            else if (psDEFChance < buffManager.DEFChance)
                percentages[3].color = lessColour;
            else
                percentages[3].color = Color.black;

            if (psATKSPDChance > buffManager.ATKSPDChance)
                percentages[4].color = moreColour;
            else if (psATKSPDChance < buffManager.ATKSPDChance)
                percentages[4].color = lessColour;
            else
                percentages[4].color = Color.black;

            if (psJUMPChance > buffManager.JUMPChance)
                percentages[5].color = moreColour;
            else if (psJUMPChance < buffManager.JUMPChance)
                percentages[5].color = lessColour;
            else
                percentages[5].color = Color.black;

            if (psREGChance > buffManager.REGChance)
                percentages[6].color = moreColour;
            else if (psREGChance < buffManager.REGChance)
                percentages[6].color = lessColour;
            else
                percentages[6].color = Color.black;
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
                    chance = (float)psMaxHPChance;
                    break;
                case 1:
                    chance = (float)psATKChance;
                    break;
                case 2:
                    chance = (float)psSPDChance;
                    break;
                case 3:
                    chance = (float)psDEFChance;
                    break;
                case 4:
                    chance = (float)psATKSPDChance;
                    break;
                case 5:
                    chance = (float)psJUMPChance;
                    break;
                case 6:
                    chance = (float)psREGChance;
                    break;
            }
            percentages[i].text = Mathf.Floor(chance / (float)buffManager.StatTotalChance * 1000f) / 10 + "%";
        }
    }

    private void ChangeStat(BuffManager.UnitStats stat, int modifier)
    {
        bool allowStatChange = false;
        switch (stat)
        {
            case BuffManager.UnitStats.MaxHP:
                if (shopKeeper.GetTotalStatDifference() != shopKeeper.allocationLimit)
                    allowStatChange = true;
                else
                {
                    if (!((shopKeeper.saMaxHP < 0 && modifier < 0) || (shopKeeper.saMaxHP > 0 && modifier > 0) || shopKeeper.saMaxHP == 0))
                        allowStatChange = true;
                }
                break;
            case BuffManager.UnitStats.ATK:
                if (shopKeeper.GetTotalStatDifference() != shopKeeper.allocationLimit)
                    allowStatChange = true;
                else
                {
                    if (!((shopKeeper.saATK < 0 && modifier < 0) || (shopKeeper.saATK > 0 && modifier > 0) || shopKeeper.saATK == 0))
                        allowStatChange = true;
                }
                break;
            case BuffManager.UnitStats.SPD:
                if (shopKeeper.GetTotalStatDifference() != shopKeeper.allocationLimit)
                    allowStatChange = true;
                else
                {
                    if (!((shopKeeper.saSPD < 0 && modifier < 0) || (shopKeeper.saSPD > 0 && modifier > 0) || shopKeeper.saSPD == 0))
                        allowStatChange = true;
                }
                break;
            case BuffManager.UnitStats.DEF:
                if (shopKeeper.GetTotalStatDifference() != shopKeeper.allocationLimit)
                    allowStatChange = true;
                else
                {
                    if (!((shopKeeper.saDEF < 0 && modifier < 0) || (shopKeeper.saDEF > 0 && modifier > 0) || shopKeeper.saDEF == 0))
                        allowStatChange = true;
                }
                break;
            case BuffManager.UnitStats.ATKSPD:
                if (shopKeeper.GetTotalStatDifference() != shopKeeper.allocationLimit)
                    allowStatChange = true;
                else
                {
                    if (!((shopKeeper.saATKSPD < 0 && modifier < 0) || (shopKeeper.saATKSPD > 0 && modifier > 0) || shopKeeper.saATKSPD == 0))
                        allowStatChange = true;
                }
                break;
            case BuffManager.UnitStats.JUMP:
                if (shopKeeper.GetTotalStatDifference() != shopKeeper.allocationLimit)
                    allowStatChange = true;
                else
                {
                    if (!((shopKeeper.saJUMP < 0 && modifier < 0) || (shopKeeper.saJUMP > 0 && modifier > 0) || shopKeeper.saJUMP == 0))
                        allowStatChange = true;
                }
                break;
            case BuffManager.UnitStats.REG:
                if (shopKeeper.GetTotalStatDifference() != shopKeeper.allocationLimit)
                    allowStatChange = true;
                else
                {
                    if (!((shopKeeper.saREG < 0 && modifier < 0) || (shopKeeper.saREG > 0 && modifier > 0) || shopKeeper.saREG == 0))
                        allowStatChange = true;
                }
                break;
        }
        if (allowStatChange)
        {
            PSChangeStatChance(stat, modifier); //Dont actually change the stats yet, we want a preview first
            UpdateStatChanceUI();
            switch (stat)
            {
                case BuffManager.UnitStats.MaxHP:
                    shopKeeper.saMaxHP += modifier;
                    break;
                case BuffManager.UnitStats.ATK:
                    shopKeeper.saATK += modifier;
                    break;
                case BuffManager.UnitStats.SPD:
                    shopKeeper.saSPD += modifier;
                    break;
                case BuffManager.UnitStats.DEF:
                    shopKeeper.saDEF += modifier;
                    break;
                case BuffManager.UnitStats.ATKSPD:
                    shopKeeper.saATKSPD += modifier;
                    break;
                case BuffManager.UnitStats.JUMP:
                    shopKeeper.saJUMP += modifier;
                    break;
                case BuffManager.UnitStats.REG:
                    shopKeeper.saREG += modifier;
                    break;
            }
            //TODO: add a way to select your upgrades before locking them in?
        }
    }

    public void ConvertPreviewToActual()
    {
        ReverseTransferFromPreviewToBuffManager();
        UpdateStatChanceUI();
    }
    public void RevertToActual()
    {
        TransferStatsFromBuffManagerToPreview();
        UpdateStatChanceUI();
    }

    #region Crystal UI Methods
    public void HPPlus()
    {
        ChangeStat(BuffManager.UnitStats.MaxHP, 1);
    }
    public void HPMinus()
    {
        ChangeStat(BuffManager.UnitStats.MaxHP, -1);
    }
    public void ATKPlus()
    {
        ChangeStat(BuffManager.UnitStats.ATK, 1);
    }
    public void ATKMinus()
    {
        ChangeStat(BuffManager.UnitStats.ATK, -1);
    }
    public void SPDPlus()
    {
        ChangeStat(BuffManager.UnitStats.SPD, 1);
    }
    public void SPDMinus()
    {
        ChangeStat(BuffManager.UnitStats.SPD, -1);
    }
    public void DEFPlus()
    {
        ChangeStat(BuffManager.UnitStats.DEF, 1);
    }
    public void DEFMinus()
    {
        ChangeStat(BuffManager.UnitStats.DEF, -1);
    }
    public void ATKSPDPlus()
    {
        ChangeStat(BuffManager.UnitStats.ATKSPD, 1);
    }
    public void ATKSPDMinus()
    {
        ChangeStat(BuffManager.UnitStats.ATKSPD, -1);
    }
    public void JUMPPlus()
    {
        ChangeStat(BuffManager.UnitStats.JUMP, 1);
    }
    public void JUMPMinus()
    {
        ChangeStat(BuffManager.UnitStats.JUMP, -1);
    }
    public void REGPlus()
    {
        ChangeStat(BuffManager.UnitStats.REG, 1);
    }
    public void REGMinus()
    {
        ChangeStat(BuffManager.UnitStats.REG, -1);
    }

    #endregion
}
