﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sakamichi46Mobile.Constant;
using Sakamichi46Mobile.Controller;
using Xamarin.Forms;

namespace Sakamichi46Mobile.Keyakizaka46
{
    public partial class KeyakiDetailPage : TabbedPage
    {
        private KeyakiController keyakiCtrl;
        public Member selectedMember { get; set; }

        public string keyakiOfficialBlog { get; set; }

        public string keyakiOfficialGoods { get; set; }

        public KeyakiDetailPage()
        {
            InitializeComponent();
        }

        public KeyakiDetailPage(KeyakiController keyakiCtrl, string keyakiOfficialBlog, string keyakiOfficialGoods) : this()
        {
            this.keyakiCtrl = keyakiCtrl;
            ChangeWebPage(null, keyakiOfficialBlog, keyakiOfficialGoods);
        }

        private void InitWebPage()
        {
            keyakiWebBlog.Source = keyakiOfficialBlog;
            keyakiWebGoods.Source = keyakiOfficialGoods;
            keyakiWebYouTube.Source = UrlConst.YOUTUBE + SakamichiConst.KEYAKIZAKA46;
            keyakiWikipedia.Source = UrlConst.WIKIPEDIA + SakamichiConst.KEYAKIZAKA46;
        }

        public void ChangeWebPage(Member selectedMember, string keyakiOfficialBlog, string keyakiOfficialGoods)
        {
            this.keyakiOfficialBlog = keyakiOfficialBlog;
            this.keyakiOfficialGoods = keyakiOfficialGoods;
            ChangeWebPage(selectedMember);
        }

        public void ChangeWebPage(Member selectedMember)
        {
            if (selectedMember == null)
            {
                InitWebPage();
                return;
            }

            int tabIdx = Children.IndexOf(CurrentPage);
            if(tabIdx == 0)
            {
                keyakiWebBlog.Source = this.selectedMember.blogUri;
            }
            else if(tabIdx == 1)
            {
                keyakiWebGoods.Source = this.selectedMember.goodsUri;
            }
            else if(tabIdx == 2)
            {
                keyakiWebYouTube.Source = UrlConst.YOUTUBE + this.selectedMember.name;
            }
            else if(tabIdx == 3)
            {
                keyakiWikipedia.Source = UrlConst.WIKIPEDIA + this.selectedMember.name;
            }
        }
    }
}
