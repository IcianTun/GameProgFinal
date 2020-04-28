using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image currentHealth;
    float originalSize;

    void Start()
    {
        originalSize = currentHealth.rectTransform.rect.width;
    }

    public void SetValue(float value)
    {
        currentHealth.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
