using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BoolVariableDisplay : MonoBehaviour
{
    public BoolVariable Variable { private get; set; }
    public Text Text;
    public Button SwitchButton;
    public Sprite TrueSprite;
    public Sprite FalseSprite;

    void Start()
    {
        SwitchButton.onClick.AddListener(OnSwitchClick);
    }

    void OnSwitchClick()
    {
        Variable.Variable.Value = !Variable.Variable.Value;
    }

    void Update()
    {
        Text.text = Variable.Name;
        SwitchButton.GetComponent<Image>().sprite = Variable.Variable.Value ? TrueSprite : FalseSprite;
    }
}
