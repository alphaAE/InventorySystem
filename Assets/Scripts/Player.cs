using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour {
    public static Player Instance { get; set; }
    public int CoinAmount { get; set; }

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        EarnCoin(500);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.G)) {
            for (int i = 0; i < 10; i++) {
                int id = Random.Range(1, 20);
                Knapsack.Instance.StoreItem(id);
            }
        }

        if (Input.GetKeyDown(KeyCode.I)) {
            Character.Instance.DisplaySwitch();
            Knapsack.Instance.DisplaySwitch();
        }

        if (Input.GetKeyDown(KeyCode.O)) {
            Chest.Instance.DisplaySwitch();
        }
    }

    /// <summary>
    /// 消费金币
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool ConsumeCoin(int amount) {
        if (CoinAmount >= amount) {
            CoinAmount -= amount;
            Character.Instance.SetCoinText(CoinAmount);
            return true;
        }

        return false;
    }

    public void EarnCoin(int amount) {
        CoinAmount += amount;
        Character.Instance.SetCoinText(CoinAmount);
    }
}