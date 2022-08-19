using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Test : MonoBehaviour {
    private void Update() {
        if (Input.GetKeyDown(KeyCode.G)) {
            int id = Random.Range(1, 2);
            Knapsack.Instance.StoreItem(id);
        }
    }
}