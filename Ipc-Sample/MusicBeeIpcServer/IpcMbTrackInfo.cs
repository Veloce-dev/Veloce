using System;

namespace MusicBeePlugin
{
    public class IpcMbTrackInfo : MarshalByRefObject
    {
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Album { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ArtworkUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IpcMbTrackInfo() { }

        public override object InitializeLifetimeService() => null;
        
    }
}
