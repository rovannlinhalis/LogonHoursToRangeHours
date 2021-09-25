using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using FontAwesome.Sharp;


namespace AdminAD
{
    public partial class frmHorarioAD : Form
    {
        public bool IsManager { get; protected set; }

        private Form currentChildForm;
        private IconButton currentBtn;
        private Panel leftBorderBtn;


        public string userlogado
        {

            get { return lbNomeCompleto.Text; }
            set { lbNomeCompleto.Text = value; }
        }

        string usuarioAD;
        string senhaAD;



        private int cv_iSunFromTime = 0, cv_iSunToTime = 0;
        private int cv_iMonFromTime = 0, cv_iMonToTime = 0;
        private int cv_iTueFromTime = 0, cv_iTueToTime = 0;
        private int cv_iWedFromTime = 0, cv_iWedToTime = 0;
        private int cv_iThuFromTime = 0, cv_iThuToTime = 0;
        private int cv_iFriFromTime = 0, cv_iFriToTime = 0;
        private int cv_iSatFromTime = 0, cv_iSatToTime = 0;

        private void cmbSunToTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void cmbMonFromTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void cmbMonToTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void cmbTueFromTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void cmbTueToTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void cmbWedFromTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void cmbWedToTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void cmbThuFromTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void cmbThuToTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void cmbFriFromTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void cmbFriToTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void cmbSatFromTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void cmbSatToTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void cmbSunFromTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void frmHorarioAD_Load(object sender, EventArgs e)
        {

            dgListUsuario.ClearSelection();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            Reset();
            this.Close();
        }

        public void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;

        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(0, 81, 135);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }


        public frmHorarioAD(string usuario, string senha, string nomecompleto)
        {
            InitializeComponent();

            lbNomeCompleto.Text = usuario;

            usuarioAD = usuario;
            senhaAD = senha;


            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);


            carregarDGUsuario(usuario, senha, nomecompleto);

            this.dgListUsuario.EnableHeadersVisualStyles = false;


            // Limpa e carrega os tempos por hora nos combobox
            DateTime dtFromTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            DateTime dtToTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            cmbSunFromTime.Items.Clear();
            cmbMonFromTime.Items.Clear();
            cmbTueFromTime.Items.Clear();
            cmbWedFromTime.Items.Clear();
            cmbThuFromTime.Items.Clear();
            cmbFriFromTime.Items.Clear();
            cmbSatFromTime.Items.Clear();
            cmbSunToTime.Items.Clear();
            cmbMonToTime.Items.Clear();
            cmbTueToTime.Items.Clear();
            cmbWedToTime.Items.Clear();
            cmbThuToTime.Items.Clear();
            cmbFriToTime.Items.Clear();
            cmbSatToTime.Items.Clear();

