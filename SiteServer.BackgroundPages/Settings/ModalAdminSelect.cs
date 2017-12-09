﻿using System;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using BaiRong.Core;
using SiteServer.BackgroundPages.Core;

namespace SiteServer.BackgroundPages.Settings
{
	public class ModalAdminSelect : BasePage
    {
        public Repeater RptDepartment;
        public Literal LtlDepartment;
        public Repeater RptUser;

        private readonly NameValueCollection _additional = new NameValueCollection();
        private int _departmentId;
        private string _scriptName;

        public static string GetShowPopWinString(int departmentId, string scriptName)
        {
            return PageUtils.GetOpenLayerString("管理员选择", PageUtils.GetSettingsUrl(nameof(ModalAdminSelect), new NameValueCollection
            {
                {"departmentID", departmentId.ToString()},
                {"scriptName", scriptName}
            }), 460, 400);
        }

		public void Page_Load(object sender, EventArgs e)
		{
            _departmentId = Body.GetQueryInt("departmentID");
            _scriptName = Body.GetQueryString("ScriptName");
		    var url = PageUtils.GetSettingsUrl(nameof(ModalAdminSelect), new NameValueCollection
            {
                {"scriptName", _scriptName}
            });
            _additional.Add("UrlFormatString", url);

			if (!IsPostBack)
			{
                LtlDepartment.Text = "管理员列表";
                if (Body.IsQueryExists("UserName"))
                {
                    var userName = Body.GetQueryString("UserName");
                    var displayName = AdminManager.GetDisplayName(userName, true);
                    string scripts = $"window.parent.{_scriptName}('{displayName}', '{userName}');";
                    PageUtils.CloseModalPageWithoutRefresh(Page, scripts);
                }
                else if (Body.IsQueryExists("departmentID"))
                {
                    if (_departmentId > 0)
                    {
                        LtlDepartment.Text = DepartmentManager.GetDepartmentName(_departmentId);
                        RptUser.DataSource = BaiRongDataProvider.AdministratorDao.GetUserNameArrayList(_departmentId, false);
                        RptUser.ItemDataBound += rptUser_ItemDataBound;
                        RptUser.DataBind();
                    }
                }
                else
                {
                    ClientScriptRegisterClientScriptBlock("NodeTreeScript", DepartmentTreeItem.GetScript(EDepartmentLoadingType.DepartmentSelect, _additional));
                }
			}

            BindGrid();
		}

        public void BindGrid()
        {
            try
            {
                RptDepartment.DataSource = BaiRongDataProvider.DepartmentDao.GetDepartmentIdListByParentId(0);
                RptDepartment.ItemDataBound += rptDepartment_ItemDataBound;
                RptDepartment.DataBind();
            }
            catch (Exception ex)
            {
                PageUtils.RedirectToErrorPage(ex.Message);
            }
        }

        private void rptDepartment_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var departmentId = (int)e.Item.DataItem;
            var departmentInfo = DepartmentManager.GetDepartmentInfo(departmentId);

            var ltlHtml = e.Item.FindControl("ltlHtml") as Literal;

            if (ltlHtml != null)
            {
                ltlHtml.Text = PageDepartment.GetDepartmentRowHtml(departmentInfo, EDepartmentLoadingType.DepartmentSelect, _additional);
            }
        }

        private void rptUser_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var userName = (string)e.Item.DataItem;

            var ltlUrl = e.Item.FindControl("ltlUrl") as Literal;

            var url = PageUtils.GetSettingsUrl(nameof(ModalAdminSelect), new NameValueCollection
            {
                {"scriptName", _scriptName},
                {"UserName", userName}
            });

            if (ltlUrl != null) ltlUrl.Text = $"<a href='{url}'>{AdminManager.GetDisplayName(userName, false)}</a>";
        }
	}
}
