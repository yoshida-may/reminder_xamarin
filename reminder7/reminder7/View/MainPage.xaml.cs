using Android.App;
using reminder7.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace reminder7.View
{
    public partial class MainPage : ContentPage
    {
        //////ObservableCollection<ListItem> items = new ObservableCollection<ListItem>();
        //////ObservableCollection<ListItem> endItems = new ObservableCollection<ListItem>();
        ObservableCollection<Model.ListItem> items = new ObservableCollection<Model.ListItem>();
        ObservableCollection<Model.ListItem> endItems = new ObservableCollection<Model.ListItem>();
        //List<string> itemNumList = new List<string>();

        public MainPage()
        {
            InitializeComponent();

            ListView.ItemsSource = items;
            EndListView.ItemsSource = endItems;

            //if (Application.Current.Properties.ContainsKey("itemNumbers"))
            //{
            //    var list = (List<string>)Application.Current.Properties["itemNumbers"];
            //    for (int i = 0; i < list.Count; ++i)
            //    {
            //        var itemNumber = (string)list[i];
            //        var title = (string)Application.Current.Properties["titile" + itemNumber];
            //        var date = (string)Application.Current.Properties["date" + itemNumber];
            //        items.Add(new ListItem { DisplayTitle = title, DisplayDateAndTime = date });
            //    }
            //
            //}

            new Model.Timer().Start(this);
        }

        void AddItem(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new InputPage(this));
        }

        void Delete(object sender, EventArgs e)
        {
            //////var item = (ListItem)((MenuItem)sender).CommandParameter;
            var item = (Model.ListItem)((MenuItem)sender).CommandParameter;
            if (item.getIsEndItem())
            {
                endItems.Remove(item);
            }
            else
            {
                items.Remove(item);
            }

            // Application.Current.Propertiesのdeleteはまだ。
        }

        void Move(object sender, EventArgs e)
        {
            //////var item = (ListItem)((MenuItem)sender).CommandParameter;
            var item = (Model.ListItem)((MenuItem)sender).CommandParameter;
            if (item.getIsEndItem())
            {
                items.Add(item);
                endItems.Remove(item);
                item.setIsEndItem(false);
            }
            else
            {
                endItems.Add(item);
                items.Remove(item);
                item.setIsEndItem(true);

                // test
                var n = DependencyService.Get<Notifications>();
                n.pushNotification(item.DisplayTitle + "(" + item.DisplayDateAndTime + ")");
                ////
            }
        }

        // リスト表示更新
        public void updateListView(String title, DateTime date, TimeSpan time)
        {
            DateTime newDateTime = date.Add(time);
            string dateAndTime = newDateTime.ToString("g");
            //////items.Add(new ListItem {DisplayTitle = title, DisplayDateAndTime = dateAndTime});
            items.Add(new Model.ListItem { DisplayTitle = title, DisplayDateAndTime = dateAndTime });

            //string stringNum = "0";
            //if (Application.Current.Properties.ContainsKey("itemNumbers"))
            //{
            //    var list = (List<string>)Application.Current.Properties["itemNumbers"];
            //    stringNum = list.Last();
            //}
            //
            //long num = long.Parse(stringNum);
            //long itemNum = num+1;
            //stringNum = itemNum.ToString();
            //itemNumList.Add(stringNum);
            //Application.Current.Properties["itemNumbers"] = itemNumList;
            //Application.Current.Properties["title" + stringNum] = title;
            //Application.Current.Properties["date" + stringNum] = dateAndTime;
            //Application.Current.SavePropertiesAsync();
        }

        // 登録してある日時と現在日時を比較して、一致したら通知する
        // 同日時に複数登録されている場合は、タイトルをまとめて1回で通知する
        public void checkItemDate(DateTime now)
        {
            Boolean isShowAlertDialog = false;
            String alertMessage = "";
            for (int i = 0; i < items.Count; i++)
            {
                //////ListItem item = items[i];
                Model.ListItem item = items[i];

                if (item.getIsEndItem()) {
                    // 終了タスクだった場合は通知しない
                    continue;
                }

                DateTime dateTime = item.getDateTime();
                if (now.Equals(dateTime))
                {
                    isShowAlertDialog = true;
                    alertMessage = alertMessage + "\n" + item.DisplayTitle + "(" + item.DisplayDateAndTime + ")";

                    item.setIsEndItem(true);
                    endItems.Add(item);
                    items.Remove(item);
                }

            }
            if (isShowAlertDialog)
            {
                // アラートダイアログ
                //DisplayAlert("タスク", alertMessage, "OK");

                // Notification(Androidのみ実装)
                var n = DependencyService.Get<Notifications>();
                n.pushNotification(alertMessage);
            }
        }
    }
}
