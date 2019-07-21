using PodCastAppFormVersion.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PodCastAppFormVersion
{
    public partial class PodcastForm : Form
    {
        public Form1 parent;
        public DatabaseService service;
        String id;

        public PodcastForm(User user)
        {
            InitializeComponent();

            NameLabel.Text = user.Name;
            AboutLabel.Text = user.About;
        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        public void setDatabaseService(DatabaseService service)
        {
            this.service = service;
        }
    }
}
