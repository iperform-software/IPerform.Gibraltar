using System;
using System.Collections.Generic;
using System.Text;
using PostSharp.Extensibility;
using PostSharp.Aspects;
using Gibraltar.Agent;

namespace IPerform.Gibraltar.Aspects
{
    [Serializable]
    public class UseGibraltarAttribute : OnMethodBoundaryAspect
    {
        readonly string companyName;
        readonly string serviceName;
        readonly string privacyPolicyUrl;
        readonly bool autoSendConsent;
        readonly bool consentDefault;
        readonly int promptUserOnStartupLimit;

        public UseGibraltarAttribute(string companyName, string serviceName, string privacyPolicyUrl, bool autoSendConsent, bool consentDefault, int promptUserOnStartupLimit)
        {
            this.companyName = companyName;
            this.serviceName = serviceName;
            this.privacyPolicyUrl = privacyPolicyUrl;
            this.autoSendConsent = autoSendConsent;
            this.consentDefault = consentDefault;
            this.promptUserOnStartupLimit = promptUserOnStartupLimit;
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            Log.Initializing += Log_Initializing;

            Log.DisplayStartupConsentDialog();

            Log.StartSession("Application starting.");
        }

        void Log_Initializing(object sender, LogInitializingEventArgs e)
        {
            e.Configuration.AutoSendConsent.CompanyName = companyName;
            e.Configuration.AutoSendConsent.ServiceName = serviceName;
            e.Configuration.AutoSendConsent.PrivacyPolicyUrl = privacyPolicyUrl;
            e.Configuration.AutoSendConsent.Enabled = autoSendConsent;
            e.Configuration.AutoSendConsent.ConsentDefault = consentDefault;
            e.Configuration.AutoSendConsent.PromptUserOnStartupLimit = promptUserOnStartupLimit;
        }

        public override void OnException(MethodExecutionArgs args)
        {
            Log.Error(args.Exception, "exception", "", "", null);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            Log.EndSession("Application stopping.");
        }
    }
}
