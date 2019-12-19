using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woodcutter : MonoBehaviour, HORNS.IVariableObserver<bool>
{
    public BoolVariable HasWood;
    public GameObject HandAxe;
    public GameObject HipAxe;
    public GameObject Log;

    private Animator _anim;

    public void ValueChanged(bool value)
    {
        if(value)
        {
            _anim.SetBool("Carry", true);
        }
    }

    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        HasWood.Variable.Observe(this);
    }

    void Update()
    {
        if(_anim.GetBool("Chop"))
        {
            HandAxe.SetActive(true);
            HipAxe.SetActive(false);
        }
        else
        {
            HandAxe.SetActive(false);
            HipAxe.SetActive(true);
        }

        Log.SetActive(_anim.GetBool("Carry"));
    }
}
