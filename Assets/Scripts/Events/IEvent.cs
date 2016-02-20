﻿using System.Collections.Generic;

namespace Events {
    public interface IEvent {
        IEventListener Listener { get; }
    }

    public interface IEvent<ActionType> : IEvent {
        new IEventListener<ActionType> Listener { get; }
    }
}
