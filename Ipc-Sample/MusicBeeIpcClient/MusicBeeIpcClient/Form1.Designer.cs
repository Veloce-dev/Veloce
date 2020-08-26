namespace MusicBeeIpcClient
{
    partial class Form1
    {
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
            this.buttonPostNowPlaying = new System.Windows.Forms.Button();
            this.pictureBoxAlbumArt = new System.Windows.Forms.PictureBox();
            this.labelTrackNumber = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelArtist = new System.Windows.Forms.Label();
            this.labelAlbum = new System.Windows.Forms.Label();
            this.checkBoxNeedAlbumArt = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoPosting = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAlbumArt)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonPostNowPlaying
            // 
            this.buttonPostNowPlaying.Font = new System.Drawing.Font("メイリオ", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonPostNowPlaying.Location = new System.Drawing.Point(187, 158);
            this.buttonPostNowPlaying.Name = "buttonPostNowPlaying";
            this.buttonPostNowPlaying.Size = new System.Drawing.Size(228, 23);
            this.buttonPostNowPlaying.TabIndex = 9;
            this.buttonPostNowPlaying.Text = "NowPlaying";
            this.buttonPostNowPlaying.UseVisualStyleBackColor = true;
            // 
            // pictureBoxAlbumArt
            // 
            this.pictureBoxAlbumArt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxAlbumArt.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxAlbumArt.Name = "pictureBoxAlbumArt";
            this.pictureBoxAlbumArt.Size = new System.Drawing.Size(169, 169);
            this.pictureBoxAlbumArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxAlbumArt.TabIndex = 8;
            this.pictureBoxAlbumArt.TabStop = false;
            // 
            // labelTrackNumber
            // 
            this.labelTrackNumber.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelTrackNumber.Location = new System.Drawing.Point(187, 9);
            this.labelTrackNumber.Name = "labelTrackNumber";
            this.labelTrackNumber.Size = new System.Drawing.Size(29, 23);
            this.labelTrackNumber.TabIndex = 7;
            this.labelTrackNumber.Text = "--.";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoEllipsis = true;
            this.labelTitle.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelTitle.ForeColor = System.Drawing.Color.Teal;
            this.labelTitle.Location = new System.Drawing.Point(212, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(210, 23);
            this.labelTitle.TabIndex = 6;
            this.labelTitle.Text = "Title";
            // 
            // labelArtist
            // 
            this.labelArtist.AutoEllipsis = true;
            this.labelArtist.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelArtist.Location = new System.Drawing.Point(187, 32);
            this.labelArtist.Name = "labelArtist";
            this.labelArtist.Size = new System.Drawing.Size(235, 23);
            this.labelArtist.TabIndex = 5;
            this.labelArtist.Text = "Artist";
            // 
            // labelAlbum
            // 
            this.labelAlbum.AutoEllipsis = true;
            this.labelAlbum.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelAlbum.Location = new System.Drawing.Point(187, 55);
            this.labelAlbum.Name = "labelAlbum";
            this.labelAlbum.Size = new System.Drawing.Size(235, 23);
            this.labelAlbum.TabIndex = 4;
            this.labelAlbum.Text = "Album";
            // 
            // checkBoxNeedAlbumArt
            // 
            this.checkBoxNeedAlbumArt.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBoxNeedAlbumArt.Location = new System.Drawing.Point(187, 126);
            this.checkBoxNeedAlbumArt.Name = "checkBoxNeedAlbumArt";
            this.checkBoxNeedAlbumArt.Size = new System.Drawing.Size(149, 24);
            this.checkBoxNeedAlbumArt.TabIndex = 3;
            this.checkBoxNeedAlbumArt.Text = "Post with AlbumArt";
            this.checkBoxNeedAlbumArt.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoPosting
            // 
            this.checkBoxAutoPosting.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBoxAutoPosting.Location = new System.Drawing.Point(187, 96);
            this.checkBoxAutoPosting.Name = "checkBoxAutoPosting";
            this.checkBoxAutoPosting.Size = new System.Drawing.Size(108, 24);
            this.checkBoxAutoPosting.TabIndex = 2;
            this.checkBoxAutoPosting.Text = "Post Auto";
            this.checkBoxAutoPosting.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel1.Location = new System.Drawing.Point(187, 87);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(235, 1);
            this.panel1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 193);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBoxAutoPosting);
            this.Controls.Add(this.checkBoxNeedAlbumArt);
            this.Controls.Add(this.labelAlbum);
            this.Controls.Add(this.labelArtist);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.labelTrackNumber);
            this.Controls.Add(this.pictureBoxAlbumArt);
            this.Controls.Add(this.buttonPostNowPlaying);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "MusicBee NowPlaying";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAlbumArt)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonPostNowPlaying;
		private System.Windows.Forms.PictureBox pictureBoxAlbumArt;
		private System.Windows.Forms.Label labelTrackNumber;
		private System.Windows.Forms.Label labelTitle;
		private System.Windows.Forms.Label labelArtist;
		private System.Windows.Forms.Label labelAlbum;
		private System.Windows.Forms.CheckBox checkBoxNeedAlbumArt;
		private System.Windows.Forms.CheckBox checkBoxAutoPosting;
		private System.Windows.Forms.Panel panel1;
	}
}

