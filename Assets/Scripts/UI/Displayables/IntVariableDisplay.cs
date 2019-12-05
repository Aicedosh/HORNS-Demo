using UnityEngine;
using UnityEngine.UI;

public class IntVariableDisplay : MonoBehaviour
{
    private class IntObserver : HORNS.IVariableObserver
    {
        private IntVariableDisplay parent;

        public IntObserver(IntVariableDisplay parent)
        {
            this.parent = parent;
        }

        public void ValueChanged()
        {
            parent.VariableUpdate();
        }
    }

    public IntVariable Variable { private get; set; }
    public Text Text;
    public InputField InputField;
    public Slider Slider;

    private IntObserver observer;

    void Start()
    {
        Text.text = Variable.Name;
        VariableUpdate();

        InputField.onEndEdit.AddListener(OnInputChange);
        Slider.onValueChanged.AddListener(OnSliderChange);

        observer = new IntObserver(this);
        Variable.AddObserver(observer);
    }

    void OnInputChange(string text)
    {
        if (int.TryParse(text, out int val))
        {
            Variable.Variable.Value = val;
        }
        // else error?
    }

    void OnSliderChange(float val)
    {
        Variable.Variable.Value = (int)val;
    }

    void VariableUpdate()
    {
        if (Variable.Variable.Value < Slider.minValue)
        {
            Slider.minValue = Variable.Variable.Value;
        }
        if (Variable.Variable.Value > Slider.maxValue)
        {
            Slider.maxValue = Variable.Variable.Value;
        }

        InputField.text = Variable.Variable.Value.ToString();
        Slider.value = Variable.Variable.Value;
    }
    
    void Update()
    {
        
    }

    void OnDestroy()
    {
        // TODO: add unobserving to DemoVariable and then change this
        Variable.Variable.Unobserve(observer);
    }
}
