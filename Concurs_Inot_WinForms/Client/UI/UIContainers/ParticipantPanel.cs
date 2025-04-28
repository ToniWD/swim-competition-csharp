using Model.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.UI.UIContainers
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
            Size = new Size(220, 120);
            BackColor = Color.White;
            BorderStyle = BorderStyle.None;
            Padding = new Padding(10);
            Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                    Color.LightGray, 2, ButtonBorderStyle.Solid,
                    Color.LightGray, 2, ButtonBorderStyle.Solid,
                    Color.LightGray, 2, ButtonBorderStyle.Solid,
                    Color.LightGray, 2, ButtonBorderStyle.Solid);
            };

            label.Text = "👤 " + participant.FirstName + " " + participant.LastName +
                "\n🎂 Age: " + participant.Age +
                "\n🏆 Nr. Events: " + participant.nrEvents;

            Controls.Add(label);
        }
    }
}

