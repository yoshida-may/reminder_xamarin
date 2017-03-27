# リマインダーアプリ
## アプリの機能
- 予定のタイトルと日時を入力して登録する
- 登録した予定を削除する
- 登録日時になったら通知する
  - アラートダイアログ表示
  - 通知領域へ通知
  - バイブレーション
- 通知したタスクは自動的に完了リストへ移動する

## 実装
### OSごとに機能を実装する
Notification周りはOS依存で実装しています。 <br>
Androidしか実装を入れていないので、他OSでは通知されません。

**1.共通部分にインターフェースを定義する** <br>
https://github.com/yoshida-may/reminder_xamarin/blob/master/reminder7/reminder7/Model/Notifications.cs
```
public interface Notifications
    {
        void pushNotification(String text);
        void clearNotification();
}
```

**2.インターフェースを継承して実装する** <br>
.Droid, .UWP, .WinPhone, .Windows, .iOSの下にそれぞれ実装する。 <br>
https://github.com/yoshida-may/reminder_xamarin/blob/master/reminder7/reminder7.Droid/A_Notification.cs
```
public class A_Notification : Notifications
    {
        public void pushNotification(String text)
        {
           ～OS独自の処理～
        }

        public void clearNotification()
        {
            ～OS独自の処理～
        }
}
```

---

### タイマー
1分間隔で処理（登録してあるアイテムの時間と一致しているか確認）しています。 <br>
https://github.com/yoshida-may/reminder_xamarin/blob/master/reminder7/reminder7/Model/Timer.cs

参考サイト（ほとんどこのまま流用しています） <br>
http://qiita.com/b-wind/items/f3ce33a0534b03c4acc6

---

### DatePicker/TimePicker
OS独自のインターフェースで日付・時間を選択できます。 <br>

**レイアウトに配置する** <br>
https://github.com/yoshida-may/reminder_xamarin/blob/master/reminder7/reminder7/View/InputPage.xaml
```
<DatePicker x:Name="datePicker" Format="d" />
<TimePicker x:Name="timePicker" Format="t" />
```

**選択した日付・時間を取得する** <br>
https://github.com/yoshida-may/reminder_xamarin/blob/master/reminder7/reminder7/View/InputPage.xaml.cs
```
var date = datePicker.Date;
var time = timePicker.Time;
```
参考サイト（OS別UIが記載されています） <br>
http://dev.classmethod.jp/smartphone/xamarin-forms-control1/
