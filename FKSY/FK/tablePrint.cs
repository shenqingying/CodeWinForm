using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bussiness.Modles;
namespace FKSY.FK
{
    class tablePrint
    {
        MODLEFKINFO modleFKINFO = new MODLEFKINFO();
        private System.Drawing.Printing.PrintDocument docToPrint =
       new System.Drawing.Printing.PrintDocument(); //打印对象
        Image imagep = null;
        Image image2 = null;
        public void print(MODLEFKINFO modleFK, Image imagept, Image _image2)
        {
            modleFKINFO = modleFK;
            image2 = _image2;
            imagep = imagept;
            docToPrint.PrintPage += new PrintPageEventHandler(this.document_PrintPage);
            docToPrint.Print();//打印
        }

        private void document_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font font18 = new Font("宋体", 18);
            Font font8 = new Font("宋体", 9);
            Brush bru = Brushes.Black;
            e.Graphics.DrawString("访客单", font18, bru, 95, 0);
            Pen blackPen = new Pen(Color.Black, 1);
            e.Graphics.DrawString("访客登记管理", font8, bru, 0, 35);
            e.Graphics.DrawString("日期：", font8, bru, 160, 35);
            e.Graphics.DrawLine(blackPen, 1, 50, 282, 50);
            e.Graphics.DrawLine(blackPen, 25, 80, 165, 80);
            e.Graphics.DrawLine(blackPen, 25, 110, 165, 110);
            e.Graphics.DrawLine(blackPen, 25, 140, 165, 140);
            e.Graphics.DrawLine(blackPen, 25, 170, 165, 170);
            e.Graphics.DrawLine(blackPen, 25, 200, 282, 200);
            e.Graphics.DrawLine(blackPen, 1, 230, 282, 230);
            e.Graphics.DrawLine(blackPen, 25, 260, 282, 260);
            e.Graphics.DrawLine(blackPen, 25, 290, 282, 290);
            e.Graphics.DrawLine(blackPen, 1, 320, 282, 320);
            e.Graphics.DrawLine(blackPen, 1, 400, 282, 400);
            e.Graphics.DrawLine(blackPen, 1, 430, 282, 430);
            e.Graphics.DrawLine(blackPen, 1, 460, 282, 460);

            e.Graphics.DrawLine(blackPen, 282, 50, 282, 460);
            e.Graphics.DrawLine(blackPen, 1, 50, 1, 460);
            e.Graphics.DrawLine(blackPen, 25, 50, 25, 320);
            e.Graphics.DrawLine(blackPen, 80, 50, 80, 320);
            e.Graphics.DrawLine(blackPen, 165, 50, 165, 200);
            e.Graphics.DrawLine(blackPen, 70, 400, 70, 460);
            e.Graphics.DrawLine(blackPen, 141, 430, 141, 460);
            e.Graphics.DrawLine(blackPen, 201, 430, 201, 460);
            
            e.Graphics.DrawString("姓    名", font8, bru, 26, 60);
            e.Graphics.DrawString("来访事由", font8, bru, 26, 90);
            e.Graphics.DrawString("来访人数", font8, bru, 26, 120);
            e.Graphics.DrawString("车 牌 号", font8, bru, 26, 150);
            e.Graphics.DrawString("访客证号", font8, bru, 26, 180);
            e.Graphics.DrawString("工作单位", font8, bru, 26, 210);
            e.Graphics.DrawString("访", font8, bru, 5, 90);
            e.Graphics.DrawString("客", font8, bru, 5, 120);
            e.Graphics.DrawString("信", font8, bru, 5, 150);
            e.Graphics.DrawString("息", font8, bru, 5, 180);
            e.Graphics.DrawString("姓    名", font8, bru, 26, 240);
            e.Graphics.DrawString("部    门", font8, bru, 26, 270);
            e.Graphics.DrawString("签    字", font8, bru, 26, 300);
            e.Graphics.DrawString("被", font8, bru, 5, 240);
            e.Graphics.DrawString("访", font8, bru, 5, 270);
            e.Graphics.DrawString("人", font8, bru, 5, 300);
            e.Graphics.DrawString("证件号码", font8, bru, 1, 410);
            e.Graphics.DrawString("执勤人签字", font8, bru, 1, 440);
            e.Graphics.DrawString("扫描时间", font8, bru, 141, 440);
            e.Graphics.DrawString("访问结束请将此单交还登记处", font8, bru, 10, 462);

            e.Graphics.DrawString(modleFKINFO.LFRQ, font8, bru, 200, 35);
            e.Graphics.DrawString(modleFKINFO.FKNAME, font8, bru, 81, 60);
            e.Graphics.DrawString(modleFKINFO.FKLFSY, font8, bru, 81, 90);
            e.Graphics.DrawString(modleFKINFO.FKSUM, font8, bru, 81, 120);
            e.Graphics.DrawString(modleFKINFO.FKCPH, font8, bru, 81, 150);
            e.Graphics.DrawString(modleFKINFO.FKZH, font8, bru, 81, 180);
            e.Graphics.DrawString(modleFKINFO.FKDW, font8, bru, 81, 210);
            e.Graphics.DrawString(modleFKINFO.SFNAME, font8, bru, 81, 240);
            e.Graphics.DrawString(modleFKINFO.SFBM, font8, bru, 81, 270);
            e.Graphics.DrawString(modleFKINFO.FKZJHM, font8, bru, 81, 410);
            if (imagep != null)
            {
                e.Graphics.DrawImage(imagep, 166, 51, 116, 149);
            }
            if (image2 != null)
            {
                e.Graphics.DrawImage(image2, 2, 325, 279, 50);
            }
            e.Graphics.DrawString(modleFKINFO.TM, font8, bru, 100, 380);
        }

        public void PrintPriview(MODLEFKINFO modleFK, Image imagept, Image _image2)
        {
            modleFKINFO = modleFK;
            image2 = _image2;
            imagep = imagept;
            try
            {
                PrintPreviewDialog PrintPriview = new PrintPreviewDialog();
                PrintPriview.Document = CreatePrintDocument();
                PrintPriview.WindowState = FormWindowState.Maximized;
                PrintPriview.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
        private PrintDocument CreatePrintDocument()
        {
            docToPrint.PrintPage += new PrintPageEventHandler(this.document_PrintPage);
            return docToPrint;
        }
    }
}
