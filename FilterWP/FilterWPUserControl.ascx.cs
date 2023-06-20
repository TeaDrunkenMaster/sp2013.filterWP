using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Linq;

namespace SharePoint2013.FilterWP.WebParts.FilterWP
{
    public partial class FilterWPUserControl : UserControl
    {

        public string thisClientID;

            public string ParameterName;
            public string WebUrl;
            public string ListName;
            public string FieldInternalName;
            public bool AllowAllValue;
            public string Exclusions;
            public bool AllowMultipleValues;
            public string DefaultValue;
            public bool AllowEmptyValue;
            public bool DontUsePostBack;
            public string CustomValues;

        private void showError(string text)
        {
            var lbError = new Label();
            lbError.Text = text;
            Controls.Add(lbError);
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            var panel = new Panel();
            panel.CssClass = "filterpanel";
            panel.Attributes.Add("style", "padding:3px;");
            Controls.Add(panel);


            panel.Controls.Add(new LiteralControl("<b>" + ParameterName + "</b>"));
            panel.Controls.Add(new LiteralControl("<br/>"));

            if (string.IsNullOrEmpty(ListName)  && string.IsNullOrEmpty(FieldInternalName) && string.IsNullOrEmpty(CustomValues))
            {
                showError("Отсутствуют настройки веб-части");
            }
            else
            {

                var options = new List<ListItem>();

                if (AllowEmptyValue)
                {
                    options.Add(new ListItem() { Text = "(Empty)", Value = "" });
                }


                if (AllowAllValue)
                {
                    options.Add(new ListItem() { Text = "All", Value = "All" });
                }


                var webUrl = string.IsNullOrEmpty(WebUrl) ? SPContext.Current.Web.Url : WebUrl;
                SPSecurity.RunWithElevatedPrivileges(
                    delegate()
                    {
                        if (string.IsNullOrEmpty(CustomValues))
                        {
                            using (var site = new SPSite(webUrl))
                            {
                                using (var web = site.OpenWeb())
                                {
                                    try
                                    {
                                        var list = web.Lists.TryGetList(ListName);
                                        if (list != null)
                                        {
                                            var q = new SPQuery();
                                            q.Query = "<OrderBy><FieldRef Name='" + FieldInternalName + "' /></OrderBy>";

                                            var items = list.GetItems();





                                            var valueItems = (from SPListItem item in items select item[FieldInternalName]).Where(x => x != null && !string.IsNullOrEmpty(x.ToString()));


                                            Object[] disctinctValueItems = null;

                                            if (list.Fields.GetField(FieldInternalName).Type != SPFieldType.Lookup && list.Fields.GetField(FieldInternalName).Type != SPFieldType.User)
                                            {
                                                disctinctValueItems = valueItems.OrderBy(x => x).Distinct().ToArray();
                                            }
                                            else
                                            {
                                                disctinctValueItems = valueItems.Select(x => x.ToString().Substring(x.ToString().IndexOf(";#") + 2)).OrderBy(x => x).Distinct().ToArray();
                                            }


                                            var exc = (Exclusions != null) ? Exclusions.Split(';') : new string[] { };

                                            foreach (var di in disctinctValueItems)
                                            {
                                                if (!exc.Contains(di.ToString()))
                                                {
                                                    var value = di.ToString();

                                                    options.Add(new ListItem() { Text = value, Value = value });
                                                }
                                            }
                                        }
                                        else
                                        {
                                            showError("Error getting data. Check your settings");
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        showError("Error getting data. Check your settings");
                                    }
                                }
                            }
                        }
                        else
                        {
                            var values = CustomValues.Split(';');
                            foreach (var v in values)
                            {
                                options.Add(new ListItem() { Text = v, Value = v });
                            }
                        }
                    });

                if (AllowMultipleValues)
                {
                    var lBox = new ListBox();
                    if (!DontUsePostBack) { lBox.AutoPostBack = true; }
                    lBox.ID = "lbFilter";
                    lBox.Items.AddRange(options.ToArray());
                    lBox.SelectionMode = ListSelectionMode.Multiple;
                    panel.Controls.Add(lBox);

                    if (DefaultValue != null)
                    {
                        var li = lBox.Items.FindByText(DefaultValue);
                        if (li != null) { li.Selected = true; }
                    }
                }
                else
                {
                    var dpList = new DropDownList();
                    dpList.Width = 300;
                    if (!DontUsePostBack) { dpList.AutoPostBack = true; }
                    dpList.CssClass = "SlectBox";
                    dpList.ID = "ddlFilter";
                    dpList.Items.AddRange(options.ToArray());
                    panel.Controls.Add(dpList);
                    if (DefaultValue != null)
                    {
                        var li = dpList.Items.FindByText(DefaultValue);
                        if (li != null) { li.Selected = true; }
                    }
                    else
                    {
                        dpList.Items[0].Selected = true;
                    }
                }
            }
            
        }
    }
}
