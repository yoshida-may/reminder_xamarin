using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace reminder7.View
{
    public partial class InputPage : ContentPage
    {
        private MainPage mainPage;
        public InputPage(MainPage page)
        {
            InitializeComponent();
            mainPage = page;
        }

        void onClickedButton(object sender, EventArgs e)
        {
            var title = titleEditor.Text;
            var date = datePicker.Date;
            var time = timePicker.Time;

            mainPage.updateListView(title, date, time);
            // 前の画面に戻る
            Navigation.PopModalAsync();
        }
    }
}
