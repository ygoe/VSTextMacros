﻿using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using System;
using System.IO;
using System.Runtime.InteropServices;
using VSTextMacros.Model;
using EnvDTE80;
using EnvDTE;

namespace VSTextMacros
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.9", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.guidVSTextMacrosPkgString)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.NoSolution_string)]
    public sealed class VSTextMacrosPackage : Package
    {
        public static VSTextMacrosPackage Current { get; private set; }

        public string MacroDirectory
        {
            get { return Path.Combine(this.UserLocalDataPath, "Macros"); }
        }

        public DTE2 DTE { get; private set; }

        public VSTextMacrosPackage()
        {
            Current = this;
        }

        protected override void Initialize()
        {
            if (!Directory.Exists(MacroDirectory))
                Directory.CreateDirectory(MacroDirectory);

            if (File.Exists(Path.Combine(MacroDirectory, "Current.xml")))
                Macro.CurrentMacro = Macro.LoadFromFile(Path.Combine(MacroDirectory, "Current.xml"));

            DTE = (DTE2)GetService(typeof(DTE));

            base.Initialize();
        }
    }
}
