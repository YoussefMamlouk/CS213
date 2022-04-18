using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ApplyColor : MonoBehaviour
{
    public FlexibleColorPicker fcp;
    public CelluloAgent cellulo;

    // Update is called once per frame
    void Update()
    {
        cellulo.SetVisualEffect(VisualEffect.VisualEffectConstAll, fcp.color, 0);
        cellulo.initialColor = fcp.color;

    }
}
