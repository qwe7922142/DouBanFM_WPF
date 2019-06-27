using System.Windows.Interactivity;

namespace SmokeMusic.Common.Interactions
{
    public class InteractionRequestTrigger : EventTrigger
    {
        /// <summary>
        /// Specifies the name of the Event this EventTriggerBase is listening for.
        /// </summary>
        /// <returns>This implementation always returns the Raised event name for ease of connection with <see cref="IInteractionRequest"/>.</returns>
        protected override string GetEventName()
        {
            return "Raised";
        }
    }
}
