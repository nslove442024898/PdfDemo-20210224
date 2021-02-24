using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string strTargetPdf = @"D:\个人文件夹\桌面\Planar_Modelling.pdf";//待处理的pdf文件

            string strOutPutPdf = @"D:\个人文件夹\桌面\Planar_Modelling_OutPut.pdf";//最终得到的pdf文件

            using (Stream inputPdfStream = new FileStream(strTargetPdf, FileMode.Open, FileAccess.Read, FileShare.Read))
                //图片的
            using (Stream inputImageStream = new FileStream(@"D:\个人文件夹\桌面\stamp.png", FileMode.Open, FileAccess.Read, FileShare.Read))
            //输出的pdf文件
            using (Stream outputPdfStream = new FileStream(strOutPutPdf, FileMode.Create, FileAccess.Write, FileShare.None))

            {
                var reader = new PdfReader(inputPdfStream);

                var stamper = new PdfStamper(reader, outputPdfStream);

                int numberOfPages = reader.NumberOfPages;

                Image jD = Image.GetInstance(inputImageStream);

                jD.ScaleAbsolute(230, 113);

                for (int i = 1; i <= numberOfPages; i++)
                {
                    var pdfSize = reader.GetPageSize(i);

                    jD.SetAbsolutePosition((float)0.5 * pdfSize.Width, 0);

                    var pdfContentByte = stamper.GetOverContent(i);

                    pdfContentByte.AddImage(jD);
                }

                stamper.Close();

                System.Diagnostics.Process.Start(strOutPutPdf);

            }

        }

    }
}
