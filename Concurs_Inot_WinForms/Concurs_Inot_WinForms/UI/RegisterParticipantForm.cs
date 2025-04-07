using Concurs_Inot_WinForms.Domain;
using Concurs_Inot_WinForms.Domain.Validator;
using Concurs_Inot_WinForms.Service;
using Concurs_Inot_WinForms.Service.Interfaces;
using Concurs_Inot_WinForms.UI.UIContainers;
using log4net;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Concurs_Inot_WinForms.UI
{
    public partial class RegisterParticipantForm : Form
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(RegisterParticipantForm));
        private IMainService mainService;
        private List<long> events = new List<long>();
        public RegisterParticipantForm(IMainService service)
        {
            InitializeComponent();
            this.mainService = service;
            eventsFlowPanel.AutoScroll = true;
            eventsFlowPanel.WrapContents = false;
            eventsFlowPanel.FlowDirection = FlowDirection.TopDown;
            LoadEventsFlowPanel();
        }

        private void button1_Click(object sender, EventArgs e)
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

                mainService.addParticipant(firstName, lastName, ageInt, events);

                MessageBox.Show(
                    "Click OK to continue",
                    "Registered",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );


                this.Close();
            }
            catch (FormatException)
            {
                errorLabel.Text = "Invalid age";
            }
            catch (ValidatorException ex)
            {
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
            IEnumerable<SwimmingEvent> events = mainService.GetSwimmingEvents();

            eventsFlowPanel.Controls.Clear();
            foreach (SwimmingEvent swimmingEvent in events)
            {

                SwimmingEventPanel panel = new SwimmingEventPanel(swimmingEvent);
                eventsFlowPanel.Controls.Add(panel);
                panel.button.Click += (s, e) =>
                {
                    AddSwimmingEvent(panel,swimmingEvent.Id);
                };
                if(this.events.Contains(swimmingEvent.Id))
                {
                    panel.button.Text = "Remove";
                }
                else
                {
                    panel.button.Text = "Add";
                }
            }
        }

        private void AddSwimmingEvent(SwimmingEventPanel panel, long swimmingEvent)
        {
            if(events.Contains(swimmingEvent))
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
    }
}
