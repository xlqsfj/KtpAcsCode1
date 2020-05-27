using KtpAcs.Infrastructure.Exceptions;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService;
using KtpAcs.KtpApiService.Model;
using KtpAcs.KtpApiService.Send;
using KtpAcs.KtpApiService.Worker;
using KtpAcs.PanelApi.Haiqing;
using KtpAcs.WinForm.Jijian.Base;
using KtpAcs.WinForm.Jijian.Device;
using KtpAcs.WinForm.Jijian.Workers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static KtpAcs.KtpApiService.Result.BankCardCheckResult;
using static KtpAcs.KtpApiService.Result.TeamListResult;
using static KtpAcs.WinForm.Jijian.Workers.WorkerAddStateForm;

namespace KtpAcs.WinForm.Jijian
{
    public partial class AddWorker : DevExpress.XtraEditors.XtraForm
    {
        public event Action<DevExpress.XtraEditors.XtraForm, bool, string> CloseDdetailedWinform;
        //端口号
        private int _synIdCardPort;
        private readonly string _msgCaption = "提示:";
        private bool _isColse = false;
        private string _facePicId;
        private string _identityBackPicId;
        //头像
        private string _upic;
        private string _url_upic;
        private string _identityPicId;
        private string _url_facePicId;
        private string _url_identityBackPicId;

        private string _url_identityPicId;
        private string _uuId;

        private AddWorerkSend add;

        private int _state = 0;
        private bool _isManualEdit = false;

        public AddWorker(int hmc = 0, int openState = 0)
        {
            _state = hmc;
            InitializeComponent();
            //
            CameraConn();
            BindNationsCb();
            BindEducationLeveCb();
            //银行
            //  BindBankCardCb();
            ContentState(_state);
            // 查询工种
            GetProjectList();
            // 劳务公司
            GetOrganizationUuidList();

        }
        protected override Point ScrollToControl(Control activeControl)
        {
            return this.AutoScrollPosition;
        }
        /// <summary>
        /// 编辑，或查看
        /// </summary>
        /// <param name="uuId"></param>
        /// <param name="hmc"></param>
        /// <param name="isEdit"></param>
        public AddWorker(string uuId, int hmc = 0, bool isEdit = false)
        {
            _state = hmc;
            _uuId = uuId;
            InitializeComponent();
            // BindNationsCb();
            BindEducationLeveCb();
            //查询工种
            GetProjectList();
            //劳务公司
            GetOrganizationUuidList();
            //控件状态
            ContentState(_state);
            //加载详细页
            GetInfo(hmc, uuId);
            SetIsEdit(isEdit);
        }


        public void CameraConn()
        {
            try
            {
                AForgeVidePlayerHelper.CameraConn(AVidePlayer);
            }
            catch (NotFoundException nfException)
            {
                f1.Visible = true;
                f2.Visible = true;
                MessageHelper.Show(nfException.Message);
            }
        }

        private void AddWorker_Load(object sender, EventArgs e)
        {

        }



