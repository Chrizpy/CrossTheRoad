using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CrossTheRoad
{
    class FTP
    {
        // Plats där banor skall sparas.
        string filePath = Path.GetFullPath("Content/");

        // Fil som letas efter.
        public string lookForFile;


        public void DownLoadFile(string serverpath, string userId, string pass, string lookForFile)
        {
            this.lookForFile = lookForFile;
            // Ifall filen inte finns påbörjas en nedladdning
            if (File.Exists(filePath + lookForFile) == false)
            {
                // Skapa objekt för nedladdning av fil
                FtpWebRequest downloader = (FtpWebRequest)WebRequest.Create(serverpath + lookForFile);
                downloader.Method = WebRequestMethods.Ftp.DownloadFile;

                // Inlogg för ftp servern
                downloader.Credentials = new NetworkCredential(userId, pass);

                // Frågar efter svar ifrån FTP servern
                using (FtpWebResponse response = (FtpWebResponse)downloader.GetResponse())

                // När anslutning etablerats kan dataöverföring börja
                using (Stream rStream = response.GetResponseStream())

                // När datan är erhållen kan den börjas läsas
                using (StreamReader sr = new StreamReader(rStream))

                // När datan är läst kan den skrivas i den nya filen som skapas
                using (StreamWriter destination = new StreamWriter(filePath + lookForFile))
                {
                    destination.Write(sr.ReadToEnd());
                }
            }
            else
            {
                // Annars behöver den inte göra någonting
            }
 
        }
    }
}
