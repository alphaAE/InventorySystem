using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knapsack : Inventory {
    public static Knapsack Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }
}