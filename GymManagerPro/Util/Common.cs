using DevComponents.DotNetBar.Controls;
using GymManagerPro.DataLayer;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace GymManagerPro.Util
{
    public static class Common
    {
        private const string DATABASE_NAME = "GymManager";
        private const string BACKUP_PATH = @"C:\";

        // highlights specified text
        // http://stackoverflow.com/questions/11851908/highlight-all-searched-word-in-richtextbox-c-sharp
        //
        public static void HighlightText(this RichTextBox myRtb, string word, Color color)
        {
            int s_start = myRtb.SelectionStart, startIndex = 0, index;

            while ((index = myRtb.Text.IndexOf(word, startIndex)) != -1)
            {
                myRtb.Select(index, word.Length);
                myRtb.SelectionColor = color;

                startIndex = index + word.Length;
            }
            myRtb.SelectionStart = s_start;
            myRtb.SelectionLength = 0;
            myRtb.SelectionColor = Color.Black;
        }

        /// <summary>
        /// exports data to xls file 
        /// (http://www.codeproject.com/Tips/545456/Exporting-DataGridview-To-Excel)
        /// </summary>
        /// <param name="dGV">datagridview</param>
        /// <param name="filename">filename</param>
        public static void ToCsV(DataGridViewX dgv, string filename)
        {
            string stOutput = "";
            // Export titles:
            string sHeaders = "";

            for (int j = 0; j < dgv.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dgv.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";
            // Export data.
            for (int i = 0; i < dgv.Rows.Count - 1; i++)
            {
                string stLine = "";
                for (int j = 0; j < dgv.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dgv.Rows[i].Cells[j].Value) + "\t";
                stOutput += stLine + "\r\n";
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();
        }

        public static void BackUpDatabase()
        {
            var sqlserverBackupPath = @"C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Backup\";
            var tempFileName = string.Format("GymManager-{0}.bak", DateTime.Now.ToString("yyyyMMddhhmmss"));

            SaveFileDialog saveFileDialog1 = new SaveFileDialog()
            {
                InitialDirectory = @"C:\",
                Title = "Save text Files",
                FileName = string.Format("{0}-{1}.bak", DATABASE_NAME, DateTime.Now.ToString("yyyy-MM-dd")),
                CheckPathExists = true,
                DefaultExt = "txt",
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true,
                OverwritePrompt = true
            };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (SqlConnection con = DB.GetSqlConnection())
                {
                    var tempFilePath = Path.Combine(sqlserverBackupPath, tempFileName);
                    var query = String.Format("BACKUP DATABASE [{0}] TO DISK='{1}'", DATABASE_NAME, tempFilePath);

                    using (var command = new SqlCommand(query, con))
                    {
                        command.ExecuteNonQuery();
                    }
                    File.Copy(tempFilePath, saveFileDialog1.FileName, true);
                    MessageBox.Show("Backup created successfully!", "Success", MessageBoxButtons.OK);
                }
            }            
        }
    }
}
