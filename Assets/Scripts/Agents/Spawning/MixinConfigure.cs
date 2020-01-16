using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixinConfigure
{
    private readonly Transform parent;
    private readonly Home home;

    public MixinConfigure(Transform parent, Home home)
    {
        this.parent = parent;
        this.home = home;
    }

    public void Add<ComponentT, BindingT>(GameObject prefab, int min, int max, Action<ComponentT, BindingT> bindAction)
        where ComponentT : MonoBehaviour
        where BindingT : MonoBehaviour
    {
        int numOfComponents = UnityEngine.Random.Range(min, max + 1);

        BindingT[] values = home.GetClosest<BindingT>(numOfComponents);

        for (int i = 0; i < numOfComponents; i++)
        {
            GameObject go = UnityEngine.Object.Instantiate(prefab, parent);
            ComponentT component = go.GetComponent<ComponentT>();
            bindAction(component, values[i]);
        }
    }
}
