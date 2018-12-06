using System;
using System.Collections.Generic;
using System.Threading;

namespace Area51Elevator {
    class Program {

        const int MAX_AGENTS = 10;
        const int ELEVATOR_PERIOD = 1000;
        const int MAX_AGENT_PERIOD = 5000;

        private static Dictionary<Agent, Thread> Agents = new Dictionary<Agent, Thread> ();
        private static Elevator Elevator = new Elevator (Floor.G);

        static void Main (string[] args) {
            for (int i = 0; i < MAX_AGENTS; i++) {
                Agent a = new Agent ("Agent 00" + i, Area51.GetRandomSecurityLevel (), Area51.GetRandomFloor ());

                Thread agentThread = new Thread (new ParameterizedThreadStart (obj => AgentLogic ((Agent) obj)));
                agentThread.Start (a);

                Agents.Add (a, agentThread);
            }

            while (true) {
                Elevator.MoveToNextFloor ();
                Thread.Sleep (ELEVATOR_PERIOD);
            }
        }
        static void AgentLogic (Agent agent) {
            while (true) {
                Thread.Sleep (Area51.Random.Next (MAX_AGENT_PERIOD));

                if ( agent.InElevator ) continue;
                agent.RequestFloor(Elevator);
            }
        }
    }
}