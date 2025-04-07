using Concurs_Inot_WinForms.Domain;
using Concurs_Inot_WinForms.Service;
using Concurs_Inot_WinForms.Service.Interfaces;
using Concurs_Inot_WinForms.UI.UIContainers;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Concurs_Inot_WinForms.UI
{

    public partial class MainPageForm : Form
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MainPageForm));
        private LoginForm loginForm;
        private RegisterParticipantForm registerParticipantForm;
        private IMainService mainService;
        private long idEventCurrent;
        public MainPageForm(LoginForm loginForm, IMainService mainService)
        {
            InitializeComponent();
            this.loginForm = loginForm;
            this.mainService = mainService;

            eventsFlowPanel.AutoScroll = true;
            eventsFlowPanel.WrapContents = false;
            eventsFlowPanel.FlowDirection = FlowDirection.TopDown;

            participantsFlowPanel.AutoScroll = true;
            participantsFlowPanel.WrapContents = false;
            participantsFlowPanel.FlowDirection = FlowDirection.TopDown;
            LoadEventsFlowPanel();
        }

        private void disconectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loginForm.Show();
            this.Close();
        }

        private void MainPageForm_Load(object sender, EventArgs e)
        {

        }


        private void MainPageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!loginForm.Visible) Application.Exit();
        }

        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (registerParticipantForm == null || !registerParticipantForm.Visible)
            {
                registerParticipantForm = new RegisterParticipantForm(mainService);
                registerParticipantForm.Show();
            }
            else
            {
                registerParticipantForm.Focus();
            }
        }


        private void LoadEventsFlowPanel()
        {
            log.Info("Loading events flow panel");
            IEnumerable<SwimmingEvent> events = mainService.GetSwimmingEvents();

            eventsFlowPanel.Controls.Clear();
            foreach (SwimmingEvent swimmingEvent in events)
            {

                SwimmingEventPanel panel = new SwimmingEventPanel(swimmingEvent);
                eventsFlowPanel.Controls.Add(panel);
                panel.button.Click += (s, e) =>
                {
                    LoadParticipantsFlowPanel(swimmingEvent.Id);
                };
            }
        }


        public void LoadParticipantsFlowPanel(long idEvent)
        {
            idEventCurrent = idEvent;
            log.Info("Loading participants flow panel");
            IEnumerable<Participant> participants = null;
            if(searchParticipantTextBox.Text.Length > 0)
            {
                participants = mainService.GetParticipantsForEvent(idEvent, searchParticipantTextBox.Text);
            }
            else
            {
                participants = mainService.GetParticipantsForEvent(idEvent);
            }


            participantsFlowPanel.Controls.Clear();
            foreach (Participant participant in participants)
            {
                ParticipantPanel panel = new ParticipantPanel(participant);
                participantsFlowPanel.Controls.Add(panel);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadParticipantsFlowPanel(idEventCurrent);
            }
        }
    }
}
