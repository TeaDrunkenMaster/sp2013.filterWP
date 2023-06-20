<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FilterWPUserControl.ascx.cs" Inherits="SharePoint2013.FilterWP.WebParts.FilterWP.FilterWPUserControl" %>
  
<style type="text/css">
   .filterpanel
   {display:none;}
</style>

    <script type="text/javascript">        
        Gazprom.jQueryLoadedExecute(function () {        
            function createDropDownCheckBox() {
                $lst = $('[id$="lbFilter"');
                if ($lst != null) {
                    var j = $;
                    $($lst).SumoSelect({ okCancelInMulti: true });
                    setTimeout(function () { j('.filterpanel').show(); }, 200);
                }
            };


            function jsNotLoaded(lib) {
                return $('[src="' + lib + '"]').length > 0;
            };

            function cssNotLoaded(css) {
                return $('[href="' + css + '"]').length > 0;
            };

            $(function () {
                var js = '/_layouts/15/SiteAssets/js/jquery.sumoselect.min.js';
                var css = '/_layouts/15/SiteAssets/css/sumoselect.css';
                if (!jsNotLoaded(js) && !cssNotLoaded(css)) {
                    $('<script src="'+js+'"/>').appendTo('body');
                    $('<link href="'+css+'" rel="stylesheet"/>').appendTo('body');
                    createDropDownCheckBox();
                }              
                
            });

            
        });
    </script>