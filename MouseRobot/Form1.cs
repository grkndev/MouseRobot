using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace MouseRobot
{
    public partial class Form1 : Form
    {
        private const int WH_MOUSE_LL = 14;
        private const int WM_LBUTTONDOWN = 0x0201;

        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int VK_F9 = 0x78; // F9 
        private IntPtr _keyBoardhookId = IntPtr.Zero;

        private static LowLevelMouseProc? _proc;
        private static LowLevelKeyboardProc? _keyboardProc;
        private static IntPtr _hookID = IntPtr.Zero;

        private bool isTracking = false;
        private bool _isTaskRunning = false;
        private bool _isLoop = false;
        private CancellationTokenSource _cancellationTokenSource;

        private int delay = 1000;

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        public Form1()
        {
            InitializeComponent();
            _proc = HookCallback;
            _keyboardProc = HookKeyboardCallback;
            _hookID = SetHook(_proc);
            _keyBoardhookId = SetkeyboardHook(_keyboardProc);
        }

        private void startRecord_Click(object sender, EventArgs e)
        {
            isTracking = !isTracking;
            _isLoop = loopBox.Checked;
            if (isTracking)
            {
                startRecord.Text = "Stop Record";
                statusLabel.Text = "Recording";
                runRecord.Enabled = newRecord.Enabled = saveRecord.Enabled = openRecord.Enabled = false;
            }
            else
            {
                startRecord.Text = "Start Record";
                statusLabel.Text = "Recorded";
                runRecord.Enabled = newRecord.Enabled = saveRecord.Enabled = openRecord.Enabled = true;
            }
        }

        private void SendMouseClick(int x, int y)
        {
            Cursor.Position = new System.Drawing.Point(x, y);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, x, y, 0, UIntPtr.Zero);
        }

        private static IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }
        private static IntPtr SetkeyboardHook(LowLevelKeyboardProc proc)
        {
            using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }
        private IntPtr HookKeyboardCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if(nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if(vkCode == VK_F9)
                {
                    if (_isTaskRunning) StopTask();
                    else StartTask();
                }
            }
          
            return CallNextHookEx(_keyBoardhookId, nCode, wParam, lParam);
        }
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
           
            if (nCode >= 0 && wParam == (IntPtr)WM_LBUTTONDOWN)
            {
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                string clickInfo = $"X={hookStruct.pt.x},Y={hookStruct.pt.y}";
                if (isTracking)
                {
                    AddToListBox(clickInfo);
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            UnhookWindowsHookEx(_hookID);
            UnhookWindowsHookEx(_keyBoardhookId);
            base.OnFormClosing(e);
        }

        private void AddToListBox(string item)
        {
            if (eventLog.InvokeRequired)
            {
                eventLog.Invoke((MethodInvoker)delegate { eventLog.Items.Add(item); });
            }
            else
            {
                eventLog.Items.Add(item);
            }
        }

        private void deleteLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (eventLog.SelectedItems.Count > 0)
            {
                eventLog.Items.Remove(eventLog.SelectedItems[0]);
            }
            if (eventLog.Items.Count == 0) runRecord.Enabled = saveRecord.Enabled = false;
            statusLabel.Text = "Ready";
        }

        private void eventLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            contextMenuStrip1.Items[0].Enabled = eventLog.SelectedItems.Count > 0 ? true : false;

        }

        private void clearLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eventLog.Items.Clear();
            statusLabel.Text = "Ready";
            runRecord.Enabled = saveRecord.Enabled = false;
        }

        private void runRecord_Click(object sender, EventArgs e)
        {
            if (!_isTaskRunning)
            {
                StartTask();
            }
            else
            {
                StopTask();
            }
        }
        private async void StartTask()
        {
            if (eventLog.Items.Count == 0) return;
            openRecord.Enabled = saveRecord.Enabled = loopBox.Enabled = newRecord.Enabled = startRecord.Enabled = delayInput.Enabled = false;
            _cancellationTokenSource = new CancellationTokenSource();
            _isTaskRunning = true;
            _isLoop = loopBox.Checked;
            runRecord.Text = "Stop Task";
            await Task.Run(async () =>
            {
                var items = new object[eventLog.Items.Count];
                eventLog.Invoke((MethodInvoker)delegate
                {
                    eventLog.Items.CopyTo(items, 0);
                    eventLog.SelectedIndex = 0;
                });

                  loop:
                    foreach (var item in items)
                    {
                        if (_cancellationTokenSource.Token.IsCancellationRequested)
                            break;

                        string[] itemText = item.ToString().Split(',');
                        int xPos = Convert.ToInt32(itemText[0].Split('=')[1]);
                        int yPos = Convert.ToInt32(itemText[1].Split('=')[1]);
                        SendMouseClick(xPos, yPos);
                        await Task.Delay(delay);
                        eventLog.Invoke((MethodInvoker)delegate
                        {
                            if (eventLog.SelectedIndex != items.Length - 1)
                            {
                                eventLog.SelectedIndex++;
                            }
                            else
                            {
                                eventLog.SelectedIndex=0;
                            }
                        });
                    }
                if(!_cancellationTokenSource.Token.IsCancellationRequested && _isLoop) goto loop;

                _isTaskRunning = false;
                eventLog.Invoke((MethodInvoker)delegate
                {
                    runRecord.Text = "Run Record";
                });
            });

            openRecord.Enabled = delayInput.Enabled = loopBox.Enabled = saveRecord.Enabled = newRecord.Enabled = startRecord.Enabled = true;
        }
        private void StopTask()
        {
            _cancellationTokenSource?.Cancel();
        }

        private void delayInput_ValueChanged(object sender, EventArgs e)
        {
            delay = (int)delayInput.Value;
        }

        private void newRecord_Click(object sender, EventArgs e)
        {
            eventLog.Items.Clear();
            runRecord.Enabled = saveRecord.Enabled = newRecord.Enabled = false;
        }

        private void saveRecord_Click(object sender, EventArgs e)
        {
            if (eventLog.Items.Count == 0) return;
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Mouse Robot (*.mouserobot)|*.mouserobot";
            saveDialog.Title = "Save Record";
            DialogResult result = saveDialog.ShowDialog();
            if (result != DialogResult.OK) return;
            SaveListBoxItemsToFile(saveDialog.FileName, eventLog);
        }

        private void openRecord_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Mouse Robot (*.mouserobot)|*.mouserobot";
            openFileDialog.Title = "Open Record";
            DialogResult result = openFileDialog.ShowDialog();
            if (result != DialogResult.OK) return;
            LoadItemsFromFile(openFileDialog.FileName, eventLog);
            runRecord.Enabled = true;
        }
        private void SaveListBoxItemsToFile(string filename, ListBox listBox)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filename, false, Encoding.UTF8))
                {
                    writer.WriteLine("#DO NOT ADD, DELETE OR CHANGE CHARACTERS IN THIS FILE!");
                    writer.WriteLine("#DOING SO WILL RENDER THE FILE UNUSABLE!");
                    foreach (var item in listBox.Items)
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(item.ToString());
                        string utf8String = Encoding.UTF8.GetString(bytes);
                        writer.WriteLine(utf8String);
                    }
                }

                MessageBox.Show("File saved successfully.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                statusLabel.Text = "File Saved";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadItemsFromFile(string filename, ListBox listBox)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filename, Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith("#") || line.Length <= 1 || String.IsNullOrEmpty(line) || String.IsNullOrWhiteSpace(line))
                            continue;

                        listBox.Items.Add(line);
                    }
                }

                MessageBox.Show("File loaded successfly", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                statusLabel.Text = "File loaded";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Win32 API
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, UIntPtr dwExtraInfo);

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        #endregion

      
    }
}
