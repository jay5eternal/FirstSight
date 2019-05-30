<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testorder.aspx.cs" Inherits="testorder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title></title>
    <link rel="stylesheet" href="styles/bg.css"/>
    <link rel="stylesheet" type="text/css" media='all' href="styles/carts.css"/>
    <link rel='stylesheet' href='https://colorlib.com/tyche/wp-content/plugins/woocommerce/assets/css/woocommerce-smallscreen.css?ver=3.5.1' type='text/css' media='only screen and (max-width: 768px)' />
</head>

<body>
    <div id="cart_display" runat="server">
                    <asp:PlaceHolder ID="PlaceHolder5" runat="server"></asp:PlaceHolder>
                    <div class="container main_s2_1g">
                        <div class="row">
                            <div class="col-xs-12">
                                <%--訂單開始--%>
                                    <div class="carts_top">
                                        <div class="container">
                                            <div class="row">
                                                <div class="col text-center">
                                                    <div class="section_title">
                                                        <h2>乾你的訂單</h2>
                                                    </div></div></div>
                                            <div class="row blogs_container">
                                            </div></div></div> </div></div></div>
                        <%--訂單標題--%>
                        <div class="woocommerce">
                            <form class="woocommerce-cart-form">
                                <table class="shop_table shop_table_responsive cart woocommerce-cart-form__contents" cellspacing="0">
                                    <thead>
                                        <tr> 
                                            <th class="product-remove">訂單日期</th>
                                            <th class="product-remove">訂單編號</th>
                                            <th class="product-remove">付款方式</th>
                                            <th class="product-remove">取貨方式</th>
                                            <th class="product-remove">寄送地址</th>
                                            <th class="product-remove">連絡電話</th>
                                            <th class="product-remove">金額</th>
                                            <th class="product-remove">備註</th>
                                        </tr>
                                    </thead>
                                    <tbody> <%--data-title=縮小後顯示--%>
                                        <tr class="woocommerce-cart-form__cart-item cart_item">
                                            <td class="temp_pname product-name" data-title="訂單日期"> 2018-11-06</td>
                                            <td class="temp_pname product-name" data-title="訂單編號">35596830</td>
                                            <td class="temp_pname product-name" data-title="付款方式">貨到付款</td>
                                            <td class="temp_pname product-name" data-title="取貨方式">超商取貨</td>
                                            <td class="temp_pname product-name" data-title="寄送地址">7-11華耀門市</td>
                                            <td class="temp_pname product-name" data-title="連絡電話">093345678</td>
                                            <td class="temp_pname product-name" data-title="金額">$799</td>
                                            <td class="temp_pname product-name" data-title="備註">買家賊78帥</td>
                                                </br>
                                        </tr>
                                    </tbody>
                                </table>
                            </form></div></div>
</body>
</html>
