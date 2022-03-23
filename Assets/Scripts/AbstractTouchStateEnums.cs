
    public abstract class AbstractTouchStateEnums
    {
        public enum TouchState
        {
            OnPanel,
            OnDrag,
        }

        public static TouchState CurrentState = TouchState.OnPanel;

        public static void SetTouchEnum(TouchState setState)
        {
            CurrentState = setState;
        }
    }