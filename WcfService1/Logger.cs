using log4net;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using Microsoft.ApplicationInsights.Log4NetAppender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService1
{
    public class Logger
    {
        public static void Setup()
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%date [%thread] %-5level %logger - %message%newline";
            patternLayout.ActivateOptions();

            var appInsightAppender = new ApplicationInsightsAppender
            {
                Layout = patternLayout
            };

            appInsightAppender.ActivateOptions();

            hierarchy.Root.AddAppender(appInsightAppender);
            hierarchy.Root.Level = Level.Verbose;
            hierarchy.Configured = true;

            BasicConfigurator.Configure(hierarchy, appInsightAppender);
        }
    }
}