        /// <summary>
        /// 身份证识别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadIC_Click(object sender, EventArgs e)
        {
            try
            {

                /***初始化控件************************************************************/
                if (IdentityHeadPic.Image != null)
                {
                    IdentityHeadPic.Image.Dispose();
                    IdentityHeadPic.Image = null;
                }
                /***阅读器设置************************************************************/
                if (_synIdCardPort <= 0)
                {
                    //查找读卡器，获取端口
                    _synIdCardPort = SynIdCardApi.Syn_FindUSBReader();
                }
                var nPort = _synIdCardPort;
                if (nPort == 0)
                {
                    MessageHelper.Show(
                        $@"{Convert.ToString(DateTime.Now, CultureInfo.InvariantCulture)} 没有找到读卡器", _msgCaption);
                    return;
                }
                string stmp;
                var pucIin = new byte[4];
                var pucSn = new byte[8];
                //byte[] cPath = new byte[255];
                //图片保存路径
                var cPath = Encoding.UTF8.GetBytes(ConfigHelper.CustomFilesDir);
                // var cPath = Encoding.UTF8.GetBytes(System.Windows.Forms.Application.StartupPath);
                SynIdCardApi.Syn_SetPhotoPath(2, ref cPath[0]); //设置照片路径，iOption 路径选项：0=C:，1=当前路径，2=指定路径
                                                                //cPhotoPath	绝对路径,仅在iOption=2时有效
                SynIdCardApi.Syn_SetPhotoType(1); //0 = bmp ,1 = jpg , 2 = base64 , 3 = WLT ,4 = 不生成
                SynIdCardApi.Syn_SetPhotoName(2); // 生成照片文件名 0=tmp 1=姓名 2=身份证号 3=姓名_身份证号 
                SynIdCardApi.Syn_SetSexType(1); // 0=卡中存储的数据	1=解释之后的数据,男、女、未知
                SynIdCardApi.Syn_SetNationType(0); // 0=卡中存储的数据	1=解释之后的数据 2=解释之后加"族"
                SynIdCardApi.Syn_SetBornType(3); // 0=YYYYMMDD,1=YYYY年MM月DD日,2=YYYY.MM.DD,3=YYYY-MM-DD,4=YYYY/MM/DD
                SynIdCardApi.Syn_SetUserLifeBType(3); // 0=YYYYMMDD,1=YYYY年MM月DD日,2=YYYY.MM.DD,3=YYYY-MM-DD,4=YYYY/MM/DD
                                                      // 0=YYYYMMDD(不转换),1=YYYY年MM月DD日,2=YYYY.MM.DD,3=YYYY-MM-DD,4=YYYY/MM/DD,0=长期 不转换,	1=长期转换为 有效期开始+50年
                                                      //string imgMsg = new string(' ', 1024); //身份证图片信息返回长度为1024
                                                      //IntPtr img = Marshal.StringToHGlobalAnsi(imgMsg); //身份证图片
                SynIdCardApi.Syn_SetUserLifeEType(3, 1);
                /***打开读取信息************************************************************/
                if (SynIdCardApi.Syn_OpenPort(nPort) == 0)
                {
                    if (SynIdCardApi.Syn_SetMaxRFByte(nPort, 80, 0) == 0)
                    {
                        var cardMsg = new SynIdCardDto();
                        SynIdCardApi.Syn_StartFindIDCard(nPort, ref pucIin[0], 0);
                        SynIdCardApi.Syn_SelectIDCard(nPort, ref pucSn[0], 0);
                        var readMsgResult = SynIdCardApi.Syn_ReadMsg(nPort, 0, ref cardMsg);
                        if (readMsgResult == 0 || readMsgResult == 1)
                        {
                            try
                            {
                                if (cardMsg.Sex == "女")
                                {
                                    txtGender.SelectedIndex = 1;
                                }
                                else
                                {
                                    txtGender.SelectedIndex = 0;
                                }
                                txtName.Text = cardMsg.Name.Trim();
                                txtBirthday.EditValue = DateTime.Parse(cardMsg.Born);
                                //txtAvg.Text = FormatHelper.GetAgeByBirthdate(DateTime.Parse(cardMsg.Born)).ToString();
                                txtIdCard.Text = cardMsg.IDCardNo.Trim();

                                txtNativePlace.Text = WorkerInfoHelper.GetProvinceDicList(cardMsg.IDCardNo.Trim());
                                txtAddress.Text = cardMsg.Address.Trim();
                                ComNation.EditValue = (int.Parse(cardMsg.Nation));
                                txtStartTime.Text = cardMsg.UserLifeBegin.Trim();
                                txtExpireTime.Text = cardMsg.UserLifeEnd.Trim();
                                txtCardAgency.Text = cardMsg.GrantDept.Trim();
                                if (!string.IsNullOrEmpty(cardMsg.PhotoFileName))
                                {
                                    //IdentityHeadPic.Image = Image.FromFile(cardMsg.PhotoFileName);
                                    _upic = $"{cardMsg.IDCardNo}.Jpg";
                                    System.IO.FileStream fs = System.IO.File.OpenRead($"{ConfigHelper.CustomFilesDir}{_upic}");
                                    IdentityHeadPic.Image = Image.FromStream(fs);
                                    fs.Close();

                                }

                            }
                            catch (Exception ex)
                            {
                                LogHelper.ExceptionLog(ex);
                                MessageHelper.Show($@"读取身份证出现错误：{ex.Message}", _msgCaption);
                            }
                        }
                        else
                        {
                            stmp = $"{FormatHelper.GetIsoDateTimeString(DateTime.Now)} 读取身份证信息错误,确认身份证放置位置，如放置正确则身份证可能损坏";
                            MessageHelper.Show(stmp, _msgCaption);
                        }
                    }
                }
                else
                {
                    stmp = $"{FormatHelper.GetIsoDateTimeString(DateTime.Now)} 打开端口失败,确认身份证阅读器是否正常连接";
                    MessageHelper.Show(stmp, _msgCaption);
                }
                if (_isManualEdit)
                {
                    ContentState(_state);
                    _isManualEdit = false;
                }
            }
            catch (Exception ex)
            {
                MessageHelper.Show(ex.Message, ex);
            }
        }

        private void btnFacePic_Click(object sender, EventArgs e)
        {
            if (f1.Visible)
            {
                MessageBox.Show("未连接摄像头,不能采集");
                return;
            }
            _facePicId = GetPic(pic_facePic);
        }

