using Concurs_Inot_WinForms.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Concurs_Inot_WinForms.UI.UIContainers
{
    public class ParticipantPanel : Panel
    {
        private Label label = new Label
        {
            Text = "",
            Location = new Point(10, 10),
            Font = new Font("Segoe UI", 10, FontStyle.Regular),
            ForeColor = Color.DimGray,
            AutoSize = true
        };

        public ParticipantPanel(Participant participant)
        {
            this.Size = new Size(220, 120);
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.None;
            this.Padding = new Padding(10);
            this.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                    Color.LightGray, 2, ButtonBorderStyle.Solid,
                    Color.LightGray, 2, ButtonBorderStyle.Solid,
                    Color.LightGray, 2, ButtonBorderStyle.Solid,
                    Color.LightGray, 2, ButtonBorderStyle.Solid);
            };

            label.Text = "👤 " + participant.FirstName + " " + participant.LastName +
                "\n🎂 Age: " + participant.Age +
                "\n🏆 Nr. Events: " + participant.nrEvents;

            this.Controls.Add(label);
        }
    }
}

