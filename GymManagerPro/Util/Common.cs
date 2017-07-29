using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace GymManagerPro.Util
{
    public static class Common
    {
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
        public static void ToCsV(DataGridViewRowCollection rows, DataGridViewColumnCollection columns, string filename)
        {
            string stOutput = "";
            // Export titles:
            string sHeaders = "";

            for (int j = 0; j < columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";
            // Export data.
            for (int i = 0; i < rows.Count - 1; i++)
            {
                string stLine = "";
                for (int j = 0; j < rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(rows[i].Cells[j].Value) + "\t";
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
    }
}
