/* JS Document */

/******************************

[Table of Contents]

1. Vars and Inits
2. Set Header
3. Init Menu
4. Init Thumbnail
5. Init Quantity
6. Init Star Rating
7. Init Favorite
8. Init Tabs



******************************/

jQuery(document).ready(function($)
{
	"use strict";

	/* 

	1. Vars and Inits

	*/

	var header = $('.header');
	var topNav = $('.top_nav')
	var hamburger = $('.hamburger_container');
	var menu = $('.hamburger_menu');
	var menuActive = false;
	var hamburgerClose = $('.hamburger_close');
	var fsOverlay = $('.fs_menu_overlay');

	setHeader();

	$(window).on('resize', function()
	{
		setHeader();
	});

	$(document).on('scroll', function()
	{
		setHeader();
	});

	initMenu();
	initThumbnail();
    initQuantity();
    initQuantitys();
    initQuantitym();
    initQuantityl();
    initQuantityxl();

    init();
    

	initStarRating();
	initFavorite();
	initTabs();

	/* 

	2. Set Header

	*/

	function setHeader()
	{
		if(window.innerWidth < 992)
		{
			if($(window).scrollTop() > 100)
			{
				header.css({'top':"0"});
			}
			else
			{
				header.css({'top':"0"});
			}
		}
		else
		{
			if($(window).scrollTop() > 100)
			{
				header.css({'top':"0"});
			}
			else
			{
				header.css({'top':"0"});
			}
		}
		if(window.innerWidth > 991 && menuActive)
		{
			closeMenu();
		}
	}

	/* 

	3. Init Menu

	*/

	function initMenu()
	{
		if(hamburger.length)
		{
			hamburger.on('click', function()
			{
				if(!menuActive)
				{
					openMenu();
				}
			});
		}

		if(fsOverlay.length)
		{
			fsOverlay.on('click', function()
			{
				if(menuActive)
				{
					closeMenu();
				}
			});
		}

		if(hamburgerClose.length)
		{
			hamburgerClose.on('click', function()
			{
				if(menuActive)
				{
					closeMenu();
				}
			});
		}

		if($('.menu_item').length)
		{
			var items = document.getElementsByClassName('menu_item');
			var i;

			for(i = 0; i < items.length; i++)
			{
				if(items[i].classList.contains("has-children"))
				{
					items[i].onclick = function()
					{
						this.classList.toggle("active");
						var panel = this.children[1];
					    if(panel.style.maxHeight)
					    {
					    	panel.style.maxHeight = null;
					    }
					    else
					    {
					    	panel.style.maxHeight = panel.scrollHeight + "px";
					    }
					}
				}	
			}
		}
	}

	function openMenu()
	{
		menu.addClass('active');
		// menu.css('right', "0");
		fsOverlay.css('pointer-events', "auto");
		menuActive = true;
	}

	function closeMenu()
	{
		menu.removeClass('active');
		fsOverlay.css('pointer-events', "none");
		menuActive = false;
	}

	/* 

	4. Init Thumbnail

	*/

	function initThumbnail()
	{
		if($('.single_product_thumbnails ul li').length)
		{
			var thumbs = $('.single_product_thumbnails ul li');
			var singleImage = $('.single_product_image_background');

			thumbs.each(function()
			{
				var item = $(this);
				item.on('click', function()
				{
					thumbs.removeClass('active');
					item.addClass('active');
					var img = item.find('img').data('image');
					singleImage.css('background-image', 'url(' + img + ')');
				});
			});
		}	
	}

	/* 

	5. Init Quantity

	*/

	function initQuantity()
	{
		if($('.plus').length && $('.minus').length)
		{
			var plus = $('.plus');
			var minus = $('.minus');
			var value = $('#quantity_value');

			plus.on('click', function()
			{
				var x = parseInt(value.text());
				value.text(x + 1);
			});

			minus.on('click', function()
			{
				var x = parseInt(value.text());
				if(x > 1)
				{
					value.text(x - 1);
				}
			});
		}
    }

    /* 

	!!!. Init

	*/

    function init() {
        $('.mminus').click(function () {
            var sc_cd_id = $(this).attr("id");
            alert(sc_cd_id.Substring(0, 10));

            //$.ajax({
            //    type: 'POST', url: 'car.aspx',
            //    data: 'cd_id=' + $(".product_name").attr("id") + '&sc_num=' + $("#quantity_value").text(),
            //    success: function (msg) {

            //        if (msg == "Y") {
            //            var number = parseInt($('.checkout_items').text(), 10);
            //            $('.checkout_items').text(number + 1)
            //            alert("新增購物車成功!!" + number);
            //        }
            //        else if (msg == "N") {
            //            alert("請先登入後，再進行操作");

            //            setTimeout(function () {
            //                window.open("login.aspx");
            //            }, 1000);
            //        }
            //        else if (msg == "R") {
            //            alert("商品已在購物車!");
            //        }
            //        //alert(msg);
            //    }
            //})
        });
    }
    

 //   function initQuantitys() {
 //       if ($('.splus').length && $('.sminus').length) {
 //           var plus = $('.splus');
 //           var minus = $('.sminus');
 //           var value = $('#quantity_value');
 //           //var value = $(this).parent("div").children.eq(1);//note. 改這裡 不應該是抓所有的 value 而是特定的位置 so... 要先回推到 value 位在哪一層 是哪一個親戚的小孩
 //           // value = $(this).parent("div").children.eq(1);
 //           //$("a").parents("div:eq(1)").children("div:eq(0)").html();
 //           plus.on('click', function () {
 //               var x = parseInt(value.text());
 //               value.text(x + 1);
 //           });

 //           minus.on('click', function () {
 //               var x = parseInt(value.text());
 //               if (x > 1) {
 //                   value.text(x - 1);
 //               }
 //           });
 //       }
 //   }

 //   function initQuantitym() {
 //       if ($('.mplus').length && $('.mminus').length) {
 //           var plus = $('.mplus');
 //           var minus = $('.mminus');
 //           var value = $('#mquantity_value');

 //           plus.on('click', function () {
 //               var x = parseInt(value.text());
 //               value.text(x + 1);
 //           });

 //           minus.on('click', function () {
 //               var x = parseInt(value.text());
 //               if (x > 1) {
 //                   value.text(x - 1);
 //               }
 //           });
 //       }
 //   }

 //   function initQuantityl() {
 //       if ($('.lplus').length && $('.lminus').length) {
 //           var plus = $('.lplus');
 //           var minus = $('.lminus');
 //           var value = $('#lquantity_value');

 //           plus.on('click', function () {
 //               var x = parseInt(value.text());
 //               value.text(x + 1);
 //           });

 //           minus.on('click', function () {
 //               var x = parseInt(value.text());
 //               if (x > 1) {
 //                   value.text(x - 1);
 //               }
 //           });
 //       }
 //   }

 //   function initQuantityxl() {
 //       if ($('.xlplus').length && $('.xlminus').length) {
 //           var plus = $('.xlplus');
 //           var minus = $('.xlminus');
 //           var value = $('#xlquantity_value');

 //           plus.on('click', function () {
 //               var x = parseInt(value.text());
 //               value.text(x + 1);
 //           });

 //           minus.on('click', function () {
 //               var x = parseInt(value.text());
 //               if (x > 1) {
 //                   value.text(x - 1);
 //               }
 //           });
 //       }
 //   }

	/* 

	6. Init Star Rating

	*/

	function initStarRating()
	{
		if($('.user_star_rating li').length)
		{
			var stars = $('.user_star_rating li');

			stars.each(function()
			{
				var star = $(this);

				star.on('click', function()
				{
					var i = star.index();

					stars.find('i').each(function()
					{
						$(this).removeClass('fa-star');
						$(this).addClass('fa-star-o');
					});
					for(var x = 0; x <= i; x++)
					{
						$(stars[x]).find('i').removeClass('fa-star-o');
						$(stars[x]).find('i').addClass('fa-star');
					};
				});
			});
		}
	}

	/* 

	7. Init Favorite

	*/

	function initFavorite()
	{
		if($('.product_favorite').length)
		{
			var fav = $('.product_favorite');

			fav.on('click', function()
			{
				fav.toggleClass('active');
            });

            alert("123");
		}
	}

	/* 

	8. Init Tabs

	*/

	function initTabs()
	{
		if($('.tabs').length)
		{
			var tabs = $('.tabs li');
			var tabContainers = $('.tab_container');

			tabs.each(function()
			{
				var tab = $(this);
				var tab_id = tab.data('active-tab');

				tab.on('click', function()
				{
					if(!tab.hasClass('active'))
					{
						tabs.removeClass('active');
						tabContainers.removeClass('active');
						tab.addClass('active');
						$('#' + tab_id).addClass('active');
					}
				});
			});
		}
	}
});