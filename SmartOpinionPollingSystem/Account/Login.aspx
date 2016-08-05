<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SmartOpinionPollingSystem.Account.Login" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
    </hgroup>

     <style type="text/css">  
        h3 {  
            font-size: 15px;
            color: blue;
        }  
          
       
    </style>
    <section id="loginForm">
        <h3>If you are an user please enter your userID. If you are an Organization please enter OrgID.</h3>
       
                <p class="validation-summary-errors">
                    <asp:Literal runat="server" ID="FailureText" />
                </p>
                    <ul style="list-style-type: none;">
                        <li>
                            <asp:Label runat="server" AssociatedControlID="txtID">ID</asp:Label>
                            <asp:TextBox runat="server" ID="txtID" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtID" CssClass="field-validation-error" ErrorMessage="The ID field is required." />
                        </li>
                        <li>
                            <asp:Label runat="server" AssociatedControlID="Password">Password</asp:Label>
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="field-validation-error" ErrorMessage="The password field is required." />
                        </li>
                        <li>
                            <asp:CheckBox runat="server" ID="RememberMe" />
                            <asp:Label runat="server" AssociatedControlID="RememberMe" CssClass="checkbox">Remember me?</asp:Label>
                        </li>
                    </ul>
                    <asp:Button runat="server" OnClick="LoginButton_Click" Text="Log in" />
               
      
        <p>
            <asp:HyperLink runat="server" ID="UserRegisterHyperLink" ViewStateMode="Disabled">User Register</asp:HyperLink>
            if you don't have an account.
        </p>

          <p>
            <asp:HyperLink runat="server" ID="OrgRegisterHyperLink" ViewStateMode="Disabled">Organization Register</asp:HyperLink>
            if you don't have an account.
        </p>

    </section>

    </asp:Content>
