using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    //remember these stats will add to the original, not set
    public enum Grade { Common, Uncommon, Rare, Epic, Legendary }
    public enum UnitStats { MaxHP, ATK, SPD, DEF, CD, JUMP, jHGHT }
    //public enum GameStats { EnemyCount, StageSize }//?
    public int statChance;

    public void SpawnBuff()
    {

    }
}