        private void btnPicturePositive_Click(object sender, EventArgs e)
        {
            if (f1.Visible)
            {
                MessageBox.Show("未连接摄像头,不能采集");
                return;
            }
            _identityPicId = GetPic(pic_picturePositive);
        }

        private void btnPictureReverse_Click(object sender, EventArgs e)
        {
            if (f1.Visible)
            {
                MessageBox.Show("未连接摄像头,不能采集");
                return;
            }
            _identityBackPicId = GetPic(pic_pictureReverse);
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            add = null;
            ConfigHelper.IsDivceAdd = true;
            string submit = btnSubmit.Text;
            btnSubmit.Text = @"正在提交";
            btnSubmit.Enabled = false;
            try
            {

                if (!SubmitBtnPreValidation())
                {
                    btnSubmit.Text = @"提交";
                    btnSubmit.Enabled = true;

                    throw new PreValidationException(PreValidationHelper.ErroMsg);
                }
                if (this.txtStartTime.Text == "yyyy-mm-dd")
                {
                    MessageHelper.Show("身份证有效开始日期未填写");
                    return;
                }
                if (WorkSysFail.workAdd.Count() < 1)
                {
                    MessageHelper.Show("未添加面板，请先添加面板");
                    btnSubmit.Text = @"提交";
                    btnSubmit.Enabled = true;
                    return;
                }
                ShowAddInfoForm();

                add = new AddWorerkSend();
                add.address = this.txtAddress.Text;
                add.age = Convert.ToInt32(this.txtAvg.Text);
                add.bankName = this.txtBankName.Text;
                add.bankNo = this.txtBankNo.Text;
                add.birthday = this.txtBirthday.Text;
                add.educationLevel = (int)this.ComEducationLevel.EditValue;
                add.emergencyContactName = this.txtEmergencyContactName.Text;
                add.emergencyContactPhone = this.txtEmergencyContactPhone.Text;
                add.gender = this.txtGender.SelectedIndex == 0 ? 1 : 2;
                add.idCard = this.txtIdCard.Text;
                add.cardAgency = this.txtCardAgency.Text;
                add.name = this.txtName.Text;
                add.nation = this.ComNation.Text;
                add.nativePlace = this.txtNativePlace.Text;
                add.startTime = this.txtStartTime.Text;
                add.expireTime = this.txtExpireTime.Text;
                add.phone = this.txtPhone.Text;
                add.projectUuid = ConfigHelper.KtpLoginProjectId;


                if (!string.IsNullOrEmpty(_facePicId))
                {
                    add.localImgFileName = _facePicId;
                }
                else if (!string.IsNullOrEmpty(_url_facePicId))
                {
                    add.facePic = _url_facePicId;
                }
                if (!string.IsNullOrEmpty(_identityPicId))
                {
                    add.localImgFileName1 = _identityPicId;
                }
                if (!string.IsNullOrEmpty(_upic))
                {//本地头像
                    add.localImgUpic = _upic;
                }
                if (!string.IsNullOrEmpty(_identityBackPicId))
                {//头像背面
                    add.localImgFileName2 = _identityBackPicId;
                }
                else if (!string.IsNullOrEmpty(_url_identityPicId))
                {
                    add.pictureReverse = _url_identityPicId;
                }

                else if (!string.IsNullOrEmpty(_url_identityBackPicId))
                {
                    add.picturePositive = _url_identityBackPicId;
                }

                else if (!string.IsNullOrEmpty(_url_upic))
                {
                    add.icon = _url_upic;
                }
                WorkSysFail.dicAddMag.Clear();
                WorkSysFail.dicWorkadd.Clear();
                SubmitData();

            }
            catch (PreValidationException ex)
            {

                MessageHelper.Show(ex.Message);
                btnSubmit.Text = submit;
                btnSubmit.Enabled = true;
            }
            catch (Exception ex)
            {


                WorkSysFail.dicWorkadd.Add(false, ex.Message);
                MessageHelper.Show(ex.Message);

                btnSubmit.Text = submit;
                btnSubmit.Enabled = true;
            }
        }

        private void SubmitData()
        {
            Task.Run(() =>
            {
                try
                {

                    if (WorkSysFail.workAdd.Count() > 0)
                    {
                        LogHelper.Info("addUser(add)调用");
                        int? uid = 0;

                        if (_state == 0)
                            uid = addUser(add);
                        else if (_state == 1)
                            uid = addJiaZiUser(add);
                        else
                            uid = addProject(add);
                        if (uid > 0)
                        {

                            WorkSysFail.dicWorkadd.Add(true, "添加成功");
                            AddWorkerToHqPanel addFaceToPanel = new AddWorkerToHqPanel();
                            addFaceToPanel.AddFaceInfo(add, uid);
                        }
                        btnSubmit.Enabled = true;
                    }
                    else
                    {
                        ConfigHelper.IsDivceAdd = false;
                        WorkSysFail.dicWorkadd.Add(false, "添加失败，未连接人脸识别面板!");
                    }
                }
                catch (Exception ex)
                {
                    WorkSysFail.dicWorkadd.Add(false, ex.Message);
                    LogHelper.ExceptionLog(ex);
                }

            });
            //return;
            //   }
        }

