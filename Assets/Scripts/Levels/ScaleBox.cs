using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScaleBox : MonoBehaviour {
    [SerializeField]
    private float step = 0.2f;
    [SerializeField]
    private float min = 0.2f;
    [SerializeField]
    private float max = 2.0f;
    [SerializeField]
    private Text text;
    private float value = 1;

    public System.Action valueChangeListener;

    void Start()
    {
        SetValue(1);
    }

    void SetValue(float value)
    {
        this.value = value;
        text.text = string.Format("{0:0.0}x", value);
        if (valueChangeListener != null) valueChangeListener();
    }

    public void Increase()
    {
        float tmp = value + step;
        if (tmp > max) tmp = max;
        SetValue(tmp);
    }

    public void Decrease()
    {
        float tmp = value - step;
        if (tmp < min) tmp = min;
        SetValue(tmp);
    }
}
