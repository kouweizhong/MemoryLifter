using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MLifter.DAL.Tools;

namespace MLifter.DAL.Interfaces
{
    /// <summary>
    /// Interface which defines an extension.
    /// </summary>
    /// <remarks>Documented by Dev02, 2009-07-01</remarks>
    public interface IExtension : IParent, ICopy
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>The id.</value>
        /// <remarks>Documented by Dev02, 2009-07-01</remarks>
        Guid Id { get; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        /// <remarks>Documented by Dev02, 2009-07-01</remarks>
        [ValueCopy]
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        /// <remarks>Documented by Dev02, 2009-07-01</remarks>
        [ValueCopy]
        Version Version { get; set; }

        /// <summary>
        /// Gets or sets the type of the extension.
        /// </summary>
        /// <value>The type.</value>
        /// <remarks>Documented by Dev02, 2009-07-01</remarks>
        [ValueCopy]
        ExtensionType Type { get; set; }

        /// <summary>
        /// Gets or sets the start file.
        /// </summary>
        /// <value>The start file.</value>
        /// <remarks>Documented by Dev02, 2009-07-01</remarks>
        [ValueCopy]
        string StartFile { get; set; }

        /// <summary>
        /// Gets or sets the data stream.
        /// </summary>
        /// <value>The data.</value>
        /// <remarks>Documented by Dev02, 2009-07-01</remarks>
        [IgnoreCopy]
        Stream Data { get; set; }

        /// <summary>
        /// Gets the actions.
        /// </summary>
        /// <value>The actions.</value>
        /// <remarks>Documented by Dev02, 2009-07-01</remarks>
        [IgnoreCopy]
        IList<ExtensionAction> Actions { get; }

        /// <summary>
        /// Extracts the data.
        /// </summary>
        /// <param name="targetPath">The target path.</param>
        /// <remarks>Documented by Dev02, 2009-07-03</remarks>
        void ExtractData(string targetPath);
    }

    /// <summary>
    /// A pair of the actions that should be done and their frequencies.
    /// </summary>
    /// <remarks>Documented by Dev02, 2009-07-01</remarks>
    [Serializable()]
    public struct ExtensionAction
    {
        /// <summary>
        /// Gets or sets the kind of action extension.
        /// </summary>
        /// <value>The kind.</value>
        /// <remarks>Documented by Dev02, 2009-07-01</remarks>
        public ExtensionActionKind Kind { get; set; }
        /// <summary>
        /// Gets or sets the execution frequency.
        /// </summary>
        /// <value>The execution.</value>
        /// <remarks>Documented by Dev02, 2009-07-01</remarks>
        public ExtensionActionExecution Execution { get; set; }
    }

    /// <summary>
    /// The type of the extension.
    /// </summary>
    public enum ExtensionType
    {
        /// <summary>
        /// Skin extension.
        /// </summary>
        Skin,
        /// <summary>
        /// Unknown extension.
        /// </summary>
        Unknown
    }

    /// <summary>
    /// The actions that should be done for the extension. Multiple actions can be stacked in the database actions table.
    /// </summary>
    public enum ExtensionActionKind
    {
        /// <summary>
        /// Install the extension.
        /// </summary>
        Install,
        /// <summary>
        /// Force the activation of the extension.
        /// </summary>
        Force,
        /// <summary>
        /// Inform the user about the installation.
        /// </summary>
        InformUser
    }

    /// <summary>
    /// The execution frequency for the extension actions.
    /// </summary>
    public enum ExtensionActionExecution
    {
        /// <summary>
        /// The action is executed only once.
        /// </summary>
        Once,
        /// <summary>
        /// The action is executed whenever a learning module is opened.
        /// </summary>
        Always,
        /// <summary>
        /// The action is not executed.
        /// </summary>
        Never
    }
}
