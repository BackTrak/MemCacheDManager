using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MemCacheDManager.UI
{
	public partial class CodeGeneration : Form
	{
		public string Output
		{
			get { return txtOutput.Text; }
			set { txtOutput.Text = value; }
		}

		public CodeGeneration()
		{
			InitializeComponent();
		}

		
	}
}