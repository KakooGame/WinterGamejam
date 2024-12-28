using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public enum State
    {
        BlackAndWhite,
        Colored
    }
    
    public enum BlackGreyWhite
    {
        Black,
        Grey,
        White
    }

    // 状态和颜色字段
    [SerializeField] public State currentState = State.Colored;

    [SerializeField] public MaterialColor currentColor = MaterialColor.White;
    
    [SerializeField] public BlackGreyWhite currentBlackGreyWhite = BlackGreyWhite.White;
    
    
}