        public void ShowAddInfoForm()
        {


            this.BeginInvoke((EventHandler)delegate
            {
                WorkerAddStateForm _workerAddState = new WorkerAddStateForm(txtName.Text.Trim(), txtIdCard.Text.Trim());
                _workerAddState.StartPosition = FormStartPosition.CenterParent;
                _workerAddState.ShowSubmit += new AgainSubmit(AddSubWorkInfo);

                _workerAddState.ShowDialog();
            });


        }
        /// <summary>
        //提交接口信息
        /// </summary>
        public void AddSubWorkInfo(string close)
        {
            try
            {
                LogHelper.Info("AddSubWorkInfo");
                //if (close == "begin")
                //{
                //    SubmitData();
                //    return;
                //}

                btnSubmit.Text = @"提交";
                btnSubmit.Enabled = true;
                if (close == "close")
                {//新增

                    reslt(_state);
                    if (_isManualEdit)
                    {
                        ContentState(_state);
                        _isManualEdit = false;
                    }
                    if (ShowProjectList != null)
                        ShowProjectList("ok");
                    if (CloseDdetailedWinform != null)
                        CloseDdetailedWinform(null, false, "");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageHelper.Show(ex.Message, ex);
            }



        }

        private void ComOrganizationUuid_EditValueChanged(object sender, EventArgs e)
        {
            var uuid = ComOrganizationUuid.EditValue;
            GetTeamInfo(uuid);
        }



        public void GetIsAVide()
        {
            try
            {
                if (AVidePlayer.IsRunning)
                {
                    AVidePlayer.SignalToStop();
                    AVidePlayer.Stop();
                }
            }
            catch (Exception)
            {

                throw;
            }


        }
        private void txtBankNo_Leave(object sender, EventArgs e)
        {
            string name = this.txtName.Text;
            string idCard = this.txtIdCard.Text;
            string Bankno = this.txtBankNo.Text;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(idCard))
            {

                MessageHelper.Show("姓名跟身份证不能为空");
                return;
            }
            IMulePusher pusherLogin = new GeBankCardCheckApi() { RequestParam = new { bankNo = Bankno, name = name, idCard = idCard } };
            PushSummary pushLogin = pusherLogin.Push();
            if (pushLogin.Success)
            {
                BankInfo bankInfo = pushLogin.ResponseData;
                this.txtBankName.Text = bankInfo.bank;

            }
            else
            {
                MessageHelper.Show(pushLogin.Message);
            }

        }



        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(_uuId))
                    reslt(_state);
                else if (CloseDdetailedWinform != null)
                    CloseDdetailedWinform(null, false, "");
                if (ShowProjectList != null)
                    ShowProjectList("ok");
                else
                    this.Close();
            }
            catch (Exception ex)
            {

                MessageHelper.Show(ex.Message, ex);
            }
        }



        private void txtBirthday_EditValueChanged(object sender, EventArgs e)
        {
            // MessageHelper.Show("时间EditValueChanged"+ txtBirthday.EditValue +""+ txtBirthday.Text);
            if (!string.IsNullOrEmpty(txtBirthday.Text))
                txtAvg.Text = FormatHelper.GetAgeByBirthdate(DateTime.Parse(txtBirthday.Text)).ToString();
        }

        private void ComEducationLevel_Properties_MouseWheel(object sender, MouseEventArgs e)
        {
            HandledMouseEventArgs h = e as HandledMouseEventArgs;
            if (h != null)
            {
                h.Handled = true;
            }
        }

        private void ComOrganizationUuid_Properties_MouseWheel(object sender, MouseEventArgs e)
        {
            HandledMouseEventArgs h = e as HandledMouseEventArgs;
            if (h != null)
            {
                h.Handled = true;
            }
        }

        private void comWorkerTeamUuid_Properties_MouseWheel(object sender, MouseEventArgs e)
        {
            HandledMouseEventArgs h = e as HandledMouseEventArgs;
            if (h != null)
            {
                h.Handled = true;
            }
        }

        private void comWorkType_Properties_MouseWheel(object sender, MouseEventArgs e)
        {
            HandledMouseEventArgs h = e as HandledMouseEventArgs;
            if (h != null)
            {
                h.Handled = true;
            }
        }

        private void txtStartTime_Click(object sender, EventArgs e)
        {
            if (this.txtStartTime.Text == "yyyy-mm-dd")
            {
                this.txtStartTime.Text = "";
            }
        }
    }
}