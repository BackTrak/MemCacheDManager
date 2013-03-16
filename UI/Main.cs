using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Microsoft.Win32;
using System.Security.Principal;
using System.Net;
using MemCacheDManager.Properties;
using System.Threading;
using System.Reflection;

namespace MemCacheDManager.UI
{
	public partial class Main : Form
	{
		private volatile bool _isFormClosing = false;

		private ServerConfiguration _serverConfiguration = new ServerConfiguration();

		private UI.UserControls.ServerDetails _serverDetails = new UI.UserControls.ServerDetails();
		private UI.UserControls.InstanceDetails _instanceDetails = new UI.UserControls.InstanceDetails();
		private List<Business.InstanceStatistics> _instanceStatistics = new List<Business.InstanceStatistics>();
		private ImageList _tvImages = new ImageList();

		private Business.InstanceMonitor _instanceMonitor = null;

		// Invoke delegates -- Allows non-UI threads to update the UI.
		private delegate void UpdateInstanceStatisticsDelegate(List<Business.InstanceStatistics> updateStatistics);

		public Main()
		{
			InitializeComponent();

			this.scMain.Width = 750;
			this.scMain.Panel2MinSize = 550;

			_serverDetails.Save += new MemCacheDManager.UI.UserControls.ServerDetails.SaveHandler(OnServerDetailsSave);
			_serverDetails.Cancel += new MemCacheDManager.UI.UserControls.UserControlBase.OnCancel(OnServerDetailsCancel);
			_serverDetails.AddInstance += new MemCacheDManager.UI.UserControls.ServerDetails.AddInstanceHandler(OnAddInstance);
			_serverDetails.StatusLabel = tsslStatus;
			_serverDetails.ServerConfiguration = _serverConfiguration;

			_instanceDetails.Save += new EventHandler(OnInstanceDetailsSave);
			_instanceDetails.Cancel += new MemCacheDManager.UI.UserControls.UserControlBase.OnCancel(OnInstanceDetailsCancel);
			_instanceDetails.StatusLabel = tsslStatus;
			_instanceDetails.ServerConfiguration = _serverConfiguration;

			dgvStatus.AutoGenerateColumns = false;

			EnsureConfigurationFileDefaultsAreSet();

			_tvImages = new ImageList();
			_tvImages.Images.Add(Resources.icnUp);
			_tvImages.Images.Add(Resources.icnCommunicationError);
			_tvImages.Images.Add(Resources.icnNeedsUpdate);
			_tvImages.Images.Add(Resources.icnEmpty);
			_tvImages.Images.Add(Resources.icnDown);
			_tvImages.Images.Add(Resources.icnServiceDown);
			tvManager.ImageList = _tvImages;

			tcMain.SelectedTab = tpConfigure;

			if (Configuration.Default.LastConfigFile != null && Configuration.Default.LastConfigFile.Length > 0 && File.Exists(Configuration.Default.LastConfigFile))
			{
				_serverConfiguration.Load(Configuration.Default.LastConfigFile);
			}
			else
			{
				Configuration.Default.LastConfigFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Default.xml");
				_serverConfiguration.Save(Configuration.Default.LastConfigFile);
				Configuration.Default.Save();
			}

			SetTitleBar(Configuration.Default.LastConfigFile);
		}

		private void SetTitleBar(string currentConfigurationFileName)
		{
			this.Text = "MemCacheD Manager v" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " - " + currentConfigurationFileName;
		}

		void OnAddInstance(MemCacheDManager.Business.Server server)
		{
			TryToChangeView(null, server, _instanceDetails);
		}

		
		void OnServerDetailsCancel(object sender, EventArgs e)
		{
			if (tvManager.Nodes.Count > 0)
			{
				tvManager.SelectedNode = tvManager.Nodes[0];
				EditServer((Business.Server)tvManager.SelectedNode.Tag);
			}
			else
			{
				pEdit.Controls.Clear();
			}
		}

