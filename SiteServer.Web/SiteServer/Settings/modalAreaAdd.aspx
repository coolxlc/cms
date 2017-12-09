﻿<%@ Page Language="C#" Inherits="SiteServer.BackgroundPages.Settings.ModalAreaAdd" %>
	<%@ Register TagPrefix="ctrl" Namespace="SiteServer.BackgroundPages.Controls" Assembly="SiteServer.BackgroundPages" %>
		<!DOCTYPE html>
		<html class="modalPage">

		<head>
			<meta charset="utf-8">
			<!--#include file="../inc/head.html"-->
		</head>

		<body>
			<!--#include file="../inc/openWindow.html"-->

			<form runat="server">
				<ctrl:alerts runat="server" />

				<div class="form-horizontal">

					<asp:PlaceHolder ID="PhParentId" runat="server">
						<div class="form-group">
							<label class="col-xs-3 text-right control-label">上级区域</label>
							<div class="col-xs-8">
								<asp:DropDownList ID="DdlParentId" class="form-control" runat="server"> </asp:DropDownList>
							</div>
							<div class="col-xs-1 help-block">

							</div>
						</div>
					</asp:PlaceHolder>

					<div class="form-group">
						<label class="col-xs-3 text-right control-label">区域名称</label>
						<div class="col-xs-8">
							<asp:TextBox cssClass="form-control" id="TbAreaName" runat="server" />
						</div>
						<div class="col-xs-1 help-block">
							<asp:RequiredFieldValidator ControlToValidate="TbAreaName" errorMessage=" *" foreColor="red" display="Dynamic" runat="server"
							/>
							<asp:RegularExpressionValidator runat="server" ControlToValidate="TbAreaName" ValidationExpression="[^']+" errorMessage=" *"
							  foreColor="red" display="Dynamic" />
						</div>
					</div>

					<hr />

					<div class="form-group m-b-0">
						<div class="col-xs-11 text-right">
							<asp:Button class="btn btn-primary m-l-10" text="确 定" runat="server" onClick="Submit_OnClick" />
							<button type="button" class="btn btn-default m-l-10" onclick="window.parent.layer.closeAll()">取 消</button>
						</div>
						<div class="col-xs-1"></div>
					</div>

				</div>

			</form>
		</body>

		</html>