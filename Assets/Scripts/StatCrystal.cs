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

    public void HPPlus()
    {
        buffManager.ChangeStatChance(BuffManager.UnitStats.MaxHP, 1);
        UpdateStatChanceUI();
    }
    public void HPMinus()
    {
        buffManager.ChangeStatChance(BuffManager.UnitStats.MaxHP, -1);
        UpdateStatChanceUI();
    }
    public void ATKPlus()
    {
        buffManager.ChangeStatChance(BuffManager.UnitStats.ATK, 1);
        UpdateStatChanceUI();
    }
    public void ATKMinus()
    {
        buffManager.ChangeStatChance(BuffManager.UnitStats.ATK, -1);
        UpdateStatChanceUI();
    }

    public void SPDPlus()
    {
        buffManager.ChangeStatChance(BuffManager.UnitStats.SPD, 1);
        UpdateStatChanceUI();
    }
    public void SPDMinus()
    {
        buffManager.ChangeStatChance(BuffManager.UnitStats.SPD, -1);
        UpdateStatChanceUI();
    }

    public void DEFPlus()
    {
        buffManager.ChangeStatChance(BuffManager.UnitStats.DEF, 1);
        UpdateStatChanceUI();
    }
    public void DEFMinus()
    {
        buffManager.ChangeStatChance(BuffManager.UnitStats.DEF, -1);
        UpdateStatChanceUI();
    }

    public void ATKSPDPlus()
    {
        buffManager.ChangeStatChance(BuffManager.UnitStats.ATKSPD, 1);
        UpdateStatChanceUI();
    }
    public void ATKSPDMinus()
    {
        buffManager.ChangeStatChance(BuffManager.UnitStats.ATKSPD, -1);
        UpdateStatChanceUI();
    }

    public void JUMPPlus()
    {
        buffManager.ChangeStatChance(BuffManager.UnitStats.JUMP, 1);
        UpdateStatChanceUI();
    }
    public void JUMPMinus()
    {
        buffManager.ChangeStatChance(BuffManager.UnitStats.JUMP, -1);
        UpdateStatChanceUI();
    }

    public void REGPlus()
    {
        buffManager.ChangeStatChance(BuffManager.UnitStats.REG, 1);
        UpdateStatChanceUI();
    }
    public void REGMinus()
    {
        buffManager.ChangeStatChance(BuffManager.UnitStats.REG, -1);
        UpdateStatChanceUI();
    }
}