		void OnServerDetailsSave(Business.Server server)
		{
			LoadUI();

			foreach (TreeNode node in tvManager.Nodes)
			{
				if (node.Tag == server)
				{
					tvManager.SelectedNode = node;
					break;
				}
			}

			tvManager.Focus();
		}

		void OnInstanceDetailsCancel(object sender, EventArgs e)
		{
			if (tvManager.SelectedNode.Tag is Business.Instance)
			{
				tvManager.SelectedNode = tvManager.SelectedNode.Parent;
			}

			EditServer((Business.Server)tvManager.SelectedNode.Tag);
		}

		void OnInstanceDetailsSave(object sender, EventArgs e)
		{
			LoadUI();
		}

		private void LoadUI()
		{
			LoadUI(null);
		}

		private void LoadUI(string instanceDisplayNameToSelect)
		{
			ResetInstanceMonitor();

			if (this._serverConfiguration.Servers != null)
			{
				foreach (Business.Server server in this._serverConfiguration.Servers)
				{
					TreeNode targetServerNode = null;
					foreach (TreeNode serverNode in tvManager.Nodes)
					{
						if (serverNode.Tag == server)
						{
							targetServerNode = serverNode;
							serverNode.Text = server.ServerName;
							break;
						}
					}

					if (targetServerNode == null)
					{
						targetServerNode = new TreeNode();
						targetServerNode.Text = server.ServerName;
						targetServerNode.Tag = server;
						targetServerNode.ContextMenuStrip = cmsServerMenu;
						tvManager.Nodes.Add(targetServerNode);

						targetServerNode.ExpandAll();
					}

					if (targetServerNode.Nodes.Count != server.Instances.Count)
						targetServerNode.Nodes.Clear();

					foreach(Business.Instance instance in server.Instances)
					{
						TreeNode targetInstanceNode = null;
						foreach (TreeNode instanceNode in targetServerNode.Nodes)
						{
							if (((Business.Instance)instanceNode.Tag).ServiceName == instance.ServiceName)
							{
								targetInstanceNode = instanceNode;
								instanceNode.Text = instance.DisplayName;
								instanceNode.Tag = instance;
								break;
							}
						}

						if (targetInstanceNode == null)
						{
							targetInstanceNode = new TreeNode();
							targetInstanceNode.Text = instance.DisplayName;
							targetInstanceNode.Tag = instance;
							targetInstanceNode.ContextMenuStrip = cmsInstanceMenu;
							targetServerNode.Nodes.Add(targetInstanceNode);

							targetServerNode.ExpandAll();
						}
					}
				}
				TreeNode currentSelectedNode = tvManager.SelectedNode;

				if (tvManager.Nodes.Count > 0 && currentSelectedNode == null)
				{
					tvManager.Sort();
					currentSelectedNode = tvManager.Nodes[0];
					EditServer((Business.Server)tvManager.Nodes[0].Tag);
				}

				tvManager.Sort();

				tvManager.SelectedNode = currentSelectedNode;
					
			}
		}

		private void Main_Load(object sender, EventArgs e)
		{
			LoadUI();
		}

		private void Main_FormClosing(object sender, FormClosingEventArgs e)
		{
			tsslStatus.Text = "Closing.";
			Application.DoEvents();

			this.Cursor = Cursors.WaitCursor;
			
			try
			{
				_isFormClosing = true;

				if (_instanceMonitor != null)
					_instanceMonitor.Stop();
			}
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}


		private void addNewServerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			pEdit.Controls.Clear();
			pEdit.Controls.Add(_serverDetails);
		}

		private void btnAddServer_Click(object sender, EventArgs e)
		{
			TryToChangeView(null, null, _serverDetails);
		}

		private void EditServer(Business.Server server)
		{
			TryToChangeView(server, null, _serverDetails);
		}

		private void EditInstance(Business.Instance instance)
		{
			TryToChangeView(instance, (Business.Server) tvManager.SelectedNode.Parent.Tag, _instanceDetails);
		}

