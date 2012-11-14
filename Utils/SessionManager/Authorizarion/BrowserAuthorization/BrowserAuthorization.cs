using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ApiCore
{
    public partial class BrowserAuthorization : Form
    {
        public int Permissions;
        public int AppId;

        public SessionInfo si;
        public bool LoginInfoReceived = false;

        public BrowserAuthorization()
        {
            InitializeComponent();
        }

        private void LoginWnd_Shown(object sender, EventArgs e)
        {
            this.LoginBrowser.Navigate(
                String.Format("https://oauth.vk.com/authorize?client_id={0}&scope={1}&redirect_uri{2}&display=popup&response_type=token",
                AppId,Permissions,"http://oauth.vk.com/blank.html"));
        }

        private void LoginBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            SessionInfo ss = new SessionInfo();
            if (e.Url.ToString().Contains("#"))
            {
                string[] param = e.Url.ToString().Split('#')[1].Split('&');

                foreach (string s in param)
                {
                    if (string.IsNullOrEmpty(s))
                        continue;
                    string[] parts = s.Split('=');

                    if (parts.Length < 2)
                        continue;

                    if (parts[0] == "error")
                        return;

                    if (parts[0] == "access_token")
                        ss.AccessToken = parts[1];

                    if (parts[0] == "expires_in")
                        ss.ExpiresIn = Convert.ToInt32(parts[1]);

                    if (parts[0] == "user_id")
                        ss.UserId = parts[1];

                }

                if (!String.IsNullOrEmpty(ss.AccessToken))
                {
                    si = ss;
                    this.LoginInfoReceived = true;
                }
                //string[] json = r.Match(e.Url.ToString()).Value.Replace("{", "").Replace("}", "").Replace("\"", "").Split(',');
                //Hashtable h = new Hashtable();
                //foreach (string str in json)
                //{
                //    string[] kv = str.Split(':');
                //    h[kv[0]] = kv[1];
                //}

                //this.si = new SessionInfo();
                //this.si.AppId = this.AppId;
                //this.si.Permissions = this.Permissions;
                //this.si.MemberId = (string)h["mid"];
                //this.si.SessionId = (string)h["sid"];
                //this.si.Expire = Convert.ToInt32(h["expire"]);
                //this.si.Secret = (string)h["secret"];
                //this.si.Signature = (string)h["sig"];

                this.Close();
            }
            
        }

        private void LoginBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            //this.wnd.LogIt("loading: "+ e.Url );
            
        }


    }
}
