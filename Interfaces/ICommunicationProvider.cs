using System;
using System.Collections.ObjectModel;

namespace SharePoint2013.FilterWP.Interfaces
{
    public interface ICommunicationProvider
    {
        string ListName { get; set; }
        string FieldInternalName { get; set; }
        bool AddAllOption { get; set;  }
        bool MultipleChoice { get; set; }
        string DefaultValue { get; set; }
        string Exclusions { get; set; }
        bool DontUsePostBack { get; set; }
    }
}
