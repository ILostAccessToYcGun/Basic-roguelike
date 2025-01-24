using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static BuffManager;

public class Crystals : HoverableInteractables
{
    protected BuffManager buffManager;
    public List<TextMeshProUGUI> percentages; //uhhhhhhh :) //TODO: preview system, to see the changes with colour before you lock them in 
    public ShopKeeper shopKeeper;
    protected int psGradeCommonChance;
    protected int psGradeUncommonChance;
    protected int psGradeRareChance;
    protected int psGradeEpicChance;
    protected int psGradeLegendaryChance;
    protected int psGradeTotalChance;
    protected int psMaxHPChance;
    protected int psATKChance;
    protected int psSPDChance;
    protected int psDEFChance;
    protected int psATKSPDChance;
    protected int psJUMPChance;
    protected int psREGChance;
    protected int psStatTotalChance;

    protected Color lessColour = new Color(200f / 255f, 90f / 255f, 40f / 255f, 255f / 255f);
    protected Color moreColour = new Color(120f / 255f, 200f / 255f, 40f / 255f, 255f / 255f);
    private void Awake()
    {
        buffManager = FindAnyObjectByType<BuffManager>();
    }

    protected void TransferStatsFromBuffManagerToPreview()
    {
        psGradeCommonChance = buffManager.GradeCommonChance;
        psGradeUncommonChance = buffManager.GradeUncommonChance;
        psGradeRareChance = buffManager.GradeRareChance;
        psGradeEpicChance = buffManager.GradeEpicChance;
        psGradeLegendaryChance = buffManager.GradeLegendaryChance;
        psGradeTotalChance = buffManager.GradeTotalChance;
        psMaxHPChance = buffManager.MaxHPChance;
        psATKChance = buffManager.ATKChance;
        psSPDChance = buffManager.SPDChance;
        psDEFChance = buffManager.DEFChance;
        psATKSPDChance = buffManager.ATKSPDChance;
        psJUMPChance = buffManager.JUMPChance;
        psREGChance = buffManager.REGChance;
        psStatTotalChance = buffManager.StatTotalChance;
    }

    protected void ReverseTransferFromPreviewToBuffManager()
    {
        buffManager.GradeCommonChance = psGradeCommonChance;
        buffManager.GradeUncommonChance = psGradeUncommonChance;
        buffManager.GradeRareChance = psGradeRareChance;
        buffManager.GradeEpicChance = psGradeEpicChance;
        buffManager.GradeLegendaryChance = psGradeLegendaryChance;
        buffManager.GradeTotalChance = psGradeTotalChance;
        buffManager.MaxHPChance = psMaxHPChance;
        buffManager.ATKChance = psATKChance;
        buffManager.SPDChance = psSPDChance;
        buffManager.DEFChance = psDEFChance;
        buffManager.ATKSPDChance = psATKSPDChance;
        buffManager.JUMPChance = psJUMPChance;
        buffManager.REGChance = psREGChance;
        buffManager.StatTotalChance = psStatTotalChance;
    }

    public void PSChangeGradeChance(Grade grade, int chanceModifier)
    {
        switch (grade)
        {
            case Grade.Common:
                if (psGradeCommonChance + chanceModifier >= 0)
                    psGradeCommonChance += chanceModifier;
                break;
            case Grade.Uncommon:
                if (psGradeUncommonChance + chanceModifier >= 0)
                    psGradeUncommonChance += chanceModifier;
                break;
            case Grade.Rare:
                if (psGradeRareChance + chanceModifier >= 0)
                    psGradeRareChance += chanceModifier;
                break;
            case Grade.Epic:
                if (psGradeLegendaryChance + chanceModifier >= 0)
                    psGradeEpicChance += chanceModifier;
                break;
            case Grade.Legendary:
                if (psGradeLegendaryChance + chanceModifier >= 0)
                    psGradeLegendaryChance += chanceModifier;
                break;
        }
        psGradeTotalChance = psGradeCommonChance + psGradeUncommonChance + psGradeRareChance + psGradeEpicChance + psGradeLegendaryChance;

    }

    public void PSChangeStatChance(UnitStats stat, int chanceModifier)
    {
        switch (stat)
        {
            case UnitStats.MaxHP:
                if (psMaxHPChance + chanceModifier >= 0)
                    psMaxHPChance += chanceModifier;
                break;
            case UnitStats.ATK:
                if (psATKChance + chanceModifier >= 0)
                    psATKChance += chanceModifier;
                break;
            case UnitStats.SPD:
                if (psSPDChance + chanceModifier >= 0)
                    psSPDChance += chanceModifier;
                break;
            case UnitStats.DEF:
                if (psDEFChance + chanceModifier >= 0)
                    psDEFChance += chanceModifier;
                break;
            case UnitStats.ATKSPD:
                if (psATKSPDChance + chanceModifier >= 0)
                    psATKSPDChance += chanceModifier;
                break;
            case UnitStats.JUMP:
                if (psJUMPChance + chanceModifier >= 0)
                    psJUMPChance += chanceModifier;
                break;
            case UnitStats.REG:
                if (psREGChance + chanceModifier >= 0)
                    psREGChance += chanceModifier;
                break;
        }
        psStatTotalChance = psMaxHPChance + psATKChance + psSPDChance + psDEFChance + psATKSPDChance + psJUMPChance + psREGChance;
    }
}

