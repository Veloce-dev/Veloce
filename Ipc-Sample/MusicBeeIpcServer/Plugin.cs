using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Threading.Tasks;

namespace MusicBeePlugin
{
    public partial class Plugin
    {
        private MusicBeeApiInterface _mbApiInterface;
        private readonly PluginInfo _about = new PluginInfo();
        private IpcServerChannel _ipcServer;
        private IpcMbTrackInfo _trackInfo = new IpcMbTrackInfo();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiInterfacePtr"></param>
        /// <returns></returns>
        public PluginInfo Initialise(IntPtr apiInterfacePtr)
        {
            _mbApiInterface = new MusicBeeApiInterface();
            _mbApiInterface.Initialise(apiInterfacePtr);
            _ipcServer = new IpcServerChannel("IpcMbChannel");
            ChannelServices.RegisterChannel(_ipcServer, true);

            _about.PluginInfoVersion = PluginInfoVersion;
            _about.Name = "MusicBee-NowPlaying";
            _about.Description = "MusicBee-NowPlaying";
            _about.Author = "Asteriskx.";
            _about.TargetApplication = "";
            _about.Type = PluginType.General;

            _about.VersionMajor = 1;
            _about.VersionMinor = 0;
            _about.Revision = 1;

            _about.MinInterfaceVersion = MinInterfaceVersion;
            _about.MinApiRevision = MinApiRevision;

            _about.ReceiveNotifications = (ReceiveNotificationFlags.PlayerEvents | ReceiveNotificationFlags.TagEvents);
            _about.ConfigurationPanelHeight = 0;
            return _about;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="panelHandle"></param>
        /// <returns></returns>
        public bool Configure(IntPtr panelHandle)
        {
            // save any persistent settings in a sub-folder of this path
            string dataPath = _mbApiInterface.Setting_GetPersistentStoragePath();
            // panelHandle will only be set if you set about.ConfigurationPanelHeight to a non-zero value
            // keep in mind the panel width is scaled according to the font the user has selected
            // if about.ConfigurationPanelHeight is set to 0, you can display your own popup window
            if (panelHandle != IntPtr.Zero)
            {
                //Panel configPanel = (Panel)Panel.FromHandle(panelHandle);
                //Label prompt = new Label();
                //prompt.AutoSize = true;
                //prompt.Location = new Point(0, 0);
                //prompt.Text = "prompt:";
                //TextBox textBox = new TextBox();
                //textBox.Bounds = new Rectangle(60, 0, 100, textBox.Height);
                //configPanel.Controls.AddRange(new Control[] { prompt, textBox });
            }
            return false;
        }

        /// <summary>
        /// called by MusicBee when the user clicks Apply or Save in the MusicBee Preferences screen.
        /// its up to you to figure out whether anything has changed and needs updating
        /// </summary>
        public void SaveSettings()
        {
            // save any persistent settings in a sub-folder of this path
            string dataPath = _mbApiInterface.Setting_GetPersistentStoragePath();
        }

        /// <summary>
        /// MusicBee is closing the plugin (plugin is being disabled by user or MusicBee is shutting down)
        /// </summary>
        /// <param name="reason"></param>
        public void Close(PluginCloseReason reason)
        {
        }

        /// <summary>
        /// uninstall this plugin - clean up any persisted files
        /// </summary>
        public void Uninstall()
        {
        }

        /// <summary>
        /// receive event notifications from MusicBee
        /// you need to set about.ReceiveNotificationFlags = PlayerEvents to receive all notifications, and not just the startup event
        /// </summary>
        /// <param name="sourceFileUrl"></param>
        /// <param name="type"></param>
        public void ReceiveNotification(string sourceFileUrl, NotificationType type)
        {
            // perform some action depending on the notification type
            switch (type)
            {
                case NotificationType.PlayStateChanged:
                    break; // FIXME

                case NotificationType.TrackChanged:
                    _trackInfo.Title = _mbApiInterface.NowPlaying_GetFileTag(MetaDataType.TrackTitle);
                    _trackInfo.Artist = _mbApiInterface.NowPlaying_GetFileTag(MetaDataType.Artist);
                    _trackInfo.Album = _mbApiInterface.NowPlaying_GetFileTag(MetaDataType.Album);
                    _trackInfo.ArtworkUrl = _mbApiInterface.NowPlaying_GetArtworkUrl();

                    Task.Run(() =>
                    {
                        try
                        {
                            RemotingServices.Marshal(_trackInfo, "MbTrackInfo", typeof(IpcMbTrackInfo));
                        } catch (Exception) {
                            // TODO
                        }
                    });
                    break;
            }
        }

        // return an array of lyric or artwork provider names this plugin supports
        // the providers will be iterated through one by one and passed to the RetrieveLyrics/ RetrieveArtwork function in order set by the user in the MusicBee Tags(2) preferences screen until a match is found
        public string[] GetProviders()
        {
            return null;
        }

        /// <summary>
        /// return lyrics for the requested artist/title from the requested provider
        /// only required if PluginType = LyricsRetrieval
        /// return null if no lyrics are found
        /// </summary>
        /// <param name="sourceFileUrl"></param>
        /// <param name="artist"></param>
        /// <param name="trackTitle"></param>
        /// <param name="album"></param>
        /// <param name="synchronisedPreferred"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public string RetrieveLyrics(string sourceFileUrl, string artist, string trackTitle, string album, bool synchronisedPreferred, string provider)
        {
            return null;
        }

        /// <summary>
        /// return Base64 string representation of the artwork binary data from the requested provider
        /// only required if PluginType = ArtworkRetrieval
        /// return null if no artwork is found
        /// </summary>
        /// <param name="sourceFileUrl"></param>
        /// <param name="albumArtist"></param>
        /// <param name="album"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public string RetrieveArtwork(string sourceFileUrl, string albumArtist, string album, string provider)
        {
            //Return Convert.ToBase64String(artworkBinaryData)
            return null;
        }
    }
}