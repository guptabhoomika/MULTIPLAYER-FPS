using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility 
{
    public static void SetLayerReccursively(GameObject _obj, int newLayer)
    {
        if (_obj == null)
            return;
        _obj.layer = newLayer;
        foreach (Transform _child in _obj.transform)
        {
            if (_child == null)
                continue;
            SetLayerReccursively(_child.gameObject, newLayer);
        }
    }

}
