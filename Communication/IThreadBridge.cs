using System;

namespace MVC {
	public delegate void CommunicationDelegate();
	public interface IThreadBridge {
		// Synchronous
		void Send(CommunicationDelegate send_me);
		// Asynchronous
		void Post(CommunicationDelegate post_me);
	}
}

