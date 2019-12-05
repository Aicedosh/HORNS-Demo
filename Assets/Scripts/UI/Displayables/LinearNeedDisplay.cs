using UnityEngine;
using UnityEngine.UI;

public class LinearNeedDisplay : MonoBehaviour
{
    public LinearNeed Need { private get; set; }
    public Text Text;
    public Text GoalText;

    void Start()
    {
        Text.text = Need.Variable.Name + " Need";
        GoalText.text = Need.DesiredValue.ToString();
    }

    void Update()
    {

    }
}
