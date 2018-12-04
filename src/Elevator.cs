using System;
using System.Collections.Generic;

namespace Area51Elevator
{
    public class Elevator
    {
        public Floor CurrentFloor { get; private set; }
        private Queue<Floor> QueuedFloors = new Queue<Floor>();
        private Agent agent;
        public Elevator(Floor startingFloor) {
            this.CurrentFloor = startingFloor;
        }

        public void EnqueueFloor(Floor floor) {
            QueuedFloors.Enqueue(floor);
        }

        internal void MoveToNextFloor()
        {
            throw new NotImplementedException();
        }
    }
}