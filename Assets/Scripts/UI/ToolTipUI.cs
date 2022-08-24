using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ToolTipUI : MonoBehaviour {
    public static ToolTipUI Instance { get; private set; }

    private RectTransform _canvasRect;
    private Text _toolTipText, _contentText;
    private CanvasGroup _canvasGroup;

    private float _smoothing = .5f;
    private Vector2 _toolTipPositionOffset = new(14, -10);

    public bool IsDisplay { get; set; }

    private void Awake() {
        Instance = this;
        _canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
        _toolTipText = GetComponent<Text>();
        _contentText = transform.Find("text").GetComponent<Text>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update() {
        if (IsDisplay) {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRect, Input.mousePosition, null,
                out position);
            transform.localPosition = position + _toolTipPositionOffset;
        }
    }

    public void Show(string content) {
        IsDisplay = true;
        _toolTipText.text = content;
        _contentText.text = content;
        DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 1, _smoothing);
    }

    public void Hide() {
        IsDisplay = false;
        DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 0, _smoothing);
    }
}