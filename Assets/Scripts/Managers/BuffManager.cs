using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public class BuffStat
    {
        public UnitStats unitStat;
        public int buffValue;

        public void PickStat()
        {
            unitStat = (UnitStats)Random.Range(0, 7);
        }
    }

    public Characters player;
    //remember these stats will add to the original, not set
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

    private void Awake()
    {
        GradeCommonChance = 45;
        GradeUncommonChance = 25;
        GradeRareChance = 15;
        GradeEpicChance = 10;
        GradeLegendaryChance = 5;
    }
    public void SpawnBuff()
    {

    }
}
