namespace WeightedRoundRobinDemo
{
    using System.Collections.Generic;

    public class WeightedRoundRobin<T>
    {
        /// <summary>
        /// The server dict.
        /// </summary>
        private readonly IDictionary<T, int> _serverDict;
        /// <summary>
        /// The server list.
        /// </summary>
        private readonly IList<ServerConfig> _serverList = new List<ServerConfig>();
        /// <summary>
        /// The sync lock.
        /// </summary>
        private readonly object _syncLock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:WeightedRoundRobinDemo.WeightedRoundRobin`1"/> class.
        /// </summary>
        /// <param name="serverDict">Server dict.</param>
        public WeightedRoundRobin(IDictionary<T, int> serverDict)
        {
            this._serverDict = serverDict;

            foreach (var item in _serverDict)
            {
                _serverList.Add(new ServerConfig
                {
                    Current_Weight = 0,
                    Weight = item.Value,
                    Server = item.Key
                });
            }
        }

        /// <summary>
        /// Gets the next item.
        /// </summary>
        /// <returns>The next item.</returns>
        public T GetNextItem()
        {
            int index = -1;
            int total = 0;
            int size = _serverList.Count;

            lock(_syncLock)
            {             
                for (int i = 0; i < size; i++)
                {
                    _serverList[i].Current_Weight += _serverList[i].Weight;
                    total += _serverList[i].Weight;

                    if (index == -1 || _serverList[index].Current_Weight < _serverList[i].Current_Weight)
                    {
                        index = i;
                    }
                }

                _serverList[index].Current_Weight -= total;
            }

            return _serverList[index].Server;
        }

        /// <summary>
        /// Server config.
        /// </summary>
        public class ServerConfig
        {
            /// <summary>
            /// Gets or sets the weight.
            /// </summary>
            /// <value>The weight.</value>
            public int Weight { get; set; }

            /// <summary>
            /// Gets or sets the current weight.
            /// </summary>
            /// <value>The current weight.</value>
            public int Current_Weight { get; set; }

            /// <summary>
            /// Gets or sets the server.
            /// </summary>
            /// <value>The server.</value>
            public T Server { get; set; }
        }
    }
}
