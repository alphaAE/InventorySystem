using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Inventory {
    public static Character Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }
}