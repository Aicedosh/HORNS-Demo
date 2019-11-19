using System;
using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public abstract class Need<T> : MonoBehaviour, INeed, IPrintable
{
    public abstract Variable<T> GenericVariable { get; }
    public T DesiredValue;

    private class LibNeed : HORNS.Need<T>
    {
        private readonly Func<T, float> evaluationFunction;

        public LibNeed(HORNS.Variable<T> variable, T desired, VariableSolver<T> solver, Func<T, float> evaluationFunction) : base(variable, desired, solver)
        {
            this.evaluationFunction = evaluationFunction;
        }

        public override float Evaluate(T value)
        {
            return evaluationFunction(value);
        }
    }
    private HORNS.Need<T> need;

    public HORNS.Need<T> GetNeed()
    {
        if(need == null)
            need = new LibNeed(GenericVariable.LibVariable, DesiredValue, GenericVariable.Solver, Evaluate);

        return need;
    }

    protected abstract float Evaluate(T value);

    public string GetName()
    {
        return GenericVariable.GetName() + " Need";
    }

    public string GetText()
    {
        return GenericVariable.GetText() + "/" + DesiredValue.ToString();
    }

    public void AddTo(Agent agent)
    {
        agent.AddNeed(GetNeed());
    }
}
