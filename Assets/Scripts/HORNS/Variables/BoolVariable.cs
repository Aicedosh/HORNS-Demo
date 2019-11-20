using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class BoolVariable : Variable<bool>
{
    public override VariableSolver<bool> Solver => BoolSolver;
    public BooleanSolver BoolSolver { get; } = new BooleanSolver();
}
