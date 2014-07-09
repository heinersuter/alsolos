namespace AttendanceRecorder.Recorder
{
    using System;
    using AttendanceRecorder.Model;

    public class StateChangedEventArgs : EventArgs
    {
        public SystemState OldState { get; set; }

        public SystemState NewState { get; set; }
    }
}
