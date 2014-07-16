using Ionic.Zip;
using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VncRecorder.Properties;
using VncSharp;

namespace VncRecorder
{
    public partial class RecorderForm : Form
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private class Student
        {
            public string Name { get; set; }
            public string Id { get; set; }
            public string Class { get; set; }
            public string Seat { get; set; }
            public DateTime Date { get; set; }
        }

        private List<Student> students;

        public RecorderForm()
        {
            InitializeComponent();

            students = new List<Student>();

            var myExeDir = (new FileInfo(Application.ExecutablePath)).Directory.FullName;

            var lines = File.ReadAllLines(Path.Combine(myExeDir, "TestStudents.csv"));
            //Format is No,Student Name,Chinese Name,Student No,Class,Seat,Date
            //Skip the first row!
            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                var items = line.Split(',');

                var student = new Student();
                student.Name = items[1];
                student.Id = items[3];
                student.Class = items[4];
                student.Seat = items[5];
               
                //7/17/2014
                string dateString = items[6];
                string[] datePart = dateString.Split(new[]{'/'});
                student.Date = new DateTime(int.Parse(datePart[2]), int.Parse(datePart[0]), int.Parse(datePart[1]));
                               
                students.Add(student);
            }
            this.students.Sort((a, b) => a.Seat.CompareTo(b.Seat));

