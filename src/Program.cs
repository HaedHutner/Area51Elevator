using System;
using System.Collections.Generic;
using System.Threading;

namespace Area51Elevator
{
    class Program
    {

        const int MAX_AGENTS = 10;

        private static Random Random = new Random();
        private static Dictionary<Agent, Thread> Agents = new Dictionary<Agent, Thread>();
        private static Elevator Elevator = new Elevator(Floor.G);
        private static Floor[] Floors = { Floor.G, Floor.S, Floor.T1, Floor.T2 };

        static void Main(string[] args)
        {
            for ( int i = 0; i < MAX_AGENTS; i++ ) {
                Agent a = new Agent("Agent 00" + i, GetRandomSecurityLevel(), GetRandomFloor());

                Thread agentThread = new Thread(new ParameterizedThreadStart(obj => AgentLogic((Agent) obj)));
                agentThread.Start(a);

                Agents.Add(a, agentThread);
            }

            while (true) {
                Elevator.MoveToNextFloor();
                Thread.Sleep(1000);
            }
        }

        static Agent.SecurityLevel GetRandomSecurityLevel() {
            Array values = Enum.GetValues(typeof(Agent.SecurityLevel));
            return (Agent.SecurityLevel) values.GetValue(Random.Next(values.Length));
        }

        static Floor GetRandomFloor() {
            return Floors[Random.Next(Floors.Length - 1)];
        }

        static void AgentLogic(Agent agent) {
            while (true) {
                if ( Elevator.CurrentFloor == agent.CurrentFloor ) {
                    Floor floor = GetRandomFloor();
                    Elevator.EnqueueFloor(floor);
                    Console.WriteLine($"{agent.Name} has entered the elevator and has requested it go to Floor {floor.Name}.");
                } else {
                    Elevator.EnqueueFloor(agent.CurrentFloor);
                    Console.WriteLine($"{agent.Name} has requested the elevator go to their current floor ( Floor {agent.CurrentFloor.Name} ).");
                }
                Thread.Sleep(Random.Next(1000));
            }
        }
    }
}
