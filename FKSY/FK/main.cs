using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Bussiness.FKSY;
using Bussiness.Modles;
namespace FKSY.FK
{
    public partial class main : Form
    {
        BardCodeHooK BarCode = new BardCodeHooK();
        modleMW modlemw = new modleMW();
        MODLESTAFF modleSTAFF = new MODLESTAFF();
        GetException getException = new GetException();

        BarcodeLib.Barcode b = new BarcodeLib.Barcode();
        int ZJLXRI = 0;
        private delegate void ShowInfoDelegate(BardCodeHooK.BarCodes barCode);
        MainService mainService = new MainService();
        string pathX = "";
        string pathD = "";
        int ISMachine = 0;//判断机器变量

        //public delegate string LoginFun(string Section, string key);
        //public LoginFun loginfun2;






        //string now = DateTime.Now.ToString("yyyyMMddHHmmss");
        public main()
        {
            InitializeComponent();
        }
        public main(string RI, string USER, LOGIN.LoginFun login)
        {
            InitializeComponent();
            modlemw = mainService.Select_FK_MWINFO_modle(RI);
            lbMWINFO.Text = modlemw.MWMS;
            modleSTAFF = mainService.Select_HG_STAFF(USER);
            toolLBUSER.Text = modleSTAFF.STAFFUSER;
            //  ddlJQ.SelectedValue = ddlstr;
            // ISMachine = ddlstr;

            strSec = Path.GetFileNameWithoutExtension(strFilePath);
            string machine = login(strSec, "ddlmachine");
            if (machine != "")
            {
                SelectMachine.SelectedValue = machine;
                ISMachine = Convert.ToInt32(machine);
            }
            else if (machine == "")
            {
                //判断是哪种机器
                DistinguishMachine();
            }
        }

        bool m_bNIDapi = false;

        byte[] pucCHMsg = new byte[512];
        byte[] pucPHMsg = new byte[1024];
        int puiCHMsgLen = 512;
        int puiPHMsgLen = 1024;
        int m_nOpenPort = 0;
        string str;
        bool m_bIsIDCardLoaded = false;

        /// <summary>
        /// newmachine argument
        /// </summary>

        public static bool IsConnected = false;
        public static bool IsAuthenticate = false;
        public static bool IsRead_Content = false;
        public static int Port = 0;
        public static int ComPort = 0;
        public const int cbDataSize = 128;
        public const int GphotoSize = 256 * 1024;
        StringBuilder sb = new StringBuilder(cbDataSize);



        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);
        private string strFilePath = Application.StartupPath + "\\FileConfig.ini";//获取INI文件路径
        private string strSec = ""; //INI文件名



        private void main_Load(object sender, EventArgs e)
        {
            //先把图片目录下的历史照片删掉
            string path = Application.StartupPath + "\\image";
            DeleteSrcFolder(path);
            path = Application.StartupPath + "\\TX";
            DeleteSrcFolder(path);

            CSH();
            LoadKernal();
            rbSY1.Select();
            rbSSWP1.Select();
            bandddlZJLX();
            BANDddlZFRYMW();

            dgvZFRY.AutoGenerateColumns = false;
            BANDdgvZFRY();
            txtFWSYRS.Text = "1";
            BANDddlBFXM();
            ddlCPSF.SelectedIndex = 0;
            SBZJ("2");
            BANDddlJQ(ISMachine);


            System.Windows.Forms.Timer t1 = new System.Windows.Forms.Timer();
            t1.Tick += new System.EventHandler(this.ReadCard);
            t1.Interval = 1000;
            t1.Enabled = true;
            System.Windows.Forms.Timer t2 = new System.Windows.Forms.Timer();
            t2.Tick += new System.EventHandler(this.AutoClassAndRecognize);
            t2.Interval = 1000;
            t2.Enabled = true;
            this.timer1.Start();
        }

        public static class MyDllsdtapi
        {
            [DllImport("kernel32")]
            public static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
            [DllImport("kernel32")]
            public static extern int LoadLibrary(string strDllName);

