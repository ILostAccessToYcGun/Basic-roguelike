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

}

