using Model.Domain;
using System.Drawing;
using System.Windows.Forms;

namespace Client.UI.UIContainers
{
    public class SwimmingEventPanel : Panel
    {
        private Label label = new Label
        {
            Text = "",
            Location = new Point(10, 10),
            Font = new Font("Segoe UI", 10, FontStyle.Regular),
            ForeColor = Color.DimGray,
            AutoSize = true
        };
        public Button button = new Button
        {
            Text = "View Participants",
            Location = new Point(10, 85),
            Size = new Size(190, 30),
            BackColor = Color.FromArgb(52, 152, 219),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };


        public SwimmingEventPanel(SwimmingEvent swimmingEvent)
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

            label.Text = "🏊 Style: " + swimmingEvent.Style +
                "\n📏 Distance: " + swimmingEvent.Distance +
                "m\n👥 Participants: " + swimmingEvent.nrParticipants;

            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 128, 185);
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(36, 113, 163);

            Controls.Add(label);
            Controls.Add(button);
        }
    }
}

