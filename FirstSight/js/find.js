$(function () {
    if ($("#scan_qr_div").is(":visible") == true) {
        $(".fa-fw").css("display", "none");
        //--------scanner-------
        let scanner = new Instascan.Scanner({ video: document.getElementById('preview') });
        scanner.addListener('scan', function (content) {
            $(".table").addClass("distable");
            $(".fa-fw").css("display", "initial");
            $("#r_id").text(content);
            scan_rabi_id = content;

            loding = setTimeout(loader, 1000);

            function loader() {
                get_info(scan_rabi_id);
                $(".table").removeClass("distable");
                $(".fa-fw").css("display", "none");
            }


        });
        Instascan.Camera.getCameras().then(function (cameras) {
            // console.log(cameras);
            $('.camera_list li').empty();
            if (cameras.length > 0) {
                cameras.forEach(function (c, i) {
                    // console.log(i);
                    camera_name = c.name;
                    camera_id = i;
                    console.log(typeof (i));
                    $('.camera_list').append('<li id=' + i + ' class="items"><i class="fa fa-camera fa-lg" aria-hidden="true"></i> 鏡頭 ' + (camera_id + 1) + ' </li>');
                    // console.log(c);	
                    // console.log(c.name);
                });

                $(".camera_list li").click(function () {
                    active = $(this).attr("id");
                    // console.log(active);
                    scanner.start(cameras[active]);
                    $(this).addClass("active").siblings().removeClass("active");
                });
                if (cameras.length >= 2) {
                    scanner.start(cameras[1]);
                    $(".camera_list li:nth(1)").addClass("active");
                } else {
                    scanner.start(cameras[0]);
                    $(".camera_list li:nth(0)").addClass("active");
                }
            } else {
                //alert("找不到相機...請確認允許使用相機");
                // console.error('找不到相機...請確認允許使用');
            }

        }).catch(function (e) {
            // console.error(e);
        });
        //--------scanner-end------

        //掃描加入購物車
        function get_info(scan_rabi_id) {
            if (scan_rabi_id.substr(0, 4) == "cart") {
                if ($('#lb_hello').is(':visible') === true) {
                    $.ajax({
                        type: 'POST', url: 'car.aspx',
                        data: 'cd_id=' + scan_rabi_id.substr(4, 14) + '&sc_num=' + "1",
                        success: function (msg) {

                            if (msg == "Y") {
                                var number = parseInt($('.checkout_items').text(), 10);
                                $('.checkout_items').text(number + 1)
                                alert("新增購物車成功!!" );
                            }
                            else if (msg == "N") {
                                alert("請先登入後，再進行操作");

                                setTimeout(function () {
                                    window.open("login.aspx");
                                }, 1000);
                            }
                            else if (msg == "R") {
                                alert("商品已在購物車!");
                            }
                        }
                    })
                }
                else {
                    window.location.href = 'https://140.126.146.41/fs/Default.aspx?pid=' + scan_rabi_id.substr(4, 14);

                    //alert(scan_rabi_id.substr(0, 4));
                }
            }
            else if (scan_rabi_id.substr(0, 4) == "wish") {
                if ($('#lb_hello').is(':visible') === true) {
                    $.ajax({
                        type: 'POST', url: 'scan_wish.aspx',
                        data: 'cd_id=' + scan_rabi_id.substr(4, 14),
                        success: function (msg) {
                            if (msg == "Y") {
                                alert("商品已加入許願池!");
                            }
                            else if (msg == "R") {
                                alert("此商品已在許願池!");
                            }
                            //alert(msg);
                        }
                    })
                }
                else {
                    window.location.href = 'https://140.126.146.41/fs/Default.aspx?pid=' + scan_rabi_id.substr(4, 14);
                    //alert(scan_rabi_id.substr(0,4));
                }
                //alert(scan_rabi_id.substr(4,14));
            }

            else if (scan_rabi_id.substr(0, 4) != "cart" && "wish") {
                window.location.href = 'https://140.126.146.41/fs/Default.aspx?pid=' + scan_rabi_id;
                //alert("444");
            }

        }
    }
    


});
