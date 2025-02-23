using System;

namespace PhantomEngine
{
    [Flags]
    public enum PlayerStatus
    {
        None = 0,
        Idle = 1 << 0,
        Walk = 1 << 1,
        Run = 1 << 2,
        Jump = 1 << 3
    }
    
    // Player direction enum
    public enum PlayerDirection
    {
        North,
        East,
        South,
        West
    }
}