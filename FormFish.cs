using Google.Protobuf;
using Grpc.Net.Client;
using SharpAdbClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Farming
{
    public partial class FormFish : Form
    {
        // Declare
        private Thread thread;
        private Process[] processes;
        private List<DeviceData> devices;
        private DeviceData device;
        private Process cmdProc = new Process();
        private IntPtr hwh;
        private bool isPlay = false;
        private IShellOutputReceiver receiver = null;
        private AdbServer adbServer;
        private AdbClient adbClient;
        private string adbPath = Application.StartupPath + "platform-tools";
        private string projectPath = Application.StartupPath;
        private Point pointClick = new Point(818, 395);
        private System.Drawing.Color pictureBg = System.Drawing.SystemColors.ActiveCaptionText;
        private decimal _count = 0;
        private decimal count = 0;
        private System.Media.SoundPlayer player = new System.Media.SoundPlayer(Application.StartupPath + "thaiwin.wav");
        private GrpcChannel grpcChannel = null;
        private ImageClassification.ImageClassificationClient grpcClient = null;
        private string grpcResult = String.Empty;
        private int maxLine = 18;
        private List<string> displayText = new List<string>();

        public FormFish()
        {
            InitializeComponent();
        }

        private void FormFish_Load(object sender, EventArgs e)
        {
            UpdateRichText(null);
            bool isReady = GetProgramReady();
            if (isReady)
            {
                GetProcesses();
                GetDevices();
            }
            if (grpcClient == null)
            {
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                grpcChannel = GrpcChannel.ForAddress("http://localhost:31700");
                grpcClient = new ImageClassification.ImageClassificationClient(grpcChannel);
            }
        }

        private bool GetProgramReady()
        {
            bool isReady = false;
            try
            {
                string adbFile = $@"{adbPath}\adb.exe";
                if (!File.Exists(adbFile))
                {
                    throw new Exception("Need adb.exe on path: " + adbFile);
                }

                string soundFile = $@"{projectPath}thaiwin.wav";
                if (!File.Exists(soundFile))
                {
                    throw new Exception("Need thaiwin.wav on path: " + soundFile);
                }

                // when pass all will normal run
                isReady = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Program is required");
            }

            return isReady;
        }

        private void GetProcesses()
        {
            numericCount.Enabled = false;
            buttonRefresh.Enabled = false;
            buttonRefresh.Image = global::Farming.Properties.Resources.refresh_16_gray;
            buttonRefresh.Cursor = System.Windows.Forms.Cursors.No;
            processes = Process.GetProcesses();
            listBoxProcesses.Items.Clear();
            if (processes.Length > 0)
            {
                listBoxProcesses.Enabled = true;
                listBoxProcesses.Cursor = System.Windows.Forms.Cursors.Hand;
                foreach (Process p in processes)
                {
                    if (!String.IsNullOrEmpty(p.MainWindowTitle))
                    {
                        listBoxProcesses.Items.Add(p.MainWindowTitle);
                    }
                }
            }
            else
            {
                listBoxProcesses.Enabled = false;
                listBoxProcesses.Cursor = System.Windows.Forms.Cursors.No;
            }
        }

        private void GetDevices()
        {
            try
            {
                numericCount.Enabled = false;
                buttonRefresh.Enabled = false;
                buttonRefresh.Image = global::Farming.Properties.Resources.refresh_16_gray;

                cmdProc.StartInfo.FileName = "cmd.exe";
                cmdProc.StartInfo.RedirectStandardInput = true;
                cmdProc.StartInfo.RedirectStandardOutput = true;
                cmdProc.StartInfo.CreateNoWindow = true;
                cmdProc.StartInfo.UseShellExecute = false;
                cmdProc.Start();

                cmdProc.StandardInput.WriteLine("cd " + adbPath + " && adb.exe devices");
                cmdProc.StandardInput.Flush();
                cmdProc.StandardInput.Close();
                cmdProc.WaitForExit();

                if (AdbServer.Instance.GetStatus().IsRunning)
                {
                    string path = adbPath + "\\adb.exe";
                    adbServer = new AdbServer();
                    adbServer.StartServer(path, false);
                }

                adbClient = new AdbClient();
                devices = adbClient.GetDevices();
                listBoxEmulators.Items.Clear();

                if (devices.Count > 0)
                {
                    listBoxEmulators.Enabled = true;
                    listBoxEmulators.Cursor = System.Windows.Forms.Cursors.Hand;
                    foreach (DeviceData d in devices)
                    {
                        if (!String.IsNullOrEmpty(d.Serial))
                        {
                            listBoxEmulators.Items.Add(d.Serial);
                        }
                    }
                }
                else
                {
                    listBoxEmulators.Enabled = false;
                    listBoxEmulators.Cursor = System.Windows.Forms.Cursors.No;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                _count = numericCount.Value;
                numericCount.Enabled = true;
                buttonRefresh.Enabled = true;
                buttonRefresh.Image = global::Farming.Properties.Resources.refresh_16;
                buttonRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            UpdateRichText(null);
            bool isReady = GetProgramReady();
            if (isReady)
            {
                GetProcesses();
                GetDevices();
            }
            if (grpcClient == null)
            {
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                grpcChannel = GrpcChannel.ForAddress("http://localhost:31700");
                grpcClient = new ImageClassification.ImageClassificationClient(grpcChannel);
            }
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            if (hwh != null && device != null)
            {
                if (isPlay == true)
                {
                    // to stop
                    isPlay = false;
                    listBoxProcesses.Enabled = true;
                    listBoxEmulators.Enabled = true;
                    buttonPlay.Image = global::Farming.Properties.Resources.play_16;
                    Stop();
                }
                else
                {
                    // to start
                    isPlay = true;
                    listBoxProcesses.Enabled = false;
                    listBoxEmulators.Enabled = false;

                    numericCount.Enabled = false;
                    numericCount.Minimum = 0;
                    count = numericCount.Value;

                    buttonPlay.Image = global::Farming.Properties.Resources.stop_16;
                    thread = new Thread(new ThreadStart(RunAuto));
                    thread.Start();
                }
            }
        }

        private void listBoxProcesses_SelectedIndexChanged(object sender, EventArgs e)
        {
            dynamic selected = sender;
            foreach (Process process in processes)
            {
                if (selected.SelectedItem != null && process.MainWindowTitle.Equals(selected.SelectedItem.ToString()))
                {
                    hwh = process.MainWindowHandle;
                    if (device != null)
                    {
                        buttonPlay.Enabled = true;
                        buttonPlay.Image = global::Farming.Properties.Resources.play_16;
                        buttonPlay.Cursor = System.Windows.Forms.Cursors.Hand;
                    }
                }
            }
        }

        private void listBoxEmulators_SelectedIndexChanged(object sender, EventArgs e)
        {
            dynamic selected = sender;
            foreach (DeviceData _device in devices)
            {
                if (selected.SelectedItem != null && _device.Serial.Equals(selected.SelectedItem.ToString()))
                {
                    device = _device;
                    if (hwh != null)
                    {
                        buttonPlay.Enabled = true;
                        buttonPlay.Image = global::Farming.Properties.Resources.play_16;
                        buttonPlay.Cursor = System.Windows.Forms.Cursors.Hand;
                    }
                }
            }
        }

        private void FormFish_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (thread != null)
            {
                Stop();
            }
        }

        private void Stop()
        {
            try
            {
                isPlay = false;

                buttonPlay.Invoke((MethodInvoker)(() =>
                {
                    buttonPlay.Image = global::Farming.Properties.Resources.play_16;
                    buttonPlay.Cursor = System.Windows.Forms.Cursors.Hand;
                }));

                //pictureBox1.Invoke((MethodInvoker)(() =>
                //{
                //    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                //    pictureBox1.BackColor = pictureBg;
                //    Bitmap bmp2 = (Bitmap)pictureBox1.Image;
                //    pictureBox1.Image = null;
                //    if (bmp2 != null)
                //    {
                //        bmp2.Dispose();
                //    }
                //}));

                numericCount.Invoke((MethodInvoker)(() =>
                {
                    numericCount.Minimum = 1;
                    numericCount.Enabled = true;
                    numericCount.Value = count;
                }));

                // Interrupt Thread
                thread.Interrupt();
            }
            catch (Exception ex)
            {
                MessageBox.Show("function: Stop, Error: " + ex.Message);
            }
        }

        private void RunAuto()
        {
            while (isPlay)
            {
                try
                {
                    TargetedImage();
                    //Bitmap img = TargetedImage();
                    //if (pictureBox1.Visible)
                    //{
                    //    pictureBox1.Invoke((MethodInvoker)(() =>
                    //    {
                    //        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    //        pictureBox1.BackColor = pictureBg;
                    //        Bitmap _img = (Bitmap)pictureBox1.Image;
                    //        pictureBox1.Image = img;
                    //        if (_img != null)
                    //        {
                    //            _img.Dispose();
                    //        }
                    //    }));
                    //}
                    //else
                    //{
                    //    pictureBox1.Invoke((MethodInvoker)(() =>
                    //    {
                    //        pictureBox1.Image = null;
                    //    }));
                    //}

                    if (grpcResult != null)
                    {
                        if (grpcResult.Equals("Reelable"))
                        {
                            // Castable
                            if (_count == 0)
                            {
                                player.Play();
                                Stop();
                            }
                            else
                            {
                                _count--;
                            }
                            // Reelable
                            SetResults("Reelable", Color.Green, _count);
                            receiver = new ConsoleOutputReceiver();
                            AdbClient.Instance.ExecuteRemoteCommandAsync($@"input tap {pointClick.X} {pointClick.Y}", device, receiver, CancellationToken.None, 0);
                            Thread.Sleep(800);
                        }

                        if (grpcResult.Equals("Castable"))
                        {
                            SetResults("Castable", Color.Blue, _count);
                            receiver = new ConsoleOutputReceiver();
                            AdbClient.Instance.ExecuteRemoteCommandAsync($@"input tap {pointClick.X} {pointClick.Y}", device, receiver, CancellationToken.None, 0);
                            Thread.Sleep(800);
                        }

                        if (grpcResult.Equals("Background"))
                        {
                            // Background
                            SetResults("Background", Color.Gray, _count);
                        }

                        if (grpcResult.Equals("Fishing"))
                        {
                            // Fishing
                            SetResults("Fishing", Color.Gray, _count);
                        }
                    }

                    Thread.Sleep(10);
                }
                catch (Exception ex)
                {
                    if (!ex.Message.Equals("Thread was interrupted from a waiting state."))
                    {
                        MessageBox.Show("function: RunAuto, Error: " + ex.Message);
                    }
                }
            }
        }

        private void SetResults(string text, Color color, Decimal __count)
        {
            buttonForResult.Invoke((MethodInvoker)(() =>
            {
                buttonForResult.Text = text;
            }));
            numericCount.Invoke((MethodInvoker)(() =>
            {
                if (__count >= 0)
                {
                    numericCount.Value = __count;
                }
            }));
        }

        public void TargetedImage()

        {
            /*
            - capture
            - create crop template
            - crop
            - create crop template
            - crop
            - save
            - load
             */
            Bitmap targetBmp = new Bitmap(10, 10, PixelFormat.Format32bppArgb);
            try
            {
                Bitmap bmp = Screenshot.GetWindowCapture(hwh);
                Bitmap clone = new Bitmap(bmp.Width - 38, bmp.Height - 35, PixelFormat.Format32bppArgb);
                Bitmap clone1st = bmp.Clone(new Rectangle(0, 35, bmp.Width - 38, bmp.Height - 35), clone.PixelFormat);
                Bitmap clone2nd = new Bitmap(clone1st.Width, clone1st.Height, PixelFormat.Format32bppArgb);
                int _x = (248 * clone1st.Width) / 328;
                int _y = (107 * clone1st.Height) / 187;
                int _l = (60 * clone1st.Width) / 328;
                Bitmap clone3rd = clone1st.Clone(new Rectangle(_x, _y, _l, _l), clone2nd.PixelFormat);
                targetBmp = resizeImage(clone3rd, new Size(48, 48));

                bmp.Dispose();
                clone.Dispose();
                clone1st.Dispose();
                clone2nd.Dispose();
                clone3rd.Dispose();

                string savepath = $@"{Application.StartupPath}{device.Serial}.jpeg";

                // save file by memory stream
                using (MemoryStream memory = new MemoryStream())
                {
                    targetBmp.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    ImageResponse _result = grpcClient.PredictImage(new ImageRequest { File = ByteString.CopyFrom(bytes) });
                    grpcResult = _result.Result;
                    string displayTxt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " | " + _result.Result + "\n";
                    UpdateRichText(displayTxt);
                    targetBmp.Dispose();
                }
            }
            catch (Exception ex)
            {
                if (!ex.Message.Equals("Thread was interrupted from a waiting state."))
                {
                    MessageBox.Show("function: TargetedImage, Error: " + ex.Message);
                }
            }

            //return targetBmp;
        }

        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        private Bitmap resizeImage(Bitmap imgToResize, Size size)
        {
            //Get the image current width  
            int sourceWidth = imgToResize.Width;
            //Get the image current height  
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //Calulate  width with new desired size  
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //Calculate height with new desired size  
            nPercentH = ((float)size.Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //New Width  
            int destWidth = (int)(sourceWidth * nPercent);
            //New Height  
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height  
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return b;
        }

        private void numericCount_ValueChanged(object sender, EventArgs e)
        {
            _count = numericCount.Value;
        }

        private static async Task<bool> UploadFilesAsync(byte[] image)
        {
            HttpClient client = new HttpClient();
            // we need to send a request with multipart/form-data
            var multiForm = new MultipartFormDataContent();

            var fileContent = new ByteArrayContent(image);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            multiForm.Add(fileContent, "files", "username.jpeg");

            // send request to API
            var url = "https://localhost:44373/machine";
            using (var response = await client.PostAsync(url, multiForm))
            {
                return response.IsSuccessStatusCode;
            }
        }

        private void UpdateRichText(string updateTxt)
        {
            if (displayText.Count == 0)
            {
                // Initial
                for (int i = 0; i < maxLine; i++)
                {
                    if (i == 0 || i == 1)
                    {
                        displayText.Add("\n");
                    }
                    else
                    {
                        if (i == maxLine - 1)
                        {
                            displayText.Add("");
                        }
                        else
                        {
                            displayText.Add("" + "\n");
                        }
                    }
                }
            }
            else
            {
                if (updateTxt != null)
                {
                    displayText.RemoveAt(2);
                    displayText.Add(updateTxt);
                }

                // Join
                string txt = String.Empty;
                for (int i = 0; i < displayText.Count; i++)
                {
                    txt += displayText[i];
                }
                pictureBox1.Invoke((MethodInvoker)(() =>
                {
                    pictureBox1.Text = txt;
                }));
            }
        }
    }
}
