using System.Collections;
using System.Collections.Generic;
using HORNS;
using UnityEngine;

public class IntVariable : Variable<int>
{
    public new IntegerSolver IntSolver { get; } = new IntegerSolver();

    public override VariableSolver<int> Solver => IntSolver ;
}
