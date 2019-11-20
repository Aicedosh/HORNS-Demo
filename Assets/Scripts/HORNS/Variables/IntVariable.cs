using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class IntVariable : Variable<int>
{
    public override VariableSolver<int> Solver => IntSolver ;
    public IntegerSolver IntSolver { get; } = new IntegerSolver();
}
