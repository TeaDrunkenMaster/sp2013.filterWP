using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using SharePoint2013.FilterWP.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Linq;
using Microsoft.SharePoint.WebPartPages;
using Microsoft.SharePoint.Portal.WebControls;
using Microsoft.Office.Server.WebControls;
using System;

namespace SharePoint2013.FilterWP.WebParts.FilterWP
{
    [ToolboxItemAttribute(false)]
    public class FilterWP : Microsoft.SharePoint.WebPartPages.WebPart, ITransformableFilterValues
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/SharePoint2013.FilterWP.WebParts/FilterWP/FilterWPUserControl.ascx";


          [WebBrowsable(true),
      WebDisplayName("Parameter"),
      WebDescription(""),
      Personalizable(PersonalizationScope.Shared),
      Category("Settings")
     ]
        public virtual string ParameterName
        {
            get;
            set;

        }




        [WebBrowsable(true),
      WebDisplayName("Web url"),
      WebDescription(""),
      Personalizable(PersonalizationScope.Shared),
      Category("Settings")
     ]
        public string WebUrl
        {
            get;
            set;
        }

        [WebBrowsable(true),
      WebDisplayName("List Title"),
      WebDescription(""),
      Personalizable(PersonalizationScope.Shared),
      Category("Settings")
     ]
        public string ListName
        {
            get;
            set;
        }

        [WebBrowsable(true),
        WebDisplayName("Column InternalName"),
        WebDescription(""),
        Personalizable(PersonalizationScope.Shared),
        Category("Settings")
       ]
        public string FieldInternalName
        {
            get;
            set;
        }


        [WebBrowsable(true),
        WebDisplayName("Multiple Coice"),
        WebDescription("Before activating, connect the web part"),
        Personalizable(PersonalizationScope.Shared),
        Category("Settings")]
        public bool AllowMultipleValues
        {
            get;
            set;

        }


        [WebBrowsable(true),
        WebDisplayName("Add value \"All\""),
        WebDescription(""),
        Personalizable(PersonalizationScope.Shared),
        Category("Settings")
       ]
        public bool AllowAllValue
        {
            get;
            set;
        }


        [WebBrowsable(true),
        WebDisplayName("Add value \"(Empty)\""),
        WebDescription(""),
        Personalizable(PersonalizationScope.Shared),
        Category("Settings")
        ]
        public virtual bool AllowEmptyValue
        {
            get;
            set;
        }

        [WebBrowsable(true),
       WebDisplayName("Do not user PostBack"),
       WebDescription(""),
       Personalizable(PersonalizationScope.Shared),
       Category("Settings")
       ]
        public virtual bool DontUsePostBack
        {
            get;
            set;
        }


       
        public virtual ReadOnlyCollection<string> ParameterValues
        {
            get
            {

                if (control != null && ((DropDownList)control.FindControl("ddlFilter")) != null)
                {
                    var selected = ((DropDownList)control.FindControl("ddlFilter")).SelectedValue;
                    string[] values = { };
                    values = new string[] { selected };
                    return new ReadOnlyCollection<string>(values);
                }

                else
                    if (control != null && ((ListBox)control.FindControl("lbFilter")) != null)
                    {
                        var lBox = ((ListBox)control.FindControl("lbFilter"));
                        var valuesList = new List<string>();
                        foreach (ListItem lbItem in lBox.Items)
                        {
                            if (lbItem.Selected)
                            {
                                valuesList.Add(lbItem.Value);
                            }
                        }
                        return new ReadOnlyCollection<string>(valuesList.ToArray());
                    }
                return (!string.IsNullOrEmpty(DefaultValue)) ? new ReadOnlyCollection<string>(new string[]{DefaultValue}) : null;

            }

        }







        protected new SPWebPartManager WebPartManager
        {
            get
            {
                if (Page != null)
                {
                    return (SPWebPartManager)System.Web.UI.WebControls.WebParts.WebPartManager.GetCurrentWebPartManager(Page);
                }
                return null;
            }
        }



        [WebBrowsable(true),
        WebDisplayName("Default value"),
        WebDescription(""),
        Personalizable(PersonalizationScope.Shared),
        Category("Settings")
       ]
        public string DefaultValue
        {
            get;
            set;
        }

        [WebBrowsable(true),
        WebDisplayName("Exclusions"),
        WebDescription("List with a semicolon"),
        Personalizable(PersonalizationScope.Shared),
        Category("Settings")
        ]
        public string Exclusions
        {
            get;
            set;
        }

        [WebBrowsable(true),
         WebDisplayName("Custom values"),
         WebDescription("List with a semicolon"),
         Personalizable(PersonalizationScope.Shared),
         Category("Settings")
        ]
        public  string CustomValues
        {
            get;
            set;
        }
        


        [ConnectionProvider("Фильтр", "ITransformableFilterValues")]

        public ITransformableFilterValues GetTransformableFilterValuesProvider()
        {
            return this;
        }

        FilterWPUserControl control;     

        protected override void CreateChildControls()
        {
            control = Page.LoadControl(_ascxPath) as FilterWPUserControl;
            control.ParameterName = ParameterName;
            control.WebUrl = (string.IsNullOrEmpty(WebUrl)) ? SPContext.Current.Web.Url : WebUrl;
            control.ListName = ListName; 
            control.FieldInternalName=FieldInternalName;
            control.AllowAllValue=AllowAllValue;
            control.Exclusions=Exclusions;
            control.AllowMultipleValues=AllowMultipleValues;
            control.DefaultValue = DefaultValue;
            control.AllowEmptyValue = AllowEmptyValue;
            control.DontUsePostBack = DontUsePostBack;
            control.CustomValues = CustomValues;
            Controls.Add(control);        
        }
    }
}
