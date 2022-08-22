using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour {
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
}