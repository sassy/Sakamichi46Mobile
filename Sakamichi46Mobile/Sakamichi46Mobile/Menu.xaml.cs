﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sakamichi46Mobile.Constant;
using Sakamichi46Mobile.Controller;
using Sakamichi46Mobile.Keyakizaka46;
using Sakamichi46Mobile.Nogizaka46;
using Plugin.Share;
using Xamarin.Forms;

namespace Sakamichi46Mobile
{
    public partial class Menu : CarouselPage
    {
        private NogiController nogiCtrl;
        private List<Member> nogiMember;
        private string nogiOfficialBlog;
        private string nogiOfficialGoods;

        private KeyakiController keyakiCtrl;
        private List<Member> keyakiMember;
        private string keyakiOfficialBlog;
        private string keyakiOfficialGoods;

        public Menu()
        {
            InitializeComponent();

            btnNogi.Clicked += (o, e) =>
            {
                if(nogiMember == null || string.IsNullOrEmpty(nogiOfficialBlog) || string.IsNullOrEmpty(nogiOfficialGoods))
                {
                    DisplayAlert("メッセージ", "データをダウンロード中です。", "OK");
                    return;
                }
                NogiMasterDetailPage nogiPage = new NogiMasterDetailPage(nogiCtrl, nogiMember, nogiOfficialBlog, nogiOfficialGoods);
                if (nogiPage != null)
                {
                    Navigation.PushModalAsync(nogiPage);
                }
            };

            btnKeyaki.Clicked += (o, e) =>
            {
                if(keyakiMember == null || string.IsNullOrEmpty(keyakiOfficialBlog) || string.IsNullOrEmpty(keyakiOfficialGoods))
                {
                    DisplayAlert("メッセージ", "データをダウンロード中です。", "OK");
                    return;
                }
                KeyakiMasterDetailPage keyakiPage = new KeyakiMasterDetailPage(keyakiCtrl, keyakiMember, keyakiOfficialBlog, keyakiOfficialGoods);
                if (keyakiPage != null)
                {
                    Navigation.PushModalAsync(keyakiPage);
                }
            };

            btnShare.Clicked += (o, e) =>
            {
                CrossShare.Current.ShareLink("https://github.com/kikutaro/Sakamichi46Mobile", "Sakamichi46 App");
            };
        }

        protected async override void OnAppearing()
        {
            nogiCtrl = new NogiController(UrlConst.NOGI.AbsoluteUri);
            nogiMember = await nogiCtrl.RunAsync();
            Debug.WriteLine("end to download NogiMember List " + nogiMember.Count);
            nogiOfficialBlog = await nogiCtrl.GetOfficialBlog();
            Debug.WriteLine("end to download NogiOfficialBlog URL " + nogiOfficialBlog);
            nogiOfficialGoods = await nogiCtrl.GetOfficialGoods();
            Debug.WriteLine("end to download NogiOfficialGoods URL " + nogiOfficialGoods);

            keyakiCtrl = new KeyakiController(UrlConst.KEYAKI.AbsoluteUri);
            keyakiMember = await keyakiCtrl.RunAsync();
            Debug.WriteLine("end to download KeyakiMember List " + keyakiMember.Count);
            keyakiOfficialBlog = await keyakiCtrl.GetOfficialBlog();
            Debug.WriteLine("end to download KeyakiOfficialBlog URL " + keyakiOfficialBlog);
            keyakiOfficialGoods = await keyakiCtrl.GetOfficialGoods();
            Debug.WriteLine("end to download KeyakiOfficialGoods URL " + keyakiOfficialGoods);
        }
    }
}
