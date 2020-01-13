using UnityEngine;
using UnityEngine.UI;

public class BooleanNeedDisplay : MonoBehaviour
{
    public BoolNeed Need { private get; set; }
    public Text Text;
    public Text GoalText;

    void Start()
    {
        Text.text = Need.GenericVariable.Name + " Need";
        GoalText.text = Need.DesiredValue.ToString();
    }
    
    void Update()
    {
        
    }
}
