using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatCrystal : Crystals
{
    private void Start()
    {
        UpdateStatChanceUI();
        cam = FindAnyObjectByType<CameraMovement>();
    }
    public void UpdateStatChanceUI()
    {
        percentages.text = Mathf.Floor((float)buffManager.MaxHPChance / (float)buffManager.StatTotalChance * 1000f) / 10 + "%<br>" +
            Mathf.Floor((float)buffManager.ATKChance / (float)buffManager.StatTotalChance * 1000f) / 10 + "%<br>" +
            Mathf.Floor((float)buffManager.SPDChance / (float)buffManager.StatTotalChance * 1000f) / 10 + "%<br>" +
            Mathf.Floor((float)buffManager.DEFChance / (float)buffManager.StatTotalChance * 1000f) / 10 + "%<br>" +
            Mathf.Floor((float)buffManager.ATKSPDChance / (float)buffManager.StatTotalChance * 1000f) / 10 + "%<br>" +
            Mathf.Floor((float)buffManager.JUMPChance / (float)buffManager.StatTotalChance * 1000f) / 10 + "%<br>" +
            Mathf.Floor((float)buffManager.REGChance / (float)buffManager.StatTotalChance * 1000f) / 10 + "%";
    }

    private void ChangeStat(BuffManager.UnitStats stat, int modifier)
    {
        //                               not sure about this one
        if (shopKeeper.GetTotalStatDifference() + modifier < shopKeeper.allocationLimit + 1)
        {
            buffManager.ChangeStatChance(stat, modifier);
            UpdateStatChanceUI();

            switch (stat)
            {
                case BuffManager.UnitStats.MaxHP:
                    shopKeeper.saMaxHP+= modifier;
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
}
