﻿using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Cometary.VSIX
{
    /// <summary>
    ///   Handler for the "Add Cometary task" command.
    /// </summary>
    internal sealed class AddCometaryTaskCommand
    {
        #region Static members
        /// <summary>
        ///   Gets the global instance of the command.
        /// </summary>
        public static AddCometaryTaskCommand Instance { get; private set; }

        /// <summary>
        ///   Initializes the singleton instance of the command.
        /// </summary>
        public static void Initialize(CometaryPackage package)
        {
            if (Instance == null)
                Instance = new AddCometaryTaskCommand(package);
        }

        /// <summary>
        ///   Command ID.
        /// </summary>
        public static int CommandId => Ids.AddTaskCommandID;

        /// <summary>
        ///   Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid(Guids.CometaryCommandPackageCmdSet);
        #endregion

        /// <summary>
        ///   Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider => this.package;

        /// <summary>
        ///   VS Package that provides this command, not null.
        /// </summary>
        private readonly CometaryPackage package;

        /// <summary>
        ///   Initializes a new instance of the <see cref="AddCometaryTaskCommand"/> class.
        ///   Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        private AddCometaryTaskCommand(CometaryPackage package)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));


            if (ServiceProvider.GetService(typeof(IMenuCommandService)) is OleMenuCommandService commandService)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            string message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.GetType().FullName);
            string title = "AddCometaryTaskCommand";

            // Show a message box to prove we were here
            VsShellUtilities.ShowMessageBox(
                this.ServiceProvider,
                message,
                title,
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }
    }
}
