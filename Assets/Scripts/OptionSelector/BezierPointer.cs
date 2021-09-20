using System;
using UnityEngine;
using VRTK;
using VRTK.Examples;

public class BezierPointer : VRTKExample_BezierPointerChanger
{
    private Action actionOnClick;
    
    protected override void StyleRenderer(VRTK_BezierPointerRenderer renderer)
    {
        ResetRenderer(renderer);
    }
    
    public void SetActionOnClick(Action action)
    {
        actionOnClick += action;
    }
    
    private void OnDestroy()
    {
        actionOnClick = null;
    }
    
    public override void Activate()
    {
        base.Activate();
        Debug.Log("ActionOnPress Invoke");
        actionOnClick?.Invoke();
    }
}