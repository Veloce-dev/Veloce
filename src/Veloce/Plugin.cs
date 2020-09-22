using System;
using System.Collections.Generic;
using System.Drawing;

namespace Veloce
{
	public class PluginSession
	{
		public DateTime ExpirationTime { get; internal set; }

		public PluginContent Content { get; private set; }

		// List of optional information supported by the plugin
		public List<PluginCapability> Capability { get; private set; }

		public PluginSession(DateTime expirationTime, PluginContent content, List<PluginCapability> capability)
		{
			ExpirationTime = expirationTime;
			Content = content;
			Capability = capability;
		}
	}

	public class PluginContent
	{
		// --- Basic informations ---

		public string Name;

		public string Description;

		public string SongName;

		public string AlbumName;

		public string AlbumArtistName;

		// --- Optional informations ---

		public Image AlbumArtwork { get; set; }

		public string SongFilePath;
	}

	public enum PluginCapability
	{
		AlbumArtwork,
		SongFilePath
	}
}
