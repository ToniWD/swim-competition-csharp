using Client.UI.UIContainers;
using Model.Domain;
using Model.Domain.Validator;
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Service.Interfaces;
using Service.Utils;
using System.Threading.Tasks;

namespace UI
{
    public partial class RegisterParticipantForm : Form
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(RegisterParticipantForm));
        private IServices services;
        private List<SwimmingEvent> events = new List<SwimmingEvent>();
        public RegisterParticipantForm(IServices service)
        {
            InitializeComponent();
            this.services = service;
            eventsFlowPanel.AutoScroll = true;
            eventsFlowPanel.WrapContents = false;
            eventsFlowPanel.FlowDirection = FlowDirection.TopDown;
            this.Load += RegisterParticipantForm_Load;
        }


        private async Task LoadEventsFlowPanelAsync()
        {
            log.Info("Loading events flow panel");

            IEnumerable<SwimmingEvent> allEvents = await Task.Run(() =>
            {
                return services.GetSwimmingEvents();
            });

            eventsFlowPanel.Controls.Clear();

            foreach (SwimmingEvent swimmingEvent in allEvents)
            {
                SwimmingEventPanel panel = new SwimmingEventPanel(swimmingEvent);
                panel.button.Click += (s, e) =>
                {
                    AddSwimmingEvent(panel, swimmingEvent);
                };

                if (this.events.Contains(swimmingEvent))
                {
                    panel.button.Text = "Remove";
                }
                else
                {
                    panel.button.Text = "Add";
                }

                eventsFlowPanel.Controls.Add(panel);
            }
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (events.Count == 0)
                {
                    errorLabel.Text = "No swimming events added";
                    return;
                }

                string firstName = textBox1.Text;
                string lastName = textBox2.Text;
                int ageInt = int.Parse(textBox3.Text);

                await Task.Run(() =>
                {
                    services.addParticipant(firstName, lastName, ageInt, events);
                });

                MessageBox.Show(
                    "Click OK to continue",
                    "Registered",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                this.Close();
            }
            catch (ServiceException ex)
            {
                log.Error(ex);
                errorLabel.Text = ex.Message;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBox.Show(
                    "Something went wrong.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void LoadEventsFlowPanel()
        {
            log.Info("Loading events flow panel");
            IEnumerable<SwimmingEvent> events = services.GetSwimmingEvents();

            eventsFlowPanel.Controls.Clear();
            foreach (SwimmingEvent swimmingEvent in events)
            {

                SwimmingEventPanel panel = new SwimmingEventPanel(swimmingEvent);
                eventsFlowPanel.Controls.Add(panel);
                panel.button.Click += (s, e) =>
                {
                    AddSwimmingEvent(panel, swimmingEvent);
                };
                if (this.events.Contains(swimmingEvent))
                {
                    panel.button.Text = "Remove";
                }
                else
                {
                    panel.button.Text = "Add";
                }
            }
        }

        private void AddSwimmingEvent(SwimmingEventPanel panel, SwimmingEvent swimmingEvent)
        {
            if (events.Contains(swimmingEvent))
            {
                events.Remove(swimmingEvent);
                panel.button.Text = "Add";
                panel.button.BackColor = Color.FromArgb(52, 152, 219);
                panel.button.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 128, 185);
            }
            else
            {
                events.Add(swimmingEvent);
                panel.button.Text = "Remove";
                panel.button.BackColor = Color.FromArgb(231, 76, 60);
                panel.button.FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 57, 43);
            }
        }

        private async void RegisterParticipantForm_Load(object sender, EventArgs e)
        {
            await LoadEventsFlowPanelAsync();
        }

    }
}
