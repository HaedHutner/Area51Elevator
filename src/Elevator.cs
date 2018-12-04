using System.Collections.Generic;

namespace Area51Elevator
{
    public class Elevator
    {
        public Floor CurrentFloor { get; private set; }
        private Queue<Floor> QueuedFloors = new Queue<Floor>();

        public Elevator() {

        }

        public void EnqueueFloor(Floor floor) {
            QueuedFloors.Enqueue(floor);
        }
    }
}