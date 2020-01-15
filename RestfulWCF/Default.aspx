<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RestfulWCF._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="button1">
        <p>Usuário</p>
        <input type="text" id="userBox" />
        <p>Senha</p>
        <input type="password" id="passBox" />
        <p>Nome</p>
        <input type="text" id="nameBox" />
        <p>E-Mail</p>
        <input type="text" id="mailBox" />
        <br />
        <button onclick="DoInclude();return false;">Cadastrar</button>
    </div>

    <script type="text/javascript">
        function DoInclude() {
            var userName = $("#userBox").val();
            var password = $("#passBox").val();
            var name = $("#nameBox").val();
            var mail = $("#mailBox").val();
            $.ajax({
                url: "Service/Service1.svc/DoInclude",
                type: "POST",
                data: JSON.stringify(userName,password,name,mail),
                dataType: "json",
                contentType: "application/json",
                success: function (result) {
                    console.info(result);
                }            
            });
        }
    </script>
</asp:Content>
