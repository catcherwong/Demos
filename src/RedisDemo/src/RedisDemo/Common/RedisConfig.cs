namespace RedisDemo.Common
{
    public class RedisConfig
    {
        /// <summary>
        /// the master server
        /// </summary>
        public string MasterServer { get; set; }

        /// <summary>
        /// the slave server
        /// </summary>
        public string SlaveServer { get; set; }
    }
}
