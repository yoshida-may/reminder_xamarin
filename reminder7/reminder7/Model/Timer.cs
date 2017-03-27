using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace reminder7.Model
{
    public class Timer
    {
        public delegate void TimeOutHandler(EventArgs e);
        public event TimeOutHandler TimeOutEvent;
        private DateTime _startDateTime;
        private bool _timerRunning;
        private View.MainPage mainPage;

        public Timer()
        {
            this._timerRunning = true;
        }

        public void Start(View.MainPage page)
        {
            if (this._timerRunning == false)
                return;

            mainPage = page;
            RegistTimer(this.HandleFunc);
        }

        public void Stop()
        {
            this._timerRunning = false;
        }

        private bool HandleFunc()
        {
            if (this._timerRunning == true)
            {
                if (this.TimeOutEvent != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                   {
                       this.TimeOutEvent(new EventArgs());
                   });
                }

                RegistTimer(this.HandleFunc);
            }
            return false;
        }

        private void RegistTimer (Func<bool> callback)
        {
            this._startDateTime = DateTime.Now;
            double spanSecond = 60 - this._startDateTime.Second;
            //double spanSecond = 10 - (this._startDateTime.Second / 6);

            this._timerRunning = true;
            Device.StartTimer(TimeSpan.FromSeconds(spanSecond), callback);

            //登録してある日時と比較して、一致したら通知
            DateTime dateTime = new DateTime(_startDateTime.Year, _startDateTime.Month, _startDateTime.Day,
                _startDateTime.Hour, _startDateTime.Minute, 0, 0);
            mainPage.checkItemDate(dateTime);
        }
    }
}
