using System;

namespace SearchListOptimizing.Mediator
{
    public interface IMediator
    {
        /// <summary>
        /// Register a ViewModel to the mediator notifications
        /// This will iterate through all methods of the target passed and will register all methods that are decorated with the MediatorMessageSink Attribute
        /// </summary>
        /// <param name="target">The ViewModel instance to register</param>
        void Register(object target);

        void Unregister(object target);

        /// <summary>
        /// Registers a specific method to the Mediator notifications
        /// </summary>
        /// <param name="message">The message to register to</param>
        /// <param name="callback">The callback function to be called when this message is broadcasted</param>
        void Register(string message, Delegate callback);

        /// <summary>
        /// Notify all registered parties that a specific message was broadcasted
        /// </summary>
        /// <typeparam name="T">The Type of parameter to be passed</typeparam>
        /// <param name="message">The message to broadcast</param>
        /// <param name="parameter">The parameter to pass together with the message</param>
        void NotifyColleagues<T>(string message, T parameter);

        /// <summary>
        /// Notify all registered parties that a specific message was broadcasted
        /// </summary>
        /// <typeparam name="T">The Type of parameter to be passed</typeparam>
        /// <param name="message">The message to broadcast</param>
        void NotifyColleagues<T>(string message);
    }
}