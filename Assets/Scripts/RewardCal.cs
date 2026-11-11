using UnityEngine;

public class RewardCal : MonoBehaviour
{
    public int playSeconds = 3725;
    public int gold = 1234;
    public int members = 4;
    public int kills = 7;
    public int expPerKill = 35;
    public int totalDamage = 250;
    public int hits = 20;
    public float expRate = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [ContextMenu("정산")]
    void Settle()
    {
        int hours = playSeconds / 3600;
        int minutes = (playSeconds % 3600) / 60;
        int seconds = playSeconds % 60;
        Debug.Log($"플레이 시간: {hours}시간 {minutes}분 {seconds}초");
        int eachGold = gold / members;
        int bonusGold = gold % members;
        Debug.Log($"골드 분배: 1인당 {eachGold}G / 보너스 {bonusGold}G");
        int baseExp = kills * expPerKill;
        int finalExp = (int)(baseExp * expRate);
        Debug.Log($"경험치: {finalExp} (기본 {baseExp})");
        int level = finalExp / 100 + 1;
        int expToNext = 100 - (finalExp % 100);
        Debug.Log($"레벨: Lv {level} / 다음 레벨까지 {expToNext}");
        float averageDamage = (float)totalDamage / hits;
        Debug.Log($"평균 데미지: {averageDamage}");
    }
}


