using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Windows.Forms;
using PodCastAppFormVersion.Services;
namespace PodCastAppFormVersion
{

    public partial class Form1 : Form
    {


        DatabaseService service;

        User user;

        public Form1()
        {
            InitializeComponent();
        }

        public void setUser(User user)
        {
            this.user = user;
        }
        private void Form1_Load(object sender, EventArgs e)
        { 
            service = new DatabaseService();
            if(service.isConnectionEstablished())
            {
                MessageBox.Show("Connection Established");
            }
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            // currently only choosing the first one.. logic is not correct -- fix later
            if(user == null)
            {
                List<User> user = await service.getUsersByName(textBox2.Text);
                this.user = user[0];
            }
            PodcastForm childForm = new PodcastForm(this.user);
            childForm.setDatabaseService(service);
            childForm.Show();
            childForm.parent = this;
        }

        private async void Button2_Click(object sender, EventArgs e)
        {
            // add dummy friend to id 1
            //List<String> s = await service.getAllFriends("1");
        //    bool result = await service.addNewFriend("1", "4");
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            CreateNewUser childForm = new CreateNewUser();

            childForm.setDatabaseService(service);
            childForm.Show();
            childForm.parent = this;
        }

        public void setNameTextBox(String name)
        {
            textBox2.Text = name;
        }
    }

}
