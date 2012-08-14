using System;
using System.Threading;
using System.Text;
using System.Reflection;
using System.Windows;
using System.Diagnostics;
namespace MVC.Communication {
    public delegate ResponseType MessageEventHandler(MessageEventArgs e);

    public class MessageHandler : CommunicationHandler {
        //protected static event MessageEventHandler MessageSent;   

        public static Boolean suppress_messages = false;

        protected static ResponseType SendException(Exception e) {
            while (e.GetType() == typeof(TypeInitializationException)) {
                e = e.InnerException;
            }
            if (e.GetType() == typeof(CommunicatableException)) {
                CommunicatableException ex = e as CommunicatableException;
                string message = ex.Message;

                return SendMessage(ex.title, message, MessageTypes.Error, ex, false);
            } else {
                return SendMessage(e.GetType().ToString(), e.Message, MessageTypes.Error, e, false);
            }

        }

        protected static ResponseType SendError(string name, string title) {
            return SendError(name, title, null as Exception);
        }
        protected static ResponseType SendError(string name, string title, Exception e) {
            ProgressHandler.state = ProgressState.Error;
            return SendMessage(name, title, MessageTypes.Error, e, false);
        }
        protected static ResponseType SendWarning(string name, string title) {
            return SendWarning(name, title, null, false);
        }
        protected static ResponseType SendWarning(string name, string title, bool suppressable) {
            return SendWarning(name, title, null, suppressable);
        }
        protected static ResponseType SendWarning(string name, string title, Exception e) {
            return SendWarning(name, title, e, false);
        }
        protected static ResponseType SendWarning(string name, string title, Exception e, bool suppressable) {
            ProgressHandler.state = ProgressState.Error;
            return SendMessage(name, title, MessageTypes.Warning, e, suppressable);
        }
        protected static ResponseType SendInfo(string name, string title) {
            ProgressHandler.state = ProgressState.Wait;
            return SendMessage(name, title, MessageTypes.Info, null, false);
        }

        protected static ResponseType SendMessage(string title, string message, MessageTypes type, Exception ex, bool suppressable) {
            MessageEventArgs e = new MessageEventArgs();
            e.type = type;
            e.exception = ex;
            e.title = title;
            e.message = message;
            e.Suppressable = suppressable;
            ICommunicationReceiver receiver = getReceiver();
            if (receiver == null)
                return ResponseType.Cancel;

            if (receiver.context != null) {
                receiver.context.Send(new SendOrPostCallback(delegate(object state) {
                    MessageEventHandler handler = receiver.sendMessage;
                    if (handler != null) {
                        handler(e);
                    }
                }), null);
            } else {
                receiver.sendMessage(e);
            }

            waitForResponse(e);

            ProgressHandler.state = ProgressState.Normal;
            return e.response;

        }



        public static string getEnvironmentInfo() {
            StringBuilder output = new StringBuilder();

            Assembly entryassem = Assembly.GetEntryAssembly();
            output.AppendLine("Application Info:");
            output.AppendLine("Entry Assembly:");
            output.AppendLine(dumpAssemblyInfo(entryassem));
            Assembly execassem = Assembly.GetExecutingAssembly();
            output.AppendLine("Executing Assembly:");
            output.AppendLine(dumpAssemblyInfo(execassem));
            return output.ToString();
        }
        private static string dumpAssemblyInfo(Assembly ass) {
            StringBuilder output = new StringBuilder();
            output.Append("Codebase: ");
            output.AppendLine(ass.CodeBase);
            output.Append("Full Name: ");
            output.AppendLine(ass.FullName);
            output.Append("Image Runtime Version: ");
            output.AppendLine(ass.ImageRuntimeVersion);
            output.Append("Location: ");
            output.AppendLine(ass.Location);
            output.Append("Version: ");
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(ass.Location);
            output.AppendLine(fvi.ProductVersion);
            return output.ToString();
        }

    }
}
