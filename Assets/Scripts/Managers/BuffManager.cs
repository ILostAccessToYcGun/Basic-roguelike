using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public class BuffStat
    {
        private BuffManager buffManager;
        public UnitStats unitStat;
        public int buffValue;
    }

    public GameObject buff;
    
    
    public enum Grade { Common, Uncommon, Rare, Epic, Legendary }
    public enum UnitStats { MaxHP, ATK, SPD, DEF, ATKSPD, JUMP, REG }
    //public enum GameStats { EnemyCount, StageSize }//?


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
    public int ATKSPDChance;
    public int JUMPChance;
    public int REGChance;
    public int StatTotalChance;

    public BuffSpawnLocation[] buffLocations;
    public int buffLocationIndex = 0;
    public List<GameObject> buffsAlive;

    public ShopKeeper currentShop;
      
    private void Awake()
    {
        ResetProbabilities();
    }

    public void PickBuffStat(BuffStat bs)
    {
        int chance = Random.Range(0, MaxHPChance + ATKChance + SPDChance + DEFChance + ATKSPDChance + JUMPChance + REGChance);

        if (chance < MaxHPChance)
            bs.unitStat = UnitStats.MaxHP;
        else if (chance < ATKChance + MaxHPChance)
            bs.unitStat = UnitStats.ATK;
        else if (chance < SPDChance + MaxHPChance + ATKChance)
            bs.unitStat = UnitStats.SPD;
        else if (chance < DEFChance + MaxHPChance + ATKChance + SPDChance)
            bs.unitStat = UnitStats.DEF;
        else if (chance < ATKSPDChance + MaxHPChance + ATKChance + SPDChance + DEFChance)
            bs.unitStat = UnitStats.ATKSPD;
        else if (chance < JUMPChance + ATKSPDChance + MaxHPChance + ATKChance + SPDChance + DEFChance)
            bs.unitStat = UnitStats.JUMP;
        else if (chance < REGChance + JUMPChance + ATKSPDChance + MaxHPChance + ATKChance + SPDChance + DEFChance)
            bs.unitStat = UnitStats.REG;
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
        ATKSPDChance = 10;
        JUMPChance = 10;
        REGChance = 10;
        TotalStatChance();
    }

    private void MoveToBuffLocation()
    {
        transform.position = buffLocations[buffLocationIndex].transform.position;
    }

    public void IncrementBuffIndex()
    {
        buffLocationIndex++;
    }

    public void ResetBuffIndex()
    {
        buffLocationIndex = 0;
    }

    public void SpawnBuff() //TODO: chnage access modifier
    {
        GameObject buffPickup;
        buffPickup = Instantiate(buff, transform.position, transform.rotation);
        buffsAlive.Add(buffPickup);
    }

    public void SpawnBuffsInShop()
    {
        for (int i = 0; i < buffLocations.Length; i++)
        {
            MoveToBuffLocation();
            SpawnBuff();
            IncrementBuffIndex();
        }
    }

    public void ClearAllAliveBuffs()
    {
        for (int i = 0; i < buffsAlive.Count;)
        {
            Destroy(buffsAlive[i]);
            buffsAlive.Remove(buffsAlive[i]);
        }
        buffsAlive.Clear();

        if (currentShop != null)
        {
            if (currentShop.purchase == ShopKeeper.ShopPurchase.None)
            {
                currentShop.SetShopPurchase(ShopKeeper.ShopPurchase.Buff);
                currentShop.allocationLimit = 0;
            }
            
        }
        else
            Debug.Log("HUH");
    }
    
    public void SetCurrentShop(ShopKeeper newShop)
    {
        currentShop = newShop;
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
            case UnitStats.ATKSPD:
                if (ATKSPDChance + chanceModifier >= 0)
                    ATKSPDChance += chanceModifier;
                break;
            case UnitStats.JUMP:
                if (JUMPChance + chanceModifier >= 0)
                    JUMPChance += chanceModifier;
                break;
            case UnitStats.REG:
                if (REGChance + chanceModifier >= 0)
                    REGChance += chanceModifier;
                break;
        }
        TotalStatChance();
    }

    public void TotalStatChance()
    {
        StatTotalChance = MaxHPChance + ATKChance + SPDChance + DEFChance + ATKSPDChance + JUMPChance + REGChance;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SpawnBuff();
        }
    }
}
