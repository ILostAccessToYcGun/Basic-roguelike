using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public class BuffStat
    {
        private BuffManager bm;
        public UnitStats unitStat;
        public int buffValue;
    }

    public Rigidbody2D buff;
    
    
    public enum Grade { Common, Uncommon, Rare, Epic, Legendary }
    public enum UnitStats { MaxHP, ATK, SPD, DEF, CD, JUMP, jHGHT }
    //public enum GameStats { EnemyCount, StageSize }//?

    //this is for later when I let the player manipulate stat odds
    public int statChance;

    public int GradeCommonChance;
    public int GradeUncommonChance;
    public int GradeRareChance;
    public int GradeEpicChance;
    public int GradeLegendaryChance;
    public int GradeTotalChance;

    public int MaxHPChance;
    public int ATKChance;
    public int SPDChance;
    public int DEFChance;
    public int CDChance;
    public int JUMPChance;
    public int jHGHTChance;
    public int StatTotalChance;

    private void Awake()
    {
        ResetProbabilities();
    }

    public void PickBuffStat(BuffStat bs)
    {
        int chance = Random.Range(0, MaxHPChance + ATKChance + SPDChance + DEFChance + CDChance + JUMPChance + jHGHTChance);

        if (chance < MaxHPChance)
            bs.unitStat = UnitStats.MaxHP;
        else if (chance < ATKChance + MaxHPChance)
            bs.unitStat = UnitStats.ATK;
        else if (chance < SPDChance + MaxHPChance + ATKChance)
            bs.unitStat = UnitStats.SPD;
        else if (chance < DEFChance + MaxHPChance + ATKChance + SPDChance)
            bs.unitStat = UnitStats.DEF;
        else if (chance < CDChance + MaxHPChance + ATKChance + SPDChance + DEFChance)
            bs.unitStat = UnitStats.CD;
        else if (chance < JUMPChance + CDChance + MaxHPChance + ATKChance + SPDChance + DEFChance)
            bs.unitStat = UnitStats.JUMP;
        else if (chance < jHGHTChance + JUMPChance + CDChance + MaxHPChance + ATKChance + SPDChance + DEFChance)
            bs.unitStat = UnitStats.jHGHT;
    }

    private void ResetProbabilities()
    {
        GradeCommonChance = 45;
        GradeUncommonChance = 25;
        GradeRareChance = 15;
        GradeEpicChance = 10;
        GradeLegendaryChance = 5;
        TotalGradeChance();

        MaxHPChance = 10;
        ATKChance = 10;
        SPDChance = 10;
        DEFChance = 10;
        CDChance = 10;
        JUMPChance = 10;
        jHGHTChance = 10;
        TotalStatChance();
    }

    public void SpawnBuff()
    {
        Rigidbody2D buffPickup;
        buffPickup = Instantiate(buff, transform.position, transform.rotation);
        //NormalEnemy enemyScript = enemy.GetComponent<NormalEnemy>();
    }

    //I'm not sure how I want to do the chances, whether everything should add up to 100, or do addative chance
    //for now, because its easy it will do addative chance
    public void ChangeGradeChance(Grade grade, int chanceModifier)
    {
        switch (grade)
        {
            case Grade.Common:
                if (GradeCommonChance + chanceModifier >= 0)
                    GradeCommonChance += chanceModifier;
                break;
            case Grade.Uncommon:
                if (GradeUncommonChance + chanceModifier >= 0)
                    GradeUncommonChance += chanceModifier;
                break;
            case Grade.Rare:
                if (GradeRareChance + chanceModifier >= 0)
                    GradeRareChance += chanceModifier;
                break;
            case Grade.Epic:
                if (GradeLegendaryChance + chanceModifier >= 0)
                    GradeEpicChance += chanceModifier;
                break;
            case Grade.Legendary:
                if (GradeLegendaryChance + chanceModifier >= 0)
                    GradeLegendaryChance += chanceModifier;
                break;
        }
        TotalGradeChance();

    }

    public void TotalGradeChance()
    {
        GradeTotalChance = GradeCommonChance + GradeUncommonChance + GradeRareChance + GradeEpicChance + GradeLegendaryChance;
    }
    public void ChangeStatChance(UnitStats stat, int chanceModifier)
    {
        switch (stat)
        {
            case UnitStats.MaxHP:
                if (MaxHPChance + chanceModifier >= 0)
                    MaxHPChance += chanceModifier;
                break;
            case UnitStats.ATK:
                if (ATKChance + chanceModifier >= 0)
                    ATKChance += chanceModifier;
                break;
            case UnitStats.SPD:
                if (SPDChance + chanceModifier >= 0)
                    SPDChance += chanceModifier;
                break;
            case UnitStats.DEF:
                if (DEFChance + chanceModifier >= 0)
                    DEFChance += chanceModifier;
                break;
            case UnitStats.CD:
                if (CDChance + chanceModifier >= 0)
                    CDChance += chanceModifier;
                break;
            case UnitStats.JUMP:
                if (JUMPChance + chanceModifier >= 0)
                    JUMPChance += chanceModifier;
                break;
            case UnitStats.jHGHT:
                if (jHGHTChance + chanceModifier >= 0)
                    jHGHTChance += chanceModifier;
                break;
        }
        TotalStatChance();
    }

    public void TotalStatChance()
    {
        StatTotalChance = MaxHPChance + ATKChance + SPDChance + DEFChance + CDChance + JUMPChance + jHGHTChance;
    }


    //TESTING

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SpawnBuff();
        }
    }
}
