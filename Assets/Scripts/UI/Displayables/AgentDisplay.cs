using HORNS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentDisplay : MonoBehaviour
{
    public AgentAI AgentAI { private get; set; }
    public Text TextPrefab;

    public void SetContent(IEnumerable<Action> actions, int curr = 0)
    {
        while(transform.childCount > 0)
        {
            Transform child = transform.GetChild(0);
            Destroy(child.gameObject);
            child.SetParent(null); //become Batman
        }

        int idx = 0;
        foreach (var a in actions)
        {
            var go = Instantiate(TextPrefab);
            Text text = go.GetComponent<Text>();
            text.text = (a as BasicAction.LibAction).Name;

            if (idx == curr)
            {
                text.fontStyle = FontStyle.Bold;
            }

            go.transform.SetParent(transform);

            ++idx;
        }
    }
}
