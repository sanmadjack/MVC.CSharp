using System.Collections.Generic;
using System.Threading;
namespace MVC.Communication {

    public delegate void RequestEventHandler(RequestEventArgs e);

    public class RequestHandler : CommunicationHandler {
        //protected static event RequestEventHandler RequestSent;

        public static RequestReply Request(RequestType type, bool suppressable) {
            return Request(type, null, null, null, suppressable);
        }
        protected static RequestReply Request(RequestType type, string title, string message, bool suppressable) {
            return Request(type, title, message, null, null, suppressable);
        }
        protected static RequestReply Request(RequestType type, string title, string message, List<string> choices, bool suppressable) {
            return Request(type, title, message, choices, null, suppressable);
        }
        protected static RequestReply Request(RequestType type, string title, string message, List<string> choices, string default_choice, bool suppressable) {
            RequestReply request = new RequestReply();

            if (type == RequestType.Choice && choices == null)
                throw new CommunicatableException("Request Error", "A choice was requested, but no options provided");


            RequestEventArgs e = new RequestEventArgs(type, title, message, choices, default_choice, request,suppressable);

            ICommunicationReceiver receiver = getReceiver();

            if (receiver == null) {
                request.cancelled = true;
                return request;
            }

            if (receiver.context != null) {
                receiver.context.Post(new SendOrPostCallback(delegate(object state) {
                    RequestEventHandler handler = receiver.requestInformation;
                    if (handler != null) {
                        handler(e);
                    }
                }), null);
            } else {
                receiver.requestInformation(e);
            }

            waitForResponse(e);

            if (e.response == ResponseType.Cancel || e.response == ResponseType.No) {
                e.result.cancelled = true;
            } else {
            }

            return e.result;
        }


    }
}
