using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using CuoreUI.Controls;
using CuoreUI.Components;

namespace CambiadorFondos
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FormPrincipal());
        }
    }

    static class IconHelper
    {
        public static Bitmap Create(int size, Action<Graphics, int> draw)
        {
            var bmp = new Bitmap(size, size, PixelFormat.Format32bppArgb);
            using var g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.Transparent);
            draw(g, size);
            return bmp;
        }

        static void WithBrush(Graphics g, Color c, Action<Graphics, Brush> draw)
        {
            using var b = new SolidBrush(c);
            draw(g, b);
        }

        static void WithPen(Graphics g, Color c, float w, Action<Graphics, Pen> draw)
        {
            using var p = new Pen(c, w);
            draw(g, p);
        }

        public static Bitmap Theme(int size = 24)
        {
            return Create(size, (g, s) =>
            {
                float r = s / 2f - 1;
                float cx = s / 2f, cy = s / 2f;
                WithPen(g, Color.White, 1.5f, (g2, p) =>
                {
                    g2.DrawEllipse(p, cx - r, cy - r, r * 2, r * 2);
                });
                WithBrush(g, Color.White, (g2, b) =>
                {
                    g2.FillEllipse(b, cx - r * 0.3f, cy - r * 0.5f, r * 0.9f, r);
                });
            });
        }

        public static Bitmap Folder(int size = 24)
        {
            return Create(size, (g, s) =>
            {
                float w = s - 4, h = s * 0.65f;
                float x = 2, y = s * 0.25f;
                var path = new GraphicsPath();
                path.AddLine(x + w * 0.2f, y, x + w * 0.35f, y);
                path.AddLine(x + w * 0.4f, y + h * 0.25f, x + w * 0.55f, y + h * 0.25f);
                path.AddLine(x + w, y + h * 0.25f, x + w, y + h);
                path.AddLine(x + w, y + h, x, y + h);
                path.CloseFigure();
                using var b = new SolidBrush(Color.White);
                g.FillPath(b, path);
            });
        }

        public static Bitmap ImageIcon(int size = 24)
        {
            return Create(size, (g, s) =>
            {
                float m = 2;
                var r = new RectangleF(m, m, s - m * 2, s - m * 2);
                using var p = new Pen(Color.White, 1.5f);
                g.DrawRectangle(p, r.X, r.Y, r.Width, r.Height);
                var pts = new PointF[]
                {
                    new(r.X + 2, r.Bottom - 4),
                    new(r.X + r.Width * 0.35f, r.Y + r.Height * 0.4f),
                    new(r.X + r.Width * 0.6f, r.Bottom - 4),
                    new(r.Right - 2, r.Y + 4)
                };
                g.DrawLines(p, pts);
            });
        }

        public static Bitmap Check(int size = 24)
        {
            return Create(size, (g, s) =>
            {
                float m = 4;
                using var p = new Pen(Color.White, 2.5f) { StartCap = LineCap.Round, EndCap = LineCap.Round };
                g.DrawLines(p, new PointF[]
                {
                    new(m, s * 0.55f),
                    new(s * 0.4f, s * 0.75f),
                    new(s - m, s * 0.25f)
                });
            });
        }

        public static Bitmap Cross(int size = 24)
        {
            return Create(size, (g, s) =>
            {
                float m = 5;
                using var p = new Pen(Color.White, 2f) { StartCap = LineCap.Round, EndCap = LineCap.Round };
                g.DrawLine(p, m, m, s - m, s - m);
                g.DrawLine(p, s - m, m, m, s - m);
            });
        }

        public static Bitmap Clock(int size = 24)
        {
            return Create(size, (g, s) =>
            {
                float cx = s / 2f, cy = s / 2f, r = s / 2f - 2;
                using var p = new Pen(Color.White, 1.5f);
                g.DrawEllipse(p, cx - r, cy - r, r * 2, r * 2);
                g.DrawLine(p, cx, cy, cx, cy - r * 0.6f);
                g.DrawLine(p, cx, cy, cx + r * 0.45f, cy);
            });
        }

        public static Bitmap Refresh(int size = 24)
        {
            return Create(size, (g, s) =>
            {
                float cx = s / 2f, cy = s / 2f, r = s / 2f - 3;
                using var p = new Pen(Color.White, 2f) { StartCap = LineCap.Round, EndCap = LineCap.ArrowAnchor };
                g.DrawArc(p, cx - r, cy - r, r * 2, r * 2, 135, 270);
            });
        }

        public static Bitmap Power(int size = 24)
        {
            return Create(size, (g, s) =>
            {
                float cx = s / 2f, cy = s / 2f, r = s / 2f - 3;
                using var p = new Pen(Color.White, 2f) { StartCap = LineCap.Round, EndCap = LineCap.Round };
                g.DrawArc(p, cx - r, cy - r * 0.5f, r * 2, r, 180, 180);
                g.DrawLine(p, cx, cy - r * 0.4f, cx, cy + r * 0.3f);
            });
        }

        public static Bitmap Monitor(int size = 24)
        {
            return Create(size, (g, s) =>
            {
                float m = 3, w = s - m * 2, h = w * 0.7f;
                using var p = new Pen(Color.White, 1.5f);
                g.DrawRectangle(p, m, m, w, h);
                g.DrawLine(p, m + w * 0.3f, m + h, m + w * 0.7f, m + h);
                g.DrawLine(p, m + w * 0.5f, m + h, m + w * 0.5f, m + h + 4);
            });
        }

        public static Bitmap Search(int size = 24)
        {
            return Create(size, (g, s) =>
            {
                float cx = s * 0.38f, cy = s * 0.38f, r = s * 0.25f;
                using var p = new Pen(Color.White, 2f) { StartCap = LineCap.Round, EndCap = LineCap.Round };
                g.DrawEllipse(p, cx - r, cy - r, r * 2, r * 2);
                g.DrawLine(p, cx + r * 0.7f, cy + r * 0.7f, s * 0.82f, s * 0.82f);
            });
        }
    }

    public class FormPrincipal : Form
    {
        [DllImport("dwmapi.dll", PreserveSig = true)]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(uint uAction, uint uParam, string lvParam, uint fuWinIni);
        private const uint SPI_SETDESKWALLPAPER = 0x0014;
        private const uint SPIF_UPDATEINIFILE = 0x01;
        private const uint SPIF_SENDCHANGE = 0x02;

        private static readonly string CarpetaApp = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string ArchivoConfig = Path.Combine(CarpetaApp, "config_desktop.txt");
        private static readonly string CarpetaBoveda = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "BovedaFondos");

        private enum EstiloWallpaper
        {
            Fill = 0, Fit = 1, Stretch = 2, Tile = 3, Center = 4, Span = 5
        }

        private List<string> rutasMonitores = new();
        private bool modoOscuro = true;
        private EstiloWallpaper estiloActual = EstiloWallpaper.Fill;
        private bool autoInicio = false;
        private bool temporizadorActivo = false;
        private int intervaloMinutos = 30;
        private NotifyIcon? notifyIcon;
        private System.Windows.Forms.Timer? timerCambio;

        private cuiPanel? panelLateral, panelContenido;
        private Label? lblTitulo;
        private cuiButton? btnAplicar, btnModoColor;
        private cuiListbox? listBoveda;
        private cuiButton? btnAbrirBoveda;
        private cuiSwitch? swAutoInicio;
        private cuiCheckbox? chkTemporizador;
        private NumericUpDown? nudIntervalo;
        private cuiComboBox? cmbEstilo;
        private FlowLayoutPanel? panelMonitores;
        private ToolStripMenuItem? menuTrayAplicar, menuTrayTema;
        private cuiProgressBarHorizontal? progressBar;
        private cuiFormRounder? formRounder;

        private Color colorFondoOscuro = Color.FromArgb(18, 18, 22);
        private Color colorFondoClaro = Color.FromArgb(243, 243, 247);
        private Color colorLateralOscuro = Color.FromArgb(25, 25, 30);
        private Color colorLateralClaro = Color.FromArgb(230, 230, 235);
        private Color colorBotonOscuro = Color.FromArgb(45, 45, 50);
        private Color colorBotonClaro = Color.FromArgb(210, 210, 215);
        private Color colorAccent = Color.FromArgb(0, 120, 215);
        private Color colorAccentHover = Color.FromArgb(0, 100, 190);
        private Color colorSuperficieOscuro = Color.FromArgb(30, 30, 35);
        private Color colorSuperficieClaro = Color.FromArgb(250, 250, 252);

        private static readonly string AutoInicioClave = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        private static readonly string AutoInicioNombre = "FlexWallpaper";

        public FormPrincipal()
        {
            if (!Directory.Exists(CarpetaBoveda))
                Directory.CreateDirectory(CarpetaBoveda);

            ConfigurarVentana();
            InicializarBandeja();
            InicializarEstructura();
            InicializarTemporizador();
            CargarConfiguracion();
            AplicarTema();
            ActualizarListaBoveda();
            ActualizarEstadoAutoInicio();
        }

        private void ConfigurarVentana()
        {
            this.Text = "FlexWallpaper - Multi-Monitor";
            this.Size = new Size(820, 620);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AllowDrop = true;
            this.BackColor = colorFondoOscuro;

            formRounder = new cuiFormRounder
            {
                TargetForm = this,
                Rounding = 8,
                OutlineColor = Color.Transparent
            };
        }

        private void InicializarBandeja()
        {
            notifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Application,
                Text = "FlexWallpaper",
                Visible = true
            };

            menuTrayAplicar = new ToolStripMenuItem("Aplicar fondos guardados");
            menuTrayTema = new ToolStripMenuItem("Alternar tema");
            var menuTraySalir = new ToolStripMenuItem("Salir");
            var menuTrayMostrar = new ToolStripMenuItem("Mostrar ventana");

            menuTrayAplicar.Click += (s, e) => AplicarDesdeBoveda();
            menuTrayTema.Click += (s, e) => { modoOscuro = !modoOscuro; AplicarTema(); };
            menuTraySalir.Click += (s, e) => { notifyIcon.Visible = false; Application.Exit(); };
            menuTrayMostrar.Click += (s, e) => { this.Show(); this.WindowState = FormWindowState.Normal; this.BringToFront(); };

            notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.AddRange(new ToolStripItem[] {
                menuTrayMostrar, menuTrayAplicar, menuTrayTema, new ToolStripSeparator(), menuTraySalir
            });

            notifyIcon.DoubleClick += (s, e) => { this.Show(); this.WindowState = FormWindowState.Normal; this.BringToFront(); };

            this.Resize += (s, e) =>
            {
                if (this.WindowState == FormWindowState.Minimized)
                    this.Hide();
            };
        }

        private void InicializarTemporizador()
        {
            timerCambio = new System.Windows.Forms.Timer();
            timerCambio.Tick += (s, e) => AplicarDesdeBoveda();
        }

        private void InicializarEstructura()
        {
            InicializarPanelLateral();
            InicializarPanelContenido();

            this.Controls.Add(panelContenido);
            this.Controls.Add(panelLateral);
        }

        private void InicializarPanelLateral()
        {
            panelLateral = new cuiPanel
            {
                Dock = DockStyle.Left,
                Width = 250,
                PanelColor = colorLateralOscuro,
                Rounding = new Padding(0),
                PanelOutlineColor = Color.Transparent,
                OutlineThickness = 0
            };

            var lblBovedaTitulo = new Label
            {
                Text = "BÓVEDA DE FONDOS",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 35,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(15, 0, 0, 0),
                ForeColor = Color.White
            };

            listBoveda = new cuiListbox
            {
                Dock = DockStyle.Top,
                Height = 200,
                Font = new Font("Segoe UI", 9.5f),
                BorderStyle = BorderStyle.None,
                Margin = new Padding(0, 5, 0, 5),
                BackColor = Color.FromArgb(35, 35, 40),
                ForeColor = Color.White,
                ItemRounding = 4,
                ItemHoverBackgroundColor = Color.FromArgb(50, 50, 55)
            };
            listBoveda.DoubleClick += ListBoveda_DoubleClick;
            listBoveda.MouseUp += ListBoveda_MouseUp;

            btnAbrirBoveda = new cuiButton
            {
                Dock = DockStyle.Bottom,
                Height = 38,
                Content = " Abrir Carpeta Bóveda",
                Image = IconHelper.Folder(),
                NormalImageTint = Color.White,
                HoverImageTint = Color.White,
                PressedImageTint = Color.LightGray,
                Rounding = new Padding(6),
                TextSpacing = 6,
                Font = new Font("Segoe UI", 9.5f),
                NormalBackground = colorBotonOscuro,
                NormalForeColor = Color.White,
                NormalOutline = Color.Transparent,
                HoverBackground = Color.FromArgb(60, 60, 65),
                HoverForeColor = Color.White,
                PressedBackground = Color.FromArgb(35, 35, 40),
                PressedForeColor = Color.FromArgb(200, 200, 200),
                OutlineThickness = 0,
                Margin = new Padding(10, 0, 10, 10)
            };
            btnAbrirBoveda.Click += (s, e) => System.Diagnostics.Process.Start("explorer.exe", CarpetaBoveda);

            var separator = new cuiSeparator
            {
                Dock = DockStyle.Bottom,
                Height = 1,
                ForeColor = Color.FromArgb(60, 60, 65),
                Thickness = 1
            };

            panelLateral.Controls.AddRange(new Control[] {
                btnAbrirBoveda, separator, listBoveda, lblBovedaTitulo
            });
        }

        private void InicializarPanelContenido()
        {
            panelContenido = new cuiPanel
            {
                Dock = DockStyle.Fill,
                PanelColor = colorFondoOscuro,
                Rounding = new Padding(0),
                PanelOutlineColor = Color.Transparent,
                OutlineThickness = 0,
                Padding = new Padding(25),
                AutoScroll = true
            };

            lblTitulo = new Label
            {
                Text = "Configuración de Pantallas",
                Font = new Font("Segoe UI Semibold", 16, FontStyle.Regular),
                Bounds = new Rectangle(25, 15, 320, 35),
                ForeColor = Color.White
            };

            btnModoColor = new cuiButton
            {
                Bounds = new Rectangle(455, 14, 100, 38),
                Content = "Modo claro",
                Image = IconHelper.Theme(),
                NormalImageTint = Color.White,
                HoverImageTint = Color.White,
                PressedImageTint = Color.LightGray,
                Rounding = new Padding(6),
                TextSpacing = 6,
                Font = new Font("Segoe UI", 9.5f),
                NormalBackground = colorBotonOscuro,
                NormalForeColor = Color.White,
                NormalOutline = Color.Transparent,
                HoverBackground = Color.FromArgb(60, 60, 65),
                HoverForeColor = Color.White,
                PressedBackground = Color.FromArgb(35, 35, 40),
                PressedForeColor = Color.FromArgb(200, 200, 200),
                OutlineThickness = 0
            };
            btnModoColor.Click += (s, e) => { modoOscuro = !modoOscuro; AplicarTema(); };

            var lblEstilo = new Label
            {
                Text = "Estilo de ajuste:",
                Font = new Font("Segoe UI", 9.5f),
                Bounds = new Rectangle(25, 60, 120, 28),
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.White
            };

            cmbEstilo = new cuiComboBox
            {
                Bounds = new Rectangle(150, 58, 180, 30),
                Font = new Font("Segoe UI", 9.5f),
                Rounding = 4,
                BackColor = Color.FromArgb(35, 35, 40),
                ForeColor = Color.White,
                BackgroundColor = Color.FromArgb(35, 35, 40),
                OutlineColor = Color.FromArgb(60, 60, 65),
                DropDownBackgroundColor = Color.FromArgb(35, 35, 40),
                DropDownForeColor = Color.White,
                ExpandArrowColor = Color.White,
                Items = Enum.GetNames<EstiloWallpaper>(),
                SelectedIndex = (int)estiloActual,
                OutlineThickness = 1
            };
            cmbEstilo.SelectedIndexChanged += (s, e) =>
            {
                if (cmbEstilo != null)
                {
                    estiloActual = (EstiloWallpaper)cmbEstilo.SelectedIndex;
                    GuardarConfiguracion();
                }
            };

            panelMonitores = new FlowLayoutPanel
            {
                Bounds = new Rectangle(25, 100, 490, 320),
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false
            };
            ReconstruirPanelesMonitor();

            var lblAutoInicio = new Label
            {
                Text = "Inicio automático con Windows:",
                Font = new Font("Segoe UI", 9f),
                Bounds = new Rectangle(25, 435, 200, 25),
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.White
            };

            swAutoInicio = new cuiSwitch
            {
                Bounds = new Rectangle(230, 435, 44, 22),
                Checked = false
            };
            swAutoInicio.CheckedChanged += (s, e) =>
            {
                if (swAutoInicio != null)
                {
                    autoInicio = swAutoInicio.Checked;
                    ConfigurarAutoInicio();
                    GuardarConfiguracion();
                }
            };

            var lblTemporizador = new Label
            {
                Text = "Cambio automático cada:",
                Font = new Font("Segoe UI", 9f),
                Bounds = new Rectangle(25, 470, 170, 25),
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.White
            };

            chkTemporizador = new cuiCheckbox
            {
                Bounds = new Rectangle(195, 470, 18, 25),
                BackColor = Color.FromArgb(35, 35, 40),
                ForeColor = Color.White,
                UncheckedOutlineColor = Color.FromArgb(60, 60, 65),
                CheckedOutlineColor = colorAccent,
                UncheckedSymbolColor = Color.Transparent,
                CheckedSymbolColor = Color.White,
                CheckedForeground = colorAccent,
                UncheckedForeground = Color.FromArgb(35, 35, 40)
            };
            chkTemporizador.CheckedChanged += (s, e) =>
            {
                if (chkTemporizador != null)
                {
                    temporizadorActivo = chkTemporizador.Checked;
                    ActualizarTemporizador();
                    GuardarConfiguracion();
                }
            };

            nudIntervalo = new NumericUpDown
            {
                Bounds = new Rectangle(218, 468, 65, 25),
                Minimum = 1,
                Maximum = 999,
                Value = intervaloMinutos,
                BackColor = Color.FromArgb(35, 35, 40),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            nudIntervalo.ValueChanged += (s, e) =>
            {
                if (nudIntervalo != null)
                {
                    intervaloMinutos = (int)nudIntervalo.Value;
                    ActualizarTemporizador();
                    GuardarConfiguracion();
                }
            };

            var lblMinutos = new Label
            {
                Text = "min",
                Font = new Font("Segoe UI", 9f),
                Bounds = new Rectangle(288, 470, 30, 25),
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.White
            };

            btnAplicar = new cuiButton
            {
                Bounds = new Rectangle(25, 505, 490, 48),
                Content = "APLICAR FONDOS",
                Image = IconHelper.Check(),
                NormalImageTint = Color.White,
                HoverImageTint = Color.White,
                Rounding = new Padding(8),
                TextSpacing = 8,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                NormalBackground = colorAccent,
                NormalForeColor = Color.White,
                NormalOutline = Color.Transparent,
                HoverBackground = Color.FromArgb(0, 100, 190),
                HoverForeColor = Color.White,
                PressedBackground = Color.FromArgb(0, 80, 160),
                PressedForeColor = Color.FromArgb(220, 220, 220),
                OutlineThickness = 0
            };
            btnAplicar.Click += BtnAplicar_Click;

            progressBar = new cuiProgressBarHorizontal
            {
                Bounds = new Rectangle(25, 558, 490, 8),
                MaxValue = 100,
                Value = 0,
                Background = Color.FromArgb(45, 45, 50),
                Foreground = colorAccent,
                Rounding = 4,
                Visible = false
            };

            AddTooltips();

            panelContenido.Controls.AddRange(new Control[] {
                lblTitulo, btnModoColor, lblEstilo, cmbEstilo,
                panelMonitores, lblAutoInicio, swAutoInicio,
                lblTemporizador, chkTemporizador, nudIntervalo, lblMinutos,
                btnAplicar, progressBar
            });
        }

        private void AddTooltips()
        {
            var tpAplicar = new cuiTooltipHover
            {
                TargetControl = btnAplicar,
                Content = "Aplica las imágenes seleccionadas como fondo de pantalla en todos los monitores",
                ForeColor = Color.White,
                BackColor = Color.FromArgb(40, 40, 45),
                TooltipPosition = cuiTooltipHover.Position.Top
            };

            var tpTema = new cuiTooltipHover
            {
                TargetControl = btnModoColor,
                Content = "Alternar entre modo oscuro y modo claro",
                ForeColor = Color.White,
                BackColor = Color.FromArgb(40, 40, 45),
                TooltipPosition = cuiTooltipHover.Position.Bottom
            };

            var tpBoveda = new cuiTooltipHover
            {
                TargetControl = btnAbrirBoveda,
                Content = "Abrir la carpeta donde se guardan los combos de fondos",
                ForeColor = Color.White,
                BackColor = Color.FromArgb(40, 40, 45),
                TooltipPosition = cuiTooltipHover.Position.Right
            };

            var tpAutoInicio = new cuiTooltipHover
            {
                TargetControl = swAutoInicio,
                Content = "Iniciar FlexWallpaper automáticamente al iniciar Windows",
                ForeColor = Color.White,
                BackColor = Color.FromArgb(40, 40, 45),
                TooltipPosition = cuiTooltipHover.Position.Top
            };

            var tpTimer = new cuiTooltipHover
            {
                TargetControl = chkTemporizador,
                Content = "Cambiar el fondo de pantalla automáticamente cada cierto intervalo",
                ForeColor = Color.White,
                BackColor = Color.FromArgb(40, 40, 45),
                TooltipPosition = cuiTooltipHover.Position.Top
            };
        }

        private void ReconstruirPanelesMonitor()
        {
            if (panelMonitores == null) return;
            panelMonitores.Controls.Clear();

            while (rutasMonitores.Count < Screen.AllScreens.Length)
                rutasMonitores.Add("");
            while (rutasMonitores.Count > Screen.AllScreens.Length)
                rutasMonitores.RemoveAt(rutasMonitores.Count - 1);

            for (int i = 0; i < Screen.AllScreens.Length; i++)
            {
                int idx = i;
                var screen = Screen.AllScreens[i];
                var bounds = screen.Bounds;

                var panelMon = new cuiPanel
                {
                    Width = 470,
                    Height = 105,
                    Margin = new Padding(0, 0, 0, 8),
                    BorderStyle = BorderStyle.None,
                    AllowDrop = true,
                    Rounding = new Padding(8),
                    PanelColor = Color.FromArgb(28, 28, 33),
                    PanelOutlineColor = Color.FromArgb(50, 50, 55),
                    OutlineThickness = 1
                };

                var lblNombre = new Label
                {
                    Text = $"Monitor {i + 1}  {bounds.Width}x{bounds.Height}",
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Bounds = new Rectangle(12, 8, 350, 20),
                    ForeColor = Color.White
                };

                var picturePreview = new PictureBox
                {
                    Bounds = new Rectangle(12, 32, 64, 64),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BorderStyle = BorderStyle.None,
                    AllowDrop = true,
                    BackColor = Color.FromArgb(22, 22, 26)
                };

                if (File.Exists(rutasMonitores[idx]))
                {
                    try
                    {
                        using var fs = new FileStream(rutasMonitores[idx], FileMode.Open, FileAccess.Read);
                        using var temp = Image.FromStream(fs);
                        picturePreview.Image = new Bitmap(temp);
                    }
                    catch { }
                }

                var lblRuta = new Label
                {
                    Text = File.Exists(rutasMonitores[idx]) ? Path.GetFileName(rutasMonitores[idx]) : "Ninguna imagen seleccionada",
                    Font = new Font("Segoe UI", 8.5f),
                    Bounds = new Rectangle(84, 36, 300, 20),
                    ForeColor = Color.LightGray
                };

                var btnBuscar = new cuiButton
                {
                    Bounds = new Rectangle(84, 62, 120, 30),
                    Content = " Seleccionar",
                    Image = IconHelper.Search(),
                    NormalImageTint = Color.White,
                    HoverImageTint = Color.White,
                    Rounding = new Padding(5),
                    TextSpacing = 4,
                    Font = new Font("Segoe UI", 9f),
                    NormalBackground = colorBotonOscuro,
                    NormalForeColor = Color.White,
                    NormalOutline = Color.Transparent,
                    HoverBackground = Color.FromArgb(60, 60, 65),
                    HoverForeColor = Color.White,
                    PressedBackground = Color.FromArgb(35, 35, 40),
                    PressedForeColor = Color.FromArgb(200, 200, 200),
                    OutlineThickness = 0
                };
                btnBuscar.Click += (s, e) => SeleccionarImagen(idx);

                var btnLimpiar = new cuiButton
                {
                    Bounds = new Rectangle(210, 62, 30, 30),
                    Content = "",
                    Image = IconHelper.Cross(),
                    NormalImageTint = Color.FromArgb(200, 100, 100),
                    HoverImageTint = Color.FromArgb(255, 120, 120),
                    Rounding = new Padding(5),
                    Font = new Font("Segoe UI", 9f),
                    NormalBackground = colorBotonOscuro,
                    NormalForeColor = Color.White,
                    NormalOutline = Color.Transparent,
                    HoverBackground = Color.FromArgb(60, 60, 65),
                    HoverForeColor = Color.White,
                    PressedBackground = Color.FromArgb(35, 35, 40),
                    OutlineThickness = 0
                };
                btnLimpiar.Click += (s, e) =>
                {
                    rutasMonitores[idx] = "";
                    ReconstruirPanelesMonitor();
                    AplicarTema();
                };

                var tpBuscar = new cuiTooltipHover
                {
                    TargetControl = btnBuscar,
                    Content = "Seleccionar una imagen para este monitor",
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(40, 40, 45),
                    TooltipPosition = cuiTooltipHover.Position.Top
                };

                var tpLimpiar = new cuiTooltipHover
                {
                    TargetControl = btnLimpiar,
                    Content = "Quitar la imagen seleccionada",
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(40, 40, 45),
                    TooltipPosition = cuiTooltipHover.Position.Top
                };

                panelMon.Controls.AddRange(new Control[] { lblNombre, picturePreview, lblRuta, btnBuscar, btnLimpiar });

                panelMon.DragEnter += Monitor_DragEnter;
                panelMon.DragDrop += (s, e) => Monitor_DragDrop(s, e, idx);
                picturePreview.DragEnter += Monitor_DragEnter;
                picturePreview.DragDrop += (s, e) => Monitor_DragDrop(s, e, idx);

                panelMonitores.Controls.Add(panelMon);
            }
        }

        private void Monitor_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void Monitor_DragDrop(object? sender, DragEventArgs e, int idx)
        {
            if (e.Data == null) return;
            var archivos = (string[]?)e.Data.GetData(DataFormats.FileDrop);
            if (archivos != null && archivos.Length > 0)
            {
                var ext = Path.GetExtension(archivos[0]).ToLower();
                if (ext is ".jpg" or ".jpeg" or ".png" or ".bmp")
                {
                    rutasMonitores[idx] = archivos[0];
                    ReconstruirPanelesMonitor();
                    AplicarTema();
                    GuardarConfiguracion();
                }
            }
        }

        private void AplicarTema()
        {
            int valorModoOscuro = modoOscuro ? 1 : 0;
            DwmSetWindowAttribute(this.Handle, DWMWA_USE_IMMERSIVE_DARK_MODE, ref valorModoOscuro, sizeof(int));

            Color bgPrincipal = modoOscuro ? colorFondoOscuro : colorFondoClaro;
            Color bgLateral = modoOscuro ? colorLateralOscuro : colorLateralClaro;
            Color textColor = modoOscuro ? Color.White : Color.FromArgb(30, 30, 30);
            Color btnBg = modoOscuro ? colorBotonOscuro : colorBotonClaro;
            Color superficie = modoOscuro ? colorSuperficieOscuro : colorSuperficieClaro;
            Color lateralText = modoOscuro ? Color.White : Color.FromArgb(30, 30, 30);

            this.BackColor = bgPrincipal;

            if (panelLateral != null)
            {
                panelLateral.PanelColor = bgLateral;
                foreach (Control c in panelLateral.Controls)
                {
                    if (c is Label l) l.ForeColor = lateralText;
                    if (c is cuiListbox lb)
                    {
                        lb.BackColor = modoOscuro ? Color.FromArgb(35, 35, 40) : Color.FromArgb(245, 245, 245);
                        lb.ForeColor = lateralText;
                        lb.ItemHoverBackgroundColor = modoOscuro ? Color.FromArgb(50, 50, 55) : Color.FromArgb(220, 220, 220);
                    }
                    if (c is cuiButton b && b != btnAbrirBoveda)
                    {
                        b.NormalBackground = btnBg;
                        b.NormalForeColor = textColor;
                        b.HoverBackground = modoOscuro ? Color.FromArgb(60, 60, 65) : Color.FromArgb(195, 195, 195);
                        b.HoverForeColor = textColor;
                        b.PressedBackground = modoOscuro ? Color.FromArgb(35, 35, 40) : Color.FromArgb(180, 180, 180);
                        b.PressedForeColor = textColor;
                        b.NormalImageTint = textColor;
                        b.HoverImageTint = textColor;
                    }
                    if (c is cuiSeparator sep)
                        sep.ForeColor = modoOscuro ? Color.FromArgb(60, 60, 65) : Color.FromArgb(200, 200, 200);
                }
            }

            if (btnAbrirBoveda != null)
            {
                btnAbrirBoveda.NormalBackground = btnBg;
                btnAbrirBoveda.NormalForeColor = textColor;
                btnAbrirBoveda.HoverBackground = modoOscuro ? Color.FromArgb(60, 60, 65) : Color.FromArgb(195, 195, 195);
                btnAbrirBoveda.HoverForeColor = textColor;
                btnAbrirBoveda.PressedBackground = modoOscuro ? Color.FromArgb(35, 35, 40) : Color.FromArgb(180, 180, 180);
                btnAbrirBoveda.PressedForeColor = textColor;
                btnAbrirBoveda.NormalImageTint = textColor;
                btnAbrirBoveda.HoverImageTint = textColor;
            }

            if (panelContenido != null)
            {
                panelContenido.PanelColor = bgPrincipal;
                foreach (Control c in panelContenido.Controls)
                {
                    if (c is Label l) l.ForeColor = textColor;
                    if (c is cuiButton b && b != btnAplicar)
                    {
                        b.NormalBackground = btnBg;
                        b.NormalForeColor = textColor;
                        b.HoverBackground = modoOscuro ? Color.FromArgb(60, 60, 65) : Color.FromArgb(195, 195, 195);
                        b.HoverForeColor = textColor;
                        b.PressedBackground = modoOscuro ? Color.FromArgb(35, 35, 40) : Color.FromArgb(180, 180, 180);
                        b.PressedForeColor = textColor;
                        b.NormalImageTint = textColor;
                        b.HoverImageTint = textColor;
                    }
                    if (c is NumericUpDown nud)
                    {
                        nud.BackColor = modoOscuro ? Color.FromArgb(35, 35, 40) : Color.White;
                        nud.ForeColor = textColor;
                    }
                }
            }

            if (btnAplicar != null)
            {
                btnAplicar.NormalBackground = colorAccent;
                btnAplicar.NormalForeColor = Color.White;
                btnAplicar.HoverBackground = colorAccentHover;
                btnAplicar.HoverForeColor = Color.White;
                btnAplicar.PressedBackground = Color.FromArgb(0, 80, 160);
                btnAplicar.PressedForeColor = Color.FromArgb(220, 220, 220);
                btnAplicar.NormalImageTint = Color.White;
                btnAplicar.HoverImageTint = Color.White;
            }

            if (btnModoColor != null)
            {
                btnModoColor.Content = modoOscuro ? "Modo claro" : "Modo oscuro";
                btnModoColor.NormalImageTint = textColor;
                btnModoColor.HoverImageTint = textColor;
            }

            if (menuTrayTema != null)
                menuTrayTema.Text = modoOscuro ? "Cambiar a modo claro" : "Cambiar a modo oscuro";

            if (cmbEstilo != null)
            {
                cmbEstilo.BackColor = modoOscuro ? Color.FromArgb(35, 35, 40) : Color.White;
                cmbEstilo.ForeColor = textColor;
                cmbEstilo.BackgroundColor = modoOscuro ? Color.FromArgb(35, 35, 40) : Color.White;
                cmbEstilo.OutlineColor = modoOscuro ? Color.FromArgb(60, 60, 65) : Color.FromArgb(200, 200, 200);
                cmbEstilo.DropDownBackgroundColor = modoOscuro ? Color.FromArgb(35, 35, 40) : Color.White;
                cmbEstilo.DropDownForeColor = textColor;
                cmbEstilo.ExpandArrowColor = textColor;
            }

            if (swAutoInicio != null)
            {
                // The switch has limited theming in CuoreUI, try to adapt colors
            }

            if (chkTemporizador != null)
            {
                chkTemporizador.BackColor = modoOscuro ? Color.FromArgb(35, 35, 40) : Color.FromArgb(220, 220, 220);
                chkTemporizador.ForeColor = textColor;
                chkTemporizador.UncheckedOutlineColor = modoOscuro ? Color.FromArgb(60, 60, 65) : Color.FromArgb(180, 180, 180);
                chkTemporizador.UncheckedForeground = modoOscuro ? Color.FromArgb(35, 35, 40) : Color.FromArgb(220, 220, 220);
            }

            if (nudIntervalo != null)
            {
                nudIntervalo.BackColor = modoOscuro ? Color.FromArgb(35, 35, 40) : Color.White;
                nudIntervalo.ForeColor = textColor;
            }

            if (progressBar != null)
            {
                progressBar.Background = modoOscuro ? Color.FromArgb(45, 45, 50) : Color.FromArgb(210, 210, 210);
                progressBar.Foreground = modoOscuro ? colorAccent : Color.FromArgb(0, 100, 190);
            }

            if (panelMonitores != null)
            {
                foreach (Control p in panelMonitores.Controls)
                {
                    if (p is cuiPanel monPanel)
                    {
                        monPanel.PanelColor = modoOscuro ? Color.FromArgb(28, 28, 33) : Color.FromArgb(245, 245, 245);
                        monPanel.PanelOutlineColor = modoOscuro ? Color.FromArgb(50, 50, 55) : Color.FromArgb(210, 210, 210);
                        foreach (Control cp in monPanel.Controls)
                        {
                            if (cp is Label lc) lc.ForeColor = textColor;
                            if (cp is PictureBox pb) pb.BackColor = modoOscuro ? Color.FromArgb(22, 22, 26) : Color.FromArgb(220, 220, 220);
                            if (cp is cuiButton bc)
                            {
                                bc.NormalBackground = btnBg;
                                bc.NormalForeColor = textColor;
                                bc.HoverBackground = modoOscuro ? Color.FromArgb(60, 60, 65) : Color.FromArgb(195, 195, 195);
                                bc.HoverForeColor = textColor;
                                bc.PressedBackground = modoOscuro ? Color.FromArgb(35, 35, 40) : Color.FromArgb(180, 180, 180);
                                bc.PressedForeColor = textColor;
                                bc.NormalImageTint = bc.Image == IconHelper.Cross() ? Color.FromArgb(200, 100, 100) : textColor;
                                bc.HoverImageTint = bc.Image == IconHelper.Cross() ? Color.FromArgb(255, 120, 120) : textColor;
                            }
                        }
                    }
                }
            }

            this.Refresh();
        }

        private async void BtnAplicar_Click(object? sender, EventArgs e)
        {
            if (progressBar != null)
            {
                progressBar.Visible = true;
                progressBar.Value = 0;
            }

            btnAplicar!.Enabled = false;
            btnAplicar.Content = "Aplicando...";

            await Task.Run(() =>
            {
                for (int step = 1; step <= 5; step++)
                {
                    this.Invoke(() =>
                    {
                        if (progressBar != null)
                            progressBar.Value = step * 20;
                    });
                    Thread.Sleep(100);
                }

                AplicarFondos();
            });

            if (progressBar != null)
            {
                progressBar.Value = 100;
                await Task.Delay(400);
                progressBar.Visible = false;
                progressBar.Value = 0;
            }

            btnAplicar.Enabled = true;
            btnAplicar.Content = "APLICAR FONDOS";
        }

        private void SeleccionarImagen(int idx)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Imagenes|*.jpg;*.jpeg;*.png;*.bmp";
                dialog.InitialDirectory = string.IsNullOrEmpty(rutasMonitores[idx]) ? Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) : Path.GetDirectoryName(rutasMonitores[idx]);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    rutasMonitores[idx] = dialog.FileName;
                    ReconstruirPanelesMonitor();
                    AplicarTema();
                    GuardarConfiguracion();
                }
            }
        }

        #region Wallpaper Logic

        private void AplicarFondos()
        {
            var rutasValidas = rutasMonitores.Where(r => File.Exists(r)).ToList();
            if (rutasValidas.Count == 0)
            {
                MessageBox.Show("Selecciona al menos una imagen para aplicar.", "Sin archivos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int anchoVirtual = SystemInformation.VirtualScreen.Width;
                int altoVirtual = SystemInformation.VirtualScreen.Height;
                int origenX = SystemInformation.VirtualScreen.Left;
                int origenY = SystemInformation.VirtualScreen.Top;

                using (Bitmap lienzoFinal = new Bitmap(anchoVirtual, altoVirtual))
                using (Graphics g = Graphics.FromImage(lienzoFinal))
                {
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    for (int i = 0; i < Screen.AllScreens.Length && i < rutasMonitores.Count; i++)
                    {
                        if (!File.Exists(rutasMonitores[i])) continue;
                        var bounds = Screen.AllScreens[i].Bounds;
                        using (Image img = Image.FromFile(rutasMonitores[i]))
                            DibujarImagenConEstilo(g, img, bounds.X - origenX, bounds.Y - origenY, bounds.Width, bounds.Height, estiloActual);
                    }

                    string rutaResultado = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "fondo_combinado_flex.png");
                    lienzoFinal.Save(rutaResultado, ImageFormat.Png);

                    RegistryKey? key = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", true);
                    if (key != null)
                    {
                        key.SetValue("WallpaperStyle", ObtenerWallpaperStyleRegistro(estiloActual));
                        key.SetValue("TileWallpaper", estiloActual == EstiloWallpaper.Tile ? "1" : "0");
                    }

                    SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, rutaResultado, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);
                }

                MessageBox.Show("Fondos aplicados con exito!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string ObtenerWallpaperStyleRegistro(EstiloWallpaper estilo) => estilo switch
        {
            EstiloWallpaper.Fill => "10",
            EstiloWallpaper.Fit => "6",
            EstiloWallpaper.Stretch => "2",
            EstiloWallpaper.Tile => "0",
            EstiloWallpaper.Center => "0",
            EstiloWallpaper.Span => "22",
            _ => "22"
        };

        private void DibujarImagenConEstilo(Graphics g, Image img, int posX, int posY, int anchoDestino, int altoDestino, EstiloWallpaper estilo)
        {
            switch (estilo)
            {
                case EstiloWallpaper.Fill:
                    DibujarImagenConAjusteRellenar(g, img, posX, posY, anchoDestino, altoDestino);
                    break;
                case EstiloWallpaper.Fit:
                    DibujarImagenConAjusteEncajar(g, img, posX, posY, anchoDestino, altoDestino);
                    break;
                case EstiloWallpaper.Stretch:
                    g.DrawImage(img, new Rectangle(posX, posY, anchoDestino, altoDestino));
                    break;
                case EstiloWallpaper.Center:
                    int cx = posX + (anchoDestino - img.Width) / 2;
                    int cy = posY + (altoDestino - img.Height) / 2;
                    g.DrawImage(img, new Rectangle(Math.Max(posX, cx), Math.Max(posY, cy), img.Width, img.Height));
                    break;
                case EstiloWallpaper.Tile:
                    using (TextureBrush tb = new TextureBrush(img))
                        g.FillRectangle(tb, new Rectangle(posX, posY, anchoDestino, altoDestino));
                    break;
                case EstiloWallpaper.Span:
                default:
                    g.DrawImage(img, new Rectangle(posX, posY, anchoDestino, altoDestino));
                    break;
            }
        }

        private void DibujarImagenConAjusteRellenar(Graphics g, Image img, int posX, int posY, int anchoDestino, int altoDestino)
        {
            double relDestino = (double)anchoDestino / altoDestino;
            double relOrigen = (double)img.Width / img.Height;
            int anchoRecorte, altoRecorte, oX = 0, oY = 0;

            if (relOrigen > relDestino)
            {
                altoRecorte = img.Height;
                anchoRecorte = (int)(img.Height * relDestino);
                oX = (img.Width - anchoRecorte) / 2;
            }
            else
            {
                anchoRecorte = img.Width;
                altoRecorte = (int)(img.Width / relDestino);
                oY = (img.Height - altoRecorte) / 2;
            }

            g.DrawImage(img, new Rectangle(posX, posY, anchoDestino, altoDestino), new Rectangle(oX, oY, anchoRecorte, altoRecorte), GraphicsUnit.Pixel);
        }

        private void DibujarImagenConAjusteEncajar(Graphics g, Image img, int posX, int posY, int anchoDestino, int altoDestino)
        {
            double rel = Math.Min((double)anchoDestino / img.Width, (double)altoDestino / img.Height);
            int anchoFinal = (int)(img.Width * rel);
            int altoFinal = (int)(img.Height * rel);
            int dX = posX + (anchoDestino - anchoFinal) / 2;
            int dY = posY + (altoDestino - altoFinal) / 2;
            g.DrawImage(img, new Rectangle(dX, dY, anchoFinal, altoFinal));
        }

        #endregion

        #region Boveda Logic

        private void ListBoveda_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && listBoveda != null && listBoveda.SelectedItem != null)
            {
                var menu = new ContextMenuStrip();
                menu.Items.Add("Aplicar", null, (s, ev) => ListBoveda_DoubleClick(s, ev));
                menu.Items.Add("Eliminar", null, (s, ev) =>
                {
                    var nombre = listBoveda.SelectedItem.ToString();
                    if (nombre == null) return;
                    if (MessageBox.Show($"Eliminar '{nombre}' de la boveda?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        foreach (var f in Directory.GetFiles(CarpetaBoveda, $"{nombre}_M*.*"))
                            try { File.Delete(f); } catch { }
                        ActualizarListaBoveda();
                    }
                });
                menu.Show(listBoveda, e.Location);
            }
        }

        private void ListBoveda_DoubleClick(object? sender, EventArgs e)
        {
            if (listBoveda == null || listBoveda.SelectedItem == null) return;
            AplicarComboBoveda(listBoveda.SelectedItem.ToString()!);
        }

        private void AplicarComboBoveda(string nombreCombo)
        {
            for (int i = 0; i < Screen.AllScreens.Length; i++)
            {
                var archivos = Directory.GetFiles(CarpetaBoveda, $"{nombreCombo}_M{i + 1}.*");
                if (archivos.Length >= 1 && i < rutasMonitores.Count)
                    rutasMonitores[i] = archivos[0];
            }

            ReconstruirPanelesMonitor();
            AplicarTema();
            GuardarConfiguracion();
            AplicarFondos();
        }

        private void AplicarDesdeBoveda()
        {
            var combos = Directory.GetFiles(CarpetaBoveda, "*_M1.*")
                .Select(f => Path.GetFileNameWithoutExtension(f))
                .Select(n => n[..^3])
                .Distinct().ToList();

            if (combos.Count == 0) return;
            var random = new Random();
            var elegido = combos[random.Next(combos.Count)];
            AplicarComboBoveda(elegido);
        }

        private void ActualizarListaBoveda()
        {
            if (listBoveda == null) return;
            listBoveda.Items.Clear();
            var archivos = Directory.GetFiles(CarpetaBoveda, "*_M1.*");
            foreach (var archivo in archivos)
            {
                string nombreOriginal = Path.GetFileNameWithoutExtension(archivo);
                string nombreCombo = nombreOriginal[..^3];
                listBoveda.Items.Add(nombreCombo);
            }
        }

        #endregion

        #region Config Logic

        private void ActualizarTemporizador()
        {
            if (timerCambio == null) return;
            timerCambio.Stop();
            if (temporizadorActivo && intervaloMinutos > 0)
                timerCambio.Interval = intervaloMinutos * 60 * 1000;
            timerCambio.Start();
        }

        private void ConfigurarAutoInicio()
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey(AutoInicioClave, true);
                if (key == null) return;
                if (autoInicio)
                    key.SetValue(AutoInicioNombre, $"\"{Application.ExecutablePath}\"");
                else
                    key.DeleteValue(AutoInicioNombre, false);
            }
            catch { }
        }

        private void ActualizarEstadoAutoInicio()
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey(AutoInicioClave, false);
                autoInicio = key?.GetValue(AutoInicioNombre) != null;
                if (swAutoInicio != null) swAutoInicio.Checked = autoInicio;
            }
            catch { }
        }

        private void GuardarConfiguracion()
        {
            try
            {
                var lineas = new List<string> { estiloActual.ToString(), modoOscuro.ToString(), autoInicio.ToString(), temporizadorActivo.ToString(), intervaloMinutos.ToString() };
                lineas.AddRange(rutasMonitores);
                File.WriteAllLines(ArchivoConfig, lineas);
            }
            catch { }
        }

        private void CargarConfiguracion()
        {
            if (!File.Exists(ArchivoConfig)) return;
            try
            {
                string[] lineas = File.ReadAllLines(ArchivoConfig);
                int idx = 0;

                bool esFormatoAntiguo = lineas.Length > 0 &&
                    !Enum.TryParse<EstiloWallpaper>(lineas[0], out _) &&
                    (lineas[0].Contains('\\') || lineas[0].Contains('.'));

                if (esFormatoAntiguo)
                {
                    if (lineas.Length > 0 && File.Exists(lineas[0])) rutasMonitores.Add(lineas[0]); else rutasMonitores.Add("");
                    if (lineas.Length > 1 && File.Exists(lineas[1])) rutasMonitores.Add(lineas[1]); else rutasMonitores.Add("");
                    if (lineas.Length > 2) bool.TryParse(lineas[2], out modoOscuro);
                }
                else
                {
                    if (lineas.Length > idx && Enum.TryParse<EstiloWallpaper>(lineas[idx], out var estilo)) { estiloActual = estilo; idx++; } else idx++;
                    if (lineas.Length > idx) bool.TryParse(lineas[idx++], out modoOscuro);
                    if (lineas.Length > idx) bool.TryParse(lineas[idx++], out autoInicio);
                    if (lineas.Length > idx) bool.TryParse(lineas[idx++], out temporizadorActivo);
                    if (lineas.Length > idx) int.TryParse(lineas[idx++], out intervaloMinutos);

                    rutasMonitores.Clear();
                    for (int i = idx; i < lineas.Length; i++)
                    {
                        if (File.Exists(lineas[i]))
                            rutasMonitores.Add(lineas[i]);
                        else
                            rutasMonitores.Add("");
                    }
                }

                while (rutasMonitores.Count < Screen.AllScreens.Length)
                    rutasMonitores.Add("");
            }
            catch { }
        }

        #endregion

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.O:
                    if (Screen.AllScreens.Length > 0) SeleccionarImagen(0);
                    return true;
                case Keys.Control | Keys.Shift | Keys.A:
                    BtnAplicar_Click(null, EventArgs.Empty);
                    return true;
                case Keys.Control | Keys.T:
                    modoOscuro = !modoOscuro;
                    AplicarTema();
                    return true;
                case Keys.Control | Keys.D1:
                    if (rutasMonitores.Count >= 1) SeleccionarImagen(0);
                    return true;
                case Keys.Control | Keys.D2:
                    if (rutasMonitores.Count >= 2) SeleccionarImagen(1);
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (notifyIcon != null)
            {
                notifyIcon.Visible = false;
                notifyIcon.Dispose();
            }
            timerCambio?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
