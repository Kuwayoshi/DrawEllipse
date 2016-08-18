using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace DrawEllipse
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 現在描画中かを示すフラグ
        /// </summary>
        private bool isDrawing = false;

        /// <summary>
        /// 現在描画中の円
        /// </summary>
        private Ellipse drawingEllipse = null;

        /// <summary>
        /// 描画する円の中心座標
        /// </summary>
        private Point centerPoint = new Point(0, 0);

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 円描画の開始
        /// </summary>
        /// <param name="startPoint">開始時のマウス座標</param>
        private void StartDrawing(Point startPoint)
        {
            // 開始時の座標を中心点とする
            this.centerPoint = startPoint;

            // 描画フラグをONにする
            this.isDrawing = true;
        }

        /// <summary>
        /// 円描画の更新
        /// </summary>
        /// <param name="movePoint">移動後のマウス座標</param>
        private void UpdateDrawing(Point movePoint)
        {
            // 中心地点とドラッグ地点の差から、辺の長さを求める
            double dx = movePoint.X - this.centerPoint.X;
            double dy = movePoint.Y - this.centerPoint.Y;

            // 辺の長さから、2点間の距離（円の半径）を求める
            int radius = (int)Math.Sqrt(dx * dx + dy * dy);

            // 半径を2倍し、円の直径を求める
            int diameter = radius * 2;

            // 中心点から半径を引き、左上の座標を求める
            double left = centerPoint.X - (radius);
            double top = centerPoint.Y - (radius);

            // 描画中の円がある場合は、まず画面から削除
            if (this.drawingEllipse != null)
            {
                this.ellipseCanvas.Children.Remove(this.drawingEllipse);
            }

            // 新しい円の作成（描画中の円を更新）
            this.drawingEllipse = new Ellipse();
            this.drawingEllipse.Stroke = System.Windows.Media.Brushes.Green;
            this.drawingEllipse.Width = diameter;
            this.drawingEllipse.Height = diameter;
            this.drawingEllipse.Margin = new Thickness(left, top, 0, 0);

            // 新しい円を画面に追加
            this.ellipseCanvas.Children.Add(this.drawingEllipse);
        }

        /// <summary>
        /// 円描画の終了
        /// </summary>
        /// <param name="endPoint">終了時のマウス座標</param>
        private void EndDrawing(Point endPoint)
        {
            // 描画フラグをOFFにする
            this.isDrawing = false;

            // 描画終了処理
            if (this.drawingEllipse != null)
            {
                // 描画中の円を初期化
                this.drawingEllipse = null;

                // 中心点の初期化
                this.centerPoint = new Point(0, 0);
            }
        }

        /// <summary>
        /// マウスの左ボタンが押されたときのイベントハンドラ
        /// </summary>
        /// <param name="sender">イベント送信元</param>
        /// <param name="e">イベント引数</param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // マウス座標の取得
            Point mousePoint = e.GetPosition(this);

            // 描画中でないときは描画開始、描画中のときは描画終了
            if (this.isDrawing == false)
            {
                // 円描画の開始
                this.StartDrawing(mousePoint);
            }
            else
            {
                // 円描画の終了
                this.EndDrawing(mousePoint);
            }
        }

        /// <summary>
        /// マウスが動かされたときのイベントハンドラ
        /// </summary>
        /// <param name="sender">イベント送信元</param>
        /// <param name="e">イベント引数</param>
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            // マウス座標の取得
            Point mousePoint = e.GetPosition(this);

            // 描画中のときは描画更新
            if (this.isDrawing)
            {
                // 円描画の更新
                this.UpdateDrawing(mousePoint);
            }
        }

        /// <summary>
        /// マウスの左ボタンが離されたときのイベントハンドラ
        /// </summary>
        /// <param name="sender">イベント送信元</param>
        /// <param name="e">イベント引数</param>
        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // マウス座標の取得
            Point mousePoint = e.GetPosition(this);

            // 円描画の終了
            this.EndDrawing(mousePoint);
        }
    }
}