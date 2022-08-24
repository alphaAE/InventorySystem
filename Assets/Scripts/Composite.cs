using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Composite : Inventory {
    public static Composite Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }
}