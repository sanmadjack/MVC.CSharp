﻿using System;
using System.Collections.Generic;
using System.Threading;
namespace MVC.Communication {
    public class CommunicationHandler {
        public static Boolean suppress_communication = false;

        private static Stack<ICommunicationReceiver> receivers;


        static CommunicationHandler() {
            receivers = new Stack<ICommunicationReceiver>();
        }


        public static void addReceiver(ICommunicationReceiver receiver) {
            lock (receivers) {
                receivers.Push(receiver);
            }
        }

        protected static ICommunicationReceiver getReceiver() {
            lock (receivers) {
                try {
                    ICommunicationReceiver receiver = receivers.Peek();
                    while (receiver == null || !receiver.Available) {
                        receivers.Pop();
                        receiver = receivers.Peek();
                    }
                    return receiver;
                } catch (InvalidOperationException) {
                    return null;
                }
            }
        }

        protected static void waitForResponse(RespondableEventArg e) {
            while (e.response == ResponseType.None)
                Thread.Sleep(100);
        }


    }
}
