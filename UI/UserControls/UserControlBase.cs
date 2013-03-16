using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MemCacheDManager.UI.UserControls
{
	public class UserControlBase : UserControl
	{
		public delegate void OnCancel(Object sender, EventArgs e);
		public event OnCancel Cancel;

		private ToolStripStatusLabel _lblStatus = null;
		public ToolStripStatusLabel StatusLabel
		{
			get { return _lblStatus; }
			set { _lblStatus = value; }
		}

		protected void SetStatusLabel(string status)
		{
			if (StatusLabel != null)
			{
				StatusLabel.Text = status;

				if(this.Parent != null)
					this.Parent.Refresh();

				Application.DoEvents();
			}
		}

		private ServerConfiguration _serverConfiguration;
		public ServerConfiguration ServerConfiguration
		{
			get { return _serverConfiguration; }
			set { _serverConfiguration = value; }
		}

		protected void RaiseCancelEvent(object sender, EventArgs e)
		{
			if (Cancel != null)
				Cancel(sender, e);
		}
	}
}
