﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.Master" AutoEventWireup="true" CodeBehind="ADMPedidos.aspx.cs" Inherits="TPC_Orihuela_Insaurralde.ADMPedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="Scripts/ScriptsValidaciones.js"></script>
    <h1>Administracion de pedidos</h1>
    <hr />
    <div class="container">
        <table class="table table-hover table-bordered">
            <thead class="thead-light text-center" style="font-size: 14px">
                <tr>
                    <th>Nro Remito</th>
                    <th>Id Carrito</th>
                    <th>Fecha</th>
                    <th>Importe</th>
                    <th>Estado</th>
                    <th class="text-right" style="width: 450px">Acciones</th>

                </tr>
            </thead>
            <tbody>

                <% if (ListaPedidos != null)
                    { %>
                <%foreach (var Item in ListaPedidos)
                    {
                %>

                <tr class="table-light text-center">
                    <td style="font-size: 12px"><% = Item.id %></td>
                    <td style="font-size: 12px"><% = Item.IDCarrito %></td>
                    <td style="font-size: 12px"><% = Item.Fecha.Day + "/" + Item.Fecha.Month + "/" + Item.Fecha.Year %></td>
                    <td style="font-size: 12px">$<% = Item.Importe %></td>
                    <td style="font-size: 12px"><% = Item.EstadoPedidos.Descripcion %></td>
                    <td class="text-right">
                        <a href="RevisionPedidos.aspx?ID=<%=Item.id %>" class="btn btn-outline-primary badge-pill" style="font-size: 15px; text-decoration: none; width: 100px">Revisar</a>
                        <%if (Item.EstadoPedidos.Descripcion != "Rechazado")
                            { %>
                        <%if (Item.EstadoPedidos.Descripcion != "Entregado")
                            { %>
                        <a href="ADMPedidos.aspx?ID=<%=Item.id %>&Estado=<%= 4 %>" class="btn btn-outline-success badge-pill" style="font-size: 15px; text-decoration: none; width: 100px">Entregado</a>
                        <a href="PedidoRechazado.aspx?ID=<%=Item.id %>&Carrito=<%= Item.IDCarrito %>" class="btn btn-outline-danger badge-pill" style="font-size: 15px; text-decoration: none; width: 100px">Rechazado</a>
                        <%if (Item.EstadoPedidos.Descripcion != "En Camino")
                            { %>
                        <a href="ADMPedidos.aspx?ID=<%=Item.id %>&Estado=<%= 3 %>" class="btn btn-outline-warning badge-pill" style="font-size: 15px; text-decoration: none; width: 100px">En Camino</a>
                        <%} %>
                        <%} %>
                        <%} %>
                    </td>
                </tr>

                <% } %>
                <% } %>
            </tbody>
        </table>


        <center>
       
        <asp:Button class="btn btn-outline-danger" Style="font-size:15px" Text="Volver" runat="server" ID="BtnVolver" OnClick="BtnVolver_Click"  />
                </center>

    </div>
</asp:Content>
