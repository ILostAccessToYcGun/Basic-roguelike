using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunStatisticsManager : MonoBehaviour
{
    public string characterType;
    public float totalRunTime;
    public int totalEnemiesKilled;
    public int totalDamageDealt;
    public int totalDamageRecieved;
    public int totalDamageHealed;
    public int totalBuffsConsumed;
    public int totalChancePointsAllocated;

    public void ResetRunStats()
    {
        SetCharacterType("");
        SetRunTimer(0f);
        RunTimerToggle(false);
        SetTotalEnemiesKilled(0);
        SetTotalDamageDealt(0);
        SetTotalDamageRecieved(0);
        SetTotalDamageHealed(0);
        SetTotalBuffsConsumed(0);
        SetTotalChancePointsAllocated(0);
    }

    private void Awake()
    {
        ResetRunStats();
    }
    private void Start()
    {
        RunTimerToggle(true);
    }

    #region characterType

    public void SetCharacterType(string newCharacter) { characterType = newCharacter; }
    public string GetCharacterType() { return characterType; }

    #endregion

    #region totalRunTime

    public void SetRunTimer(float runTime) { totalRunTime = runTime; }
    public float GetRunTime() { return totalRunTime; }
    public void IncrementRunTimer() { totalRunTime += Time.deltaTime; }

    private bool runTimerIsOn;
    public void RunTimerToggle(bool isOn) { runTimerIsOn = isOn; }
    public float GetRunTimerPolarity() { return totalRunTime; }
    private void Update()
    {
        if (runTimerIsOn)
            IncrementRunTimer();
    }

    #endregion

    #region totalEnemiesKilled

    public void SetTotalEnemiesKilled(int killCount) { totalEnemiesKilled = killCount; } //could be useful for saves
    public int GetTotalEnemiesKilled() { return totalEnemiesKilled; }
    public void IncrementTotalEnemiesKilled() { totalEnemiesKilled++; }

    #endregion

    #region totalDamageDealt

    public void SetTotalDamageDealt(int damage) { totalDamageDealt = damage; } //could be useful for saves
    public int GetTotalDamageDealt() { return totalDamageDealt; }
    public void IncreaseTotalDamageDealt(int damage) { totalDamageDealt += damage; }

    #endregion

    #region totalDamageRecieved

    public void SetTotalDamageRecieved(int damageRecieved) { totalDamageRecieved = damageRecieved; } //could be useful for saves
    public int GetTotalDamageRecieved() { return totalDamageRecieved; }
    public void IncreaseTotalDamageRecieved(int damageRecieved) { totalDamageRecieved += damageRecieved; }

    #endregion

    #region totalDamageHealed

    public void SetTotalDamageHealed(int damageHealed) { totalDamageHealed = damageHealed; } //could be useful for saves
    public int GetTotalDamageHealed() { return totalDamageHealed; }
    public void IncreaseTotalDamageHealed(int damageHealed) { totalDamageHealed += damageHealed; }

    #endregion

    #region totalBuffsConsumed

    public void SetTotalBuffsConsumed(int buffsConsumed) { totalBuffsConsumed = buffsConsumed; } //could be useful for saves
    public int GetTotalBuffsConsumed() { return totalBuffsConsumed; }
    public void IncrementTotalBuffsConsumed() { totalBuffsConsumed++; }

    #endregion

    #region totalChancePointsAllocated

    public void SetTotalChancePointsAllocated(int chancePointsAllocated) { totalChancePointsAllocated = chancePointsAllocated; } //could be useful for saves
    public int GetTotalChancePointsAllocated() { return totalChancePointsAllocated; }
    public void IncrementTotalChancePointsAllocated(int points) { totalChancePointsAllocated+= points; }

    #endregion
}
