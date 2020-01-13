using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasicAgent : MonoBehaviour
{
    public IntVariable Hunger;
    public IEnumerable<BoolVariable> IsInTavernVariables => GetComponentsInChildren<TavernClient>().Select(tc => tc.IsInTavern);
}
