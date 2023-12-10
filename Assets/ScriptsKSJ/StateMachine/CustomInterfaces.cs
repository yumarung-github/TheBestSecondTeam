using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomInterface {
    public enum MASTATE_TYPE
    {
        ////////////////
        CAT_WAIT,
        CAT_MORNING,
        CAT_AFTERNOON,
        CAT_DINNER,
        ////////////////
        BIRD_WAIT,
        BIRD_MORNING,
        BIRD_AFTERNOON,
        BIRD_DINNER,
        ////////////////
        WOOD_WAIT,
        WOOD_MORNING1,
        WOOD_MORNING2,
        WOOD_AFTERNOON,
        WOOD_DINNER
        ////////////////
    }
    public enum STATE_TYPE
    {
        IDLE,
        MOVE
    }
    public interface IStateMachine
    {
        public void SetState(MASTATE_TYPE type);
        public object GetOwner();
    }
    public enum TRIBE_TYPE
    {
        FOX,
        RABBIT,
        RAT,
        BIRD
    }
}

