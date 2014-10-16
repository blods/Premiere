using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Text;
using System.Web.UI;

namespace PremiereInvite.VisualWebPart1
{
    [ToolboxItemAttribute(false)]
    public partial class VisualWebPart1 : WebPart
    {
        StringBuilder sb = new StringBuilder();
        public string siteURL;

        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        protected override void RenderContents(System.Web.UI.HtmlTextWriter writer)
        {

            using (SPSite site = new SPSite(SPContext.Current.Web.Url))
            {

                siteURL = SPContext.Current.Web.Url;

                // Create the references to the scripts
                //Add qtip2 CSS
                writer.WriteBeginTag("link");
                writer.WriteAttribute("type", "text/css");
                writer.WriteAttribute("rel", "stylesheet");
                writer.WriteAttribute("href", siteURL + "/scripts/jquery.qtip.min.css");
                writer.Write(HtmlTextWriter.SlashChar);
                writer.Write(HtmlTextWriter.TagRightChar);

                // Add jquery 1.7.2 from google
                writer.WriteBeginTag("script");
                writer.WriteAttribute("type", "text/javascript");
                writer.WriteAttribute("src", "//ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js");
                writer.Write(HtmlTextWriter.TagRightChar);
                writer.WriteEndTag("script");


                // Add qtip javascript
                writer.WriteBeginTag("script");
                writer.WriteAttribute("type", "text/javascript");
                writer.WriteAttribute("src", siteURL + "/scripts/jquery.qtip.min.js");
                writer.Write(HtmlTextWriter.TagRightChar);
                writer.WriteEndTag("script");

                // Add SPServices javascript
                writer.WriteBeginTag("script");
                writer.WriteAttribute("type", "text/javascript");
                writer.WriteAttribute("src", siteURL + "/scripts/jquery.SPServices-2014.01.min.js");
                writer.Write(HtmlTextWriter.TagRightChar);
                writer.WriteEndTag("script");

                // Called when a check box is clicked
                writer.WriteBeginTag("script");
                writer.WriteAttribute("language", "javascript");
                writer.WriteAttribute("type", "text/javascript");
                writer.Write(HtmlTextWriter.TagRightChar);
                writer.Write(
                    @"function handlechange(cb)
                {
                    var record=cb.id.substring(0,cb.id.length-1).split(""-"");
                    var id=record[0];
                    var choice=record[1];

                    $().SPServices({
                        operation: ""UpdateListItems"",
                        async: false,
                        batchCmd: ""Update"",
                        listName: ""Premiere Invite List"",
                        ID: """" + id + """",
                        valuepairs: [[""Hobbit"", """" + choice + """"]],
                        completefunc: function (xData, Status) {
                            //alert(xData.responseText);
                            }
                        }); 
                    //alert(""hello"");
                   //debugger;

                }");
                writer.WriteEndTag("script");

                // Edit dialog box
                writer.WriteBeginTag("script");
                writer.WriteAttribute("language", "javascript");
                writer.WriteAttribute("type", "text/javascript");
                writer.Write(HtmlTextWriter.TagRightChar);
                writer.Write(
                @"function openDialog(pURL) {
           
                    SP.UI.ModalDialog.showModalDialog (
                        {
                            url: pURL,
                            width: 750,
                            height: 600,
                            title: ""Edit Window""
                        }
                    )
                }");
                writer.WriteEndTag("script");


                // Called when a check box is clicked
                writer.WriteBeginTag("script");
                writer.WriteAttribute("language", "javascript");
                writer.WriteAttribute("type", "text/javascript");
                writer.Write(HtmlTextWriter.TagRightChar);
                writer.Write(
                    @"function handlechange2(cb)
                {
                    //debugger;
                    var record=cb.id.substring(0,cb.id.length-2).split(""-"");
                    var id=record[0];
                    var choice=record[1];

                    $().SPServices({
                        operation: ""UpdateListItems"",
                        async: false,
                        batchCmd: ""Update"",
                        listName: ""Premiere Invite List"",
                        ID: """" + id + """",
                        valuepairs: [[""Unbroken"", """" + choice + """"]],
                        completefunc: function (xData, Status) {
                            //alert(xData.responseText);
                            }
                        }); 
                    //alert(""hello"");
                   //debugger;

                }");
                writer.WriteEndTag("script");




                // Add CSS to hide tooltips and add padding
                writer.Write("<style>  .hidden {display:none;} </style>");

                writer.Write(@"<head><style>

</style></head>");

                using (SPWeb web = site.OpenWeb())
                {
                    string columnName = "";       // The column name that we will use
                    string nextPremiere = "";     // The name of the next oremiere
                    DateTime displayUntil = DateTime.Now;      // Display this premiere until


                    // First open the 'NextPremiere List' and read its single record
                    // in order to determine what Premiere to display and what column to us

                    // Currently dont need this section which works out the next premiere
                    /*SPList nextPremiereList = web.Lists["NextPremiere"];


                    int itemCount = nextPremiereList.ItemCount;

                    foreach (SPListItem item in nextPremiereList.Items)
                    {
                        columnName = item["ColumnName"].ToString();
                        nextPremiere = item["Title"].ToString();
                        displayUntil = (DateTime)item["DisplayUntil"];
                    }*/

                    //sb.Append(@"Invite to " + nextPremiere.ToString() + @" " + @" Ending: " + displayUntil.ToShortDateString());
                    // Start the creation of the table
                    sb.Append(@"<BR><table class=""detail"" align=""left"" style=""border: 1px solid #D4D0C8"">");
                    sb.Append(@"<tbody>");
                    sb.Append(@"<td style=""text-align: center;background-color: #909090; color:#FFFFFF""><strong>Hobbit Priority<strong></td>"); //First column
                    sb.Append(@"<td style=""text-align: center;background-color: #909090; color:#FFFFFF""><strong>Unbroken Priority<strong></td>"); //First column
                    sb.Append(@"<td style=""text-align: center;background-color: #909090; color:#FFFFFF""><strong>Company<strong></td>"); //First column
                    sb.Append(@"<td style=""text-align: center;background-color: #909090; color:#FFFFFF""><strong>Contact<strong></td>"); //Second column
                    sb.Append(@"<td style=""text-align: center;background-color: #909090; color:#FFFFFF""><strong>Job Title<strong></td>"); //Third column
                    sb.Append(@"<td style=""text-align: center;background-color: #909090; color:#FFFFFF""><strong>Nominated<strong></td>"); //Third column
                    sb.Append(@"<td style=""text-align: center;background-color: #909090; color:#FFFFFF""><strong>Email<strong></td>");

                    SPList lists = web.Lists["Premiere Invite List"];
                    SPView view = lists.Views["All Items"];  // This is to ensure the sort order is company (or whatever All items is set to
                    SPListItemCollection items = lists.GetItems(view);

                    string editurl = siteURL + @"/_layouts/listform.aspx?PageType=6&ListId={" + lists.ID.ToString() + @"}&ID=";

                    int banding = 0;    // Keeps track of the banding colors
                    foreach (SPListItem item in items)
                    {
                        string contact = "";
                        string company = "";
                        string jobTitle = "";
                        string nominatee = "";
                        string email = "";

                        int listid = 0;

                        listid = Convert.ToInt32(item["ID"].ToString());
                        if (item["Contact"] != null) contact = item["Contact"].ToString();
                        if (item["Company"] != null) company = item["Company"].ToString();
                        if (item["Job Title"] != null) jobTitle = item["Job Title"].ToString();
                        if (item["Email"] != null) email = item["Email"].ToString();
                        if (item["Name of Person Nominating"] != null) nominatee = item["Name of Person Nominating"].ToString();

                        int inviteToUnbroken = 0;
                        int inviteToHobbit = 0;

                        if (item["Unbroken"] != null) inviteToUnbroken = Convert.ToInt32(item["Unbroken"].ToString());
                        if (item["Hobbit"] != null) inviteToHobbit = Convert.ToInt32(item["Hobbit"].ToString());

                        // Do the color banding between white and grey
                        sb.Append(@"<tr");
                        if (banding == 0)
                        {
                            sb.Append(@" bgcolor=""#E8E8E8""><td>");
                            banding = 1;
                        }
                        else
                        {
                            sb.Append(@" bgcolor=""#FFFFFF""><td>");
                            banding = 0;
                        }


                        sb.Append(@"<fieldset  id=" + listid.ToString() + @" data-type=""horizontal"">

              <input type=""radio"" name=" + listid.ToString() + @" id=" + listid.ToString() + @"-1"" value=""1""");
                        if (inviteToHobbit == 1) sb.Append(@" checked ");
                        sb.Append(@" onclick=""handlechange(this);"" />
              <label for=""radio-view-1"">1</label>
              <input type=""radio"" name=" + listid.ToString() + @" id=" + listid.ToString() + @"-2"" value=""2""");
                        if (inviteToHobbit == 2) sb.Append(@" checked ");
                        sb.Append(@" onclick=""handlechange(this);"" />
              <label for=""radio-view-2"">2</label>
              <input type=""radio"" name=" + listid.ToString() + @" id=" + listid.ToString() + @"-3"" value=""3""");
                        if (inviteToHobbit == 3) sb.Append(@" checked ");
                        sb.Append(@" onclick=""handlechange(this);"" />
              <label for=""radio-view-3"">3</label>
              <input type=""radio"" name=" + listid.ToString() + @" id=" + listid.ToString() + @"-0"" value=""0""");
                        if (inviteToHobbit == 0) sb.Append(@" checked ");
                        sb.Append(@" onclick=""handlechange(this);"" />
              <label for=""radio-view-0"">N/A</label>
            </fieldset></td>

            
            <td><fieldset  id=" + listid.ToString() + @"-2 data-type=""horizontal"">

              <input type=""radio"" name=" + listid.ToString() + @"-2 id=" + listid.ToString() + @"-12"" value=""1""");
                        if (inviteToUnbroken == 1) sb.Append(@" checked ");
                        sb.Append(@" onclick=""handlechange2(this);"" />
              <label for=""radio-view-1"">1</label>
              <input type=""radio"" name=" + listid.ToString() + @"-2 id=" + listid.ToString() + @"-22"" value=""2""");
                        if (inviteToUnbroken == 2) sb.Append(@" checked ");
                        sb.Append(@" onclick=""handlechange2(this);"" />
              <label for=""radio-view-2"">2</label>
              <input type=""radio"" name=" + listid.ToString() + @"-2 id=" + listid.ToString() + @"-32"" value=""3""");
                        if (inviteToUnbroken == 3) sb.Append(@" checked ");
                        sb.Append(@" onclick=""handlechange2(this);"" />
              <label for=""radio-view-3"">3</label>
              <input type=""radio"" name=" + listid.ToString() + @"-2 id=" + listid.ToString() + @"-02"" value=""0""");
                        if (inviteToUnbroken == 0) sb.Append(@" checked ");
                        sb.Append(@" onclick=""handlechange2(this);"" />
              <label for=""radio-view-0"">N/A</label>
            </fieldset></td>");





                        /*sb.Append(@"<tr><td><fieldset class=""radiogroup"" data-type=""horizontal"">
                        <input class=""myclass"" type=""radio"" name=""radio-choice"" id=""radio-choice-0"" value=""choice-0"" />
                        <label  class=""myclass2"" for=""radio-choice-0"">0</label>
                        <input class=""myclass"" type=""radio"" name=""radio-choice"" id=""radio-choice-1"" value=""choice-1"" />
                        <label  class=""myclass2"" for=""radio-choice-1"">1</label>
                        <input class=""myclass"" type=""radio"" name=""radio-choice"" id=""radio-choice-2"" value=""choice-2"" />
                        <label  class=""myclass2"" for=""radio-choice-2"">2</label>
                        <input class=""myclass"" type=""radio"" name=""radio-choice"" id=""radio-choice-3"" value=""choice-3"" />
                        <label  class=""myclass2"" for=""radio-choice-3"">3</label>
                        </fieldset>");

                        /*sb.Append(@"<tr><td><input name=""Checkbox1"" class=""thecheckboxes"" title=""Select to nominate for this permiere"" ID=""" + item.ID + @""" ");
                        if (inviteToPremiere != 0)
                        {
                            sb.Append(@" checked ");
                        }
                        sb.AppendLine(@" type=""checkbox"" onclick=""handlechange(this);"" /></td>"); 
                         */


                        string hrefstring = @"""javascript:openDialog('" + editurl + listid.ToString() + @"')""";
                        //string testing = @"<td><a href=""javascript:openDialog('" + editurl + listid.ToString() + @"')""> Open Item</a></td>";
                        //sb.AppendLine(testing);
                        sb.AppendLine(@"<td><a href=" + hrefstring + @"> <img src=""" + siteURL + @"/scripts/edit.jpg"" border=""0"" /></a>");
                        sb.AppendLine(@"" + company + @"</td>");
                        sb.AppendLine(@"<td>" + contact + @"</td>");
                        sb.AppendLine(@"<td>" + jobTitle + @"</td>");
                        sb.AppendLine(@"<td>" + nominatee + @"</td>");
                        sb.AppendLine(@"<td>" + email + @"</td></tr>");


                    }
                    sb.AppendLine("</tbody>");
                    sb.AppendLine("</table>");
                    writer.Write(sb);
                }
            }




            base.RenderContents(writer);
        }
        
        
        
        public VisualWebPart1()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}