            [DllImport("sdtapi.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
            public static extern int SDT_OpenPort(int iPort);
            [DllImport("sdtapi.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
            public static extern int SDT_ClosePort(int iPort);

            [DllImport("sdtapi.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
            public static extern int SDT_StartFindIDCard(int iPort, ref byte pRAPDU, int iIfOpen);

            [DllImport("sdtapi.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
            public static extern int SDT_SelectIDCard(int iPort, ref byte pRAPDU, int iIfOpen);

            [DllImport("sdtapi.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
            public static extern int SDT_ReadBaseMsg(int iPort, ref byte pucCHMsg, ref int puiCHMsgLen, ref byte pucPHMsg, ref int puiPHMsgLen, int iIfOpen);

            [DllImport("sdtapi.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
            public static extern int SDT_ReadNewAppMsg(int iPort, ref byte pucAppMsg, ref int puiAppMsgLen, int iIfOpen);

            [DllImport("WltRS.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
            public static extern int GetBmp(string filename, int nType);
        }

        public static class MyDllIDCard
        {
            [DllImport("kernel32")]
            public static extern int LoadLibrary(string strDllName);

            [DllImport("IDCard", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int InitIDCard(char[] cArrUserID, int nType, char[] cArrDirectory);

            [DllImport("IDCard", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int GetRecogResult(int nIndex, char[] cArrBuffer, ref int nBufferLen);

            [DllImport("IDCard", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int RecogIDCard();

            [DllImport("IDCard", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int GetFieldName(int nIndex, char[] cArrBuffer, ref int nBufferLen);

            [DllImport("IDCard", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int AcquireImage(int nCardType);

            [DllImport("IDCard", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int SaveImage(char[] cArrFileName);
            [DllImport("IDCard", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int SaveHeadImage(char[] cArrFileName);


            [DllImport("IDCard", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int GetCurrentDevice(char[] cArrDeviceName, int nLength);

            [DllImport("IDCard", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern void GetVersionInfo(char[] cArrVersion, int nLength);

            [DllImport("IDCard", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern bool CheckDeviceOnline();

            [DllImport("IDCard", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern bool SetAcquireImageType(int nLightType, int nImageType);

            [DllImport("IDCard", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern bool SetUserDefinedImageSize(int nWidth, int nHeight);

            [DllImport("IDCard", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern bool SetAcquireImageResolution(int nResolutionX, int nResolutionY);

            [DllImport("IDCard", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int SetIDCardID(int nMainID, int[] nSubID, int nSubIdCount);

            [DllImport("IDCard", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int AddIDCardID(int nMainID, int[] nSubID, int nSubIdCount);

            [DllImport("IDCard", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int RecogIDCardEX(int nMainID, int nSubID);

            [DllImport("IDCard", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int GetButtonDownType();

            [DllImport("IDCard", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int GetGrabSignalType();

            [DllImport("IDCard", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int SetSpecialAttribute(int nType, int nSet);

            [DllImport("IDCard", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern void FreeIDCard();
            [DllImport("IDCard", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int GetDeviceSN(char[] cArrSn, int nLength);

            [DllImport("IDCard", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int GetBusinessCardResult(int nID, int nIndex, char[] cArrBuffer, ref int nBufferLen);

            [DllImport("IDCard", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int RecogBusinessCard(int nType);

            [DllImport("IDCard", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int GetBusinessCardFieldName(int nID, char[] cArrBuffer, ref int nBufferLen);

            [DllImport("IDCard", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int GetBusinessCardResultCount(int nID);

            [DllImport("IDCard", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int LoadImageToMemory(string path, int nType);
        }

        //中控机器
        public static class NewMachine
        {
            [DllImport("termb.dll")]
            public static extern int InitComm(int port);//连接身份证阅读器 

            //[DllImport("termb.dll")]
            [DllImport("termb.dll")]
            public static extern int InitCommExt();//自动搜索身份证阅读器并连接身份证阅读器 

            [DllImport("termb.dll")]
            public static extern int CloseComm();//断开与身份证阅读器连接 

            [DllImport("termb.dll")]
            public static extern int Authenticate();//判断是否有放卡，且是否身份证 

            [DllImport("termb.dll")]
            public static extern int Read_Content(int index);//读卡操作,信息文件存储在dll所在下

            [DllImport("termb.dll")]
            public static extern int ReadContent(int index);//读卡操作,信息文件存储在dll所在下

            [DllImport("termb.dll")]
            public static extern int GetSAMID(StringBuilder SAMID);//获取SAM模块编号

            [DllImport("termb.dll")]
            static extern int GetSAMIDEx(StringBuilder SAMID);//获取SAM模块编号（10位编号）

            [DllImport("termb.dll")]
            static extern int GetBmpPhoto(string PhotoPath);//解析身份证照片

            [DllImport("termb.dll")]
            static extern int GetBmpPhotoToMem(byte[] imageData, int cbImageData);//解析身份证照片

            [DllImport("termb.dll")]
            public static extern int GetBmpPhotoExt();//解析身份证照片

            [DllImport("termb.dll")]
            static extern int Reset_SAM();//重置Sam模块

            [DllImport("termb.dll")]
            static extern int GetSAMStatus();//获取SAM模块状态 

            [DllImport("termb.dll")]
            static extern int GetCardInfo(int index, StringBuilder value);//解析身份证信息 

            [DllImport("termb.dll")]
            static extern int ExportCardImageV();//生成竖版身份证正反两面图片(输出目录：dll所在目录的cardv.jpg和SetCardJPGPathNameV指定路径)

            [DllImport("termb.dll")]
            static extern int ExportCardImageH();//生成横版身份证正反两面图片(输出目录：dll所在目录的cardh.jpg和SetCardJPGPathNameH指定路径) 

            [DllImport("termb.dll")]
            static extern int SetTempDir(string DirPath);//设置生成文件临时目录

            [DllImport("termb.dll")]
            static extern int GetTempDir(StringBuilder path, int cbPath);//获取文件生成临时目录

            [DllImport("termb.dll")]
            static extern void GetPhotoJPGPathName(StringBuilder path, int cbPath);//获取jpg头像全路径名 


            [DllImport("termb.dll")]
            static extern int SetPhotoJPGPathName(string path);//设置jpg头像全路径名

            [DllImport("termb.dll")]
            static extern int SetCardJPGPathNameV(string path);//设置竖版身份证正反两面图片全路径

            [DllImport("termb.dll")]
            static extern int GetCardJPGPathNameV(StringBuilder path, int cbPath);//获取竖版身份证正反两面图片全路径

            [DllImport("termb.dll")]
            static extern int SetCardJPGPathNameH(string path);//设置横版身份证正反两面图片全路径

            [DllImport("termb.dll")]
            static extern int GetCardJPGPathNameH(StringBuilder path, int cbPath);//获取横版身份证正反两面图片全路径

            [DllImport("termb.dll")]
            public static extern int getName(StringBuilder data, int cbData);//获取姓名

            [DllImport("termb.dll")]
            public static extern int getSex(StringBuilder data, int cbData);//获取性别

            [DllImport("termb.dll")]
            public static extern int getNation(StringBuilder data, int cbData);//获取民族

            [DllImport("termb.dll")]
            public static extern int getBirthdate(StringBuilder data, int cbData);//获取生日(YYYYMMDD)

            [DllImport("termb.dll")]
            public static extern int getAddress(StringBuilder data, int cbData);//获取地址

            [DllImport("termb.dll")]
            public static extern int getIDNum(StringBuilder data, int cbData);//获取身份证号

            [DllImport("termb.dll")]
            public static extern int getIssue(StringBuilder data, int cbData);//获取签发机关

            [DllImport("termb.dll")]
            public static extern int getEffectedDate(StringBuilder data, int cbData);//获取有效期起始日期(YYYYMMDD)

            [DllImport("termb.dll")]
            public static extern int getExpiredDate(StringBuilder data, int cbData);//获取有效期截止日期(YYYYMMDD) 

            [DllImport("termb.dll")]
            public static extern int getBMPPhotoBase64(StringBuilder data, int cbData);//获取BMP头像Base64编码 

            [DllImport("termb.dll")]
            static extern int getJPGPhotoBase64(StringBuilder data, int cbData);//获取JPG头像Base64编码

            [DllImport("termb.dll")]
            static extern int getJPGCardBase64V(StringBuilder data, int cbData);//获取竖版身份证正反两面JPG图像base64编码字符串

            [DllImport("termb.dll")]
            static extern int getJPGCardBase64H(StringBuilder data, int cbData);//获取横版身份证正反两面JPG图像base64编码字符串

            [DllImport("termb.dll")]
            static extern int HIDVoice(int nVoice);//语音提示。。仅适用于与带HID语音设备的身份证阅读器（如ID200）

            [DllImport("termb.dll")]
            static extern int IC_SetDevNum(int iPort, StringBuilder data, int cbdata);//设置发卡器序列号

            [DllImport("termb.dll")]
            static extern int IC_GetDevNum(int iPort, StringBuilder data, int cbdata);//获取发卡器序列号

            [DllImport("termb.dll")]
            public static extern int IC_GetDevVersion(int iPort, StringBuilder data, int cbdata);//设置发卡器序列号 

            [DllImport("termb.dll")]
            static extern int IC_WriteData(int iPort, int keyMode, int sector, int idx, StringBuilder key, StringBuilder data, int cbdata, ref uint snr);//写数据

            [DllImport("termb.dll")]
            static extern int IC_ReadData(int iPort, int keyMode, int sector, int idx, StringBuilder key, StringBuilder data, int cbdata, ref uint snr);//du数据

            [DllImport("termb.dll")]
            static extern int IC_GetICSnr(int iPort, ref uint snr);//读IC卡物理卡号 

            [DllImport("termb.dll")]
            static extern int IC_GetIDSnr(int iPort, StringBuilder data, int cbdata);//读身份证物理卡号 

            [DllImport("termb.dll")]
            static extern int getEnName(StringBuilder data, int cbdata);//获取英文名

            [DllImport("termb.dll")]
            static extern int getCnName(StringBuilder data, int cbdata);//获取中文名 

            [DllImport("termb.dll")]
            static extern int getPassNum(StringBuilder data, int cbdata);//获取港澳台居通行证号码

            [DllImport("termb.dll")]
            static extern int getVisaTimes();//获取签发次数

            [DllImport("termb.dll")]
            static extern int IC_ChangeSectorKey(int iPort, int keyMode, int nSector, StringBuilder oldKey, StringBuilder newKey);
        }

        //初始化
        private void CSH()
        {
            try
            {
                if (ISMachine == 2) //旧机器
                {
                    int nRet;

                    //加载动态库
                    if (m_bNIDapi == false)
                    {
                        nRet = MyDllsdtapi.LoadLibrary("sdtapi.dll");
                        if (nRet == 0)
                        {
                            MessageBox.Show("加载sdtapi.dll失败");
                            return;
                        }
                        else
                        {
                            // m_bNIDapi = true;
                            for (int iPort = 1001; iPort < 1017; iPort = iPort + 1)
                            {
                                if (MyDllsdtapi.SDT_OpenPort(iPort) == 0x90)
                                {
                                    m_nOpenPort = iPort;
                                    m_bNIDapi = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (m_bNIDapi != true)
                    {
                        MessageBox.Show("初始化失败,扫描仪是否开启！");
                    }
                    else
                    {
                        //MessageBox.Show("初始化成功！");
                    }
                }//新机器 判断是否链接成功
                else if (ISMachine == 1)
                {
                    NewMachineConnnect();
                }
            }
            catch (Exception e)
            {
                // MessageBox.Show("初始化失败");
                MessageBox.Show(e.ToString());
            }
        }

        private void ReadCard(object sender, EventArgs e)
        {
            string BirthDay = "";
            int nRet;
            if (ISMachine == 2)
            {
                //CSH();
                //LoadKernal();

                if (m_bNIDapi != true)
                {
                    //MessageBox.Show("没有初始化核心");
                    return;
                }
                byte[] pRAPDU = new byte[30];
                byte[] pucAppMsg = new byte[320];
                int len = 320;
                nRet = MyDllsdtapi.SDT_ReadNewAppMsg(m_nOpenPort, ref pucAppMsg[0], ref len, 0);
                if (nRet == 0x91 || nRet == 0x90)
                {
                    //MessageBox.Show("此卡已读过！");
                    return;
                }
                nRet = MyDllsdtapi.SDT_StartFindIDCard(m_nOpenPort, ref pRAPDU[0], 0);
                if (nRet != 0x9F)
                {
                    //MessageBox.Show("寻找卡失败");
                    return;
                }
                if (MyDllsdtapi.SDT_SelectIDCard(m_nOpenPort, ref pRAPDU[0], 0) != 0x90)
                {
                    //MessageBox.Show("选卡失败");
                    return;
                }
                nRet = MyDllsdtapi.SDT_ReadBaseMsg(m_nOpenPort, ref pucCHMsg[0], ref puiCHMsgLen, ref pucPHMsg[0], ref puiPHMsgLen, 0);
                if (nRet != 0x90)
                {
                    //MessageBox.Show("读取数据到数组失败");
                    return;
                }
            }
            else if (ISMachine == 1)
            {
                int FindCard = NewMachine.Authenticate();
                if (FindCard != 1)
                {
                    // CLEARTEXTBOX();
                    //连接未成功，尝试再次连接，保证下次读卡前连接是正常的。
                 //   NewMachineConnnect();
                    return;
                }
                //读卡
                int rs = NewMachine.Read_Content(1);
                if (rs != 1 && rs != 2 && rs != 3)
                {
                    //  CLEARTEXTBOX();
                    return;
                }
            }
            CLEARTEXTBOX();
            try
            {
                txtNAME.Text = GetName();
                lbNAME.Text = txtNAME.Text;
                txtSEX.Text = GetSex();
                lbSEX.Text = txtSEX.Text;
                txtMZ.Text = GetPeople();
                lbMZ.Text = txtMZ.Text;
                BirthDay = GetBirthday();
                if (BirthDay.Length == 8)
                {
                    lbYEAR.Text = BirthDay.Substring(0, 4);
                    lbMONTH.Text = BirthDay.Substring(4, 2);
                    lbDAY.Text = BirthDay.Substring(6, 2);
                    txtBirthDay.Text = lbYEAR.Text + "年" + lbMONTH.Text + "月" + lbDAY.Text + "日";
                }
                txtCARDID.Text = GetIDCode();
                lbIDCARD.Text = txtCARDID.Text;
                txtADDress.Text = GetAddress();
                lbADD.Text = txtADDress.Text;
                //老机器
                if (ISMachine == 2)
                {
                    string path = Application.StartupPath;
                    SavePhoto(path, 2);
                    path += "\\image\\head" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bmp";
                    pbTX.ImageLocation = path;
                    pathX = path;

                    pathD = "";
                    pictureBox3.Image = null;

                }
                //新机器
                if (ISMachine == 1)
                {
                    //显示头像
                    NewMachine.GetBmpPhotoExt();
                    int cbPhoto = 256 * 1024;
                    StringBuilder sbPhoto = new StringBuilder(cbPhoto);
                    int nRet1 = NewMachine.getBMPPhotoBase64(sbPhoto, cbPhoto);
                    byte[] byPhoto = Convert.FromBase64String(sbPhoto.ToString());
                    if (nRet1 == 1)
                    {
                        MemoryStream ms = new MemoryStream(byPhoto);
                        Image a = Image.FromStream(ms);
                        string path = Application.StartupPath;
                        path += "\\image\\head" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bmp";
                        a.Save(path);
                        pbTX.ImageLocation = path;
                        pathX = path;
                    }
                    //SBZJ("2");
                }
                ZJLXRI = 2;
                if (ZJLXRI != 0)
                {
                    ddlZJLX.SelectedValue = ZJLXRI;
                }
            }
            catch (Exception ex)
            {
                getException.Insert_FK_ExceptionINFO(ex.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "");
                MessageBox.Show("读卡失败");
            }
        }

        string GetName()
        {
            if (ISMachine == 2)
            {
                if (puiCHMsgLen == 0)
                {
                    return "";
                }
                str = System.Text.Encoding.Unicode.GetString(pucCHMsg, 0, 30);
            }
            else
            {
                // StringBuilder sb = new StringBuilder(cbDataSize);
                NewMachine.getName(sb, cbDataSize);
                str = sb.ToString();

            }
            return str;
        }
        string GetSex()
        {
            if (ISMachine == 2)
            {
                if (puiCHMsgLen == 0)
                {
                    return " ";
                }

                byte sex = pucCHMsg[30];

                if (sex == '1')
                {
                    return "男";
                }
                else
                    return "女";
            }
            else
            {
                NewMachine.getSex(sb, cbDataSize);
                return sb.ToString();
            }
        }
        string GetPeople()
        {
            if (ISMachine == 2)
            {
                if (puiCHMsgLen == 0)
                {
                    return " ";
                }

                str = System.Text.Encoding.Unicode.GetString(pucCHMsg, 32, 4);
                switch (str)
                {
                    case "01": return "汉";
                    case "02": return "蒙古";
                    case "03": return "回";
                    case "04": return "藏";
                    case "05": return "维吾尔";
                    case "06": return "苗";
                    case "07": return "彝";
                    case "08": return "壮";
                    case "09": return "布依";
                    case "10": return "朝鲜";
                    case "11": return "满";
                    case "12": return "侗";
                    case "13": return "瑶";
                    case "14": return "白";
                    case "15": return "土家";
                    case "16": return "哈尼";
                    case "17": return "哈萨克";
                    case "18": return " 傣";
                    case "19": return " 黎";
                    case "20": return " 傈僳";
                    case "21": return " 佤";
                    case "22": return " 畲";
                    case "23": return " 高山";
                    case "24": return " 拉祜";
                    case "25": return " 水";
                    case "26": return " 东乡";
                    case "27": return " 纳西";
                    case "28": return " 景颇";
                    case "29": return " 柯尔克孜";
                    case "30": return " 土";
                    case "31": return " 达斡尔";
                    case "32": return " 仫佬";
                    case "33": return "羌";
                    case "34": return "布朗";
                    case "35": return "撒拉";
                    case "36": return "毛南";
                    case "37": return "仡佬";
                    case "38": return "锡伯";
                    case "39": return "阿昌";
                    case "40": return "普米";
                    case "41": return "塔吉克";
                    case "42": return "怒";
                    case "43": return "乌孜别克";
                    case "44": return "俄罗斯";
                    case "45": return "鄂温克";
                    case "46": return "德昂";
                    case "47": return "保安";
                    case "48": return "裕固";
                    case "49": return "京";
                    case "50": return "塔塔尔";
                    case "51": return "独龙";
                    case "52": return "鄂伦春";
                    case "53": return "赫哲";
                    case "54": return "门巴";
                    case "55": return "珞巴";
                    case "56": return "基诺";
                    case "97": return "其他";
                    case "98": return "外国血统中国籍人士";
                    default: return "";
                }
            }
            else
            {
                NewMachine.getNation(sb, cbDataSize);
                return sb.ToString();
            }
        }
        string GetBirthday()
        {
            if (ISMachine == 2)
            {
                if (puiCHMsgLen == 0)
                {
                    return " ";
                }
                str = System.Text.Encoding.Unicode.GetString(pucCHMsg, 36, 16);
            }
            else if (ISMachine == 1)
            {
                NewMachine.getBirthdate(sb, cbDataSize);
                str = sb.ToString();
            }

            return str;
        }
        string GetAddress()
        {
            if (ISMachine == 2)
            {
                if (puiCHMsgLen == 0)
                {
                    return " ";
                }
                str = System.Text.Encoding.Unicode.GetString(pucCHMsg, 52, 36);
            }
            else if (ISMachine == 1)
            {
                NewMachine.getAddress(sb, cbDataSize);
                str = sb.ToString();
            }
            return str;
        }
        string GetAuthority()
        {
            if (puiCHMsgLen == 0)
                return " ";
            str = System.Text.Encoding.Unicode.GetString(pucCHMsg, 158, 30);
            return str;
        }
        string GetIDCode()
        {
            if (ISMachine == 2)
            {
                if (puiCHMsgLen == 0)
                {
                    return " ";
                }
                str = System.Text.Encoding.Unicode.GetString(pucCHMsg, 122, 36);
            }
            else if (ISMachine == 1)
            {
                NewMachine.getIDNum(sb, cbDataSize).ToString();
                str = sb.ToString();
            }
            return str;
        }
        string GetIssueDay()
        {
            if (puiCHMsgLen == 0)
                return "";
            str = System.Text.Encoding.Unicode.GetString(pucCHMsg, 188, 16);
            return str;
        }
        string GetExpityDay()
        {
            if (puiCHMsgLen == 0)
                return "";
            str = System.Text.Encoding.Unicode.GetString(pucCHMsg, 204, 16);
            return str;
        }
        int SavePhoto(string retFileName, int nType)
        {
            try
            {
                string savepath = retFileName + "\\image\\head.wlt";
                FileStream fs;
                fs = new FileStream(savepath, FileMode.Create, FileAccess.ReadWrite);
                fs.Write(pucPHMsg, 0, pucPHMsg.Length);
                fs.Close();
                int b = MyDllsdtapi.GetBmp(savepath, 2);
                return 0;
            }
            catch
            {
                MessageBox.Show("读取图像失败");
                return 0;
            }
        }
        public void DeleteSrcFolder(string file)
        {
            //去除文件夹和子文件的只读属性
            //去除文件夹的只读属性
            System.IO.DirectoryInfo fileInfo = new DirectoryInfo(file);
            fileInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;
            //去除文件的只读属性
            System.IO.File.SetAttributes(file, System.IO.FileAttributes.Normal);
            //判断文件夹是否还存在
            if (Directory.Exists(file))
            {
                foreach (string f in Directory.GetFileSystemEntries(file))
                {
                    if (File.Exists(f))
                    {
                        //如果有子文件删除文件
                        File.Delete(f);
                    }
                }
                //删除空文件夹
                //Directory.Delete(file);
            }
        }

        private void LoadKernal()
        {
            try
            {
                if (m_bIsIDCardLoaded)
                {
                    //textBoxDisplayResult.Text = "核心已经加载成功";
                    return;
                }

                int nRet;
                nRet = MyDllIDCard.LoadLibrary("IDCard");
                if (nRet == 0)
                {
                    //textBoxDisplayResult.Text = "加载IDCard.dll失败";
                    return;
                }

                //初始化识别核心

                char[] arr = lbYHID.Text.ToCharArray();

                nRet = MyDllIDCard.InitIDCard(arr, 1, null);
                if (nRet != 0)
                {
                    //textBoxDisplayResult.Text = "初始化识别核心失败\r\n";
                    MessageBox.Show("加载识别核心失败");
                    String strtmp = nRet.ToString();
                    //textBoxDisplayResult.Text += "返回值：" + strtmp;
                    return;
                }
                MyDllIDCard.SetSpecialAttribute(1, 1);
                m_bIsIDCardLoaded = true;
                //MessageBox.Show("加载识别核心成功");
            }
            catch
            {
                MessageBox.Show("加载识别核心失败");
            }
        }

        //识别证件
        private void SBZJ(string CardType)
        {
            if (!m_bIsIDCardLoaded)
            {
                //textBoxDisplayResult.Text = "识别核心未加载，请先加载识别核心";
                return;
            }
            //扫描
            int nScanSizeType = int.Parse("2");
            if (nScanSizeType <= 0)
            {
                //textBoxDisplayResult.Text = "无效的扫描尺寸，请重新设置";
                return;
            }

            //get image from device
            int nRet = MyDllIDCard.AcquireImage(nScanSizeType);
            if (nRet != 0)
            {
                //textBoxDisplayResult.Text = "返回值:";
                //textBoxDisplayResult.Text += nRet.ToString();
                //textBoxDisplayResult.Text += "\r\n";
                //textBoxDisplayResult.Text += "采集图像失败";
                MessageBox.Show("采集图像失败");
            }
            //识别
            int nCardType = int.Parse(CardType);
            if (nCardType <= 0)
            {
                //textBoxDisplayResult.Text = "无效的证件类型";
                MessageBox.Show("无效的证件类型");
                return;
            }

            int[] nSubID = new int[1];
            nSubID[0] = 0;
            nRet = MyDllIDCard.SetIDCardID(nCardType, nSubID, 1);
            //若要实现证件的自动分类识别，可使用SetIDCardID和AddIDCardID函数添加证种，以区分识别一代证，驾照，行驶证为例

            //int nCardTypeID=1;
            //nRet = MyDll.SetIDCardID(nCardType, nSubID, 1);/*清空以前证种并添加一代证*/
            //int nCardType=5;
            //nRet = MyDll.SetIDCardID(nCardType, nSubID, 1);/*添加驾照*/
            //nCardType=6;
            //nRet = MyDll.SetIDCardID(nCardType, nSubID, 1);/*添加行驶证*/
            //nRet=MyDll.RecogIDCard();
            nRet = MyDllIDCard.RecogIDCard();
            if (nRet <= 0)
            {
                //textBoxDisplayResult.Text = "返回值:";
                //textBoxDisplayResult.Text += nRet.ToString();
                //textBoxDisplayResult.Text += "\r\n";
                //textBoxDisplayResult.Text += "识别失败";

                return;

            }

            int MAX_CH_NUM = 128;
            char[] cArrFieldValue = new char[MAX_CH_NUM];
            char[] cArrFieldName = new char[MAX_CH_NUM];
            //textBoxDisplayResult.Text = "识别成功\r\n";
            DataTable dt = new DataTable();
            dt.Columns.Add("cArrFieldName", typeof(string));
            dt.Columns.Add("cArrFieldValue", typeof(string));
            for (int i = 1; ; i++)
            {

                nRet = MyDllIDCard.GetRecogResult(i, cArrFieldValue, ref MAX_CH_NUM);
                if (nRet == 3)
                {
                    break;
                }
                MyDllIDCard.GetFieldName(i, cArrFieldName, ref MAX_CH_NUM);
                DataRow dr = dt.NewRow();
                dr[0] = new String(cArrFieldName).Trim();
                dr[1] = new String(cArrFieldValue).Trim();
                //textBoxDisplayResult.Text += new String(cArrFieldName);
                //textBoxDisplayResult.Text += ":";
                //textBoxDisplayResult.Text += new String(cArrFieldValue);
                //textBoxDisplayResult.Text += "\r\n";
                dt.Rows.Add(dr);
            }
            if (dt.Rows.Count > 0)
            {
                CLEARTEXTBOX();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string s = dt.Rows[i][0].ToString().Trim().Replace("\0", "");
                    string ss = dt.Rows[i][1].ToString().Trim().Replace("\0", "");
                    if (s == "姓名")
                    {
                        txtNAME.Text = ss;
                        lbNAME.Text = txtNAME.Text;
                    }
                    else if (s == "性别")
                    {
                        txtSEX.Text = ss;
                        lbSEX.Text = txtSEX.Text;
                    }
                    else if (s == "民族")
                    {
                        txtMZ.Text = ss;
                        lbMZ.Text = txtMZ.Text;
                    }
                    else if (s == "出生")
                    {
                        try
                        {
                            DateTime dtime = Convert.ToDateTime(ss);
                            lbYEAR.Text = dtime.Year.ToString();
                            lbMONTH.Text = dtime.Month.ToString();
                            lbDAY.Text = dtime.Day.ToString();
                            txtBirthDay.Text = lbYEAR.Text + "年" + lbMONTH.Text + "月" + lbDAY.Text + "日";
                        }
                        catch
                        {

                        }
                    }
                    else if (s == "公民身份号码")
                    {
                        txtCARDID.Text = ss;
                        lbIDCARD.Text = txtCARDID.Text;
                    }
                    else if (s == "住址")
                    {
                        txtADDress.Text = ss;
                        lbADD.Text = txtADDress.Text;
                    }
                }
            }
            //保存采集的图像
            string now = DateTime.Now.ToString("yyyyMMddHHmmss");

            String strRunPath = System.Windows.Forms.Application.StartupPath + "\\";
            String strImgPath = strRunPath + "image\\test" + now + ".jpg";
            char[] carrImgPath = strImgPath.ToCharArray();
            nRet = MyDllIDCard.SaveImage(carrImgPath);
            if (nRet != 0)
            {
                MessageBox.Show("保存图像失败！");
                return;
            }
            String strHeadPath = strRunPath + "image\\test_Head" + now + ".jpg";
            char[] carrHeadPath = strHeadPath.ToCharArray();
            MyDllIDCard.SaveHeadImage(carrHeadPath);
            ZJLXRI = Convert.ToInt32(CardType);
            if (ZJLXRI != 0)
            {
                ddlZJLX.SelectedValue = ZJLXRI;
            }
            pbTX.ImageLocation = strHeadPath;
            pictureBox3.ImageLocation = strImgPath;
            pathX = strHeadPath;
            pathD = strImgPath;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SBZJ("2");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SBZJ("5");
        }

        private void AutoClassAndRecognize(object sender, EventArgs e)
        {
            int ID = 0;



            if (!m_bIsIDCardLoaded)
            {
                //  textBoxDisplayResult.Text = "识别核心未加载，请先加载识别核心";
                return;
            }
            int a = MyDllIDCard.GetGrabSignalType();
            if (a == 1)
            {
                //textBoxDisplayResult.Text = "已进入";
                int nRet = MyDllIDCard.AcquireImage(21);
                if (nRet != 0)
                {
                    //textBoxDisplayResult.Text = "返回值:";
                    //textBoxDisplayResult.Text += nRet.ToString();
                    //textBoxDisplayResult.Text += "\r\n";
                    //textBoxDisplayResult.Text += "采集图像失败";
                }

                int[] nSubID = new int[1];
                nSubID[0] = 0;
                MyDllIDCard.AddIDCardID(1, nSubID, 1);
                MyDllIDCard.AddIDCardID(2, nSubID, 1);
                MyDllIDCard.AddIDCardID(3, nSubID, 1);
                MyDllIDCard.AddIDCardID(4, nSubID, 1);
                MyDllIDCard.AddIDCardID(5, nSubID, 1);
                MyDllIDCard.AddIDCardID(6, nSubID, 1);
                MyDllIDCard.AddIDCardID(7, nSubID, 1);
                MyDllIDCard.AddIDCardID(9, nSubID, 1);
                MyDllIDCard.AddIDCardID(10, nSubID, 1);
                MyDllIDCard.AddIDCardID(11, nSubID, 1);
                MyDllIDCard.AddIDCardID(12, nSubID, 1);
                MyDllIDCard.AddIDCardID(13, nSubID, 1);
                MyDllIDCard.AddIDCardID(14, nSubID, 1);
                MyDllIDCard.AddIDCardID(15, nSubID, 1);
                MyDllIDCard.AddIDCardID(16, nSubID, 1);
                MyDllIDCard.AddIDCardID(1000, nSubID, 1);
                MyDllIDCard.AddIDCardID(1001, nSubID, 1);
                MyDllIDCard.AddIDCardID(1003, nSubID, 1);
                MyDllIDCard.AddIDCardID(1004, nSubID, 1);
                MyDllIDCard.AddIDCardID(1005, nSubID, 1);
                MyDllIDCard.AddIDCardID(1107, nSubID, 1);
                MyDllIDCard.AddIDCardID(1008, nSubID, 1);
                MyDllIDCard.AddIDCardID(1009, nSubID, 1);
                MyDllIDCard.AddIDCardID(1010, nSubID, 1);

                nRet = MyDllIDCard.RecogIDCard();
                ID = nRet;
                if (nRet <= 0)
                {
                    //textBoxDisplayResult.Text = "返回值:";
                    //textBoxDisplayResult.Text += nRet.ToString();
                    //textBoxDisplayResult.Text += "\r\n";
                    //textBoxDisplayResult.Text += "识别失败";
                    return;
                }

                int MAX_CH_NUM = 128;
                char[] cArrFieldValue = new char[MAX_CH_NUM];
                char[] cArrFieldName = new char[MAX_CH_NUM];
                //textBoxDisplayResult.Text = "识别成功\r\n";
                //DataTable dt = new DataTable();
                //dt.Columns.Add("cArrFieldName", typeof(string));
                //dt.Columns.Add("cArrFieldValue", typeof(string));
                CLEARTEXTBOX();
                for (int i = 1; ; i++)
                {

                    nRet = MyDllIDCard.GetRecogResult(i, cArrFieldValue, ref MAX_CH_NUM);
                    if (nRet == 3)
                    {
                        break;
                    }
                    MyDllIDCard.GetFieldName(i, cArrFieldName, ref MAX_CH_NUM);
                    //DataRow dr = dt.NewRow();
                    //dr[0] = new String(cArrFieldName).Trim();
                    //dr[1] = new String(cArrFieldValue).Trim();
                    //dt.Rows.Add(dr);
                    if (ID == 5)
                    {
                        string s = new String(cArrFieldName).Replace("\0", "");
                        if (s == "证号")
                        {
                            txtCARDID.Text = new String(cArrFieldValue);
                            if (txtCARDID.Text.Length >= 18)
                            {
                                txtCARDID.Text = txtCARDID.Text.Substring(0, 18);
                            }
                            lbIDCARD.Text = txtCARDID.Text;
                        }
                        else if (s == "姓名")
                        {
                            txtNAME.Text = new String(cArrFieldValue);
                            lbNAME.Text = txtNAME.Text;
                        }
                        else if (s == "性别")
                        {
                            txtSEX.Text = new String(cArrFieldValue);
                            if (txtSEX.Text.Length > 0)
                            {
                                txtSEX.Text = txtSEX.Text.Substring(0, 1);
                            }
                            lbSEX.Text = txtSEX.Text;
                        }
                        else if (s == "住址")
                        {
                            txtADDress.Text = new String(cArrFieldValue);
                            lbADD.Text = txtADDress.Text;
                        }
                        else if (s == "出生日期")
                        {
                            txtBirthDay.Text = new String(cArrFieldValue);
                            try
                            {
                                DateTime dtime = Convert.ToDateTime(txtBirthDay.Text);
                                lbYEAR.Text = dtime.Year.ToString();
                                lbMONTH.Text = dtime.Month.ToString();
                                lbDAY.Text = dtime.Day.ToString();
                                txtBirthDay.Text = lbYEAR.Text + "年" + lbMONTH.Text + "月" + lbDAY.Text + "日";
                            }
                            catch
                            {

                            }
                        }
                    }
                    if (ID == 2)
                    {
                        string s = new String(cArrFieldName).Replace("\0", "");
                        if (s == "姓名")
                        {
                            txtNAME.Text = new String(cArrFieldValue);
                            lbNAME.Text = txtNAME.Text;
                        }
                        else if (s == "性别")
                        {
                            txtSEX.Text = new String(cArrFieldValue);
                            if (txtSEX.Text.Length > 0)
                            {
                                txtSEX.Text = txtSEX.Text.Substring(0, 1);
                            }
                            lbSEX.Text = txtSEX.Text;
                        }
                        else if (s == "民族")
                        {
                            txtMZ.Text = new String(cArrFieldValue);
                            lbMZ.Text = txtMZ.Text;
                        }
                        else if (s == "出生")
                        {
                            txtBirthDay.Text = new String(cArrFieldValue);
                            try
                            {
                                DateTime dtime = Convert.ToDateTime(txtBirthDay.Text.Trim());
                                lbYEAR.Text = dtime.Year.ToString();
                                lbMONTH.Text = dtime.Month.ToString();
                                lbDAY.Text = dtime.Day.ToString();
                                txtBirthDay.Text = lbYEAR.Text + "年" + lbMONTH.Text + "月" + lbDAY.Text + "日";
                            }
                            catch (Exception ex)
                            {
                                getException.Insert_FK_ExceptionINFO(ex.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), txtBirthDay.Text.Trim());

                            }
                        }
                        else if (s == "公民身份号码")
                        {
                            txtCARDID.Text = new String(cArrFieldValue);
                            if (txtCARDID.Text.Length >= 18)
                            {
                                txtCARDID.Text = txtCARDID.Text.Substring(0, 18);
                            }
                            lbIDCARD.Text = txtCARDID.Text;
                        }
                        else if (s == "住址")
                        {
                            txtADDress.Text = new String(cArrFieldValue);
                            lbADD.Text = txtADDress.Text;
                        }
                    }
                }
                //if (dt.Rows.Count > 0 && ID != 5)
                //{
                //    CLEARTEXTBOX();
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        string s = dt.Rows[i][0].ToString().Trim().Replace("\0", "");
                //        string ss = dt.Rows[i][1].ToString().Trim().Replace("\0", "");
                //        if (s == "姓名")
                //        {
                //            txtNAME.Text = ss;
                //            lbNAME.Text = txtNAME.Text;
                //        }
                //        else if (s == "性别")
                //        {
                //            txtSEX.Text = ss;
                //            if (txtSEX.Text.Length > 0)
                //            {
                //                txtSEX.Text = txtSEX.Text.Substring(0, 1);
                //            }
                //            lbSEX.Text = txtSEX.Text;
                //        }
                //        else if (s == "民族")
                //        {
                //            txtMZ.Text = ss;
                //            lbMZ.Text = txtMZ.Text;
                //        }
                //        else if (s == "出生")
                //        {
                //            try
                //            {
                //                DateTime dtime = Convert.ToDateTime(ss);
                //                lbYEAR.Text = dtime.Year.ToString();
                //                lbMONTH.Text = dtime.Month.ToString();
                //                lbDAY.Text = dtime.Day.ToString();
                //                txtBirthDay.Text = lbYEAR.Text + "年" + lbMONTH.Text + "月" + lbDAY.Text + "日";
                //            }
                //            catch
                //            {

                //            }
                //        }
                //        else if (s == "公民身份号码")
                //        {
                //            txtCARDID.Text = ss;
                //            if (txtCARDID.Text.Length >= 18)
                //            {
                //                txtCARDID.Text = txtCARDID.Text.Substring(0, 18);
                //            }
                //            lbIDCARD.Text = txtCARDID.Text;
                //        }
                //        else if (s == "住址")
                //        {
                //            txtADDress.Text = ss;
                //            lbADD.Text = txtADDress.Text;
                //        }
                //    }
                //}
                //保存采集的图像
                string now = DateTime.Now.ToString("yyyyMMddHHmmss");

                String strRunPath = System.Windows.Forms.Application.StartupPath + "\\";
                string timeNOW = "";
                String strImgPath = strRunPath + "/TX/test" + now + ".jpg";
                char[] carrImgPath = strImgPath.ToCharArray();
                nRet = MyDllIDCard.SaveImage(carrImgPath);
                if (nRet != 0)
                {
                    MessageBox.Show("保存图像失败！");
                    return;
                }

                //如果需要在识别后可以保存头像

                String strHeadPath = strRunPath + "/TX/test_Head" + now + ".jpg";
                char[] carrHeadPath = strHeadPath.ToCharArray();
                MyDllIDCard.SaveHeadImage(carrHeadPath);
                string path = Application.StartupPath;
                pbTX.ImageLocation = strHeadPath;
                pictureBox3.ImageLocation = strImgPath;
                pathX = strHeadPath;
                pathD = strImgPath;
                ZJLXRI = ID;
                if (ZJLXRI != 0)
                {
                    ddlZJLX.SelectedValue = ZJLXRI;
                }
            }
            //get image from device
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //try
            //{
            DataTable dt = mainService.Select_FK_BL("ISTBRY");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["BL"].ToString().ToUpper() == "TRUE")
                {
                    MessageBox.Show("正在更新数据库，请稍等！");
                    return;
                }
            }
            else
            {
                MessageBox.Show("查询变量出错，请联系管理员！");
                return;
            }
            BFRY2 bfry = new BFRY2();
            DialogResult dr = bfry.ShowDialog();
            if (dr == DialogResult.OK)
            {
                txtSFBM.Text = bfry.DEPT + "-" + bfry.GSMC;
                ddlBFXM.Text = bfry.BFNAME;
                txtBFRGH.Text = bfry.GH;
            }
            //}
            //catch (Exception ex)
            //{
            //    getException.Insert_FK_ExceptionINFO(ex.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "");
            //    MessageBox.Show("被访人员弹出框出错！");
            //}
        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {

        }


        private void TIME(object sender, EventArgs e)
        {
            try
            {
                string timestr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                toolLBTIME.Text = timestr;
            }
            catch
            {
                MessageBox.Show("时间出错");
            }
        }

        private void btnPRINT_Click(object sender, EventArgs e)
        {
            if (txtNAME.Text.Trim() == "")
            {
                MessageBox.Show("访客姓名不能为空");
                return;
            }
            if (txtCARDID.Text.Trim() == "")
            {
                MessageBox.Show("证件号码不能为空");
                return;
            }
            try
            {
                MODLEFKINFO modleFKINF = new MODLEFKINFO();

                DateTime dtime = DateTime.Now;
                string time = dtime.ToString("yyyy-MM-dd");
                string time1 = dtime.ToString("yyyyMMdd");
                DataTable dt = mainService.Select_FK_sysinfo(modlemw.GSDMRI, time);
                if (dt.Rows.Count > 0)
                {
                    modleFKINF.TM = modlemw.GSDMRI + time1.Substring(2, 6) + (Convert.ToInt32(dt.Rows[0]["NUM"].ToString()) + 1).ToString("0000");
                    mainService.UPDATE_FK_sysinfo(modlemw.GSDMRI, time, Convert.ToInt32(dt.Rows[0]["NUM"].ToString()) + 1);
                }
                else
                {
                    modleFKINF.TM = modlemw.GSDMRI + time1.Substring(2, 6) + "0001";
                    mainService.INSERT_FK_sysinfo(modlemw.GSDMRI, time, 1);
                }
                modleFKINF.FKNO = modleFKINF.TM;
                modleFKINF.FKNAME = txtNAME.Text;
                modleFKINF.FKSEX = txtSEX.Text;
                modleFKINF.FKMZ = txtMZ.Text;
                modleFKINF.FKCSRQ = txtBirthDay.Text;
                modleFKINF.FKZJLXRI = ddlZJLX.SelectedValue.ToString();
                modleFKINF.FKZJLX = ddlZJLX.Text;
                modleFKINF.FKZJHM = txtCARDID.Text;
                modleFKINF.FKADD = txtADDress.Text;
                modleFKINF.FKDW = txtDW.Text;
                modleFKINF.FKCPH = ddlCPSF.Text + " " + txtCP.Text.Trim();
                modleFKINF.FKZH = txtFKZH.Text;
                modleFKINF.FKZLX = "";
                modleFKINF.FKZLXRI = "0";
                modleFKINF.XDH = txtXDH.Text;
                modleFKINF.FXH = txtFXH.Text;
                modleFKINF.SFBM = txtSFBM.Text;
                modleFKINF.SFNAME = ddlBFXM.Text;
                modleFKINF.SFGH = txtBFRGH.Text;
                modleFKINF.SFRYDH = "";
                if (rbSY1.Checked == true)
                {
                    modleFKINF.FKLFSY = rbSY1.Text;
                }
                else if (rbSY2.Checked == true)
                {
                    modleFKINF.FKLFSY = rbSY2.Text;
                }
                else if (rbSY3.Checked == true)
                {
                    modleFKINF.FKLFSY = rbSY3.Text;
                }
                modleFKINF.FKLFSYMX = txtFWSYSY.Text;
                modleFKINF.FKSUM = txtFWSYRS.Text;
                if (rbSSWP1.Checked == true)
                {
                    modleFKINF.FKISHAVESSWP = "TRUE";
                }
                else if (rbSSWP2.Checked == true)
                {
                    modleFKINF.FKISHAVESSWP = "FALSE";
                }
                modleFKINF.FKSSWP = txtXDWP.Text;
                modleFKINF.BZ = txtBZ.Text;
                modleFKINF.JRMWRI = modlemw.RI;
                modleFKINF.JRMW = modlemw.MWMS;
                modleFKINF.LFTIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                modleFKINF.ISLK = "FALSE";
                modleFKINF.GSDMRI = modlemw.GSDMRI;
                modleFKINF.GSDM = modlemw.GSDM;
                modleFKINF.GSDMMS = modlemw.GSDMMS;
                modleFKINF.JRDJR = modleSTAFF.STAFFNAME;
                modleFKINF.JRDJRRI = modleSTAFF.STAFFID;
                modleFKINF.ISDELETE = "FALSE";
                modleFKINF.FRI = "0";
                modleFKINF.LFRQ = time;
                mainService.INSERT_FK_FKINFO(modleFKINF);

                if (pathX != "" && pathD != "")
                {
                    FileStream fs = new FileStream(pathX, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    byte[] imgBytesIn = br.ReadBytes(Convert.ToInt32(fs.Length));

                    FileStream fs1 = new FileStream(pathD, FileMode.Open, FileAccess.Read);
                    BinaryReader br1 = new BinaryReader(fs1);
                    byte[] imgBytesIn1 = br1.ReadBytes(Convert.ToInt32(fs1.Length));

                    mainService.Insert_FK_IMAGE(modleFKINF.FKNO, imgBytesIn, imgBytesIn1);
                }
                else if (pathX != "" && pathD == "")
                {
                    FileStream fs = new FileStream(pathX, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    byte[] imgBytesIn = br.ReadBytes(Convert.ToInt32(fs.Length));

                    mainService.Insert_FK_IMAGE(modleFKINF.FKNO, imgBytesIn);
                }

                int W = 200;
                int H = 60;
                BarcodeLib.TYPE type = BarcodeLib.TYPE.UNSPECIFIED;
                type = BarcodeLib.TYPE.CODE128;
                b.IncludeLabel = false;
                Image TM = b.Encode(type, modleFKINF.TM, Color.Black, Color.White, W, H);
                new tablePrint().print(modleFKINF, pbTX.Image, TM);//这是浏览函数，需要打印请调用print();
                BANDdgvZFRY();
            }
            catch (Exception ex)
            {
                getException.Insert_FK_ExceptionINFO(ex.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "");
                MessageBox.Show("打印保存出错");
            }
            txtNAME.Focus();
        }

        private void bandddlZJLX()
        {
            DataTable dt = mainService.Select_FK_ZJLX();
            DataTable dtCopy = dt.Copy();
            DataView dv = dt.DefaultView;
            dv.Sort = "RI";
            dtCopy = dv.ToTable();
            ddlZJLX.SelectedIndexChanged -= ddlZJLX_SelectedIndexChanged;
            ddlZJLX.DataSource = dtCopy;
            ddlZJLX.ValueMember = "RI";
            ddlZJLX.DisplayMember = "ZJLX";
            ddlZJLX.SelectedIndexChanged += ddlZJLX_SelectedIndexChanged;
        }

        private void main_FormClosed(object sender, FormClosedEventArgs e)
        {
            //System.Environment.Exit(0);
            //储存选择的机器值
            MyDllsdtapi.WritePrivateProfileString(strSec, "ddlmachine", SelectMachine.SelectedValue.ToString(), strFilePath);


            Application.Exit();
        }

        private void CLEARTEXTBOX()
        {
            txtNAME.Text = "";
            txtSEX.Text = "";
            txtMZ.Text = "";
            txtBirthDay.Text = "";
            ddlZJLX.SelectedValue = "1";
            txtCARDID.Text = "";
            txtADDress.Text = "";
            txtDW.Text = "";
            ddlCPSF.SelectedIndex = 0;
            txtCP.Text = "";
            txtFKZH.Text = "";
            txtXDH.Text = "";
            txtFXH.Text = "";

            pbTX.Image = null;
            pathD = "";
            pathX = "";
            lbNAME.Text = "";
            lbSEX.Text = "";
            lbMZ.Text = "";
            lbADD.Text = "";
            lbYEAR.Text = "";
            lbMONTH.Text = "";
            lbDAY.Text = "";
            lbIDCARD.Text = "";

            txtSFBM.Text = "";
            txtBFRGH.Text = "";
            ddlBFXM.Text = "";
        }

        private void btnLK_Click(object sender, EventArgs e)
        {
            try
            {
                FKLK fklk = new FKLK(modlemw, modleSTAFF);
                DialogResult dr = fklk.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    MessageBox.Show(fklk.MEG);
                }
                BANDdgvZFRY();
            }
            catch (Exception ex)
            {
                getException.Insert_FK_ExceptionINFO(ex.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "");
                MessageBox.Show("离开界面出错");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //System.Environment.Exit(0);
            Application.Exit();
        }

        private void BANDdgvZFRY()
        {
            DataTable dt = mainService.Select_FK_FKINFO_ZFRY(modlemw.GSDMRI, ddlZFRYMW.SelectedValue.ToString());
            dgvZFRY.DataSource = dt;
        }

        private void btnCSH_Click(object sender, EventArgs e)
        {
            CSH();
            LoadKernal();
        }

        private void btnZFRYSX_Click(object sender, EventArgs e)
        {
            BANDdgvZFRY();
        }

        private void btnZFRYQRLK_Click(object sender, EventArgs e)
        {
            DateTime dtime = new DateTime();
            string ZFRYRI = dgvZFRY.CurrentRow.Cells[0].Value.ToString();
            string LKTIME = "";
            try
            {
                LKTIME = dgvZFRY.CurrentRow.Cells[2].Value.ToString();
            }
            catch
            {

            }
            if (LKTIME == "")
            {
                dtime = DateTime.Now;
            }
            else
            {
                try
                {
                    dtime = Convert.ToDateTime(LKTIME);
                }
                catch (Exception ex)
                {
                    getException.Insert_FK_ExceptionINFO(ex.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "");
                    MessageBox.Show("离开时间输入格式错误应该为”yyyy-MM-dd HH:mm:ss“");
                    return;
                }
            }
            mainService.Update_FK_FKINFO(modlemw.RI, modlemw.MWMS, dtime.ToString("yyyy-MM-dd HH:mm:ss"), modleSTAFF.STAFFNAME, modleSTAFF.STAFFID, ZFRYRI);
            MessageBox.Show("访客 " + dgvZFRY.CurrentRow.Cells[3].Value.ToString() + " 离开！");
            BANDdgvZFRY();
        }

        private void btnZFRYSCJL_Click(object sender, EventArgs e)
        {
            string ZFRYRI = dgvZFRY.CurrentRow.Cells[0].Value.ToString();
            mainService.DELETE_FK_FKINFO(ZFRYRI);
            MessageBox.Show("记录删除！");
            BANDdgvZFRY();
        }

        private void btnFKDJ_Click(object sender, EventArgs e)
        {
            CLEARTEXTBOX();
            txtNAME.Focus();
        }

        private void btnCXTJ_Click(object sender, EventArgs e)
        {
            DataTable dt = mainService.Select_FK_BL("BBCX");
            string url = "";
            if (dt.Rows.Count > 0)
            {
                url = dt.Rows[0]["BL"].ToString();
            }
            if (url != "")
            {
                System.Diagnostics.Process.Start(url + "?ID=" + modleSTAFF.STAFFID + "");
            }
        }

        private void btnCLEAR_Click(object sender, EventArgs e)
        {
            CLEARTEXTBOX();
        }

        private void btnXGMM_Click(object sender, EventArgs e)
        {
            ChangePassword changePasswpord = new ChangePassword(modleSTAFF);
            DialogResult dr = changePasswpord.ShowDialog();
            if (dr == DialogResult.OK)
            {
                MessageBox.Show("密码修改成功");
            }
        }

        private void txtCARDID_TextChanged(object sender, EventArgs e)
        {
            if (txtCARDID.Text.Trim() != "")
            {
                DataTable dt = mainService.Select_FK_FKINFO_FKZJHM(txtCARDID.Text);
                if (dt.Rows.Count > 0)
                {
                    txtDW.Text = dt.Rows[0]["FKDW"].ToString();
                    if (dt.Rows[0]["FKCPH"].ToString().Trim() != "")
                    {
                        ddlCPSF.Text = dt.Rows[0]["FKCPH"].ToString().Trim().Substring(0, 1);
                        if (dt.Rows[0]["FKCPH"].ToString().Trim().Length > 1)
                        {
                            txtCP.Text = dt.Rows[0]["FKCPH"].ToString().Trim().Substring(1, dt.Rows[0]["FKCPH"].ToString().Trim().Length - 1);
                        }
                    }
                    txtSFBM.Text = dt.Rows[0]["SFBM"].ToString();
                    ddlBFXM.Text = dt.Rows[0]["SFNAME"].ToString();
                    txtBFRGH.Text = dt.Rows[0]["SFGH"].ToString();
                }
            }
        }

        private void BANDddlZFRYMW()
        {
            DataTable dt = mainService.Select_FK_MWINFO_GSDMRI(modlemw.GSDMRI);
            DataRow dr = dt.NewRow();
            dr["RI"] = "0";
            dr["MWMS"] = "=请选择=";
            dt.Rows.Add(dr);
            DataTable dtCopy = dt.Copy();
            DataView dv = dt.DefaultView;
            dv.Sort = "RI";
            dtCopy = dv.ToTable();
            ddlZFRYMW.DataSource = dtCopy;
            ddlZFRYMW.ValueMember = "RI";
            ddlZFRYMW.DisplayMember = "MWMS";
        }

        private void ddlZFRYMW_SelectedValueChanged(object sender, EventArgs e)
        {
            BANDdgvZFRY();
        }


        private void BANDddlBFXM()
        {
            DataTable dt = mainService.Select_FK_LXRINFO_BFNAME();
            //   DataTable dt = mainService.Select_FK_LXRINFO("", ddlBFXM.Text.ToString(), "");
            dt.Columns.Add("NAME1", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["NAME1"] = dt.Rows[i]["BFNAME"].ToString() + "-" + dt.Rows[i]["DEPT"].ToString();
                }
            }
            DataRow dr = dt.NewRow();
            dr["RI"] = "0";
            dr["NAME1"] = "";
            dt.Rows.Add(dr);
            DataTable dtCopy = dt.Copy();
            DataView dv = dt.DefaultView;
            dv.Sort = "NAME1";
            dtCopy = dv.ToTable();
            ddlBFXM.SelectedIndexChanged -= ddlBFXM_SelectedIndexChanged;
            ddlBFXM.DataSource = dtCopy;
            ddlBFXM.ValueMember = "RI";
            ddlBFXM.DisplayMember = "NAME1";
            ddlBFXM.SelectedIndexChanged += ddlBFXM_SelectedIndexChanged;
        }

        private void ddlBFXM_SelectedIndexChanged(object sender, EventArgs e)
        {
            string RI = ddlBFXM.SelectedValue.ToString();
            DataTable dt = mainService.Select_FK_LXRINFO_RI(RI);
            if (dt.Rows.Count > 0)
            {
                txtSFBM.Text = dt.Rows[0]["DEPT"].ToString() + "-" + dt.Rows[0]["GSMC"].ToString();
                txtBFRGH.Text = dt.Rows[0]["GH"].ToString();
            }
        }

        private void ddlZJLX_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlZJLX.SelectedValue == null || ddlZJLX.SelectedValue.ToString() == "7")
            {
                txtNAME.Text = "";
                txtSEX.Text = "";
                txtMZ.Text = "";
                txtBirthDay.Text = "";
                txtCARDID.Text = "";
                txtADDress.Text = "";
                txtDW.Text = "";
                ddlCPSF.SelectedIndex = 0;
                txtCP.Text = "";
                txtFKZH.Text = "";
                txtXDH.Text = "";
                txtFXH.Text = "";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string timestr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                toolLBTIME.Text = timestr;
            }
            catch (Exception ex)
            {
                getException.Insert_FK_ExceptionINFO(ex.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "");
                MessageBox.Show("时间出错");
            }
        }

        private void btnCLSJ_Click(object sender, EventArgs e)
        {
            DataTable dt = mainService.Select_FK_INFO_ISLK();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DateTime dtime = Convert.ToDateTime(dt.Rows[i]["LFTIME"].ToString()).AddHours(2);
                mainService.Update_FK_FKINFO(dt.Rows[i]["JRMWRI"].ToString(), dt.Rows[i]["JRMW"].ToString(), dtime.ToString("yyyy-MM-dd HH:mm:ss"), dt.Rows[i]["JRDJR"].ToString(), dt.Rows[i]["JRDJRRI"].ToString(), dt.Rows[i]["RI"].ToString());
            }
        }

        private void ddlJQ_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void ReadCardByNewMachine()
        {
            int FindCard = NewMachine.Authenticate();
            if (FindCard != 1)
            {
                CLEARTEXTBOX();
                return;
            }
            //读卡
            int rs = NewMachine.Read_Content(1);
            if (rs != 1 && rs != 2 && rs != 3)
            {
                CLEARTEXTBOX();
                return;
            }
        }
        public void NewMachineConnnect()
        {
            int AutoSearchReader = NewMachine.InitCommExt();
            if (AutoSearchReader > 0)
            {
                Port = AutoSearchReader;
                IsConnected = true;
            }
            else
            {
                MessageBox.Show("设备连接异常，请联系管理员");
            }
        }
        private void BANDddlJQ(int ISMachine)
        {
            DataTable dtSource = new DataTable();
            dtSource.Columns.Add("id");
            dtSource.Columns.Add("value");
            //    dtSource.Rows.Add(0, "请选择");
            dtSource.Rows.Add(1, "中控读卡器");
            dtSource.Rows.Add(2, "文通读卡器");

            SelectMachine.DataSource = dtSource;
            SelectMachine.ValueMember = "id";
            SelectMachine.DisplayMember = "value";
            SelectMachine.SelectedValue = ISMachine;
        }

        private void SelectMachine_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }
            e.DrawBackground();
            e.DrawFocusRectangle();
            StringFormat strFmt = new System.Drawing.StringFormat();

            Array allColors = Enum.GetValues(typeof(KnownColor));

            strFmt.Alignment = StringAlignment.Near; //文本水平居中

            strFmt.LineAlignment = StringAlignment.Center; //文本垂直居中
            e.Graphics.DrawString(SelectMachine.GetItemText(SelectMachine.Items[e.Index]).ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds, strFmt);
        }

        private void SelectMachine_SelectedIndexChanged(object sender, EventArgs e)
        {
            ISMachine = Convert.ToInt32(((DataRowView)SelectMachine.SelectedItem).Row["id"].ToString());
            if (ISMachine == 1)
            {
             //   NewMachineConnnect();
            }
            if (ISMachine == 2)
            {
                CSH();
                LoadKernal();
            }
        }


        private void DistinguishMachine()
        {
            //  int AutoSearchReader = NewMachine.InitCommExt();
            if (MyDllsdtapi.LoadLibrary("sdtapi.dll") > 0)
            {
                ISMachine = 2;
            }
            else
            {
                ISMachine = 1;
            }
            SelectMachine.SelectedValue = ISMachine;
        }
    }
}
