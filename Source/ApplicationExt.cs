﻿using System;
using System.Collections.Generic;
using System.Windows;

namespace SkyEdge
{
    /// <summary>
    /// The application implementation.
    /// </summary>
    public class ApplicationExt : Application
    {
        static readonly Dictionary<Type, DriverBase> map = new Dictionary<Type, DriverBase>();

        internal static readonly Dictionary<string, WrapBase> features = new Dictionary<string, WrapBase>();

        static readonly ApplicationLogger logger;

        // logging level
        static int logging;


        static ApplicationExt()
        {
            // file-based logger
            logging = 3;
            var logfile = DateTime.Now.ToString("yyyyMM") + ".log";
            logger = new ApplicationLogger(logfile)
            {
                Level = logging
            };
        }

        public static void MakeDriver<F, W, D>(string name) where F : IFeature where W : WrapBase<F>, F, new() where D : DriverBase, F, new()
        {
            var typ = typeof(D);
            if (!map.TryGetValue(typ, out var drv))
            {
                drv = new D();
                map.Add(typ, drv);
            }
            if (!features.TryGetValue(name, out var wrap))
            {
                wrap = new W();
                ((WrapBase<F>) wrap).Add((D) drv);
                features.Add(name, wrap);
            }
            else
            {
                ((WrapBase<F>) wrap).Add((D) drv);
            }
        }

        public static void MakeDriver<F, W, D1, D2>(string name) where F : IFeature where W : WrapBase<F>, F, new() where D1 : DriverBase, F, new() where D2 : DriverBase, F, new()
        {
            MakeDriver<F, W, D1>(name);
            MakeDriver<F, W, D2>(name);
        }

        protected static void Start()
        {
            var mainwin = new MainWindow()
            {
                Title = "SkyEdge",
                WindowStyle = WindowStyle.SingleBorderWindow,
                WindowState = WindowState.Maximized
            };
            var app = new ApplicationExt()
            {
                MainWindow = mainwin,
                ShutdownMode = ShutdownMode.OnMainWindowClose,
            };
            mainwin.Show();
            app.Run();
        }
    }
}