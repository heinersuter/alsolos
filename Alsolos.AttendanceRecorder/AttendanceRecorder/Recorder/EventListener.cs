namespace AttendanceRecorder.Recorder
{
    using System;
    using AttendanceRecorder.Model;
    using Microsoft.Win32;

    public class EventListener
    {
        private SystemState _currentState;

        public SystemState CurrentState
        {
            get { return this._currentState; }
        }

        public event EventHandler<StateChangedEventArgs> StateChanged = delegate { };

        public EventListener()
        {
            this._currentState = SystemState.Active;
            SystemEvents.SessionSwitch += this.OnSystemEventsSessionSwitch;
            SystemEvents.SessionEnding += SystemEvents_SessionEnding;
        }

        private void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            this.StateChanged.Invoke(this, new StateChangedEventArgs { OldState = _currentState, NewState = SystemState.Down });
        }

        private void OnSystemEventsSessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            var oldState = this._currentState;

            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLogon:
                    this._currentState = SystemState.Active;
                    break;
                case SessionSwitchReason.SessionLogoff:
                    this._currentState = SystemState.Down;
                    break;
                case SessionSwitchReason.SessionLock:
                    this._currentState = SystemState.Locked;
                    break;
                case SessionSwitchReason.SessionUnlock:
                    this._currentState = SystemState.Active;
                    break;
                default:
                    Console.WriteLine("Session changed to {0} while in State {1} at {2}.", e.Reason, this._currentState, DateTime.Now);
                    throw new ArgumentOutOfRangeException();
            }

            if (this._currentState != oldState)
            {
                this.StateChanged.Invoke(this, new StateChangedEventArgs { OldState = oldState, NewState = this._currentState });
            }
        }
    }
}
