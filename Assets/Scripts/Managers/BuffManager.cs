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

    public Characters player;
    public enum Grade { Common, Uncommon, Rare, Epic, Legendary }
    public enum UnitStats { MaxHP, ATK, SPD, DEF, CD, JUMP, jHGHT }
    //public enum GameStats { EnemyCount, StageSize }//?

    //this is for later when I let the player manipulate stat odds
    public int statChance;

    public int GradeCommonChance = 45;
    public int GradeUncommonChance = 25;
    public int GradeRareChance = 15;
    public int GradeEpicChance = 10;
    public int GradeLegendaryChance = 5;

    public int MaxHPChance;
    public int ATKChance;
    public int SPDChance;
    public int DEFChance;
    public int CDChance;
    public int JUMPChance;
    public int jHGHTChance;

    private void Awake()
    {
        ResetProbabilities();
    }

    public void PickBuffStat(BuffStat bs)
    {
        int chance = Random.Range(0, MaxHPChance + ATKChance + SPDChance + DEFChance + CDChance + JUMPChance + jHGHTChance);

        if (chance < MaxHPChance)
        {
            bs.unitStat = UnitStats.MaxHP;
        }
        else if (chance < ATKChance + MaxHPChance)
        {
            bs.unitStat = UnitStats.ATK;
        }
        else if (chance < SPDChance + MaxHPChance + ATKChance)
        {
            bs.unitStat = UnitStats.SPD;
        }
        else if (chance < DEFChance + MaxHPChance + ATKChance + SPDChance)
        {
            bs.unitStat = UnitStats.DEF;
        }
        else if (chance < CDChance + MaxHPChance + ATKChance + SPDChance + DEFChance)
        {
            bs.unitStat = UnitStats.CD;
        }
        else if (chance < JUMPChance + CDChance + MaxHPChance + ATKChance + SPDChance + DEFChance)
        {
            bs.unitStat = UnitStats.JUMP;
        }
        else if (chance < jHGHTChance + JUMPChance + CDChance + MaxHPChance + ATKChance + SPDChance + DEFChance)
        {
            bs.unitStat = UnitStats.jHGHT;
        }
    }

    private void ResetProbabilities()
    {
        GradeCommonChance = 45;
        GradeUncommonChance = 25;
        GradeRareChance = 15;
        GradeEpicChance = 10;
        GradeLegendaryChance = 5;

        MaxHPChance = 10;
        ATKChance = 10;
        SPDChance = 10;
        DEFChance = 10;
        CDChance = 10;
        JUMPChance = 10;
        jHGHTChance = 10;
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
                GradeCommonChance += chanceModifier;
                break;
            case Grade.Uncommon:
                GradeUncommonChance += chanceModifier;
                break;
            case Grade.Rare:
                GradeRareChance += chanceModifier;
                break;
            case Grade.Epic:
                GradeEpicChance += chanceModifier;
                break;
            case Grade.Legendary:
                GradeLegendaryChance += chanceModifier;
                break;
        }
    }
}
