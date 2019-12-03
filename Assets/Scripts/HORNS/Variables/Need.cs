using System;
using HORNS;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public abstract class DemoNeed<T> : MonoBehaviour, IDemoNeed, IDisplayable
{
    public abstract DemoVariable<T> GenericVariable { get; }
    public T DesiredValue;

    private class LibNeed : Need<T>
    {
        private readonly Func<T, float> evaluationFunction;

        public LibNeed(Variable<T> variable, T desired, Func<T, float> evaluationFunction) : base(variable, desired)
        {
            this.evaluationFunction = evaluationFunction;
        }

        public override float Evaluate(T value)
        {
            return evaluationFunction(value);
        }
    }

    private Need<T> need;

    public Need<T> GetNeed()
    {
        if (need == null)
            need = new LibNeed(GenericVariable.LibVariable, DesiredValue, Evaluate);

        return need;
    }

    protected abstract float Evaluate(T value);

    public void AddTo(Agent agent)
    {
        agent.AddNeed(GetNeed());
    }

    public abstract LayoutGroup GetComponent();
}