            for (int iIndex = 0; iIndex < 24; iIndex++)
            {
                cmbSunFromTime.Items.Add(dtFromTime.ToShortTimeString());
                cmbMonFromTime.Items.Add(dtFromTime.ToShortTimeString());
                cmbTueFromTime.Items.Add(dtFromTime.ToShortTimeString());
                cmbWedFromTime.Items.Add(dtFromTime.ToShortTimeString());
                cmbThuFromTime.Items.Add(dtFromTime.ToShortTimeString());
                cmbFriFromTime.Items.Add(dtFromTime.ToShortTimeString());
                cmbSatFromTime.Items.Add(dtFromTime.ToShortTimeString());
                cmbSunToTime.Items.Add(dtToTime.ToShortTimeString());
                cmbMonToTime.Items.Add(dtToTime.ToShortTimeString());
                cmbTueToTime.Items.Add(dtToTime.ToShortTimeString());
                cmbWedToTime.Items.Add(dtToTime.ToShortTimeString());
                cmbThuToTime.Items.Add(dtToTime.ToShortTimeString());
                cmbFriToTime.Items.Add(dtToTime.ToShortTimeString());
                cmbSatToTime.Items.Add(dtToTime.ToShortTimeString());
                dtFromTime = dtFromTime.AddHours(1);
                dtToTime = dtToTime.AddHours(1);
                if (iIndex == Convert.ToInt32(23))
                {
                    dtFromTime = dtFromTime.AddHours(-1);
                    dtFromTime = dtFromTime.AddMinutes(59);

                    dtToTime = dtToTime.AddHours(-1);
                    dtToTime = dtToTime.AddMinutes(59);

                    cmbSunFromTime.Items.Add(dtFromTime.ToShortTimeString());
                    cmbMonFromTime.Items.Add(dtFromTime.ToShortTimeString());
                    cmbTueFromTime.Items.Add(dtFromTime.ToShortTimeString());
                    cmbWedFromTime.Items.Add(dtFromTime.ToShortTimeString());
                    cmbThuFromTime.Items.Add(dtFromTime.ToShortTimeString());
                    cmbFriFromTime.Items.Add(dtFromTime.ToShortTimeString());
                    cmbSatFromTime.Items.Add(dtFromTime.ToShortTimeString());
                    cmbSunToTime.Items.Add(dtToTime.ToShortTimeString());
                    cmbMonToTime.Items.Add(dtToTime.ToShortTimeString());
                    cmbTueToTime.Items.Add(dtToTime.ToShortTimeString());
                    cmbWedToTime.Items.Add(dtToTime.ToShortTimeString());
                    cmbThuToTime.Items.Add(dtToTime.ToShortTimeString());
                    cmbFriToTime.Items.Add(dtToTime.ToShortTimeString());
                    cmbSatToTime.Items.Add(dtToTime.ToShortTimeString());
                    
                  
                }
            }
        }


        public void carregarDGUsuario(string usuario, string senha, string nomecompleto)

        {
            try
            {
                List<string> usuarioSubordinado = new List<string>();

                DataTable tabela = new DataTable();
                tabela.Columns.Add("Nome", typeof(string));
                tabela.Columns.Add("UsuarioAD", typeof(string));
                tabela.Columns.Add("Função", typeof(string));


                DirectoryEntry Servidor = new DirectoryEntry("LDAP://10.0.64.151", usuario, senha);
                object nativeObject = Servidor.NativeObject;

                DirectorySearcher pesquisa = new DirectorySearcher(Servidor);

                pesquisa.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(manager=" + nomecompleto + "))";
                pesquisa.PropertiesToLoad.Add("samaccountname");
                pesquisa.PropertiesToLoad.Add("DisplayName");
                pesquisa.PropertiesToLoad.Add("userAccountControl");
                pesquisa.PropertiesToLoad.Add("mail");
                pesquisa.PropertiesToLoad.Add("title");
                pesquisa.PropertiesToLoad.Add("company");


                SearchResult result;
                SearchResultCollection resultCol = pesquisa.FindAll();
                if (resultCol != null)
                {
                    for (int counter = 0; counter < resultCol.Count; counter++)
                    {
                        result = resultCol[counter];

                        if (result.Properties.Contains("samaccountname"))

                        {
                            var vaPropertiy = result.Properties["userAccountControl"];

                            if (vaPropertiy.Count > 0)
                            {
                                if (vaPropertiy[0].ToString() == "512" || vaPropertiy[0].ToString() == "66048" || vaPropertiy[0].ToString() == "544")
                                {
                                    DataRow dr = tabela.NewRow();
                                    dr["Nome"] = result.Properties["DisplayName"][0];
                                    dr["UsuarioAD"] = result.Properties["samaccountname"][0];
                                    dr["Função"] = result.Properties["title"][0];


                                    tabela.Rows.Add(dr);


                                }

                                dgListUsuario.DataSource = tabela;
                                dgListUsuario.ReadOnly = true;
                            }


                        }
                    }


                }

                else
                {

                    MessageBox.Show("Usuário não é Gerente", "SGAD-Sistema de Gestão do AD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Menu formenu = new Menu();
                    this.Hide();
                    formenu.Reset();

                }

            }

            catch
            {
                MessageBox.Show("Usuário não é Manager");
            }
        }

        private void dgListUsuario_SelectionChanged(object sender, EventArgs e)
        {


            foreach (DataGridViewRow row in dgListUsuario.SelectedRows)
            {

                string nomeusuario = row.Cells[1].Value.ToString();
                carregarHorario(nomeusuario);
                lbUserSelecionado.Text = row.Cells[1].Value.ToString();

            }


        }

        public void carregarHorario(string NomeUsuario)
        {
            var varSelectedUserLogonTimes = new List<LogonTime>();
            String sSelectedUserName = "";
            PrincipalContext pctxLocalMachine;
            UserPrincipal usrpSelectedUser;

            string servidor = "10.0.64.151";
            string userAD = usuarioAD;
            string pwdAD = senhaAD;
            sSelectedUserName = NomeUsuario;
            pctxLocalMachine = new PrincipalContext(ContextType.Domain, servidor, userAD, pwdAD);
            usrpSelectedUser = UserPrincipal.FindByIdentity(pctxLocalMachine, IdentityType.SamAccountName, sSelectedUserName);


            if (usrpSelectedUser.Enabled == true)
            {
                cmbSunFromTime.Enabled = true;
                cmbSunToTime.Enabled = true;
                cmbMonFromTime.Enabled = true;
                cmbMonToTime.Enabled = true;
                cmbTueFromTime.Enabled = true;
                cmbTueToTime.Enabled = true;
                cmbWedFromTime.Enabled = true;
                cmbWedToTime.Enabled = true;
                cmbThuFromTime.Enabled = true;
                cmbThuToTime.Enabled = true;
                cmbFriFromTime.Enabled = true;
                cmbFriToTime.Enabled = true;
                cmbSatFromTime.Enabled = true;
                cmbSatToTime.Enabled = true;
                btnSave.Enabled = true;
            }
            else
            {
                cmbSunFromTime.Enabled = false;
                cmbSunToTime.Enabled = false;
                cmbMonFromTime.Enabled = false;
                cmbMonToTime.Enabled = false;
                cmbTueFromTime.Enabled = false;
                cmbTueToTime.Enabled = false;
                cmbWedFromTime.Enabled = false;
                cmbWedToTime.Enabled = false;
                cmbThuFromTime.Enabled = false;
                cmbThuToTime.Enabled = false;
                cmbFriFromTime.Enabled = false;
                cmbFriToTime.Enabled = false;
                cmbSatFromTime.Enabled = false;
                cmbSatToTime.Enabled = false;
                btnSave.Enabled = false;
            }


            varSelectedUserLogonTimes = PermittedLogonTimes.GetLogonTimes(usrpSelectedUser.PermittedLogonTimes);

            cmbSunFromTime.SelectedIndex = 0;
            cmbMonFromTime.SelectedIndex = 0;
            cmbTueFromTime.SelectedIndex = 0;
            cmbWedFromTime.SelectedIndex = 0;
            cmbThuFromTime.SelectedIndex = 0;
            cmbFriFromTime.SelectedIndex = 0;
            cmbSatFromTime.SelectedIndex = 0;
            cmbSunToTime.SelectedIndex = 0;
            cmbMonToTime.SelectedIndex = 0;
            cmbTueToTime.SelectedIndex = 0;
            cmbWedToTime.SelectedIndex = 0;
            cmbThuToTime.SelectedIndex = 0;
            cmbFriToTime.SelectedIndex = 0;
            cmbSatToTime.SelectedIndex = 0;

            for (int iIndex = 0; iIndex < varSelectedUserLogonTimes.Count; iIndex++)
            {
                LogonTime lExtractedTime = varSelectedUserLogonTimes.ElementAt(iIndex);

                switch (lExtractedTime.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        cv_iSunFromTime = cmbSunFromTime.SelectedIndex = cmbSunFromTime.Items.IndexOf(lExtractedTime.BeginTime.ToShortTimeString());
                        cv_iSunToTime = cmbSunToTime.SelectedIndex = cmbSunToTime.Items.IndexOf(lExtractedTime.EndTime.ToShortTimeString());
                        break;
                    case DayOfWeek.Monday:
                        cv_iMonFromTime = cmbMonFromTime.SelectedIndex = cmbMonFromTime.Items.IndexOf(lExtractedTime.BeginTime.ToShortTimeString());
                        cv_iMonToTime = cmbMonToTime.SelectedIndex = cmbMonToTime.Items.IndexOf(lExtractedTime.EndTime.ToShortTimeString());
                        break;
                    case DayOfWeek.Tuesday:
                        cv_iTueFromTime = cmbTueFromTime.SelectedIndex = cmbTueFromTime.Items.IndexOf(lExtractedTime.BeginTime.ToShortTimeString());
                        cv_iTueToTime = cmbTueToTime.SelectedIndex = cmbTueToTime.Items.IndexOf(lExtractedTime.EndTime.ToShortTimeString());
                        break;
                    case DayOfWeek.Wednesday:
                        cv_iWedFromTime = cmbWedFromTime.SelectedIndex = cmbWedFromTime.Items.IndexOf(lExtractedTime.BeginTime.ToShortTimeString());
                        cv_iWedToTime = cmbWedToTime.SelectedIndex = cmbWedToTime.Items.IndexOf(lExtractedTime.EndTime.ToShortTimeString());
                        break;
                    case DayOfWeek.Thursday:
                        cv_iThuFromTime = cmbThuFromTime.SelectedIndex = cmbThuFromTime.Items.IndexOf(lExtractedTime.BeginTime.ToShortTimeString());
                        cv_iThuToTime = cmbThuToTime.SelectedIndex = cmbThuToTime.Items.IndexOf(lExtractedTime.EndTime.ToShortTimeString());
                        break;
                    case DayOfWeek.Friday:
                        cv_iFriFromTime = cmbFriFromTime.SelectedIndex = cmbFriFromTime.Items.IndexOf(lExtractedTime.BeginTime.ToShortTimeString());
                        cv_iFriToTime = cmbFriToTime.SelectedIndex = cmbFriToTime.Items.IndexOf(lExtractedTime.EndTime.ToShortTimeString());
                        break;
                    case DayOfWeek.Saturday:
                        cv_iSatFromTime = cmbSatFromTime.SelectedIndex = cmbSatFromTime.Items.IndexOf(lExtractedTime.BeginTime.ToShortTimeString());
                        cv_iSatToTime = cmbSatToTime.SelectedIndex = cmbSatToTime.Items.IndexOf(lExtractedTime.EndTime.ToShortTimeString());
                        break;
                }
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DateTime dtSunFromTime, dtMonFromTime, dtTueFromTime, dtWedFromTime, dtThuFromTime, dtFriFromTime, dtSatFromTime;
            DateTime dtSunToTime, dtMonToTime, dtTueToTime, dtWedToTime, dtThuToTime, dtFriToTime, dtSatToTime;
            String sSelectedUserName = "", sFromTime = "", sToTime = "";
            LogonTime lSunLogonTime, lMonLogonTime, lTueLogonTime, lWedLogonTime;
            LogonTime lThuLogonTime, lFriLogonTime, lSatLogonTime;
            List<LogonTime> lstLogonTimes = new List<LogonTime>();
            PrincipalContext pctxLocalMachine;
            UserPrincipal usrpSelectedUser;


            string servidor = "10.0.64.151";
            string userAD = usuarioAD;
            string pwdAD = senhaAD;
            sSelectedUserName = lbUserSelecionado.Text;//cmbUserList.SelectedItem.ToString();
            pctxLocalMachine = new PrincipalContext(ContextType.Domain, servidor, userAD, pwdAD);
            usrpSelectedUser = UserPrincipal.FindByIdentity(pctxLocalMachine, IdentityType.SamAccountName, sSelectedUserName);

            if (cmbSunFromTime.SelectedItem != null & cmbSunToTime.SelectedItem != null)
            {
                sFromTime = "1/1/" + DateTime.Now.Year + " "; sToTime = "1/1/" + DateTime.Now.Year + " ";
                sFromTime = sFromTime + cmbSunFromTime.SelectedItem.ToString();
                sToTime = sToTime + cmbSunToTime.SelectedItem.ToString();
                dtSunFromTime = DateTime.Parse(sFromTime);
                dtSunToTime = DateTime.Parse(sToTime);
                lSunLogonTime = new LogonTime(System.DayOfWeek.Sunday, dtSunFromTime, dtSunToTime);
                lstLogonTimes.Add(lSunLogonTime);
            }
            else
            {

                sFromTime = "1/1/" + DateTime.Now.Year + " "; sToTime = "1/1/" + DateTime.Now.Year + " ";
                sFromTime = sFromTime + "00:00";
                sToTime = sToTime + "00:00";
                dtSunFromTime = DateTime.Parse(sFromTime);
                dtSunToTime = DateTime.Parse(sToTime);
                lSunLogonTime = new LogonTime(System.DayOfWeek.Sunday, dtSunFromTime, dtSunToTime);
                lstLogonTimes.Add(lSunLogonTime);
            }

            if (cmbMonFromTime.SelectedItem != null & cmbMonToTime.SelectedItem != null)
            {

                sFromTime = "1/1/" + DateTime.Now.Year + " "; sToTime = "1/1/" + DateTime.Now.Year + " ";
                sFromTime = sFromTime + cmbMonFromTime.SelectedItem.ToString();
                sToTime = sToTime + cmbMonToTime.SelectedItem.ToString();
                dtMonFromTime = DateTime.Parse(sFromTime);
                dtMonToTime = DateTime.Parse(sToTime);
                lMonLogonTime = new LogonTime(System.DayOfWeek.Monday, dtMonFromTime, dtMonToTime);
                lstLogonTimes.Add(lMonLogonTime);
            }
            else
            {
                sFromTime = "1/1/" + DateTime.Now.Year + " "; sToTime = "1/1/" + DateTime.Now.Year + " ";
                sFromTime = sFromTime + "00:00";
                sToTime = sToTime + "00:00";
                dtMonFromTime = DateTime.Parse(sFromTime);
                dtMonToTime = DateTime.Parse(sToTime);
                lMonLogonTime = new LogonTime(System.DayOfWeek.Monday, dtMonFromTime, dtMonToTime);
                lstLogonTimes.Add(lMonLogonTime);


            }

            if (cmbTueFromTime.SelectedItem != null & cmbTueToTime.SelectedItem != null)
            {
                sFromTime = "1/1/" + DateTime.Now.Year + " "; sToTime = "1/1/" + DateTime.Now.Year + " ";
                sFromTime = sFromTime + cmbTueFromTime.SelectedItem.ToString();
                sToTime = sToTime + cmbTueToTime.SelectedItem.ToString();
                dtTueFromTime = DateTime.Parse(sFromTime);
                dtTueToTime = DateTime.Parse(sToTime);
                lTueLogonTime = new LogonTime(System.DayOfWeek.Tuesday, dtTueFromTime, dtTueToTime);
                lstLogonTimes.Add(lTueLogonTime);
            }
            else
            {
                sFromTime = "1/1/" + DateTime.Now.Year + " "; sToTime = "1/1/" + DateTime.Now.Year + " ";
                sFromTime = sFromTime + "00:00";
                sToTime = sToTime + "00:00";
                dtTueFromTime = DateTime.Parse(sFromTime);
                dtTueToTime = DateTime.Parse(sToTime);
                lTueLogonTime = new LogonTime(System.DayOfWeek.Tuesday, dtTueFromTime, dtTueToTime);
                lstLogonTimes.Add(lTueLogonTime);

            }

            if (cmbWedFromTime.SelectedItem != null & cmbWedToTime.SelectedItem != null)
            {

                sFromTime = "1/1/" + DateTime.Now.Year + " "; sToTime = "1/1/" + DateTime.Now.Year + " ";
                sFromTime = sFromTime + cmbWedFromTime.SelectedItem.ToString();
                sToTime = sToTime + cmbWedToTime.SelectedItem.ToString();
                dtWedFromTime = DateTime.Parse(sFromTime);
                dtWedToTime = DateTime.Parse(sToTime);
                lWedLogonTime = new LogonTime(System.DayOfWeek.Wednesday, dtWedFromTime, dtWedToTime);
                lstLogonTimes.Add(lWedLogonTime);
            }
            else
            {
                sFromTime = "1/1/" + DateTime.Now.Year + " "; sToTime = "1/1/" + DateTime.Now.Year + " ";
                sFromTime = sFromTime + "00:00";
                sToTime = sToTime + "00:00";
                dtWedFromTime = DateTime.Parse(sFromTime);
                dtWedToTime = DateTime.Parse(sToTime);
                lWedLogonTime = new LogonTime(System.DayOfWeek.Wednesday, dtWedFromTime, dtWedToTime);
                lstLogonTimes.Add(lWedLogonTime);

            }

            if (cmbThuFromTime.SelectedItem != null & cmbThuToTime.SelectedItem != null)
            {
                sFromTime = "1/1/" + DateTime.Now.Year + " "; sToTime = "1/1/" + DateTime.Now.Year + " ";
                sFromTime = sFromTime + cmbThuFromTime.SelectedItem.ToString();
                sToTime = sToTime + cmbThuToTime.SelectedItem.ToString();
                dtThuFromTime = DateTime.Parse(sFromTime);
                dtThuToTime = DateTime.Parse(sToTime);
                lThuLogonTime = new LogonTime(System.DayOfWeek.Thursday, dtThuFromTime, dtThuToTime);
                lstLogonTimes.Add(lThuLogonTime);

            }
            else
            {
                sFromTime = "1/1/" + DateTime.Now.Year + " "; sToTime = "1/1/" + DateTime.Now.Year + " ";
                sFromTime = sFromTime + "00:00";
                sToTime = sToTime + "00:00";
                dtThuFromTime = DateTime.Parse(sFromTime);
                dtThuToTime = DateTime.Parse(sToTime);
                lThuLogonTime = new LogonTime(System.DayOfWeek.Thursday, dtThuFromTime, dtThuToTime);
                lstLogonTimes.Add(lThuLogonTime);

            }


            if (cmbFriFromTime.SelectedItem != null & cmbFriToTime.SelectedItem != null)
            {
                sFromTime = "1/1/" + DateTime.Now.Year + " "; sToTime = "1/1/" + DateTime.Now.Year + " ";
                sFromTime = sFromTime + cmbFriFromTime.SelectedItem.ToString();
                sToTime = sToTime + cmbFriToTime.SelectedItem.ToString();
                dtFriFromTime = DateTime.Parse(sFromTime);
                dtFriToTime = DateTime.Parse(sToTime);
                lFriLogonTime = new LogonTime(System.DayOfWeek.Friday, dtFriFromTime, dtFriToTime);
                lstLogonTimes.Add(lFriLogonTime);

            }
            else
            {
                sFromTime = "1/1/" + DateTime.Now.Year + " "; sToTime = "1/1/" + DateTime.Now.Year + " ";
                sFromTime = sFromTime + "00:00";
                sToTime = sToTime + "00:00";
                dtFriFromTime = DateTime.Parse(sFromTime);
                dtFriToTime = DateTime.Parse(sToTime);
                lFriLogonTime = new LogonTime(System.DayOfWeek.Friday, dtFriFromTime, dtFriToTime);
                lstLogonTimes.Add(lFriLogonTime);

            }

            if (cmbSatFromTime.SelectedItem != null & cmbSatToTime.SelectedItem != null)
            {
                sFromTime = "1/1/" + DateTime.Now.Year + " "; sToTime = "1/1/" + DateTime.Now.Year + " ";
                sFromTime = sFromTime + cmbSatFromTime.SelectedItem.ToString();
                sToTime = sToTime + cmbSatToTime.SelectedItem.ToString();
                dtSatFromTime = DateTime.Parse(sFromTime);
                dtSatToTime = DateTime.Parse(sToTime);
                lSatLogonTime = new LogonTime(System.DayOfWeek.Saturday, dtSatFromTime, dtSatToTime);
                lstLogonTimes.Add(lSatLogonTime);
            }
            else
            {
                sFromTime = "1/1/" + DateTime.Now.Year + " "; sToTime = "1/1/" + DateTime.Now.Year + " ";
                sFromTime = sFromTime + "00:00";
                sToTime = sToTime + "00:00";
                dtSatFromTime = DateTime.Parse(sFromTime);
                dtSatToTime = DateTime.Parse(sToTime);
                lSatLogonTime = new LogonTime(System.DayOfWeek.Saturday, dtSatFromTime, dtSatToTime);
                lstLogonTimes.Add(lSatLogonTime);
            }



            if (cmbSunFromTime.SelectedIndex == cv_iSunFromTime &&
                cmbSunToTime.SelectedIndex == cv_iSunToTime &&
                cmbMonFromTime.SelectedIndex == cv_iMonFromTime &&
                cmbMonToTime.SelectedIndex == cv_iMonToTime &&
                cmbTueFromTime.SelectedIndex == cv_iTueFromTime &&
                cmbTueToTime.SelectedIndex == cv_iTueToTime &&
                cmbWedFromTime.SelectedIndex == cv_iWedFromTime &&
                cmbWedToTime.SelectedIndex == cv_iWedToTime &&
                cmbThuFromTime.SelectedIndex == cv_iThuFromTime &&
                cmbThuToTime.SelectedIndex == cv_iThuToTime &&
                cmbFriFromTime.SelectedIndex == cv_iFriFromTime &&
                cmbFriToTime.SelectedIndex == cv_iFriToTime &&
                cmbSatFromTime.SelectedIndex == cv_iSatFromTime &&
                cmbSatToTime.SelectedIndex == cv_iSatToTime)
            {
                MessageBox.Show("Nenhuma alteração feita que precise ser salva!", "SGAD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
            }
            else
            {
                byte[] byteMaskForUser;

                // salvando tempos de logon permitidos com base na seleção
                byteMaskForUser = PermittedLogonTimes.GetByteMask(lstLogonTimes);
                usrpSelectedUser.PermittedLogonTimes = byteMaskForUser;
                usrpSelectedUser.Save();



                MessageBox.Show("As alterações de limite de tempo do usuário:" + usrpSelectedUser.Name + " foram salvas com sucesso no Servidor!", "SGAD", MessageBoxButtons.OK, MessageBoxIcon.Information);

                usrpSelectedUser.Dispose();
                pctxLocalMachine.Dispose();

                btnSave.Enabled = false;
            }

            carregarHorario(lbUserSelecionado.Text);

        }

    }

}


