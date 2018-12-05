using System;
using System.Collections.Generic;

namespace Area51Elevator
{
    public static class Area51
    {
        public static Random Random = new Random();
        public static Floor[] Floors = {Floor.G, Floor.S, Floor.T1, Floor.T2};

        public static Queue<Floor> GetFloorsInbetween(Floor floorA, Floor floorB) {
            int indexA = Array.FindIndex(Floors, floor => floor == floorA); // get the index of floor A
            int indexB = Array.FindIndex(Floors, floor => floor == floorB); // get the index of floor B

            if ( indexA > indexB ) return GetFloorsInbetween(floorB, floorA);

            Queue<Floor> inbetweenFloors = new Queue<Floor>();

            for ( int i = indexA; i <= indexB; i++ ) {
                inbetweenFloors.Enqueue(Floors[i]);
            }

            return inbetweenFloors;
        }

        public static Agent.SecurityLevel GetRandomSecurityLevel () {
            Array values = Enum.GetValues (typeof (Agent.SecurityLevel));
            return (Agent.SecurityLevel) values.GetValue (Random.Next (values.Length));
        }

        public static Floor GetRandomFloor () {
            return Floors[Random.Next (Floors.Length)];
        }
    }
}