		private bool TryToChangeView(Business.EditableEntityBase entityToBeEdited, Business.EditableEntityBase parentEntity, UI.UserControls.ISupportEntityEditing entityEditor)
		{
			UI.UserControls.ISupportEntityEditing currentView = GetCurrentView();

			bool canChangeView = true;

			// Is the view already on the object the change is requested to go to?
			if (currentView != null && entityToBeEdited == currentView.EditableEntity)
				canChangeView = false;
			else if (currentView != null && currentView.DoesControlHaveUnsavedChanges() == true)
				canChangeView = false;

			if (canChangeView == false)
			{
				// Move the selection back to the item with unapplied changes.
				if (tvManager.SelectedNode != null && tvManager.SelectedNode.Tag != currentView.EditableEntity)
					SetHighlightedTreeNodeByTag(tvManager.Nodes, currentView.EditableEntity);
			}
			else
			{
				if (entityToBeEdited == null)
					entityEditor.CreateNew(parentEntity);
				else
					entityEditor.Edit(entityToBeEdited, parentEntity);

				if (pEdit.Controls.Contains((UserControl) entityEditor) == false)
				{
					pEdit.Controls.Clear();
					pEdit.Controls.Add((UserControl) entityEditor);
				}

				entityEditor.SetFocusToDefaultControl();
			}

			return canChangeView;
		}

		private UI.UserControls.ISupportEntityEditing GetCurrentView()
		{
			UI.UserControls.ISupportEntityEditing currentView = null;

			foreach (Control control in pEdit.Controls)
			{
				if (control is UI.UserControls.ISupportEntityEditing && control is UserControl)
					currentView = (UI.UserControls.ISupportEntityEditing)control;
			}

			return currentView;
		}

		private bool SetHighlightedTreeNodeByTag(TreeNodeCollection treeNodes, Business.EditableEntityBase entityToBeEdited)
		{
			foreach (TreeNode treeNode in treeNodes)
			{
				if (treeNode.Tag == entityToBeEdited)
				{
					tvManager.SelectedNode = treeNode;
					return true;
				}

				if (SetHighlightedTreeNodeByTag(treeNode.Nodes, entityToBeEdited) == true)
					return true;
			}

			return false;
		}