            this.dateTimePickerTestDate.Value = DateTime.Now;
            UpdateSeatItems();
            this.textBoxVncIp.Text = Settings.Default.VncIpPrefix;
            this.textBoxFtpIp.Text = Settings.Default.FtpIp;
            this.numericUpDownCapturePeriod.Value = Settings.Default.CapturePeriod;
        }

        public class ComboBoxItem
        {

            public string Text { get; set; }
            public object Value { get; set; }

            public ComboBoxItem(string Text, object Value)
            {
                this.Text = Text;
                this.Value = Value;
            }

            public override string ToString()
            {
                return Text;
            }
        }

        private void UpdateSeatItems()
        {
            comboBoxSeat.Items.Clear();
            var date = this.dateTimePickerTestDate.Value;

            foreach (var student in this.students)
            {
                //Filter the date
                if (student.Date.Date == date.Date)
                {
                    comboBoxSeat.Items.Add(new ComboBoxItem(student.Seat + "-" + student.Name, student.Id));
                }
            }
        }

        private void buttonStartRecord_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.textBoxStudentId.Text) ||
                string.IsNullOrEmpty(this.textBoxVncIp.Text)
                )
            {
                MessageBox.Show("You must input student ID and IP!");
                return;
            }
            else if (!IsValidIpAddress(this.textBoxVncIp.Text))
            {
                MessageBox.Show("Incorrect IP Format!");
                return;
            }
            else if (!IsValidStudentId(this.textBoxStudentId.Text))
            {
                MessageBox.Show("Incorrect Student ID Format!");
                return;
            }


            if (!Directory.Exists(this.textBoxStudentId.Text))
                Directory.CreateDirectory(this.textBoxStudentId.Text);



            // assuming your RemoteDesktop control is named rd
            rd.ConnectComplete +=
                new ConnectCompleteHandler(ConnectComplete);
            rd.ConnectionLost +=
                new EventHandler(ConnectionLost);

            // rd.Authenticate(this.textBoxPassword.Text);
            rd.GetPassword += () => this.textBoxPassword.Text;
            rd.AutoSize = true;
            rd.Connect(this.textBoxVncIp.Text, true, true);

            this.buttonStartRecord.Enabled = false;
            this.buttonStop.Enabled = true;

        }

        protected void ConnectComplete(object sender,
                   ConnectEventArgs e)
        {
            // Update Form to match geometry of remote desktop
            //ClientSize = new Size(e.DesktopWidth,
            //                      e.DesktopHeight);


            // Change the Form's title to match desktop name
            Text = e.DesktopName;
            this.timerCapture.Interval = ((int)this.numericUpDownCapturePeriod.Value) * 1000;
            this.timerCapture.Enabled = true;
        }

        protected void ConnectionLost(object sender,
                                      EventArgs e)
        {
            // Let the user know of lost connection
            MessageBox.Show("Lost Connection to Host!");
        }

        private string getZipFolder()
        {
            return this.textBoxStudentId.Text + @"/";
        }

        private string getZipPath()
        {
            return this.textBoxStudentId.Text + @".zip";
        }


        private void buttonStop_Click(object sender, EventArgs e)
        {

            this.buttonStop.Enabled = false;

            rd.Disconnect();
            this.timerCapture.Enabled = false;

            string startPath = getZipFolder();
            string zipPath = getZipPath();

            if (File.Exists(zipPath))
            {
                File.Delete(zipPath);
            }

            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(startPath, this.textBoxStudentId.Text);
                zip.SaveProgress += zip_SaveProgress;
                zip.Save(zipPath);
            }

            this.buttonStartRecord.Enabled = true;
        }

        void zip_SaveProgress(object sender, SaveProgressEventArgs e)
        {
            if (e.EventType == ZipProgressEventType.Saving_Completed)
            {
                // Thread.Sleep(100);
                this.timerUpload.Enabled = true;
                Console.WriteLine("Saving_Completed");
            }
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
        private void timerCapture_Tick(object sender, EventArgs e)
        {
            var savetime = DateTime.Now.ToFileTimeUtc();
            var myImageCodecInfo = GetEncoderInfo("image/jpeg");
            var myEncoderParameters = new EncoderParameters(1);
            myEncoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 25L);
            rd.Desktop.Save(this.textBoxStudentId.Text + @"/" + this.textBoxStudentId.Text + "_" + savetime + ".jpeg",
                myImageCodecInfo, myEncoderParameters);
        }

        private void timerUpload_Tick(object sender, EventArgs e)
        {
            this.timerUpload.Enabled = false;
            string zipPath = getZipPath();
            var ftpClient = new Ftp(@"ftp://" + this.textBoxFtpIp.Text, Settings.Default.FtpUserName, Settings.Default.FtpPassword);
            string tempFile = "T" + zipPath;
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
            File.Copy(zipPath, tempFile);
            ftpClient.delete(zipPath);
            var value = ftpClient.upload(zipPath, Path.Combine(Directory.GetCurrentDirectory(), tempFile));
            Console.WriteLine("Uploaded!" + value);
        }

        private bool IsValidStudentId(string studentId)
        {
            int n;
            return studentId.Length == 9 && int.TryParse(studentId, out n);
        }
        private bool IsValidIpAddress(string ipAddress)
        {
            try
            {
                // Create an instance of IPAddress for the specified address string (in  
                // dotted-quad, or colon-hexadecimal notation).
                IPAddress address = IPAddress.Parse(ipAddress);

                // Display the address in standard notation.
                Console.WriteLine("Parsing your input string: " + "\"" + ipAddress + "\"" + " produces this address (shown in its standard notation): " + address.ToString());
                return true;
            }

            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            }

            catch (FormatException e)
            {
                Console.WriteLine("FormatException caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            }

            catch (Exception e)
            {
                Console.WriteLine("Exception caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            }
            return false;
        }

        private void buttonDownloadTestMaterial_Click(object sender, EventArgs e)
        {
            var files = Settings.Default.TestMaterials.Split(',');

            var ftpClient = new Ftp(@"ftp://" + this.textBoxFtpIp.Text, Settings.Default.FtpUserName, Settings.Default.FtpPassword);

            var downloadFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string drive = Path.GetPathRoot(@"D:\");
            if (!this.checkBoxDesktop.Checked && Directory.Exists(drive))
            {
                downloadFolderPath = drive;
            }

            Parallel.ForEach(files, currentFile =>
                {
                    // The more computational work you do here, the greater 
                    // the speedup compared to a sequential foreach loop.
                    var filepathname = Path.Combine(downloadFolderPath, currentFile);
                    if (File.Exists(filepathname))
                    {
                        File.Delete(filepathname);
                    }

                    ftpClient.download(currentFile, filepathname);
                    // Peek behind the scenes to see how work is parallelized.
                    // But be aware: Thread contention for the Console slows down parallel loops!!!
                    Console.WriteLine("Processing {0} on thread {1}", filepathname,
                                        Thread.CurrentThread.ManagedThreadId);

                } //close lambda expression
            ); //close method invocation


        }

        private void buttonUploadYourTestWork_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Test files(*.7z;*.zip)|*.7z;*.zip|All files (*.*)|*.* ";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Multiselect = true;


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var ftpClient = new Ftp(@"ftp://" + this.textBoxFtpIp.Text, Settings.Default.FtpUserName, Settings.Default.FtpPassword);

                var ip = LocalIPAddress().Replace(".", "_");
                ftpClient.createDirectory(ip);

                Parallel.ForEach(openFileDialog1.FileNames, currentFilePathName =>
                    {
                        var currentFileName = Path.GetFileName(currentFilePathName);
                        string tempFilePathName = Path.Combine(Path.GetDirectoryName(currentFilePathName), "T" + currentFileName);
                        if (File.Exists(tempFilePathName))
                        {
                            File.Delete(tempFilePathName);
                        }
                        File.Copy(currentFilePathName, tempFilePathName);

                        // ftpClient.delete(ip + "/" + currentFileName);

                        var value = ftpClient.upload(ip + "/" + currentFileName, tempFilePathName);
                        // Peek behind the scenes to see how work is parallelized.
                        // But be aware: Thread contention for the Console slows down parallel loops!!!
                        Console.WriteLine("Processing {0} on thread {1}", currentFileName,
                                            Thread.CurrentThread.ManagedThreadId);

                    } //close lambda expression
                ); //close method invocation
            }
        }

        public string LocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }

        private void comboBoxSeat_SelectedIndexChanged(object sender, EventArgs e)
        {
            var c = (ComboBoxItem)this.comboBoxSeat.SelectedItem;

            this.labelStudentName.Text = c.Text;
            this.textBoxStudentId.Text = c.Value.ToString();
        }

        private void dateTimePickerTestDate_ValueChanged(object sender, EventArgs e)
        {
            UpdateSeatItems();
        }

    }

}
