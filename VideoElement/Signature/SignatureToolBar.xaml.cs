using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VideoElement.Signature
{
    /// <summary>
    /// SignatureToolBar.xaml 的交互逻辑
    /// </summary>
    public partial class SignatureToolBar : UserControl
    {
        public static readonly RoutedEvent SaveSignatureEvent = EventManager.RegisterRoutedEvent("OnSaveSignature", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(SignatureToolBar));
        public event RoutedEventHandler OnSaveSignature
        {
            add { AddHandler(SaveSignatureEvent, value); }
            remove { RemoveHandler(SaveSignatureEvent, value); }
        }
        public static readonly RoutedEvent ClearSignatureEvent = EventManager.RegisterRoutedEvent("OnClearSignature", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(SignatureToolBar));
        public event RoutedEventHandler OnClearSignature
        {
            add { AddHandler(ClearSignatureEvent, value); }
            remove { RemoveHandler(ClearSignatureEvent, value); }
        }
        public static readonly RoutedEvent EraserSignatureEvent = EventManager.RegisterRoutedEvent("OnEraserSignature", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(SignatureToolBar));
        public event RoutedEventHandler OnEraserSignature
        {
            add { AddHandler(EraserSignatureEvent, value); }
            remove { RemoveHandler(EraserSignatureEvent, value); }
        }
        public static readonly RoutedEvent ChangeBrushColorEvent = EventManager.RegisterRoutedEvent("OnChangeBrushColor", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(SignatureToolBar));
        public event RoutedEventHandler OnChangeBrushColor
        {
            add { AddHandler(ChangeBrushColorEvent, value); }
            remove { RemoveHandler(ChangeBrushColorEvent, value); }
        }
        public static readonly RoutedEvent ChangePenWeightEvent = EventManager.RegisterRoutedEvent("OnChangePenWeight", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(SignatureToolBar));
        public event RoutedEventHandler OnChangePenWeight
        {
            add { AddHandler(ChangePenWeightEvent, value); }
            remove { RemoveHandler(ChangePenWeightEvent, value); }
        }
        private SignatureToolItem currentColorBrush = null;
        //public static readonly RoutedEvent OpenColorChooseEvent = EventManager.RegisterRoutedEvent("OpenColorChoose", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(SignatureToolBar));
        //public event RoutedEventHandler OnOpenColorChoose
        //{
        //    add { AddHandler(OpenColorChooseEvent, value); }
        //    remove { RemoveHandler(OpenColorChooseEvent, value); }
        //}
        public SignatureToolBar()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(SignatureToolBar_Loaded);
        }

        void SignatureToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            currentColorBrush = blackSign;
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void save_Click(object sender, RoutedEventArgs e)
        {
            this.penBar.Visibility = Visibility.Collapsed;
            this.eraserTool.IsChecked = false;
            if (currentColorBrush != null)
                currentColorBrush.IsChecked = true;
            //this.colorBar.Visibility = Visibility.Collapsed;
            this.RaiseEvent(new RoutedEventArgs(SignatureToolBar.SaveSignatureEvent));
        }
        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clear_Click(object sender, RoutedEventArgs e)
        {
            this.penBar.Visibility = Visibility.Collapsed;
            //this.colorBar.Visibility = Visibility.Collapsed;
            this.RaiseEvent(new RoutedEventArgs(SignatureToolBar.ClearSignatureEvent));
        }
        /// <summary>
        /// 橡皮擦
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eraser_Click(object sender, RoutedEventArgs e)
        {
            this.penBar.Visibility = Visibility.Collapsed;
            
            foreach (var item in stackpanel.Children)
            {
                if (item is SignatureToolItem)
                {
                    ((SignatureToolItem)item).IsChecked = false;
                }
            }
            eraserTool.IsChecked = true;
            //this.colorBar.Visibility = Visibility.Collapsed;
            this.RaiseEvent(new RoutedEventArgs(SignatureToolBar.EraserSignatureEvent, eraserTool.IsChecked));
        }
        private void ShowPenBar(object sender, RoutedEventArgs e)
        {
            this.penBar.Visibility = this.penBar.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            //this.colorBar.Visibility = Visibility.Collapsed;
        }
        private void ShowColorBar(object sender, RoutedEventArgs e)
        {
            //this.colorBar.Visibility = this.colorBar.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            this.penBar.Visibility = Visibility.Collapsed;
        }
        private void ChangePenWeight(object sender, RoutedEventArgs e)
        {
            EffectButton _sender = sender as EffectButton;
            this.RaiseEvent(new RoutedEventArgs(ChangePenWeightEvent, _sender.Tag));
            this.penBar.Visibility = Visibility.Collapsed;
        }
        private void ChangeColor(object sender, RoutedEventArgs e)
        {
            SignatureToolItem _sender = sender as SignatureToolItem;
            SolidColorBrush _brush = null;
            foreach (var item in stackpanel.Children)
            {
                if (item is SignatureToolItem)
                {
                    ((SignatureToolItem)item).IsChecked = false;
                }
            }
            _sender.IsChecked = true;
           
            switch (_sender.Tag as String)
            {
                case "black":
                    _brush = Brushes.Black;
                    break;
                case "red":
                    _brush = Brushes.Red;
                    break;
                case "yellow":
                    _brush = Brushes.Yellow;
                    break;
                case "blue":
                    _brush = Brushes.Blue;
                    break;
                case "green":
                    _brush = Brushes.Green;
                    break;
                case "white":
                    _brush = Brushes.White;
                    break;
            }
            if (_brush != null)
            {
                //this.colorBar.Visibility = Visibility.Collapsed;
                //this.choosedImg.Source = _sender.Source;
                currentColorBrush = _sender;
                this.RaiseEvent(new RoutedEventArgs(ChangeBrushColorEvent, _brush));
            }
        }
        public void ShowSuccessImage(bool visible)
        {
            saveSuccessedImg.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
        }
        public void Reset()
        {
            //this.colorBar.Visibility = Visibility.Collapsed;
            this.penBar.Visibility = Visibility.Collapsed;
            foreach (var item in stackpanel.Children)
            {
                if (item is SignatureToolItem)
                {
                    ((SignatureToolItem)item).IsChecked = false;
                }
            }
            blackSign.IsChecked = true;
            saveSuccessedImg.Visibility = Visibility.Collapsed;
            currentColorBrush = blackSign;
        }
    }
}