		private void tvManager_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Tag is Business.Server)
				EditServer((Business.Server)e.Node.Tag);
			else if(e.Node.Tag is Business.Instance )
				EditInstance((Business.Instance) e.Node.Tag);
		}

		
		private void deleteServerToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(this, "Are you sure you wish to delete " + tvManager.SelectedNode.Text + "?\n\nAny installed MemCacheD instances will not stopped or removed, you will only be removing the server from MemCacheD Manager.", "Delete this server?", MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					SetStatusLabel("Deleting Server.");

					this._serverConfiguration.Servers.Remove((Business.Server)tvManager.SelectedNode.Tag);
					this._serverConfiguration.Save(Configuration.Default.LastConfigFile);

					tvManager.Nodes.Clear();

					pEdit.Controls.Clear();

					LoadUI();
				}
				finally
				{
					SetStatusLabel("Ready.");
					this.Cursor = Cursors.Default;
				}
			}
			
		}

		private void tsmiAddNewInstance_Click(object sender, EventArgs e)
		{
			TryToChangeView(null, (Business.Server) tvManager.SelectedNode.Tag, _instanceDetails);
		}
		
		private void tvManager_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				tvManager.SelectedNode = e.Node;

				switch ((Business.StatusIconIndex) e.Node.ImageIndex)
				{
					case Business.StatusIconIndex.ServiceDown:
						tsmiStartService.Enabled = true;
						tsmiStopService.Enabled = false;
						tsmiRestartService.Enabled = false;
						break;

					case Business.StatusIconIndex.CommunicationError:
						tsmiStartService.Enabled = false;
						tsmiStopService.Enabled = true;
						tsmiRestartService.Enabled = true;
						break;

					case Business.StatusIconIndex.ServiceNonControllable:
						tsmiStartService.Enabled = false;
						tsmiStopService.Enabled = false;
						tsmiRestartService.Enabled = false;
						break;

					case Business.StatusIconIndex.Empty:
						tsmiStartService.Enabled = false;
						tsmiStopService.Enabled = false;
						tsmiRestartService.Enabled = false;
						break;

					case Business.StatusIconIndex.NeedsUpdate:
						tsmiStartService.Enabled = false;
						tsmiStopService.Enabled = true;
						tsmiRestartService.Enabled = true;
						break;

					case Business.StatusIconIndex.Up:
						tsmiStartService.Enabled = false;
						tsmiStopService.Enabled = true;
						tsmiRestartService.Enabled = true;
						break;
						
				}
			}
		}

		private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (tvManager.SelectedNode == null || tvManager.SelectedNode.Tag is Business.Server == false)
				return;

			Business.Server server = (Business.Server)tvManager.SelectedNode.Tag;

			tvManager.SelectedNode.Nodes.Clear();

			try
			{
				server.ReadInstancesFromServer();

				_serverConfiguration.Save(Configuration.Default.LastConfigFile);
			}
			catch (Exception ex)
			{
				MessageBox.Show("There was an error reading the MemCacheD instances from the server.\n\n" + ex.Message);
			}

			LoadUI();
		}

		private void deleteInstanceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you wish to remove " + tvManager.SelectedNode.Text + " from this server?", "Are you sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;

					Business.Instance instance = (Business.Instance)tvManager.SelectedNode.Tag;
					Business.Server server = (Business.Server)tvManager.SelectedNode.Parent.Tag;

					instance.RemoveFromServer(server, SetStatusLabel);
					server.Instances.Remove(instance);

					_serverConfiguration.Save(Configuration.Default.LastConfigFile);
					LoadUI();
				}
				finally
				{
					this.Cursor = Cursors.Default;
				}
			}
		}


		protected void SetStatusLabel(string status)
		{
			this.tsslStatus.Text = status;
			this.Refresh();
			Application.DoEvents();
		}

		private void enyimConfigSectionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UI.CodeGeneration codeGeneration = new CodeGeneration();

			StringBuilder output = new StringBuilder();
			output.Append(@"
<sectionGroup name=""enyim.com"">
	<section name=""memcached"" type=""Enyim.Caching.Configuration.MemcachedClientSection, Enyim.Caching"" />
</sectionGroup>

	<enyim.com>
		<memcached>
			<servers>
");

			if(this._serverConfiguration.Servers != null)
			{
				foreach (Business.Server server in this._serverConfiguration.Servers)
				{
					IPAddress[] addresses = Dns.GetHostEntry(server.ServerName).AddressList;

					if (addresses.Length > 0)
					{
						foreach(Business.Instance instance in server.Instances)
						{
							string ipAddress = addresses[0].ToString();
							if (instance.IpAddress != null && instance.IpAddress != String.Empty)
								ipAddress = instance.IpAddress;

							output.AppendFormat(@"
					<add address=""{0}"" port=""{1}"" /> <!-- {2} -->
							",
								ipAddress,
								instance.TcpPort,
								server.ServerName);
						}
					}
				}
			}

			output.Append(@"
			</servers>
			<socketPool minPoolSize=""10"" maxPoolSize=""100"" connectionTimeout=""00:00:05"" deadTimeout=""00:02:00"" />
		</memcached>
	</enyim.com>
");
			codeGeneration.Output = output.ToString();
			codeGeneration.Text = "Configuration Section for Enyim MemCacheD API";
			codeGeneration.ShowDialog();
		}

		private void tcMain_Selected(object sender, TabControlEventArgs e)
		{
			if (e.TabPage == tpStatus)
			{
				UpdateStatusDatagrid();
			}
			else if (e.TabPage == tpOptions)
			{
				UpdateOptionsTab();
			}
		}

		private void btnSelectMemCacheDBinary_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.InitialDirectory = Path.GetDirectoryName(Configuration.Default.MemcachedBinarySource);
			openFileDialog.FileName = Path.GetFileName(Configuration.Default.MemcachedBinarySource);
			openFileDialog.Multiselect = false;
			openFileDialog.Title = "Select MemCacheD Binary File";
			openFileDialog.Filter = "MemCacheD Application|memcached.exe|All Applications (*.exe)|*.exe|All Files (*.*)|*.*";

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				Configuration.Default.MemcachedBinarySource = openFileDialog.FileName;
				Configuration.Default.Save();
				UpdateOptionsTab();
			}
		}

		private void btnSelectMsvcrBinary_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.InitialDirectory = Path.GetDirectoryName(Configuration.Default.MsvcrBinarySource);
			openFileDialog.FileName = Path.GetFileName(Configuration.Default.MsvcrBinarySource);
			openFileDialog.Multiselect = false;
			openFileDialog.Title = "Select MSVCR71.DLL Binary File";
			openFileDialog.Filter = "msvcr71.dll|msvcr71.dll|All Dynamic Link Libraries (*.dll)|*.dll|All Files (*.*)|*.*";

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				Configuration.Default.MsvcrBinarySource = openFileDialog.FileName;
				Configuration.Default.Save();
				UpdateOptionsTab();
			}
		}

		private void UpdateStatusDatagrid()
		{
			/*
			ResetInstanceMonitor();
 * */
			this.Cursor = Cursors.WaitCursor;
			try
			{
				_instanceMonitor.RefreshServerStatistics();
			}
			finally
			{
				this.Cursor = Cursors.Default;
			}
			
		}

		

		private void UpdateOptionsTab()
		{
			txtMemcachedBinaryPath.Text = Configuration.Default.MemcachedBinarySource;
			txtMsvcrBinaryPath.Text = Configuration.Default.MsvcrBinarySource;

			string memcachedVersion = "<unknown>";
			if (File.Exists(txtMemcachedBinaryPath.Text) == true)
				memcachedVersion = FileVersionInfo.GetVersionInfo(txtMemcachedBinaryPath.Text).FileVersion;

			txtMemcachedVersion.Text = memcachedVersion;
		}

		private void EnsureConfigurationFileDefaultsAreSet()
		{
			string applicationPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);

			if (Configuration.Default.MemcachedBinarySource == null || Configuration.Default.MemcachedBinarySource == String.Empty)
			{
				Configuration.Default.MemcachedBinarySource = Path.Combine(applicationPath, @"BinaryFiles\memcached.exe");
				Configuration.Default.Save();
			}

			if (Configuration.Default.MsvcrBinarySource == null || Configuration.Default.MsvcrBinarySource == String.Empty)
			{
				Configuration.Default.MsvcrBinarySource = Path.Combine(applicationPath, @"BinaryFiles\msvcr71.dll");
				Configuration.Default.Save();
			}

			// Test to ensure that the default password is decryptable if it is set.
			// Some users have reported that the decryption explodes when using the impersonation.
			if ((Configuration.Default.DefaultPassword ?? String.Empty) != String.Empty)
			{
				try
				{
					Encryption.Decrypt(Configuration.Default.DefaultPassword);
				}
				catch (Exception)
				{
					MessageBox.Show("Couldn't decrypt the default password from the config file. This may happen if you copied MemCacheD from one computer to another and included the .config file. The default password will be reset to a blank string.");
					Configuration.Default.DefaultPassword = String.Empty;
					Configuration.Default.Save();
				}
			}
		}

		
		void OnInstanceUpdated(MemCacheDManager.Business.Server server, MemCacheDManager.Business.Instance instance, MemCacheDManager.Business.InstanceStatistics instanceStatistics)
		{
			Business.InstanceStatistics targetStatistics = _instanceStatistics.Find(new Predicate<MemCacheDManager.Business.InstanceStatistics>(delegate(MemCacheDManager.Business.InstanceStatistics testValue)
				{
					return (testValue.Instance == instance);
				}));

			if (targetStatistics != null)
				_instanceStatistics[_instanceStatistics.IndexOf(targetStatistics)] = instanceStatistics;
			else
				_instanceStatistics.Add(instanceStatistics);

			if (this.InvokeRequired == true)
			{
				if (_isFormClosing == false)
				{
					this.Invoke(new UpdateInstanceStatisticsDelegate(delegate(List<Business.InstanceStatistics> updateStatistics)
					{

						UpdateInstanceStatistics(updateStatistics);

					}), new object[] { _instanceStatistics });
				}
			}
			else
				UpdateInstanceStatistics(_instanceStatistics);
		}

		// This is a seperate method so that it can be called via invoke and non-invoke methods. 
		private void UpdateInstanceStatistics(List<Business.InstanceStatistics> updateStatistics)
		{
			string memcachedBinaryVersion = null;
			if(File.Exists(Configuration.Default.MemcachedBinarySource) == true)
				memcachedBinaryVersion = FileVersionInfo.GetVersionInfo(Configuration.Default.MemcachedBinarySource).FileVersion;

			BindingSource bs = new BindingSource();
			bs.DataSource = updateStatistics;
			dgvStatus.DataSource = bs;

			foreach (TreeNode serverNode in tvManager.Nodes)
			{
				if (serverNode.Nodes.Count > 0)
				{
					Business.StatusIconIndex serverNodeIconIndex = Business.StatusIconIndex.Up;
					string serverNodeTooltip = Constants.TooltipUp;

					foreach (TreeNode instanceNode in serverNode.Nodes)
					{
						Business.StatusIconIndex instanceNodeIconIndex = Business.StatusIconIndex.Up;
						string instanceNodeTooltip = Constants.TooltipUp;

						Business.InstanceStatistics targetStatistics = updateStatistics.Find(new Predicate<MemCacheDManager.Business.InstanceStatistics>(delegate(Business.InstanceStatistics compareStatistics)
						{
							return compareStatistics.Instance == (Business.Instance)instanceNode.Tag;
						}));


						if (targetStatistics != null)
						{
							instanceNodeIconIndex = targetStatistics.StatusIconIndex;
							instanceNodeTooltip = targetStatistics.StatusTooltip;
						}



						if (instanceNodeIconIndex == Business.StatusIconIndex.NeedsUpdate && serverNodeIconIndex == Business.StatusIconIndex.Up)
						{
							serverNodeIconIndex = instanceNodeIconIndex;
							serverNodeTooltip = instanceNodeTooltip;
						}

						if (instanceNodeIconIndex == Business.StatusIconIndex.ServiceDown
							&& (serverNodeIconIndex == Business.StatusIconIndex.Up
							|| serverNodeIconIndex == Business.StatusIconIndex.NeedsUpdate))
						{
							serverNodeIconIndex = instanceNodeIconIndex;
							serverNodeTooltip = instanceNodeTooltip;
						}

						if (instanceNodeIconIndex == Business.StatusIconIndex.CommunicationError
							&& (serverNodeIconIndex == Business.StatusIconIndex.Up
							|| serverNodeIconIndex == Business.StatusIconIndex.NeedsUpdate
							|| serverNodeIconIndex == Business.StatusIconIndex.ServiceDown))
						{
							serverNodeIconIndex = instanceNodeIconIndex;
							serverNodeTooltip = instanceNodeTooltip;
						}

						if (instanceNodeIconIndex == Business.StatusIconIndex.ServiceNonControllable
							&& (serverNodeIconIndex == Business.StatusIconIndex.Up
							|| serverNodeIconIndex == Business.StatusIconIndex.NeedsUpdate
							|| serverNodeIconIndex == Business.StatusIconIndex.ServiceDown
							|| serverNodeIconIndex == Business.StatusIconIndex.CommunicationError))
						{
							serverNodeIconIndex = instanceNodeIconIndex;
							serverNodeTooltip = instanceNodeTooltip;
						}

						instanceNode.ImageIndex = (int)instanceNodeIconIndex;
						instanceNode.SelectedImageIndex = (int)instanceNodeIconIndex;
						instanceNode.ToolTipText = instanceNodeTooltip;
					}

					serverNode.ImageIndex = (int)serverNodeIconIndex;
					serverNode.SelectedImageIndex = (int)serverNodeIconIndex;
					serverNode.ToolTipText = serverNodeTooltip;
				}
				else
				{
					serverNode.ImageIndex = (int)Business.StatusIconIndex.Empty;
					serverNode.SelectedImageIndex = (int)Business.StatusIconIndex.Empty;
					serverNode.ToolTipText = Constants.TooltipEmpty;
				}
			}
		}


		private void ResetInstanceMonitor()
		{
			if (_instanceMonitor != null)
				_instanceMonitor.Stop();

			_instanceStatistics = new List<MemCacheDManager.Business.InstanceStatistics>();
			_instanceMonitor = new MemCacheDManager.Business.InstanceMonitor(this._serverConfiguration.Servers);
			_instanceMonitor.InstanceUpdated += new MemCacheDManager.Business.InstanceMonitor.OnInstanceUpdated(OnInstanceUpdated);
			_instanceMonitor.Start();
		}

		private void tvManager_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
		{
			//tsslStatus.Text = e.Node.ToolTipText; 
		}

		private void tvManager_MouseMove(object sender, MouseEventArgs e)
		{
			TreeViewHitTestInfo treeViewHitTestInfo = tvManager.HitTest(e.Location);
			if (treeViewHitTestInfo.Node != null && treeViewHitTestInfo.Node.ToolTipText != String.Empty)
				tsslStatus.Text = treeViewHitTestInfo.Node.ToolTipText;
			else
				tsslStatus.Text = "Ready.";
		}

		private void tvManager_MouseLeave(object sender, EventArgs e)
		{
			tsslStatus.Text = "Ready.";
		}

		private void dgvStatus_MouseMove(object sender, MouseEventArgs e)
		{
			DataGridView.HitTestInfo hitTestInfo = dgvStatus.HitTest(e.Location.X, e.Location.Y);
			
			int tooltipIndex = dgvStatus.Columns.IndexOf(colStatusTooltip);

			string status = "Ready.";

			if (hitTestInfo.RowIndex >= 0)
			{
				DataGridViewCell cell = dgvStatus.Rows[hitTestInfo.RowIndex].Cells[tooltipIndex];
				if(cell.Value != null)
				status = cell.Value.ToString();
			}
			
			tsslStatus.Text = status;
		}

		private void tsmiRedeployUpdate_Click(object sender, EventArgs e)
		{
			if (tvManager.SelectedNode == null || tvManager.SelectedNode.Tag is Business.Server == false)
				return;

			Business.Server server = (Business.Server)tvManager.SelectedNode.Tag;

			if (MessageBox.Show("Would you like to update " + server.ServerName + " with your copy of MemCacheD?\n(This will restart all currently running instances of MemCacheD on this server.)", "Update MemCacheD", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
			{
				Cursor = Cursors.WaitCursor;

				try
				{
					foreach (Business.Instance instance in server.Instances)
					{
						SetStatusLabel("Stopping instance: " + instance.DisplayName + " on server: " + server.ServerName);
						instance.Stop(server, SetStatusLabel);
					}

					List<string> pushedImages = new List<string>();
					foreach (Business.Instance instance in server.Instances)
					{
						if (pushedImages.Contains(instance.ImageBasePath) == false)
						{
							SetStatusLabel("Sending new software version to server: " + server.ServerName);

							server.EnsureExecutableIsAvailabeOnServer(instance.ImageBasePath, true);
							pushedImages.Add(instance.ImageBasePath);
						}
					}

					foreach (Business.Instance instance in server.Instances)
					{
						SetStatusLabel("Starting instance: " + instance.DisplayName + " on server: " + server.ServerName);
						instance.Start(server, SetStatusLabel);
					}

					ResetInstanceMonitor();

					SetStatusLabel("Refreshing stats.");

					_instanceMonitor.RefreshServerStatistics();
				}
				finally
				{
					SetStatusLabel("Ready.");

					Cursor = Cursors.Default;
				}
			}
		}

		private void tsmiStopService_Click(object sender, EventArgs e)
		{
			if (tvManager.SelectedNode == null || tvManager.SelectedNode.Tag is Business.Instance == false)
				return;

			Business.Instance instance = (Business.Instance)tvManager.SelectedNode.Tag;
			Business.Server server = (Business.Server)tvManager.SelectedNode.Parent.Tag;


			if (MessageBox.Show("Would you like to stop the running service?", "Are you sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
			{
				Cursor = Cursors.WaitCursor;
				try
				{
					instance.Stop(server, SetStatusLabel);
					_instanceMonitor.RefreshServerStatistics();
				}
				finally
				{
					Cursor = Cursors.Default;
				}

				ResetInstanceMonitor();
			}
		}

		private void tsmiStartService_Click(object sender, EventArgs e)
		{
			if (tvManager.SelectedNode == null || tvManager.SelectedNode.Tag is Business.Instance == false)
				return;

			Business.Instance instance = (Business.Instance)tvManager.SelectedNode.Tag;
			Business.Server server = (Business.Server)tvManager.SelectedNode.Parent.Tag;

			if (MessageBox.Show("Would you like to start the service?", "Are you sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
			{
				Cursor = Cursors.WaitCursor;
				try
				{
					instance.Start(server, SetStatusLabel);
					_instanceMonitor.RefreshServerStatistics();
				}
				finally
				{
					Cursor = Cursors.Default;
				}

				ResetInstanceMonitor();
			}
		}

		private void tsmiRestartService_Click(object sender, EventArgs e)
		{
			if (tvManager.SelectedNode == null || tvManager.SelectedNode.Tag is Business.Instance == false)
				return;

			Business.Instance instance = (Business.Instance)tvManager.SelectedNode.Tag;
			Business.Server server = (Business.Server)tvManager.SelectedNode.Parent.Tag;

			if (MessageBox.Show("Would you like to start the service?", "Are you sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
			{
				Cursor = Cursors.WaitCursor;
				try
				{
					instance.Stop(server, SetStatusLabel);
					
					Thread.Sleep(1000);

					instance.Start(server, SetStatusLabel);

					_instanceMonitor.RefreshServerStatistics();
				}
				finally
				{
					Cursor = Cursors.Default;
				}

				ResetInstanceMonitor();
			}
		}

		private void showHelpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.OnHelpRequested(new HelpEventArgs(this.Cursor.HotSpot));
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutBox aboutBox = new AboutBox();
			aboutBox.ShowDialog();
		}

		private void tsmiNewConfiguration_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.DefaultExt = ".xml";
			saveFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
			saveFileDialog.OverwritePrompt = true;
			saveFileDialog.Title = "New Configuration File";
			saveFileDialog.Filter = "Configuration Files (*.xml)|*.xml|All Files (*.*)|*.*";

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				Configuration.Default.LastConfigFile = saveFileDialog.FileName;
				Configuration.Default.Save();
				_serverConfiguration = new ServerConfiguration();
				_serverConfiguration.Save(Configuration.Default.LastConfigFile);

				_serverDetails.ServerConfiguration = _serverConfiguration;

				SetTitleBar(Configuration.Default.LastConfigFile);

				tvManager.Nodes.Clear();

				LoadUI();
			}
		}

		private void tsmiLoadConfiguration_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.DefaultExt = ".xml";
			openFileDialog.FileName = Configuration.Default.LastConfigFile;
			openFileDialog.Filter = "Configuration Files (*.xml)|*.xml|All Files (*.*)|*.*";
			openFileDialog.InitialDirectory = Path.GetDirectoryName(Configuration.Default.LastConfigFile);
			openFileDialog.Multiselect = false;
			openFileDialog.Title = "Select MemCacheD Manager Configuration File";

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				Configuration.Default.LastConfigFile = openFileDialog.FileName;
				Configuration.Default.Save();
				_serverConfiguration = new ServerConfiguration();
				_serverConfiguration.Load(Configuration.Default.LastConfigFile);

				_serverDetails.ServerConfiguration = _serverConfiguration;

				SetTitleBar(Configuration.Default.LastConfigFile);

				tvManager.Nodes.Clear();

				LoadUI();
			}
		}


	}
}