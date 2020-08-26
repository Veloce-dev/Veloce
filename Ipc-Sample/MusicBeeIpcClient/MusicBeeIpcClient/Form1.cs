using MusicBeeIpcClient.Service;
using MusicBeePlugin;

using System;
using System.IO;
using System.Net;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicBeeIpcClient
{
    public partial class Form1 : Form
    {
        private IpcClientChannel _ipcClient;
        private IpcMbTrackInfo _trackInfo;
        private System.Timers.Timer _timer { get; set; } = new System.Timers.Timer(500);
        private Twitter _Twitter { get; set; } = new Twitter();

        public Form1()
        {
            InitializeComponent();

            _ipcClient = new IpcClientChannel();
            ChannelServices.RegisterChannel(_ipcClient, true);

            try
            {
                _timer.Elapsed += (s, e) =>
                {
                    var url = "ipc://IpcMbChannel/MbTrackInfo";
                    _trackInfo = (IpcMbTrackInfo)Activator.GetObject(typeof(IpcMbTrackInfo), url);
                    this.Invoke((MethodInvoker)(() =>
                        UpdateCurrentTrackInfo(_trackInfo.Title, _trackInfo.Artist, _trackInfo.Album, _trackInfo.ArtworkUrl))
                    );
                };
                _timer.Start();

                this.buttonPostNowPlaying.Click += async (_, __) => await PostAsync();
            } catch (RemotingException) {
                // catching exception.
            }
        }

        public void UpdateCurrentTrackInfo(string aTitle, string aArtist, string aAlbum, string aArtworkUrl)
        {
            this.labelTitle.Text = aTitle;
            this.labelArtist.Text = aArtist;
            this.labelAlbum.Text = aAlbum;
            this.pictureBoxAlbumArt.ImageLocation = aArtworkUrl;
        }

        public async Task PostAsync()
        {
            var tw = new StringBuilder();
            tw.Append($"🎵 {this.labelTitle.Text}\r\n");
            tw.Append($"🎙 {this.labelArtist.Text}\r\n");
            tw.Append($"💿 {this.labelAlbum.Text}\r\n");
            tw.Append("#nowplaying #MusicBee");

            using var client = new WebClient();
            using var stream = new MemoryStream(client.DownloadData(this.pictureBoxAlbumArt.ImageLocation));
            await _Twitter.Twist.UpdateWithMediaAsync(tw.ToString(), stream);
        }
    }
}
