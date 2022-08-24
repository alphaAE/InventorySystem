using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ToolTipUI : MonoBehaviour {
    public static ToolTipUI Instance { get; private set; }

    private RectTransform _canvasRect;
    private RectTransform _rect;
    private Text _toolTipText, _contentText;
    private CanvasGroup _canvasGroup;

    private float _smoothing = .5f;
    private Vector2 _toolTipPositionOffset = new(14, -10);

    public bool IsDisplay { get; set; }

    private void Awake() {
        Instance = this;
        _canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
        _rect = GetComponent<RectTransform>();
        _toolTipText = GetComponent<Text>();
        _contentText = transform.Find("text").GetComponent<Text>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update() {
        if (IsDisplay) {
            ResetPivot();
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRect, Input.mousePosition, null,
                out position);
            transform.localPosition = position + _toolTipPositionOffset;
        }
    }

    private void ResetPivot() {
        // 计算轴心
        Vector2 position;
        float xOffset = 0, yOffset = 1;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRect, Input.mousePosition, null,
            out position);
        if (position.x + _canvasRect.rect.width / 2 + _rect.rect.width > _canvasRect.rect.width) {
            print("宽度超出");
            xOffset = 1;
        }

        if (Mathf.Abs(position.y - _canvasRect.rect.height / 2) + _rect.rect.height > _canvasRect.rect.height) {
            print("高度超出");
            yOffset = 0;
        }

        _rect.pivot = new Vector2(xOffset, yOffset);
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