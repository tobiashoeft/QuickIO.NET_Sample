using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SchwabenCode.QuickIO;

namespace QuickIO.NET_Sample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var start = DateTime.Now;
            IEnumerable<KeyValuePair<QuickIOPathInfo, QuickIOFileSystemEntryType>> allSubEntries =
                QuickIODirectory.EnumerateFileSystemEntries(@"I:\Ordner", "*.txt", SearchOption.AllDirectories);

            foreach (KeyValuePair<QuickIOPathInfo, QuickIOFileSystemEntryType> subEntry in allSubEntries)
            {
                var pathInfo = subEntry.Key;
                var type = subEntry.Value;
                Console.WriteLine("Entry found: {0} Readonly: {1}", pathInfo.FullName, type);
            }
            var end = DateTime.Now - start;
            label1.Text = String.Format("{0} sekunden",end.TotalSeconds);
        }

        private void button2_Click(object sender, EventArgs e)
        {
              string path = @"I:\Ordner";
                if (File.Exists(path))
                {
                    // This path is a file
                    ProcessFile(path);
                }
                else if (Directory.Exists(path))
                {
                    var start = DateTime.Now;
                    // This path is a directory
                   int count =  ProcessDirectory(path);
                    var end = DateTime.Now -start;
                    label2.Text = String.Format("{0} sekunden für {1}", end.TotalSeconds,count);
                }
                else
                {
                    Console.WriteLine("{0} is not a valid file or directory.", path);
                }
         

        }


      
        public static int ProcessDirectory(string targetDirectory)
        {
            int count = 0; 

            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                count++;
                ProcessFile(fileName);
                
            }
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
            {
                count++;
                ProcessDirectory(subdirectory);
            }
            return count; 
        }
        public static void ProcessFile(string path)
        {
            Console.WriteLine("Processed file '{0}'.", path);
        }
    }
}
        
    

