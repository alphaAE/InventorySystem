using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CompositeSlotUI : SlotUI {
    public override void OnPointerUp(PointerEventData eventData) {
        base.OnPointerUp(eventData);
        Composite.Instance.RefreshSlot();
    }
}