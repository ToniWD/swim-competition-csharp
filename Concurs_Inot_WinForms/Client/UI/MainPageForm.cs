using Client.UI.UIContainers;
using Model.Domain;
using log4net;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Service.Interfaces;
using System.Threading.Tasks;

namespace UI
{

    public partial class MainPageForm : Form, IObserver
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MainPageForm));
        private LoginForm loginForm;
        private RegisterParticipantForm registerParticipantForm;
        private IServices services;
        private SwimmingEvent eventCurrent;
        private User user;
        public MainPageForm(LoginForm loginForm, IServices services, User user)
        {
            InitializeComponent();
            this.loginForm = loginForm;
            this.services = services;

            eventsFlowPanel.AutoScroll = true;
            eventsFlowPanel.WrapContents = false;
            eventsFlowPanel.FlowDirection = FlowDirection.TopDown;

            participantsFlowPanel.AutoScroll = true;
            participantsFlowPanel.WrapContents = false;
            participantsFlowPanel.FlowDirection = FlowDirection.TopDown;
            
            LoadEventsFlowPanelAsync().ConfigureAwait(false);

            this.user = user;
        }

        private void disconectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            services.logout(user, this);
            loginForm.Show();
            this.Close();
        }

        private void MainPageForm_Load(object sender, EventArgs e)
        {

        }


        private void MainPageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!loginForm.Visible)
            {
                try
                {
                    if (user != null)
                    {
                        services.logout(user, this);
                        user = null;
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
                Application.Exit();
            }
        }

        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (registerParticipantForm == null || !registerParticipantForm.Visible)
            {
                registerParticipantForm = new RegisterParticipantForm(services);
                registerParticipantForm.Show();
            }
            else
            {
                registerParticipantForm.Focus();
            }
        }

        //public async Task LoadEventsFlowPanelAsync()
        //{
        //    log.Info("Loading events flow panel");

        //    IEnumerable<SwimmingEvent> events = await Task.Run(() =>
        //    {
        //        return services.GetSwimmingEvents();
        //    });

        //    eventsFlowPanel.Controls.Clear();

        //    foreach (SwimmingEvent swimmingEvent in events)
        //    {
        //        SwimmingEventPanel panel = new SwimmingEventPanel(swimmingEvent);
        //        eventsFlowPanel.Controls.Add(panel);

        //        panel.button.Click += async (s, e) =>
        //        {
        //            await LoadParticipantsFlowPanelAsync(swimmingEvent);
        //        };
        //    }
        //}
        private async Task LoadEventsFlowPanelAsync()
        {
            log.Info("Loading events flow panel");

            // Încarcă datele în thread separat
            IEnumerable<SwimmingEvent> allEvents = await Task.Run(() =>
            {
                return services.GetSwimmingEvents();
            });

            // Revii pe UI thread cu 'Invoke'
            if (eventsFlowPanel.InvokeRequired)
            {
                eventsFlowPanel.Invoke(() =>
                {
                    UpdateEventControls(allEvents);
                });
            }
            else
            {
                UpdateEventControls(allEvents);
            }
        }

        private void UpdateEventControls(IEnumerable<SwimmingEvent> allEvents)
        {
            eventsFlowPanel.Controls.Clear();

            eventsFlowPanel.Controls.Clear();

            foreach (SwimmingEvent swimmingEvent in allEvents)
            {
                SwimmingEventPanel panel = new SwimmingEventPanel(swimmingEvent);
                eventsFlowPanel.Controls.Add(panel);

                panel.button.Click += async (s, e) =>
                {
                    await LoadParticipantsFlowPanelAsync(swimmingEvent);
                };
            }
        }


        public async Task LoadParticipantsFlowPanelAsync(SwimmingEvent ev)
        {
            log.Info("Loading participants flow panel");

            IEnumerable<Participant> participants = null;
            eventCurrent = ev;

            // Execută logica în fundal (non-UI)
            await Task.Run(() =>
            {
                if (searchParticipantTextBox.InvokeRequired)
                {
                    searchParticipantTextBox.Invoke(new Action(() =>
                    {
                        if (searchParticipantTextBox.Text.Length > 0)
                        {
                            log.Info(ev.Id);
                            participants = services.GetParticipantsForEvent(ev, searchParticipantTextBox.Text);
                        }
                        else
                        {
                            participants = services.GetParticipantsForEvent(ev);
                        }
                    }));
                }
                else
                {
                    if (searchParticipantTextBox.Text.Length > 0)
                    {
                        log.Info(ev.Id);
                        participants = services.GetParticipantsForEvent(ev, searchParticipantTextBox.Text);
                    }
                    else
                    {
                        participants = services.GetParticipantsForEvent(ev);
                    }
                }
            });

            if (participantsFlowPanel.InvokeRequired)
            {
                participantsFlowPanel.Invoke(new Action(() =>
                {
                    UpdateParticipantsControls(participants);
                }));
            }
            else
            {
                UpdateParticipantsControls(participants);
            }

        }

        private void UpdateParticipantsControls(IEnumerable<Participant> participants)
        {
            participantsFlowPanel.Controls.Clear();

            foreach (Participant participant in participants)
            {
                ParticipantPanel panel = new ParticipantPanel(participant);
                participantsFlowPanel.Controls.Add(panel);
            }
        }


        private async void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                await LoadParticipantsFlowPanelAsync(eventCurrent);
            }
        }

        public async void Update()
        {
            await LoadEventsFlowPanelAsync();
            if(eventCurrent!=null) await LoadParticipantsFlowPanelAsync(eventCurrent);
        }
    }
}
