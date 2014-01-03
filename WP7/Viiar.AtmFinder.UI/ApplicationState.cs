namespace Viiar.AtmFinder.UI
{
    public static class ApplicationState
    {
        private static readonly object syncRoot = true;

        private static bool onlineRetrievalFailedMessageShown;
        private static bool entityTooFarMessageShown;

        static ApplicationState()
        {
            OnlineRetrievalFailedMessageShown = false;
            EntityTooFarMessageShown = false;
        }

        public static bool OnlineRetrievalFailedMessageShown
        {
            get { return onlineRetrievalFailedMessageShown; }
            set
            {
                lock (syncRoot)
                {
                    onlineRetrievalFailedMessageShown = value;
                }
            }
        }

        public static bool EntityTooFarMessageShown
        {
            get { return entityTooFarMessageShown; }
            set
            {
                lock (syncRoot)
                {
                    entityTooFarMessageShown = value;
                }
            }
        }
    }
}
